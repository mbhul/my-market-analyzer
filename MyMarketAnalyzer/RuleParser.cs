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
        PCT = 0xE04
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
        public static Variable[] VarList = { Variable.PRICE, Variable.VOLUME, Variable.PCT, Variable.MACD_SIG, Variable.MACD_DIFF };
        public static String[] VarCaptions = { "PRICE", "VOLUME", "% CHANGE", "MACD SIG", "MACD DIFF" };
        public static Fn[] Fns = { Fn.AVG, Fn.MAX, Fn.MIN, Fn.TREND, Fn.STDEV };
        public static String[] Operators = { "AND", "OR" };
        public static String[] Comparators = { ">=", "<=", ">", "<", "=" };
    }

    public class RuleParser
    {
        private Hashtable VariablesTable = new Hashtable(100);

        //private Variable[] VarList = { Variable.MACD_SIG, Variable.MACD_DIFF, Variable.PCT, Variable.PRICE, Variable.VOLUME };
        //private static String[] operators = { "AND", "OR" };
        //private static String[] comparators = { ">=", "<=", ">", "<", "=" };
        //private static Fn[] fns = { Fn.AVG, Fn.MAX, Fn.MIN, Fn.TREND, Fn.STDEV };

        private List<Fn> current_buy_functions;
        private List<Fn> current_sell_functions;

        //Input parameters
        private Equity inEquity;
        private String inBuyRule;
        private String inSellRule;
        private Double inPrincipalAmt;

        public Double PercentComplete { get; private set; }

        public RuleParser()
        {
            PercentComplete = 0.0;
            current_buy_functions = new List<Fn>();
            current_sell_functions = new List<Fn>();

            foreach (Variable var in RuleParserInputs.VarList)
            {
                VariablesTable.Add(StringEnum.GetStringValue(var),var);
            }
        }

        public void SetInputParams(Equity pEqIn, String pBuyRule, String pSellRule, Double pPrincipal)
        {
            inEquity = pEqIn;
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

            String[] buy_conditions = inBuyRule.Split(RuleParserInputs.Operators, StringSplitOptions.RemoveEmptyEntries);
            String[] sell_conditions = inSellRule.Split(RuleParserInputs.Operators, StringSplitOptions.RemoveEmptyEntries);

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

            //For every day in data
            for(i = 0; i < inEquity.HistoricalPriceDate.Count; i++)
            {
                //Initialize analysis result values
                analysis_result.cash_totals.Add(0.0);
                analysis_result.investments_totals.Add(0.0);
                analysis_result.net_change_daily.Add(0.0);
                analysis_result.units_change_daily.Add(0);

                //Evaluate the given historical data point against each of the buy rules
                buy = false;
                str_evaluated_rule = inBuyRule;

                //Get the number of units to buy
                units = GetNumberOfUnits(str_evaluated_rule, out str_evaluated_rule, inEquity.HistoricalPrice[i], cash);
                
                //Parse each of the individual conditional expressions and evaluate the overall rule
                foreach(String buy_cond in buy_conditions)
                {
                    str_expression = Parse(buy_cond, inEquity, i, current_buy_functions);
                    str_evaluated_rule = str_evaluated_rule.Replace(buy_cond, str_expression);
                }
                buy = Evaluate(str_evaluated_rule);

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
                    str_evaluated_rule = str_evaluated_rule.Replace(sell_cond, str_expression);
                }
                sell = Evaluate(str_evaluated_rule);

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
            foreach (Fn func in pFNs)
            {
                fn_str = "";
                if(pExpression.Contains(StringEnum.GetStringValue(func)))
                {
                    index1 = pExpression.IndexOf(StringEnum.GetStringValue(func));
                    index2 = pExpression.IndexOf("]", index1);

                    if (pExpression.Length > index2 && pExpression.ToCharArray()[index2 + 1] == '[')
                    {
                        index2 = pExpression.IndexOf("]", index2 + 1);
                    }

                    fn_str = pExpression.Substring(index1, index2 - index1 + 1);
                    str_expression = str_expression.Replace(fn_str.Trim(), GetFunction(func, pEqIn, fn_str, pIndex).ToString());
                }
            }

            split_str = str_expression.Split(RuleParserInputs.Comparators, StringSplitOptions.RemoveEmptyEntries);

            foreach(string exp_part in split_str)
            {
                if(Helpers.ValidateNumeric(exp_part) == false)
                {
                    parameter = CleanExpression(exp_part.Trim());
                    in_param = GetParameterType(parameter);

                    //If the data to be analyzed is one of the MACD signals, ensure the passed index is valid
                    if (in_param == Variable.MACD_DIFF || in_param == Variable.MACD_SIG)
                    {
                        if (pIndex > (pEqIn.HistoricalPrice.Count() - pEqIn.MACD_C.Count()))
                        {
                            parameter_value = GetParameter(pEqIn, parameter, pIndex);
                            str_expression = str_expression.Replace(parameter, parameter_value.ToString());
                        }
                    }
                    else
                    {
                        parameter_value = GetParameter(pEqIn, parameter, pIndex);
                        str_expression = str_expression.Replace(parameter, parameter_value.ToString());
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
                        break;
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

                    if ((filter_index1 <= 0 && filter_index2 <= 0) &&
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
                                break;
                        }
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

            regex = new Regex(Regex.Escape("[") + "U.*" + Regex.Escape("]"));
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

            regex = new Regex(Regex.Escape("[") + "U.*" + Regex.Escape("]"));
            str_return = regex.Replace(str_return, "").Trim();

            pReturnString = str_return;

            return units_int;
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
            Variable get_param = 0;

            get_param = GetParameterType(pKey);

            switch(get_param)
            {
                case Variable.MACD_SIG:
                    macd_i_diff = pEqIn.HistoricalPrice.Count - pEqIn.MACD_B.Count;
                    if(pIndex >= macd_i_diff)
                    {
                        return_val = pEqIn.MACD_B[pIndex - macd_i_diff];
                    }
                    break;
                case Variable.MACD_DIFF:
                    macd_i_diff = pEqIn.HistoricalPrice.Count - pEqIn.MACD_C.Count;
                    if (pIndex >= macd_i_diff)
                    {
                        return_val = pEqIn.MACD_C[pIndex - macd_i_diff];
                    }
                    break;
                case Variable.PRICE:
                    return_val = pEqIn.HistoricalPrice[pIndex];
                    break;
                case Variable.PCT:
                    return_val = pEqIn.HistoricalPctChange[pIndex];
                    break;
                case Variable.VOLUME:
                    return_val = pEqIn.HistoricalVolumes[pIndex];
                    break;
                default:
                    break;
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

            try
            {
                return_type = (Variable)VariablesTable[pKey];
            }
            catch(NullReferenceException)
            {
                return_type = 0;
            }

            return return_type;
        }

    }
}
