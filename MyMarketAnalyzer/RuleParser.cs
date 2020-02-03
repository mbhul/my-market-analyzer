/*****************************************************************************
 *  CLASS: RuleParser
 *  Description: Used to interpret custom syntax for buy and sell signals 
 *  
 *  Example Buy:
 *  [U%50] AVG[MACD_DIFF][-5..0] > 0
 *  
 *  [U%50]:
 *   - Invest 50% of the remaining principal amount if the given condition is 
 *     true
 *  [-5..0]:
 *   - returns an array of the last 6 (including today's) closing values of the
 *     given signal
*****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;

namespace MyMarketAnalyzer
{
    public enum Variable
    {
        //Available Parameters
        [StringValue("MACD_SIG")]
        MACD_SIG = 0xE00,
        [StringValue("MACD_DIFF")]
        MACD_DIFF = 0xE01,
        [StringValue("P")]
        PRICE = 0xE02,
        [StringValue("V")]
        VOLUME = 0xE03,
        [StringValue("PCT")]
        PCT = 0xE04,
        [StringValue("VX")]
        VX = 0xE05,
        [StringValue("last_bought")]
        LASTBUY = 0xE06,
        [StringValue("last_sold")]
        LASTSELL = 0xE07
    }

    public enum Fn
    {
        //Functions
        [StringValue("AVG")]
        AVG = 0xF01,
        [StringValue("MAX")]
        MAX = 0xF02,
        [StringValue("MIN")]
        MIN = 0xF03,
        [StringValue("TREND")]
        TREND = 0xF04,
        [StringValue("STDEV")]
        STDEV = 0xF05
    }

    public static class RuleParserInputs
    {
        public static Variable[] VarList = { Variable.PRICE, Variable.VOLUME, Variable.PCT, Variable.MACD_SIG, Variable.MACD_DIFF, Variable.VX };
        public static Variable[] VarList2 = { Variable.LASTBUY, Variable.LASTSELL };
        public static String[] VarCaptions = { "PRICE", "VOLUME", "% CHANGE", "MACD SIG", "MACD DIFF", "VOLATILITY" };
        public static Fn[] Fns = { Fn.AVG, Fn.MAX, Fn.MIN, Fn.TREND, Fn.STDEV };
        public static String[] Operators = { "AND", "OR" };
        public static String[] Comparators = { ">=", "<=", ">", "<", "=" };
    }

    public class RuleParser
    {
        private Hashtable VariablesTable = new Hashtable(100);
        private List<Fn> current_buy_functions;
        private List<Fn> current_sell_functions;
        private List<bool> buy_history;
        private List<bool> sell_history;

        //Input parameters
        private Equity inEquity;
        private ExchangeMarket mktData;
        private String inBuyRule;
        private String inSellRule;
        private Double inPrincipalAmt;

        public int[] errorIndex { get; private set; } = new int[2];

        public Double PercentComplete { get; private set; }

        public RuleParser()
        {
            PercentComplete = 0.0;
            current_buy_functions = new List<Fn>();
            current_sell_functions = new List<Fn>();
            buy_history = new List<bool>();
            sell_history = new List<bool>();

            foreach (Variable var in RuleParserInputs.VarList)
            {
                VariablesTable.Add(StringEnum.GetStringValue(var),var);
            }
        }

        public void SetInputParams(ExchangeMarket pMktData, int pEqIn, String pBuyRule, String pSellRule, Double pPrincipal)
        {
            mktData = pMktData;
            inEquity = pMktData.Constituents[pEqIn];
            inBuyRule = pBuyRule;
            inSellRule = pSellRule;
            inPrincipalAmt = pPrincipal;
        }


        /*****************************************************************************
         *  FUNCTION:       RunAnalysis
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public AnalysisResult RunAnalysis()
        {
            int i;
            bool buy, sell;
            string str_expression, str_evaluated_rule;
            double gain_loss = 0.0, cash = inPrincipalAmt, investments = 0.0, transaction_amt = 0.0;
            int units = 0, units_held = 0, total_transactions = 0;
            AnalysisResult analysis_result = new AnalysisResult();

            inBuyRule = PreprocessRule(inBuyRule);
            inSellRule = PreprocessRule(inSellRule);

            String[] buy_conditions = CleanExpression(inBuyRule).Split(RuleParserInputs.Operators, StringSplitOptions.RemoveEmptyEntries);
            String[] sell_conditions = CleanExpression(inSellRule).Split(RuleParserInputs.Operators, StringSplitOptions.RemoveEmptyEntries);

            PercentComplete = 0.0;

            //Get the list of functions passed in the current Buy and Sell rules 
            //(so that we don't have to itterate again for each data point)
            current_buy_functions.Clear();
            current_sell_functions.Clear();
            foreach (Fn func in RuleParserInputs.Fns)
            {
                foreach (String buy_cond in buy_conditions)
                {
                    if (buy_cond.Contains(StringEnum.GetStringValue(func)))
                    {
                        current_buy_functions.Add(func);
                    }
                }
                foreach (String sell_cond in sell_conditions)
                {
                    if (sell_cond.Contains(StringEnum.GetStringValue(func)))
                    {
                        current_sell_functions.Add(func);
                    }
                }
            }

            //ex. ((MACD_DIFF > 0) AND (P < AVG[P][-5..0]))

            buy_history.Clear();
            sell_history.Clear();

            //For every day in data
            for (i = 0; i < inEquity.HistoricalPriceDate.Count; i++)
            {
                //Initialize analysis result values
                analysis_result.cash_totals.Add(0.0);
                analysis_result.investments_totals.Add(0.0);
                analysis_result.net_change_daily.Add(0.0);
                analysis_result.units_change_daily.Add(0);

                //Evaluate the given historical data point against each of the buy rules
                buy = false;
                str_evaluated_rule = inBuyRule;

                //Parse the unit specifier in the buy rule
                units = GetNumberOfUnits(str_evaluated_rule, out str_evaluated_rule, inEquity.HistoricalPrice[i], cash);
                
                //Parse each of the individual conditional expressions and evaluate the overall rule
                foreach(String buy_cond in buy_conditions)
                {
                    //str_expression = PreprocessRule(buy_cond);
                    str_expression = Parse(buy_cond, inEquity, i, current_buy_functions);

                    if(str_expression == "ERROR")
                    {
                        analysis_result.message_string = "Syntax error in buy rule";
                        PercentComplete = 1.0;
                        return analysis_result;
                    }

                    //replace the sub-expression string with it's parsed value
                    str_evaluated_rule = str_evaluated_rule.Replace(buy_cond, str_expression);
                }

                //Evaluate the overall parsed buy rule. At this point, all functions and variables 
                // must have been replaced with their parsed values 
                // (ex. "P < AVG[P][-5..0]" must be something like "6.5 < 7.67")
                buy = Evaluate(str_evaluated_rule);
                buy_history.Add(buy);

                //Update holdings if the buy rule evaluates to True
                if (buy)
                {
                    transaction_amt = inEquity.HistoricalPrice[i] * units;
                    if (cash >= (transaction_amt))
                    {
                        units_held += units;
                        cash -= transaction_amt;
                        investments = (inEquity.HistoricalPrice[i] * units_held);
                        total_transactions++;
                    }
                }

                //Evaluate the given historical data point against each of the sell rules
                sell = false;
                str_evaluated_rule = inSellRule;

                //Get the number of units to sell
                units = GetNumberOfUnits(str_evaluated_rule, out str_evaluated_rule, 1.0, investments);

                foreach (String sell_cond in sell_conditions)
                {
                    str_expression = Parse(sell_cond, inEquity, i, current_sell_functions);

                    if (str_expression == "ERROR")
                    {
                        analysis_result.message_string = "Syntax error in sell rule";
                        PercentComplete = 1.0;
                        return analysis_result;
                    }

                    str_evaluated_rule = str_evaluated_rule.Replace(sell_cond, str_expression);
                }
                sell = Evaluate(str_evaluated_rule);
                sell_history.Add(sell);

                if (sell)
                {
                    transaction_amt = inEquity.HistoricalPrice[i] * units;
                    if (investments >= transaction_amt)
                    {
                        units_held -= units;
                        cash += transaction_amt;
                        investments = (inEquity.HistoricalPrice[i] * units_held);
                        total_transactions++;
                        units = 0;
                    }
                }

                //update Analyis result structure
                analysis_result.cash_totals[i] = cash;
                analysis_result.investments_totals[i] = investments;
                analysis_result.net_change_daily[i] = (cash + investments);
                analysis_result.units_change_daily[i] = units_held;
                analysis_result.dates_from_to = new Tuple<DateTime, DateTime>(inEquity.HistoricalPriceDate[0],
                    inEquity.HistoricalPriceDate[inEquity.HistoricalPriceDate.Count - 1]);

                if(i > 0)
                {
                    analysis_result.net_change_daily[i] -= (analysis_result.cash_totals[i - 1] + 
                        analysis_result.investments_totals[i - 1]);

                    analysis_result.units_change_daily[i] -= analysis_result.units_change_daily[i - 1];
                }

                
                //Update progress
                PercentComplete = ((double)(i + 1) / (double)inEquity.HistoricalPriceDate.Count);
            }

            PercentComplete = 1.0;

            gain_loss = (cash + investments) - inPrincipalAmt;
            analysis_result.net_change = gain_loss;

            return analysis_result;
        }

        private String PreprocessRule(String pRuleString)
        {
            string subexpression = "", returnString = "";
            int index1 = 0, index2 = 0;
            string indexStr = "", newRule = "";

            returnString = pRuleString;
            returnString = (new Regex("\\s+")).Replace(returnString, " ");

            //Find all instances of the "becomes" keyword and create the edge-detection string
            foreach (int index in pRuleString.AllIndexesOf("becomes"))
            {
                index1 = ExpressionStartIndex(pRuleString, index - 1);
                index2 = ExpressionEndIndex(pRuleString, index + 7);

                subexpression = pRuleString.Substring(index1, index2 - index1);

                //Replace all existing index specifiers with ones decremented by 1
                index1 = subexpression.IndexOf('[');
                newRule = subexpression;
                while (index1 >= 0)
                {
                    index2 = subexpression.IndexOf(']', index1);

                    if (index2 > index1)
                    {
                        indexStr = subexpression.Substring(index1 + 1, index2 - index1 - 1);
                    }
                    else { break; }

                    //replace the indexStr with indexes decremented by 1
                    foreach(Match si in Regex.Matches(indexStr, "[-]*[0-9]+"))
                    {
                        indexStr = indexStr.ReplaceFirstOccurrence(si.Value, (int.Parse(si.Value) - 1).ToString());
                    }

                    newRule = newRule.ReplaceFirstOccurrence(subexpression.Substring(index1 + 1, index2 - index1 - 1), indexStr);
                    index1 = subexpression.IndexOf('[', index2);
                }

                //For variables used without an index specifier (indicating current data), need to add [-1] specifier
                foreach(Variable vari in RuleParserInputs.VarList)
                {
                    MatchCollection mc = Regex.Matches(newRule, "\\b" + StringEnum.GetStringValue(vari) + "\\b");
                    int i = 0, offset = 0;

                    while(i < mc.Count)
                    {
                        index1 = mc[i++].Index + offset;
                        index2 = index1 + StringEnum.GetStringValue(vari).Length;
                        if(newRule[index2] != '[' && newRule[index2] != ']')
                        {
                            newRule = newRule.Insert(index2, "[-1]");
                            offset += 4;
                        }
                    }
                }
                
                //Create the new subexpression, replacing "becomes" with the same rule using 'previous' indexes
                newRule = "(" + subexpression.Replace("becomes", "") + " AND NOT(" + newRule.Replace("becomes", "") + "))";
                newRule = newRule.Replace("  ", " ").Trim();

                returnString = returnString.Replace(subexpression, newRule);
                returnString = (new Regex("\\s+")).Replace(returnString, " ");
            }

            return returnString;
        }

        private int ExpressionStartIndex(string pRuleString, int pFromIndex)
        {
            int nesting_level = 0, param_level = 0;
            int index1 = 0;
            string tempStr = "";
            bool isfound = false;

            //find the beginning of the expression first
            nesting_level = 0;
            param_level = 0;
            isfound = false;
            index1 = pFromIndex;
            while (index1 > 0)
            {
                switch (pRuleString[index1])
                {
                    case '(':
                        nesting_level--;
                        break;
                    case ')':
                        nesting_level++;
                        break;
                    case '[':
                        param_level--;
                        break;
                    case ']':
                        param_level++;
                        break;
                    default:
                        break;
                }
                tempStr = pRuleString.SubWord(index1);

                if (RuleParserInputs.Fns.Where(a => StringEnum.GetStringValue(a) == tempStr).Count() > 0)
                {
                    isfound = true;
                }
                else if (RuleParserInputs.VarList.Where(a => StringEnum.GetStringValue(a) == tempStr).Count() > 0 &&
                    (param_level == 0))
                {
                    isfound = true;
                }

                if (isfound && (param_level == 0) && (nesting_level == 0))
                {
                    break;
                }

                index1--;
            }

            return index1;
        }

        private int ExpressionEndIndex(string pRuleString, int pFromIndex)
        {
            int nesting_level = 0, param_level = 0;
            int index1 = 0;
            string tempStr = "";
            bool isfound = false;

            //find the end of the expression
            nesting_level = 0;
            param_level = 0;
            isfound = false;
            index1 = pFromIndex;

            while (index1 < pRuleString.Length)
            {
                switch (pRuleString[index1])
                {
                    case '(':
                        nesting_level++;
                        break;
                    case ')':
                        nesting_level--;
                        break;
                    case '[':
                        param_level++;
                        break;
                    case ']':
                        param_level--;
                        break;
                    default:
                        break;
                }
                tempStr = pRuleString.SubWord(index1);

                try
                {
                    double.Parse(tempStr);
                    isfound = true;
                    index1 += tempStr.Length;
                }
                catch
                {
                    if (RuleParserInputs.Fns.Where(a => StringEnum.GetStringValue(a) == tempStr).Count() > 0)
                    {
                        isfound = true;
                        index1 += tempStr.Length - 1;
                    }
                    else if (RuleParserInputs.VarList.Where(a => StringEnum.GetStringValue(a) == tempStr).Count() > 0 &&
                        (param_level == 0))
                    {
                        isfound = true;
                        index1 += tempStr.Length - 1;
                    }
                }

                if (isfound && (param_level == 0) && (nesting_level == 0))
                {
                    //look ahead to the next character in case a valid fn or param string is found
                    // to ensure the whole parameter is taken
                    if(index1 < (pRuleString.Length - 1) && pRuleString[index1+1] == '[')
                    {
                        param_level++;
                    }
                    else
                    {
                        break;
                    }
                }

                index1++;
            }

            return index1;
        }

        /*****************************************************************************
         *  FUNCTION:       Parse
         *  Description:    Replaces a primitive expression with corresponding numeric values
         *                  to enable evaluation. 
         *                  
         *                  Ex. (P < AVG[P][-5..0])
         *  Parameters:     
         *  
         *****************************************************************************/
        private String Parse(String pExpression, Equity pEqIn, int pIndex, List<Fn> pFNs)
        {
            string parameter, str_expression, fn_str;
            double parameter_value = 0.0;
            string[] split_str;
            int index1, index2;
            Variable in_param;

            str_expression = pExpression;
            //For each function in the expression being parsed
            foreach (Fn func in pFNs)
            {
                fn_str = "";
                if(pExpression.Contains(StringEnum.GetStringValue(func)))
                {
                    //Get the substring containing only the logical expression for this function
                    index1 = pExpression.IndexOf(StringEnum.GetStringValue(func));
                    index2 = pExpression.IndexOf("]", index1);

                    if (pExpression.Length > index2 && pExpression.ToCharArray()[index2 + 1] == '[')
                    {
                        index2 = pExpression.IndexOf("]", index2 + 1);
                    }

                    fn_str = pExpression.Substring(index1, index2 - index1 + 1);

                    try
                    {
                        //Evaluate the function and replace the substring in the original parent expression with the value
                        str_expression = str_expression.Replace(fn_str.Trim(), GetFunction(func, pEqIn, fn_str, pIndex).ToString());
                    }
                    catch(IndexOutOfRangeException)
                    {
                        //The index will be out of range even for a valid command if the function operates on past data that 
                        // isn't available at the start of a series. 
                        //
                        // Ex. AVG[P][-5..0] --> at the start of the price data series, there aren't 5 previous data points available 
                        //     to calculate an average on.
                        //
                        // In this case, just return false to allow the rest of the command to be processed.
                        return "false";
                    }
                    catch(Exception)
                    {
                        errorIndex[0] = index1;
                        errorIndex[1] = index2;
                        return "ERROR";
                    }
                }
            }

            split_str = str_expression.Split(RuleParserInputs.Comparators, StringSplitOptions.RemoveEmptyEntries);

            foreach(string exp_part in split_str)
            {
                parameter = CleanExpression(exp_part.Trim()).Replace("NOT","");

                if (Helpers.ValidateNumeric(parameter) == false)
                {
                    in_param = GetParameterType(parameter);

                    try
                    {
                        //If the data to be analyzed is one of the MACD signals, ensure the passed index is valid
                        if (in_param == Variable.MACD_DIFF || in_param == Variable.MACD_SIG)
                        {
                            if (pIndex > (pEqIn.HistoricalPrice.Count() - pEqIn.MACD_C.Count()))
                            {
                                parameter_value = GetParameter(pEqIn, parameter, pIndex);
                                str_expression = str_expression.ReplaceFirstOccurrence(parameter, parameter_value.ToString());
                            }
                        }
                        else
                        {
                            parameter_value = GetParameter(pEqIn, parameter, pIndex);
                            str_expression = str_expression.ReplaceFirstOccurrence(parameter, parameter_value.ToString());
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        return "false";
                    }
                    catch (Exception)
                    {
                        errorIndex[0] = 0;
                        errorIndex[1] = 0;
                        return "ERROR";
                    }
                }
            }

            return str_expression;
        }

        /*****************************************************************************
         *  FUNCTION:       GetFunction
         *  Description:    Evaluates the passed funuction on the given Equity at the 
         *                  passed index (date/price)
         *  Parameters:     None
         *****************************************************************************/
        private Double GetFunction(Fn pFn, Equity pEqIn, String pFnStr, int pIndex)
        {
            List<double> func_inputs;
            Double eval_data = 0.0;
            int index1, index2, filter_index1, filter_index2;
            Variable input_param;
            String input_param_str, filter_str;

            func_inputs = new List<double>();
            index1 = pFnStr.IndexOf('[');
            index2 = pFnStr.IndexOf(']');
            
            if(index1 >= 0 && index2 > index1)
            {
                input_param_str = pFnStr.Substring(index1 + 1, index2 - index1 - 1);
                input_param = GetParameterType(input_param_str);

                //Set the input array
                switch (input_param)
                {
                    case Variable.MACD_SIG:
                        func_inputs = new List<double>(pEqIn.MACD_B);
                        break;
                    case Variable.MACD_DIFF:
                        func_inputs = new List<double>(pEqIn.MACD_C);
                        break;
                    case Variable.PRICE:
                        func_inputs = new List<double>(pEqIn.HistoricalPrice);
                        break;
                    case Variable.VOLUME:
                        func_inputs = new List<double>(pEqIn.HistoricalVolumes);
                        break;
                    case Variable.PCT:
                        func_inputs = new List<double>(pEqIn.HistoricalPctChange);
                        break;
                    default:
                        //unknown or malformed function command
                        throw new ArgumentException();
                }

                if ((pFnStr.Length > (index2+1)) && pFnStr.ToCharArray()[index2 + 1] == '[')
                {
                    index1 = index2 + 1;
                    index2 = pFnStr.IndexOf(']', index1);

                    filter_index1 = 1;
                    filter_index2 = 1;

                    if (index2 > index1)
                    {
                        filter_str = pFnStr.Substring(index1 + 1, index2 - index1 - 1);
                        
                        if(Regex.Matches(filter_str,"[-][0-9][.][.][-]?[0-9]").Count > 0)
                        {
                            index1 = filter_str.IndexOf('.');
                            filter_index1 = int.Parse(filter_str.Substring(0, index1));
                            filter_index2 = int.Parse(filter_str.Substring(index1 + 2, filter_str.Length - index1 - 2));
                        }
                    }

                    if ((filter_index1 <= filter_index2) && (filter_index2 <= 0) &&
                        (pIndex + filter_index1 >= 0) &&
                        func_inputs.Count > pIndex)
                    {
                        func_inputs = func_inputs.GetRange(pIndex + filter_index1, filter_index2 - filter_index1 - 1);

                        switch (pFn)
                        {
                            case Fn.AVG:
                                eval_data = func_inputs.Average();
                                break;
                            case Fn.MAX:
                                eval_data = func_inputs.Max();
                                break;
                            case Fn.MIN:
                                eval_data = func_inputs.Min();
                                break;
                            case Fn.TREND:
                                //Need some kind of curve/line fitting algorithm
                                break;
                            case Fn.STDEV:
                                eval_data = Helpers.StdDev(func_inputs);
                                break;
                            default:
                                throw new ArgumentException();
                        }
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
                else
                {
                    //If the data to be analyzed is one of the MACD signals, ensure the passed index is valid
                    // The index needs to be greater than the first day for which the MACD can be calculated.
                    // ie. for the standard 12,26,9 series, pIndex must be greater than (26 + 9) = 35
                    if(input_param == Variable.MACD_DIFF || input_param == Variable.MACD_SIG) 
                    {
                        if(pIndex > (pEqIn.HistoricalPrice.Count() - pEqIn.MACD_C.Count()))
                        {
                            eval_data = func_inputs[pIndex];
                        }
                        else
                        {
                            eval_data = 0.0;
                        }
                    }
                    else
                    {
                        eval_data = func_inputs[pIndex];
                    }
                    
                }
                
            }

            return eval_data;
        }

        /*****************************************************************************
         *  FUNCTION:       GetParameter
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private Double GetParameter(Equity pEqIn, String pKey, int pIndex)
        {
            double return_val = 0.0;
            pKey = pKey.Trim();
            int macd_i_diff;
            int index1 = -1, index2 = -1, filter_index;
            string IndexStr = "";
            Variable get_param = 0;

            index1 = pKey.IndexOf('[', 1);
            if(index1 > 0)
                index2 = pKey.IndexOf(']', index1);

            filter_index = pIndex;

            //If the parameter asks for a specific index other than pIndex
            if (index2 > index1)
            {
                IndexStr = pKey.Substring(index1 + 1, index2 - index1 - 1).Trim();

                if (Regex.Matches(IndexStr, "^[-][0-9]*$").Count > 0)
                {
                    filter_index = pIndex + int.Parse(IndexStr);
                }
                else
                {
                    throw new ArgumentException();
                }

                //clear the index specifier
                pKey = pKey.Substring(0, index1);
            }

            //Ex. If the passed parameter string is "P", filter_index = pIndex
            //    If the passed parameter string is "P[-2], filter_index = pIndex - 2
            //
            // At this point, in both cases pKey will be "P"

            if (filter_index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            get_param = GetParameterType(pKey);

            switch (get_param)
            {
                case Variable.MACD_SIG:
                    macd_i_diff = pEqIn.HistoricalPrice.Count - pEqIn.MACD_B.Count;
                    if (filter_index >= macd_i_diff)
                    {
                        return_val = pEqIn.MACD_B[filter_index - macd_i_diff];
                    }
                    break;
                case Variable.MACD_DIFF:
                    macd_i_diff = pEqIn.HistoricalPrice.Count - pEqIn.MACD_C.Count;
                    if (filter_index >= macd_i_diff)
                    {
                        return_val = pEqIn.MACD_C[filter_index - macd_i_diff];
                    }
                    break;
                case Variable.PRICE:
                    return_val = pEqIn.HistoricalPrice[filter_index];
                    break;
                case Variable.PCT:
                    return_val = pEqIn.HistoricalPctChange[filter_index];
                    break;
                case Variable.VOLUME:
                    return_val = pEqIn.HistoricalVolumes[filter_index];
                    break;
                case Variable.VX:
                    return_val = Helpers.StdDev(pEqIn.HistoricalPrice.GetRange(0, filter_index));
                    break;
                case Variable.LASTBUY:
                    return_val = (double)(filter_index - this.buy_history.LastIndexOf(true));
                    break;
                case Variable.LASTSELL:
                    return_val = (double)(filter_index - this.sell_history.LastIndexOf(true));
                    break;
                default:
                    throw new ArgumentException();
            }

            return return_val;
        }

        /*****************************************************************************
         *  FUNCTION:       GetParameterType
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private Variable GetParameterType(String pKey)
        {
            Variable return_type;
            Regex regex;

            try
            {
                regex = new Regex(Regex.Escape("[") + ".*?" + Regex.Escape("]"));
                pKey = regex.Replace(pKey, "").Trim();
                return_type = (Variable)VariablesTable[pKey];
            }
            catch (NullReferenceException)
            {
                return_type = 0;
            }

            return return_type;
        }

        /*****************************************************************************
         *  FUNCTION:       Evaluate
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private Boolean Evaluate(String pExpression)
        {
            bool result;

            DataTable dt = new DataTable();
            try
            {
                result = (bool)(dt.Compute(CleanExpression(pExpression), ""));
            }
            catch(Exception)
            {
                result = false;
            }

            return result;
        }

        /*****************************************************************************
         *  FUNCTION:       CleanExpression
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private String CleanExpression(String pSubString)
        {
            String str_return = "";
            int nesting_level = 0;
            Regex regex;
            str_return = pSubString;

            foreach(char c in str_return.ToCharArray())
            {
                if (c == '(')
                {
                    nesting_level++;
                }
                else if (c == ')')
                {
                    nesting_level--;
                }
                else { }
            }
            
            //Remove leading or trailing brackets if there is no pair for it in the string
            if(nesting_level > 0)
            {
                regex = new Regex(Regex.Escape("("));
                str_return = regex.Replace(str_return, "", nesting_level);
            }
            else if (nesting_level < 0)
            {
                regex = new Regex(Regex.Escape(")"));
                str_return = regex.Replace(str_return, "", -nesting_level);
            }
            else { }

            //Remove the units specifier
            regex = new Regex(Regex.Escape("[") + "U.*?" + Regex.Escape("]"));
            str_return = regex.Replace(str_return, "").Trim();

            return str_return;
        }

        /*****************************************************************************
         *  FUNCTION:       GetNumberOfUnits
         *  Description:    Determines the number of units to be bought/sold for each
         *                  transaction that meets the given rule. A decimal value between
         *                  0 and 1 denotes a percentage of total holdings.
         *                  
         *                  Ex.  [U%50] [U100]            
         *  Parameters:     
         *  
         *****************************************************************************/
        private int GetNumberOfUnits(String pSubString, out String pReturnString, double unit_price, double cash)
        {
            String str_return = "", u_str;
            int index1, index2, units_int;
            double units = 0.0;
            const String units_specifier = "[U";
            bool isPct = false;
            Regex regex;

            str_return = pSubString;
            index1 = str_return.IndexOf(units_specifier);

            if(index1 >= 0)
            {
                index1 += units_specifier.Length;
                index2 = str_return.IndexOf("]", index1);
                if(index2 > index1)
                {
                    u_str = str_return.Substring(index1, index2 - index1);

                    if(u_str[0] == '%')
                    {
                        u_str = u_str.Remove(0, 1);
                        units = double.Parse(u_str) / 100.0;
                        isPct = true;
                    }
                    else
                    {
                        units = Math.Floor(double.Parse(u_str));
                    }

                    str_return.Remove(index1, (index2 - index1 + 1));
                }
            }

            //Default in case no units specifier exists
            if (units == 0.0)
            {
                units = cash / unit_price;
            }

            //If the number of units is relative (ie. percentage of total cash holdings)
            if (isPct)
            {
                if (units > 0)
                {
                    units_int = (int)Math.Floor((cash / unit_price) * units);
                }
                else
                {
                    units_int = 0;
                }
            }
            else
            {
                units_int = (int)units;
            }

            regex = new Regex(Regex.Escape("[") + "U.*?" + Regex.Escape("]"));
            str_return = regex.Replace(str_return, "").Trim();

            pReturnString = str_return;

            return units_int;
        }

    }
}
