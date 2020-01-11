using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyMarketAnalyzer
{
    static class Helpers
    {
        private const string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";

        /*****************************************************************************
         *  FUNCTION:       ValidateNumeric
         *  Description:    Returns true if the passed string value is numeric. False otherwise.
         *  Parameters:     
         *      arg0        - the input to validate 
         *****************************************************************************/
        public static Boolean ValidateNumeric(String arg0)
        {
            Double testValue;
            Boolean returnValue = false;

            //Prevent nuissance failure caused by empty string
            //if(arg0 == "")
            //{
            //    arg0 = "0";
            //}

            try
            {
                testValue = Double.Parse(arg0);
                returnValue = true;
            }
            catch (Exception e)
            {
                //do nothing
            }
            return returnValue;
        }

        /*****************************************************************************
         *  FUNCTION:       RemoveNonAlphanumeric
         *  Description:    Returns the input value, stripped of all non-alphanumeric
         *                  characters, as a string. ie. removes all characters that
         *                  do not match the regular expression [^a-zA-Z0-9_]
         *  Parameters:     
         *      pInput      - the input value to start with
         *****************************************************************************/
        public static String RemoveNonAlphanumeric(String pInput)
        {
            String return_value;

            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_]");
            return_value = rgx.Replace(pInput, "");

            return return_value;
        }

        /*****************************************************************************
         *  FUNCTION:       ConvertVolumeString
         *  Description:    Converts a string containing an abbreviated numeric value with
         *                  either 'K' (for thousand) or 'M' (for million) as a the corresponding
         *                  double value. ie "1.3M" will be returned as (double)1,300,000
         *  Parameters:     
         *      pVol        - A string indicating trading volume. Typically specified
         *                    as "200K" or "1.3M" etc. 
         *****************************************************************************/
        public static double ConvertVolumeString(String pVol)
        {
            int volLen;
            double volume = 0.0;

            volLen = pVol.Length;
            volLen = volLen - Regex.Replace(pVol, "[.].*", "").Length - 2;
            pVol = pVol.Replace(".", "");
            if (volLen > 0 && pVol.Contains("K"))
            {
                pVol += String.Join("", Enumerable.Repeat("0", 3 - volLen));
            }
            else if (volLen > 0 && pVol.Contains("M"))
            {
                pVol += String.Join("", Enumerable.Repeat("0", 6 - volLen));
            }
            pVol = Regex.Replace(pVol, "[A-Z]", "");

            if(ValidateNumeric(pVol))
            {
                volume = Double.Parse(pVol);
            }

            return volume;
        }

        /*****************************************************************************
         *  FUNCTION:       ValidatePath
         *  Description:    Converts a string containing an abbreviated numeric value with
         *                  either 'K' (for thousand) or 'M' (for million) as a the corresponding
         *                  double value. ie "1.3M" will be returned as (double)1,300,000
         *  Parameters:     
         *      pVol        - A string indicating trading volume. Typically specified
         *                    as "200K" or "1.3M" etc. 
         *****************************************************************************/
        public static Boolean ValidatePath(String pPath)
        {
            Boolean success = true;

            success = System.IO.Directory.Exists(pPath);

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:       StdDev
         *  Description:    Computes the standard deviation of the input data
         *  Parameters:     
         *      pData       - the input data 
         *****************************************************************************/
        public static double StdDev(List<double> pInput)
        {
            int i;
            double variance_sum, avgInput, variance, std_dev;
            variance_sum = 0;

            avgInput = pInput.Average();
            for (i = 0; i < pInput.Count(); i++)
            {
                variance_sum = variance_sum + Math.Pow((pInput[i] - avgInput), 2);
            }
            variance = variance_sum / (pInput.Count() - 1);

            std_dev = Math.Round(Math.Sqrt(variance), 4);

            return std_dev;
        }

        /*****************************************************************************
         *  FUNCTION:       AccumulationDistributionIndex
         *  Description:    Computes the Accumulation/Distribution Index for a given set
         *                  of corresponding equity data.
         *  Parameters:     
         *      pClose      - List of close prices
         *      pHigh       - List of daily high-prices
         *      pLow        - List of daily low-prices
         *      pVolume     - List of daily trading volumes
         *  
         *  Note: all input lists must have the same number of elements. It is assumed that
         *        all points at index 'n' correspond to the same point in time.
         *****************************************************************************/
        public static List<Double> AccumulationDistributionIndex(List<Double> pClose, List<Double> pHigh, List<Double> pLow, List<Double> pVolume)
        {
            Double CLV = 0;
            List<Double> AccumDistrIndex;
            int i;

            //*** Accumulation / Distribution Index ***//
            AccumDistrIndex = new List<double>(pClose.Count);
            AccumDistrIndex.Add(0);

            for (i = 1; i < pClose.Count; i++)
            {
                CLV = ((pClose[i] - pLow[i]) - (pHigh[i] - pClose[i])) /
                    (pHigh[i] - pLow[i]);
                AccumDistrIndex.Add(AccumDistrIndex[i - 1] + (pVolume[i] * CLV));
            }

            return AccumDistrIndex;
        }

        /*****************************************************************************
         *  FUNCTION:       ComputeMACD
         *  Description:    Computes the 3 standard MACD series' for a given set of 
         *                  historical prices.
         *                  
         *                  MACD computes 2 exponential moving averages using the first
         *                  two time base values (typically 12 and 26). The difference
         *                  between these 2 series' is referred to as the 'MACD'. The third 
         *                  time base is used to compute an exponential moving average of the
         *                  MACD. The zero-crossing of the difference between this 3rd signal
         *                  and the MACD is often used to indicate price momentum.
         *                  
         *  Parameters:     
         *      MACD_TIME_BASES - The time windows for computing the moving averages 
         *                        required for MACD calculation. (12, 26, 9) is standard.
         *      hist_price      - Array of historical prices to use in the computation
         *      MACD_A          - MACD A series (first moving average)
         *      MACD_B          - MACD B series (seond moving average)
         *      MACD_C          - MACD C series (moving average of the difference between the first 
         *                        two moving averages)
         *****************************************************************************/
        public static void ComputeMACD(int[] MACD_TIME_BASES, List<Double> hist_price, out List<Double> MACD_A, out List<Double> MACD_B, out List<Double> MACD_C)
        {
            Double alpha, EMA1, EMA2, SIG;
            int i, sig_count;

            MACD_A = new List<double>();
            MACD_B = new List<double>();
            MACD_C = new List<double>();

            if (MACD_TIME_BASES.Min() > 0 && MACD_TIME_BASES.Max() < hist_price.Count)
            {
                //Initial averages (default = 12 / 26 day)
                EMA1 = hist_price.Take(MACD_TIME_BASES[0]).Average();
                EMA2 = hist_price.Take(MACD_TIME_BASES[1]).Average();

                //Initial factors
                alpha = 0;
                SIG = 0;
                sig_count = 0;

                for (i = MACD_TIME_BASES[0]; i < hist_price.Count; i++)
                {
                    alpha = 2.0 / ((double)MACD_TIME_BASES[0] + 1.0);
                    EMA1 = (hist_price[i] * alpha) + (1 - alpha) * EMA1;
                    if (i >= MACD_TIME_BASES[1])
                    {
                        alpha = 2.0 / ((double)MACD_TIME_BASES[1] + 1.0);
                        EMA2 = (hist_price[i] * alpha) + (1 - alpha) * EMA2;
                        MACD_A.Add((EMA1 - EMA2));
                    }

                    if (i >= MACD_TIME_BASES[1] + MACD_TIME_BASES[2])
                    {
                        if (i == MACD_TIME_BASES[1] + MACD_TIME_BASES[2])
                        {
                            SIG = MACD_A.Take(MACD_TIME_BASES[2]).Average();
                        }
                        else
                        {
                            alpha = 2.0 / ((double)MACD_TIME_BASES[2] + 1.0);
                            SIG = (MACD_A[MACD_TIME_BASES[2] + sig_count] * alpha) + (1 - alpha) * SIG;
                        }
                        MACD_B.Add(SIG);
                        MACD_C.Add(MACD_A[i - MACD_TIME_BASES[1]] - SIG);
                        sig_count++;
                    }

                }
            }
        }

        public static Boolean ValidateListSize<T>(params List<T>[] args) where T: new()
        {
            Boolean return_value = true;
            int i, lSize;

            lSize = args[0].Count;
            for (i = 1; i < args.Length; i++)
            {
                if(args[i].Count != lSize)
                {
                    return_value = false;
                    break;
                }
            }

            return return_value;
        }

        public static List<double> ListDoubleOperation(ListOperator pOperator, params List<double>[] args)
        {
            List<double> outputList = (new List<double>());
            int[] arglengths = args.Select(x => x.Count).ToArray();
            double[] argAt;
            int i, j;
            double opValue;

            if(arglengths.Max() == arglengths.Min())
            {
                for (i = 0; i < args[0].Count; i++)
                {
                    argAt = args.Select(x => x[i]).ToArray();
                    opValue = 0;
                    switch (pOperator)
                    {
                        case ListOperator.SUM:
                            for (j = 0; j < argAt.Length; j++)
                            {
                                opValue += argAt[j];
                            }
                            break;
                        case ListOperator.DIFF:
                            opValue = argAt[0];
                            for (j = 1; j < argAt.Length; j++)
                            {
                                opValue -= argAt[j];
                            }
                            break;
                        case ListOperator.MULTIPLY:
                            for (j = 0; j < argAt.Length; j++)
                            {
                                opValue *= argAt[j];
                            }
                            break;
                        case ListOperator.DIVIDE:
                            opValue = argAt[0];
                            for (j = 1; j < argAt.Length; j++)
                            {
                                opValue /= argAt[j];
                            }
                            break;
                        default:
                            break;
                    }

                    outputList.Add(Math.Round(opValue, 4));
                }
 
            }

            return outputList;
        }

        public static double MinimumDifference(List<double> pInput)
        {
            double return_value = 0;
            List<double> differences = new List<double>();
            if (pInput.Count > 1)
            {
                pInput.Sort();
                for (int i = 1; i < pInput.Count; i++)
                {
                    differences.Add(Math.Abs(pInput[i] - pInput[i - 1]));
                }
                return_value = differences.Min();
            }
            return return_value;
        }

        /*****************************************************************************
         *  FUNCTION:       PearsonProductCoefficient
         *  
         *  Description:    Calculates the Pearson Product Coefficient (PPC) for each 
         *                  Equity in pInputList based on it's HistoricalPrice list, against
         *                  every other Equity in pInputList.
         *                  
         *                  The intent is to find correlation between Equities whose
         *                  HistoricalPrice values tend to move together. 
         *                  Each coefficient is a value between -1 and 1. A value of 
         *                  1 corresponds to a high positive correlation. That is, the
         *                  Equities tend to move in the same direction at the same time
         *                  over the series of data provided. A value of -1 corresponds to
         *                  a high negative correlation. That is, the Equities tend to move
         *                  in opposite directions at the same time. A value of 0 means there
         *                  is no identifiable correlation between the direction of movement.
         *                  
         *  Parameters:     
         *      pInputList  - the input data, consisting of multiple Equity instances
         *****************************************************************************/
        public static Double[] PearsonProductCoefficient(List<Equity> pInputList)
        {
            int N, cSize, i, j, k, count, key;
            Double coeff, numerator, denominator;
            Double meanRange1, meanRange2;
            Double[] CorrelationCoefficients;

            /*Create the empty correlation table. Note that we only need to calculate the correlation between any 2 equities once.
             *Ex. Given 5 inputs {a,b,c,d,e}:
             *    a  b  c  d  e
             *    ---------------   cSize = (5 * (4)) / 2 = 10
             *  a|-  C0 C1 C2 C3
             *  b|   -  C4 C5 C6
             *  c|      -  C7 C8
             *  d|         -  C9
             *  e|            -
             */
            N = pInputList.Count();
            cSize = N * (N - 1) / 2;
            CorrelationCoefficients = new Double[cSize];

            if (N > 0)
            {
                count = 1;
                for (i = 0; i < N; i++)
                {
                    coeff = 0;
                    denominator = 0;
                    meanRange1 = 0;
                    meanRange2 = 0;

                    for (j = count; j < N; j++)
                    {
                        if (pInputList[i].HistoricalPrice.Count() == pInputList[j].HistoricalPrice.Count())
                        {
                            numerator = 0;

                            //Recalculate the average instead of using the 'avgPrice' member 
                            // because the rounded value introduces significant inaccuracy in some transformations
                            meanRange1 = pInputList[i].HistoricalPrice.Average(); 
                            meanRange2 = pInputList[j].HistoricalPrice.Average();

                            for (k = 0; k < pInputList[j].HistoricalPrice.Count(); k++)
                            {
                                numerator += (pInputList[i].HistoricalPrice[k] * pInputList[j].HistoricalPrice[k]);
                            }
                            numerator -= (pInputList[j].HistoricalPrice.Count() * meanRange1 * meanRange2);

                            denominator = Helpers.StdDev(pInputList[i].HistoricalPrice) * Helpers.StdDev(pInputList[j].HistoricalPrice);
                            denominator *= (pInputList[j].HistoricalPrice.Count() - 1);

                            coeff = numerator / denominator;
                        }

                        key = getHash(i + 1, j + 1, pInputList.Count());
                        CorrelationCoefficients[key] = coeff;
                    }

                    count++;
                }
            }

            return CorrelationCoefficients;
        }

        public static int getHash(int A, int B, int N)
        {
            int temp, hash_value;

            if (B < A)
            {
                temp = A;
                A = B;
                B = temp;
            }

            hash_value = N * (A - 1) - (A * (A - 1) / 2) + (B - A - 1);

            return hash_value;
        }

        public static double[,] XRandPointsInSpace(int x, double[,] pPopulation)
        {
            double[,] return_array = new double[x, 2];
            double x_min, x_max, y_min, y_max;
            int num_points = pPopulation.GetUpperBound(0) + 1;
            Random randGen = new Random();

            x_min = System.Linq.Enumerable.Range(0, num_points).Select(i => pPopulation[i, 0]).Min();
            x_max = System.Linq.Enumerable.Range(0, num_points).Select(i => pPopulation[i, 0]).Max();
            y_min = System.Linq.Enumerable.Range(0, num_points).Select(i => pPopulation[i, 1]).Min();
            y_max = System.Linq.Enumerable.Range(0, num_points).Select(i => pPopulation[i, 1]).Max();

            //Add 10% buffer to each dimension
            x_min -= ((x_max - x_min) / 10.0);
            x_max += ((x_max - x_min) / 10.0);
            y_min -= ((y_max - y_min) / 10.0);
            y_max += ((y_max - y_min) / 10.0);

            for (int i = 0; i < x; i++)
            {
                return_array[i, 0] = x_min + (randGen.NextDouble() * (x_max - x_min));
                return_array[i, 1] = y_min + (randGen.NextDouble() * (y_max - y_min));
            }

            return return_array;
        }

        //returns the first string of all alpha-numeric characters within the input string
        public static String GetFirstWord(String pInput)
        {
            String return_value = "";

            //Method 1
            System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9_]");
            return_value = rgx.Replace(pInput, " ");
            return_value = return_value.Substring(0, return_value.Trim().IndexOf(' ') + 1).Trim();

            //Method 2 - much slower
            //char[] arr = pInput.ToCharArray();
            //arr = Array.FindAll<char>(arr, (c => (!char.IsLetterOrDigit(c) && c != '_')));
            //return_value = pInput;
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    return_value.Replace(arr[i], ' ');
            //}
            
            return return_value;
        }

        public static void TestPOSTRequest()
        {
            WebRequest request;
            WebResponse response;
            StreamReader reader;
            GZipStream gzipStream;
            string responseText;
            MSHTML.HTMLDocument htmlresponse = new MSHTML.HTMLDocument();
            MSHTML.IHTMLDocument2 webresponse = (MSHTML.IHTMLDocument2)htmlresponse;
            string body = "curr_id=24442&smlID=1169009&st_date=11%2F18%2F2016&end_date=11%2F18%2F2017&interval_sec=Daily&sort_col=date&sort_ord=DESC&action=historical_data";

            try
            {
                request = WebRequest.Create("https://ca.investing.com/instruments/HistoricalDataAjax");
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).Accept = "*/*";
                ((HttpWebRequest)request).Method = "POST";
                ((HttpWebRequest)request).UserAgent = DEFAULT_USER_AGENT;
                ((HttpWebRequest)request).Referer = "http://ca.investing.com/equities/canada";
                ((HttpWebRequest)request).Headers.Add("Accept-Encoding", "gzip,deflate");
                ((HttpWebRequest)request).Headers.Add("X-Requested-With", "XMLHttpRequest");
                ((HttpWebRequest)request).Timeout = 20000;

                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.ContentLength = body.Length;

                Stream rStream = request.GetRequestStream();
                rStream.Write(body.Select(c => (byte)c).ToArray(), 0, body.Length);

                response = request.GetResponse();

                if (response.Headers.Get("Content-Encoding").ToLower().Contains("gzip"))
                {
                    gzipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    reader = new StreamReader(gzipStream);
                }
                else
                {
                    reader = new StreamReader(response.GetResponseStream());
                }
                responseText = reader.ReadToEnd();
                webresponse.write(responseText);

                while (webresponse.body == null)
                {
                    System.Windows.Forms.Application.DoEvents();
                }

                webresponse.close();

                reader.Close();
                response.Close();
                
            }
            catch (Exception e)
            {
                /* Do nothing */
                int stophere = 1;
            }
        }

        public static MSHTML.IHTMLDocument2 HTMLRequestResponse(String pUrl)
        {
            WebRequest request;
            WebResponse response;
            StreamReader reader;
            GZipStream gzipStream;
            string responseText;
            MSHTML.HTMLDocument htmlresponse = new MSHTML.HTMLDocument();
            MSHTML.IHTMLDocument2 webresponse = (MSHTML.IHTMLDocument2)htmlresponse;

            try
            {
                if (pUrl != "")
                {
                    request = WebRequest.Create(pUrl);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    ((HttpWebRequest)request).Accept = "*/*";
                    ((HttpWebRequest)request).UserAgent = DEFAULT_USER_AGENT;
                    ((HttpWebRequest)request).Referer = "http://ca.investing.com/equities/canada";
                    ((HttpWebRequest)request).Headers.Add("Accept-Encoding", "gzip,deflate,br");
                    ((HttpWebRequest)request).Headers.Add("X-Requested-With", "XMLHttpRequest");
                    ((HttpWebRequest)request).KeepAlive = true;
                    ((HttpWebRequest)request).Timeout = 20000;

                    response = request.GetResponse();

                    if (response.Headers.Get("Content-Encoding").ToLower().Contains("gzip"))
                    {
                        gzipStream = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                        reader = new StreamReader(gzipStream);
                    }
                    else
                    {
                        reader = new StreamReader(response.GetResponseStream());
                    }
                    responseText = reader.ReadToEnd();
                    webresponse.write(responseText);

                    while(webresponse.body == null)
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }

                    webresponse.close();

                    reader.Close();
                    response.Close();
                }
            }
            catch(Exception e)
            {
                /* Do nothing */
                int stophere = 1;
            }

            return webresponse;
        }

        public static List<String> ParseIHTMLElementCollection(MSHTML.IHTMLElementCollection pInputCollection)
        {
            List<String> return_list;
            return_list = new List<string>();

            foreach (MSHTML.IHTMLElement item in pInputCollection)
            {
                if (item.innerText != null && item.innerText.Trim() != "")
                {
                    return_list.Add(item.innerText);
                }
            }

            return return_list;
        }

        public static MSHTML.IHTMLElement2 FindHTMLElement(MSHTML.IHTMLElement2 pParent, String pTag, String pByAttribute = "", String pAttrValue = "")
        {
            MSHTML.IHTMLElementCollection searchCollection;
            MSHTML.IHTMLElement2 returnElement;
            dynamic attr_value;
            String attr_value_str;

            searchCollection = (MSHTML.IHTMLElementCollection)pParent.getElementsByTagName(pTag);
            returnElement = null;

            if(pByAttribute != "" && pAttrValue != "")
            {
                foreach (MSHTML.IHTMLElement item in searchCollection)
                {
                    attr_value = item.getAttribute(pByAttribute);
                    attr_value_str = ((object)attr_value == DBNull.Value) ? "" : (String)attr_value;

                    //class is a special case. The above code always returns null even if a class name exists
                    if (pByAttribute == "class")
                    {
                        attr_value_str = item.className;
                    }

                    if (attr_value_str == pAttrValue)
                    {
                        returnElement = (MSHTML.IHTMLElement2)item;
                        break;
                    }
                }
            }
            else
            {
                if(searchCollection != null && searchCollection.length > 0)
                {
                    returnElement = (MSHTML.IHTMLElement2)searchCollection.item(0);
                }
            }

            return returnElement;
        }

        //Returns a unique app-local id
        private static UInt32 ID = 0;
        public static UInt32 GetSimpleID()
        {
            return (++ID);
        }
    }
    //End of Helpers Class

    static class TechnicalIndicatorConst
    {
        public const string MACD = "MACD";
        public const string MACD_SIG = "MACD Signal";
        public const string MACD_DIF = "MACD Diff.";

        public const string ACC_DIST = "Accum. / Dist.";
        public const string TI_CHARTAREA_NAME = "TechnicalIndicators";
        public const string TI_CHARTLEGEND_NAME = "TIChartLegend";
    }

    static class Analysis
    {
        private static String[] chart_types = { StringEnum.GetStringValue(AnalysisChartType.AVERAGE), 
                                              StringEnum.GetStringValue(AnalysisChartType.ANIMATED)};
        private static String[] chart_features = { "Daily % Spread (high - low)", "Daily % Change (close - open)" };

        public static String[] ChartTypes
        {
            get { return chart_types; }
        }

        public static String[] ChartFeatures
        {
            get { return chart_features; }
        }
    }

    public enum AnalysisChartType
    {
        [StringValue("AVERAGE")]
        AVERAGE = 0,
        [StringValue("DAILY ANIMATED")]
        ANIMATED = 1
    }

    public enum StatTableType
    {
        HIST_STATS = 0,
        ANALYSIS_PPC = 1,
        INDIVIDUAL_PPC = 2,
        LIVE_STATS = 3
    }

    public enum Transformation
    {
        [StringValue("Gaussian")]
        GAUSS = 0,
        [StringValue("Mean")]
        MEAN = 1,
        [StringValue("Normalize")]
        NORMALIZE = 2
    }

    public enum ListOperator
    {
        SUM = 0,
        DIFF = 1,
        MULTIPLY = 2,
        DIVIDE = 3
    }

    public enum DisplayedDataSet
    {
        NONE,
        HISTORICAL,
        LIVE,
        BOTH
    }

    static class TableHeadings
    {
        public const string Name = "Name";
        public static readonly string[] PctChange = {"PctChange", "% Change"};

        public static readonly string[] Hist_Avg = {"AvgPrice", "Avg. Price"};
        public const string Hist_Vlty = "Volatility";
        public static readonly string[] Hist_DtStart = {"DateStart", "Start Date"};
        public static readonly string[] Hist_DtEnd = {"DateEnd", "End Date"};

        public const string PPC_Coeff = "Coeff";
        public static readonly string[] PPC_Max = { "MaxCoeff", "Max +ve" };
        public static readonly string[] PPC_Min = { "MinCoeff", "Max -ve" };

        public const string Live_Last = "Last";
        public const string Live_High = "High";
        public const string Live_Low = "Low";
        public const string Live_Chg = "Chg";
        public const string Live_Vol = "Volume";
        public const string Live_Time = "Time";
    }

    class ListDouble : List<double>
    {
        public ListDouble() : base()
        {
            //Interface for base constructor. No additional initialization required.
        }

        public ListDouble(List<double> pCopy) : base(pCopy)
        {
            //Interface for base constructor. No additional initialization required.
        }

        public static ListDouble operator +(ListDouble arg0, ListDouble arg1)
        {
            ListDouble outputList = new ListDouble();
            if(arg0.Count == arg1.Count && arg0.Count > 0)
            {
                for(int i = 0; i < arg0.Count; i++)
                {
                    outputList.Add(arg0[i] + arg1[i]);
                }
            }

            return outputList;
        }

        public static ListDouble operator *(ListDouble arg0, double arg1)
        {
            ListDouble outputList = new ListDouble();

            for (int i = 0; i < arg0.Count; i++)
            {
                outputList.Add(arg0[i] * arg1);
            }

            return outputList;
        }
    }

    public class StringValue : System.Attribute
    {
        private string _value;

        public StringValue(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public static class StringEnum
    {
        public static string GetStringValue(Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs =
               fi.GetCustomAttributes(typeof(StringValue),
                                       false) as StringValue[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }

    public class AnalysisResult
    {
        public double net_change;
        public List<double> net_change_daily;
        public List<double> cash_totals;
        public List<double> investments_totals;
        public List<int> units_change_daily;
        public Tuple<DateTime, DateTime> dates_from_to;
        
        public AnalysisResult()
        {
            net_change = 0.0;
            net_change_daily = new List<double>();
            cash_totals = new List<double>();
            investments_totals = new List<double>();
            units_change_daily = new List<int>();
        }
    }
}
