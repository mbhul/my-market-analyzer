using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace MyMarketAnalyzer
{
    public class Equity : IXmlSerializable
    {
        //***** CONSTANTS *****//
        private const int MAX_DATA_SIZE = 256;
        private const int LIVE_DATA_BUFFER_SIZE = 256;

        //***** DATA MEMBERS *****//
        private List<String> hist_data_in;

        //Historical Data
        private List<DateTime> hist_dates = new List<DateTime>();
        private List<double> hist_price = new List<double>();
        private List<double> hist_price_opens = new List<double>();
        private List<double> hist_price_highs = new List<double>();
        private List<double> hist_price_lows = new List<double>();
        private List<double> hist_volumes = new List<double>();
        private List<double> hist_daily_change = new List<double>();

        //Live Data
        private List<DateTime> live_timestamps = new List<DateTime>();
        private List<double> live_price = new List<double>();
        private double live_open;
        private double live_low;
        private double live_high;
        private double live_chg;
        private double live_chg_pct;
        private double live_volume;

        private int[] MACD_TIME_BASES = { 12, 26, 9 };

        private String dataFileName;
        private String liveDataAddress;
        private String listedMarket;
        public String Name { get; private set; }

        //***** PROPERTIES *****//
        //Numerical properties
        public Double pctChange { get; private set; }
        public Double Volatility { get; private set; }
        public Double avgPrice { get; private set; }
        public Double avgDailyPctChange { get; private set; }
        public Double avgDailyVolume { get; private set; }

        //List properties
        public List<int> Trend { get; private set; }
        public List<Double> WeightedTrend { get; private set; }

        public List<Double> AccumDistrIndex { get; private set; }
        public List<Double> MACD_A { get; private set; }
        public List<Double> MACD_B { get; private set; }
        public List<Double> MACD_C { get; private set; }

        //Public Flags
        public bool ContainsLiveData { get; private set; }
        public bool ContainsHistData { get; private set; }

        public Equity()
        {
            Trend = new List<int>();
            WeightedTrend = new List<double>();
            ContainsLiveData = false;
            ContainsHistData = false;
        }

        public Equity(String pName)
        {
            this.Name = pName;
            Trend = new List<int>();
            WeightedTrend = new List<double>();
            ContainsLiveData = false;
            ContainsHistData = false;
        }

        #region Get/Set Interface Functions
        //****** Historical Data Interfaces ******//
        public List<double> HistoricalPrice
        {
            get { return hist_price; }
        }

        public List<DateTime> HistoricalPriceDate
        {
            get { return hist_dates; }
        }

        public List<double> HistoricalPctChange
        {
            get { return hist_daily_change; }
        }

        public List<double> HistoricalOpens
        {
            get { return hist_price_opens; }
        }

        public List<double> HistoricalHighs
        {
            get { return hist_price_highs; }
        }

        public List<double> HistoricalLows
        {
            get { return hist_price_lows; }
        }

        public List<double> HistoricalVolumes
        {
            get { return hist_volumes; }
        }

        //****** Live Data Interfaces *******//
        public Double DailyHigh
        {
            get { return live_high; }
        }

        public Double DailyLow
        {
            get { return live_low; }
        }

        public Double DailyChg
        {
            get { return live_chg; }
        }

        public Double DailyChgPct
        {
            get { return live_chg_pct; }
        }

        public Double DailyVolume
        {
            get { return live_volume; }
        }

        public List<Double> DailyLast
        {
            get { return live_price; }
        }

        public List<DateTime> DailyTime
        {
            get { return live_timestamps; }
        }

        //****** Get/Set Property Interfaces *******//
        public String DataFileName
        {
            get { return dataFileName; }
            set { dataFileName = value; }
        }

        public String LiveDataAddress
        {
            get { return liveDataAddress; }
            set 
            { 
                liveDataAddress = value;
                if(!liveDataAddress.Contains("http:"))
                {
                    liveDataAddress = "http://" + liveDataAddress;
                }
            }
        }

        public String ListedMarket
        {
            get { return listedMarket; }
            set { listedMarket = value; }
        }
        #endregion

        #region XML Serialization Functions
        public XmlElement ToXmlElement()
        {
            XmlDocument doc = new XmlDocument();
            
            using (XmlWriter writer = doc.CreateNavigator().AppendChild())
            {
                new System.Xml.Serialization.XmlSerializer(typeof(Equity)).Serialize(writer, this);
            }

            return doc.DocumentElement;
        }

        // Xml Serialization Infrastructure
        public void WriteXml(XmlWriter writer)
        {
            XmlDocument doc = new XmlDocument();

            //write Name field
            writer.WriteElementString("Name", Name);

            writer.WriteStartElement("HistData");
            if(ValidateHistoricalData() == true)
            {
                writer.WriteElementString("HistDataLabels", "Date,Close,Open,High,Low,Volume");
                for(int i = 0; i < hist_dates.Count(); i++)
                {
                    writer.WriteStartElement("HistElement", "HistNS");
                    writer.WriteElementString("DataValues", hist_dates[i].ToString() + "," +
                        hist_price[i].ToString() + "," + hist_price_opens[i].ToString() + "," +
                        hist_price_highs[i].ToString() + "," + hist_price_lows[i].ToString() + "," +
                        hist_volumes[i].ToString());

                    writer.WriteEndElement();
                }
                
            }
            writer.WriteEndElement();   
        }

        public void ReadXml(XmlReader reader)
        {
           // personName = reader.ReadString();
        }

        public XmlSchema GetSchema()
        {
            return (null);
        }
        #endregion

        /*****************************************************************************
         *  FUNCTION:  ReadDataFile
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public Boolean ReadDataFile()
        {
            Boolean success = false;
            List<String[]> rawData = null;
            int index1, index2, volLen;

            if (dataFileName != "")
            {
                //parse the CSV into a list of string arrays
                rawData = CSVParser.CSVtoListArray(dataFileName, MAX_DATA_SIZE);

                //array index of each column of data hardcoded based on ICom downloads
                //historical data - Dates
                hist_data_in = rawData.Select(arr => arr[0]).ToList();
                hist_dates = hist_data_in.Skip(1).Select(date => DateTime.Parse(date)).ToList();
                hist_dates.Reverse();

                //historical data - Closing Price
                hist_data_in = rawData.Select(arr => arr[1]).ToList();
                hist_price = hist_data_in.Skip(1).Select(dbl => double.Parse(dbl)).ToList();
                hist_price.Reverse();

                //historical data - Opening Price
                hist_data_in = rawData.Select(arr => arr[2]).ToList();
                hist_price_opens = hist_data_in.Skip(1).Select(dbl => double.Parse(dbl)).ToList();
                hist_price_opens.Reverse();

                //historical data - High
                hist_data_in = rawData.Select(arr => arr[3]).ToList();
                hist_price_highs = hist_data_in.Skip(1).Select(dbl => double.Parse(dbl)).ToList();
                hist_price_highs.Reverse();

                //historical data - Low
                hist_data_in = rawData.Select(arr => arr[4]).ToList();
                hist_price_lows = hist_data_in.Skip(1).Select(dbl => double.Parse(dbl)).ToList();
                hist_price_lows.Reverse();

                //historical data - Volume
                hist_data_in = rawData.Select(arr => arr[5]).ToList();
                for (int i = 1; i < hist_data_in.Count(); i++)
                {
                    try
                    {
                        volLen = hist_data_in[i].Length;
                        volLen = volLen - Regex.Replace(hist_data_in[i], "[.].*", "").Length - 2;
                        hist_data_in[i] = hist_data_in[i].Replace(".", "");
                        if (volLen > 0 && hist_data_in[i].Contains("K"))
                        {
                            hist_data_in[i] += String.Join("", Enumerable.Repeat("0", 3 - volLen));
                        }
                        else if (volLen > 0 && hist_data_in[i].Contains("M"))
                        {
                            hist_data_in[i] += String.Join("", Enumerable.Repeat("0", 6 - volLen));
                        }
                        hist_data_in[i] = Regex.Replace(hist_data_in[i], "[A-Z]", "");
                    }
                    catch (Exception e)
                    {
                        hist_data_in[i] = "0";
                    }
                }
                hist_volumes = hist_data_in.Skip(1).Select(dbl => double.Parse(dbl)).ToList();
                hist_volumes.Reverse();
                

                //historical data - percent change
                hist_data_in = rawData.Select(arr => arr[6]).ToList();
                hist_daily_change = hist_data_in.Skip(1).Select(dbl => Math.Round(double.Parse(dbl.Replace("%", "")), 8)).ToList();
                hist_daily_change.Reverse();
                
                index1 = dataFileName.LastIndexOf("\\") + 1;
                index2 = dataFileName.LastIndexOf(".csv");
                if(index1 > 0 && index2 > 0)
                    this.Name = dataFileName.Substring(index1, index2 - index1);
                
                //Compute technical indicators
                CalculatePctChange();
                CalculateAvgPrice();
                CalculateVolatility();
                ComputeTrend();
                ComputeTechnicalIndicators();

                if(ValidateHistoricalData())
                {
                    this.ContainsHistData = true;
                }

                //set return status successful
                success = true;
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateLiveData
         *  Description:    
         *  Parameters: 
         *          pDataLabels - 
         *          pDataValues - 
         *****************************************************************************/
        public Boolean UpdateLiveData(List<String> pDataLabels, List<String> pDataValues, String pLiveUrl = "")
        {
            Boolean success = false;
            int item_index = 0;
            string item_value = "";
            DateTime last_time;

            //Ensure lists are initialized
            if (live_price == null)
            {
                live_price = new List<double>();
            }
            if (live_timestamps == null)
            {
                live_timestamps = new List<DateTime>();
            }

            //Set the live data source address
            if (pLiveUrl != "")
            {
                LiveDataAddress = pLiveUrl;
            }

            foreach (String label in pDataLabels)
            {
                item_value = pDataValues[item_index];

                //ToDo: optimize
                switch (label)
                {
                    case "Last":
                        if (Helpers.ValidateNumeric(pDataValues[item_index]))
                        {
                            this.live_price.Add(Double.Parse(pDataValues[item_index]));
                            if (this.live_price.Count > LIVE_DATA_BUFFER_SIZE)
                            {
                                this.live_price.RemoveAt(0);
                            }
                        }
                        break;
                    case "High":
                        if (Helpers.ValidateNumeric(pDataValues[item_index]))
                        {
                            this.live_high = Double.Parse(pDataValues[item_index]);
                        }
                        break;
                    case "Low":
                        if (Helpers.ValidateNumeric(pDataValues[item_index]))
                        {
                            this.live_low = Double.Parse(pDataValues[item_index]);
                        }
                        break;
                    case "Chg.":
                        if (Helpers.ValidateNumeric(pDataValues[item_index]))
                        {
                            this.live_chg = Double.Parse(pDataValues[item_index]);
                        }
                        break;
                    case "Chg. %":
                        if (pDataValues[item_index].Contains("%"))
                        {
                            this.live_chg_pct = Double.Parse(pDataValues[item_index].Replace("%", ""));
                        }
                        break;
                    case "Vol.":
                        this.live_volume = Helpers.ConvertVolumeString(pDataValues[item_index]);
                        break;
                    case "Time":
                        last_time = DateTime.Parse(pDataValues[item_index]);

                        if (this.live_timestamps.Count > 0 &&
                            this.live_timestamps[this.live_timestamps.Count - 1] == last_time)
                        {
                            if(this.live_price.Count > this.live_timestamps.Count)
                            {
                                this.live_price.RemoveAt(this.live_price.Count - 1);
                            }
                            item_index = pDataLabels.Count;
                        }
                        else
                        {
                            this.live_timestamps.Add(last_time);
                            if (this.live_timestamps.Count > LIVE_DATA_BUFFER_SIZE)
                            {
                                this.live_timestamps.RemoveAt(0);
                            }
                        }
                        break;
                    default:
                        break;
                }

                item_index++;

                if(item_index > pDataLabels.Count)
                {
                    break;
                }
            }

            if(ValidateLiveData())
            {
                this.ContainsLiveData = true;
            }
            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateLiveData
         *  Description:    
         *  Parameters: 
         *          pLiveUrl - 
         *****************************************************************************/
        public Boolean UpdateLiveData(String pLiveUrl = "")
        {
            bool success = true;
            MSHTML.HTMLDocument htmlresponse = new MSHTML.HTMLDocument();
            MSHTML.IHTMLDocument2 webresponse = (MSHTML.IHTMLDocument2)htmlresponse;

            try
            {
                if(pLiveUrl != "")
                {
                    LiveDataAddress = pLiveUrl;
                }

                if (LiveDataAddress != "")
                {
                    webresponse = Helpers.HTMLRequestResponse(LiveDataAddress);
                    ParseLiveData(webresponse);
                }
            }
            catch(Exception e)
            {
                success = false;
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  ParseLiveData
         *  Description:    
         *  Parameters: 
         *          pHtmlData - 
         *****************************************************************************/
        private void ParseLiveData(MSHTML.IHTMLDocument2 pHtmlData)
        {
            MSHTML.IHTMLElement2 docBody;
            MSHTML.IHTMLElement2 quoteElement, quoteElement2;
            MSHTML.IHTMLElementCollection searchCollection;
            List<String> values = null;

            try
            {
                docBody = (MSHTML.IHTMLElement2)pHtmlData.body;

                //build the array of column headers
                quoteElement = Helpers.FindHTMLElement(docBody,"div","id","quotes_summary_current_data");

                if(quoteElement != null)
                {
                    quoteElement2 = Helpers.FindHTMLElement(quoteElement, "div", "class", "inlineblock");

                    if(quoteElement2 != null)
                    {
                        searchCollection = quoteElement2.getElementsByTagName("span");
                        values = Helpers.ParseIHTMLElementCollection(searchCollection);

                        this.live_price.Add(double.Parse(values[0]));
                        this.live_chg = double.Parse(values[1]);
                        this.live_chg_pct = double.Parse(values[2].Replace("%",""));
                        this.live_timestamps.Add(DateTime.Parse(values[3]));
                    }
                }

            }
            catch(Exception e)
            {
                //TBD
            }
        }

        /*****************************************************************************
         *  FUNCTION:  LoadDataFromProfile
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void LoadDataFromProfile(Profile pUser, Double pLastPrice, String pChange, DateTime pDate)
        {
            String substr;
            int index;

            if(pUser.IsInitializing)
            {
                this.live_price = new List<double>();
                this.live_price.Add(pLastPrice);

                this.live_timestamps = new List<DateTime>();
                this.live_timestamps.Add(pDate);

                try
                {
                    index = pChange.IndexOf('(');
                    substr = pChange.Substring(0, index);
                    this.live_chg = Double.Parse(substr);

                    substr = pChange.Substring(index + 1, pChange.IndexOf('%') - index - 1);
                    this.live_chg_pct = Double.Parse(substr);
                }
                catch(Exception e)
                {  
                    //Do Nothing
                }

                if (ValidateLiveData())
                {
                    this.ContainsLiveData = true;
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:  ValidateHistoricalData
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private Boolean ValidateHistoricalData()
        {
            Boolean validated = true;

            validated &= (hist_dates.Count() == hist_price.Count());
            validated &= (hist_dates.Count() == hist_price_opens.Count());
            validated &= (hist_dates.Count() == hist_price_highs.Count());
            validated &= (hist_dates.Count() == hist_price_lows.Count());
            validated &= (hist_dates.Count() == hist_volumes.Count());

            return validated;
        }

        /*****************************************************************************
         *  FUNCTION:  ValidateLiveData
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private Boolean ValidateLiveData()
        {
            Boolean validated = true;

            validated &= (live_timestamps.Count == live_price.Count);
            validated &= (this.live_chg != null);
            validated &= (this.live_chg_pct != null);
            validated &= (this.live_high != null);
            validated &= (this.live_low != null);
            validated &= (this.live_volume != null);

            return validated;
        }

        /*****************************************************************************
         *  FUNCTION:  TrimDataLeft
         *  Description:    
         *  Parameters: 
         *          pNumPointsToRemove - 
         *****************************************************************************/
        public bool TrimDataLeft(int pNumPointsToRemove)
        {
            bool success = true;

            if(pNumPointsToRemove >= 0 && pNumPointsToRemove < this.hist_dates.Count)
            {
                this.hist_dates = this.hist_dates.Skip(pNumPointsToRemove).ToList();
                this.hist_price = this.hist_price.Skip(pNumPointsToRemove).ToList();
                this.hist_price_opens = this.hist_price_opens.Skip(pNumPointsToRemove).ToList();
                this.hist_price_highs = this.hist_price_highs.Skip(pNumPointsToRemove).ToList();
                this.hist_price_lows = this.hist_price_lows.Skip(pNumPointsToRemove).ToList();
                this.hist_volumes = this.hist_volumes.Skip(pNumPointsToRemove).ToList();
                this.hist_daily_change = this.hist_daily_change.Skip(pNumPointsToRemove).ToList();
            }
            else
            {
                success = false;
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  TrimDataRight
         *  Description:    
         *  Parameters: 
         *          pNumPointsToRemove - 
         *****************************************************************************/
        public bool TrimDataRight(int pNumPointsToRemove)
        {
            bool success = true;

            if (pNumPointsToRemove >= 0 && pNumPointsToRemove < this.hist_dates.Count)
            {
                this.hist_dates = this.hist_dates.Take(this.hist_dates.Count - pNumPointsToRemove).ToList();
                this.hist_price = this.hist_price.Take(this.hist_price.Count - pNumPointsToRemove).ToList();
                this.hist_price_opens = this.hist_price_opens.Take(this.hist_price_opens.Count - pNumPointsToRemove).ToList();
                this.hist_price_highs = this.hist_price_highs.Take(this.hist_price_highs.Count - pNumPointsToRemove).ToList();
                this.hist_price_lows = this.hist_price_lows.Take(this.hist_price_lows.Count - pNumPointsToRemove).ToList();
                this.hist_volumes = this.hist_volumes.Take(this.hist_volumes.Count - pNumPointsToRemove).ToList();
                this.hist_daily_change = this.hist_daily_change.Take(this.hist_daily_change.Count - pNumPointsToRemove).ToList();
            }
            else
            {
                success = false;
            }

            return success;
        }

        #region Math Functions
        /*****************************************************************************
         *  FUNCTION:  CalculatePctChange
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CalculatePctChange()
        {
            if(hist_price.Count() > 0)
            {
                pctChange = Math.Round(((hist_price.Last() - hist_price.First()) / hist_price.First()) * 100, 2);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  CalculateAvgPrice
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CalculateAvgPrice()
        {
            if (hist_price.Count() > 0)
            {
                avgPrice = Math.Round(hist_price.Average(), 2);
                avgDailyPctChange = hist_daily_change.Average();
                avgDailyVolume = (long)hist_volumes.Average();
            }
        }

        /*****************************************************************************
         *  FUNCTION:  CalculateVolatility
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CalculateVolatility()
        {
            Volatility = Helpers.StdDev(hist_daily_change);
        }

        /*****************************************************************************
         *  FUNCTION:  ComputeTechnicalIndicators
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void ComputeTechnicalIndicators()
        {
            List<Double> out_macd_a, out_macd_b, out_macd_c;

            //*** Accumulation / Distribution Index ***//
            AccumDistrIndex = Helpers.AccumulationDistributionIndex(hist_price, hist_price_highs, hist_price_lows, hist_volumes);

            //*** MACD ***//
            Helpers.ComputeMACD(MACD_TIME_BASES, hist_price, out out_macd_a, out out_macd_b, out out_macd_c);
            MACD_A = out_macd_a;
            MACD_B = out_macd_b;
            MACD_C = out_macd_c;
        }

        /*****************************************************************************
         *  FUNCTION:  ComputeTrend
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void ComputeTrend()
        {
            int i, windowSize;
            double alpha, emAvg, weight, xmin, xmax;
            List<double> scaled_volumes;

            //daily percent change less than this value will be considered as zero
            const double pct_change_zero = 0.00001;

            //set scaled volume such that -1 < x < 1
            scaled_volumes = hist_volumes.Select(x => (double)((double)x / (double)hist_volumes.Max())).ToList();


            //Trend will be one of {-1, 0, +1}
            Trend = hist_daily_change.Select(sign => (Math.Abs(sign) < pct_change_zero) ? 0 : (int)(sign / Math.Abs(sign))).ToList();
            WeightedTrend.Clear();

            windowSize = 5;

            emAvg = scaled_volumes[0];
            for(i = 0; i < Trend.Count; i++)
            {
                if(i < windowSize - 1)
                {
                    alpha = 2.0 / ((double)(i + 1) + 1.0);
                }
                else
                {
                    alpha = 2.0 / ((double)(windowSize) + 1.0);
                }

                scaled_volumes[i] += 1;
                emAvg = (scaled_volumes[i] * alpha) + ((1 - alpha) * emAvg);

                //weight = (scaled volume / exp. average volume) ^ daily % change
                // note: 100% = 1.0
                weight = Math.Pow((scaled_volumes[i] / emAvg), Math.Abs(hist_daily_change[i]));

                WeightedTrend.Add(Trend[i] * weight);
            }

            //Normalize the weighted trend such that -1 < x < 1
            // x' = -1 + (x - xmin)(1 - (-1)) / (xmax - xmin)
            xmin = WeightedTrend.Min();
            xmax = WeightedTrend.Max();
            for (i = 0; i < WeightedTrend.Count; i++)
            {
                WeightedTrend[i] = -1.0 + ((WeightedTrend[i] - xmin) * 2) / (xmax - xmin);
            }

            i = 0;
        }

        /*****************************************************************************
         *  FUNCTION:  ClearTransformation
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void ClearTransformation()
        {
            ReadDataFile();
        }

        /*****************************************************************************
         *  FUNCTION:  TransformData
         *  Description:    
         *  Parameters: 
         *          pType   - 
         *          pParams -
         *****************************************************************************/
        public void TransformData(Transformation pType, Dictionary<String, String> pParams)
        {
            double pSigma;
            int window_size;

            switch(pType)
            {
                case Transformation.GAUSS:
                    try
                    {
                        pSigma = Double.Parse(pParams["pSigma"]);
                        hist_price = Algorithms.GaussianBlur(hist_price, pSigma);
                    }
                    catch(Exception e){ }
                    break;
                case Transformation.MEAN:
                    try
                    {
                        window_size = int.Parse(pParams["Window"]);
                        hist_price = Algorithms.MeanFilter(hist_price, window_size);
                    }
                    catch (Exception e)
                    {
                        window_size = 1;
                    }
                    break;
                case Transformation.NORMALIZE:
                    try
                    {
                        hist_price = Algorithms.Normalize(hist_price, 0);
                    }
                    catch(Exception e){ }
                    break;
                default:
                    break;
            }
        }
        #endregion

        public void Clear()
        {
            this.hist_daily_change.Clear();
            this.hist_data_in.Clear();
            this.hist_dates.Clear();
            this.hist_price.Clear();
            this.hist_price_highs.Clear();
            this.hist_price_lows.Clear();
            this.hist_price_opens.Clear();
            this.hist_volumes.Clear();
            this.live_price.Clear();
            this.live_timestamps.Clear();
        }
    }
}
