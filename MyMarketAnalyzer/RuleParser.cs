/*****************************************************************************
 *  CLASS: RuleParser
 *  Description: Used to interpret custom syntax for buy and sell signals 
 *  
 *  Example Buy:
 *  [U%50] AVG(MACD_DIFF[-5..0]) > 0
 *  
 *  [U%50]:
 *   - Invest 50% of the remaining principal amount if the given condition is 
 *     true
 *  [-5..0]:
 *   - returns an array of the last 6 (including today's) closing values of the
 *     given signal
*****************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data;

namespace MyMarketAnalyzer
{
    static class RuleParser
    {
        private enum Variable
        {
            [StringValue("MACD_SIG")]
            MACD_SIG = 0xE00,
            [StringValue("MACD_DIFF")]
            MACD_DIFF = 0xE01,

            //Functions
            [StringValue("AVG")]
            AVG = 0xF01,
            [StringValue("MAX")]
            MAX = 0xF02,
            [StringValue("MIN")]
            MIN = 0xF03
        }

        private static String[] operators = { "AND", "OR" };
        private static String[] comparators = { "=", ">", "<", ">=", "<=" };

        /*****************************************************************************
         *  FUNCTION:       CalculateGainLoss
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public static double CalculateGainLoss(Equity pEqIn, String pBuyRule, String pSellRule, Double pPrincipal, out List<double> pGLArray, out List<int> pBSUnits)
        {
            int i;
            bool buy, sell;
            string parameter, str_expression, str_evaluated_rule, str_units;
            double parameter_value = 0.0, gain_loss = 0.0;
            String[] buy_conditions = pBuyRule.Split(operators, StringSplitOptions.RemoveEmptyEntries);
            String[] sell_conditions = pSellRule.Split(operators, StringSplitOptions.RemoveEmptyEntries);

            pGLArray = new List<double>();
            pBSUnits = new List<int>();
            
            //ex. AVG(MACD_DIFF[-5..0]) > 0

            //For every day in data
            for(i = 0; i < pEqIn.HistoricalPriceDate.Count; i++)
            {
                //First determine whether the day is a buy or sell (or neither)
                buy = false;
                str_evaluated_rule = pBuyRule;
                foreach(String buy_cond in buy_conditions)
                {
                    parameter = buy_cond.Split(comparators, StringSplitOptions.RemoveEmptyEntries)[0];

                    if(parameter != null)
                    {
                        parameter = parameter.Trim();
                        parameter_value = GetParameter(pEqIn, parameter, i);
                        str_expression = buy_cond.Replace(parameter, parameter_value.ToString());
                        str_evaluated_rule = str_evaluated_rule.Replace(buy_cond, Evaluate(str_expression).ToString());
                    }
                }
                buy = Evaluate(str_evaluated_rule);

                sell = false;
                str_evaluated_rule = pSellRule;
                foreach (String sell_cond in sell_conditions)
                {
                    parameter = sell_cond.Split(comparators, StringSplitOptions.RemoveEmptyEntries)[0];

                    if (parameter != null)
                    {
                        parameter_value = GetParameter(pEqIn, parameter, i);
                        str_expression = sell_cond.Replace(parameter.Trim(), parameter_value.ToString());
                        str_evaluated_rule = str_evaluated_rule.Replace(sell_cond, Evaluate(str_expression).ToString());
                    }
                }
                sell = Evaluate(str_evaluated_rule);

                if(buy)
                {


                }
                else if (sell)
                {

                }
                else { }
            }

            return gain_loss;
        }

        /*****************************************************************************
         *  FUNCTION:       Evaluate
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private static Boolean Evaluate(String pExpression)
        {
            bool result = false;

            DataTable dt = new DataTable();
            var v = dt.Compute(pExpression, "");

            return (bool)result;
        }

        /*****************************************************************************
         *  FUNCTION:       CleanExpression
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private static String CleanExpression(String pSubString)
        {
            String str_return = "";
            int nesting_level = 0;
            str_return = RemoveUnitsSpecifier(pSubString);

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

            var regex = new Regex(Regex.Escape("("));
            str_return = regex.Replace(str_return, "", nesting_level);

            return str_return;
        }

        /*****************************************************************************
         *  FUNCTION:       RemoveUnitsSpecifier
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private static String RemoveUnitsSpecifier(String pSubString)
        {
            String str_return = "";
            int index1, index2;

            str_return = pSubString.Replace(" ", "");
            index1 = str_return.IndexOf("[U");

            while(index1 >= 0)
            {
                index2 = str_return.IndexOf("]", index1);
                if(index2 > index1)
                {
                    str_return.Remove(index1, (index2 - index1 + 1));
                    index1 = str_return.IndexOf("[U");
                }
                else
                {
                    index1 = -1;
                }
            }

            return str_return;
        }

        /*****************************************************************************
         *  FUNCTION:       GetParameter
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private static Double GetParameter(Equity pEqIn, String pKey, int pIndex)
        {
            double return_val = 0.0;
            pKey = pKey.Trim();

            if (pKey == StringEnum.GetStringValue(Variable.MACD_SIG))
            {
                return_val = pEqIn.MACD_B[pIndex];
            }
            else if (pKey == StringEnum.GetStringValue(Variable.MACD_DIFF))
            {
                return_val = pEqIn.MACD_C[pIndex];
            }
            else { }

            return return_val;
        }

    }
}
