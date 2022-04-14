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
        private const int DATA_FILE_HEADER_ROWS = 2;

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

        private int start_date_index = 0;
        private int end_date_index = 0;

        //Live Data
        private List<DateTime> live_timestamps = new List<DateTime>();
        private List<double> live_price = new List<double>();
        private double live_open;
        private double live_low;
        private double live_high;
        private double live_chg;
        private double live_chg_pct;
        private double live_volume;

        //General Properties
        private int[] MACD_TIME_BASES = { 12, 26, 9 };

        private String dataFileName;
        private String liveDataAddress;
        private String listedMarket;
        public String Name { get; private set; }
        public String Symbol { get; private set; }
        public String Industry { get; private set; }
        public String Sector { get; private set; }

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
        //Public properties return a subset of the private data read from the CSV file, bounded by modifiable start and end indices
        public List<double> HistoricalPrice
        {
            get; private set;
        }

        public List<DateTime> HistoricalPriceDate
        {
            get; private set;
        }

        public List<double> HistoricalPctChange
        {
            get; private set;
        }

        public List<double> HistoricalOpens
        {
            get; private set;
        }

        public List<double> HistoricalHighs
        {
            get; private set;
        }

        public List<double> HistoricalLows
        {
            get; private set;
        }

        public List<double> HistoricalVolumes
        {
            get; private set;
        }

        //Define usable data range
        public DateTime HistStartDate
        {
            get
            {
                return hist_dates[start_date_index];
            }

            set
            {
                if(value > hist_dates[0] && value < hist_dates[hist_dates.Count - 1])
                {
                    start_date_index = hist_dates.Where(x => x < value).Count();
                }
                else
                {
                    start_date_index = 0;
                }

                updateHistPublicProperties();
            }
        }
        public DateTime HistEndDate
        {
            get
            {
                return hist_dates[end_date_index];
            }

            set
            {
                if (value > hist_dates[0] && value < hist_dates[hist_dates.Count - 1])
                {
                    end_date_index = hist_dates.Where(x => x < value).Count();
                }
                else
                {
                    end_date_index = hist_dates.Count - 1;
                }

                updateHistPublicProperties();
            }
        }

        private void updateHistPublicProperties()
        {
            int count = end_date_index - start_date_index + 1;

            this.HistoricalPriceDate = hist_dates.GetRange(start_date_index, count);
            this.HistoricalPrice = hist_price.GetRange(start_date_index, count);
            this.HistoricalPctChange = hist_daily_change.GetRange(start_date_index, count);
            this.HistoricalOpens = hist_price_opens.GetRange(start_date_index, count);
            this.HistoricalHighs = hist_price_highs.GetRange(start_date_index, count);
            this.HistoricalLows = hist_price_lows.GetRange(start_date_index, count);
            this.HistoricalVolumes = hist_volumes.GetRange(start_date_index, count);

            CalculatePctChange();
            CalculateAvgPrice();
            CalculateVolatility();
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
            int index1, index2;
            string linkStr;

            if (dataFileName != "")
            {
                //parse the CSV into a list of string arrays
                rawData = CSVParser.CSVtoListArray(dataFileName, MAX_DATA_SIZE);

                linkStr = rawData[0][0];
                if(linkStr != null && linkStr.Contains("http"))
                {
                    this.liveDataAddress = linkStr;
                }

                //array index of each column of data 
                //Indexes hardcoded based on ICom tables
                //historical data - Dates
                hist_data_in = rawData.Select(arr => arr[0]).ToList();
                hist_dates = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(date => DateTime.Parse(date)).ToList();
                hist_dates.Reverse();

                //historical data - Closing Price
                hist_data_in = rawData.Select(arr => arr[1]).ToList();
                hist_price = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(dbl => Helpers.CustomParseDouble(dbl)).ToList();
                hist_price.Reverse();

                //historical data - Opening Price
                hist_data_in = rawData.Select(arr => arr[2]).ToList();
                hist_price_opens = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(dbl => Helpers.CustomParseDouble(dbl)).ToList();
                hist_price_opens.Reverse();

                //historical data - High
                hist_data_in = rawData.Select(arr => arr[3]).ToList();
                hist_price_highs = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(dbl => Helpers.CustomParseDouble(dbl)).ToList();
                hist_price_highs.Reverse();

                //historical data - Low
                hist_data_in = rawData.Select(arr => arr[4]).ToList();
                hist_price_lows = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(dbl => Helpers.CustomParseDouble(dbl)).ToList();
                hist_price_lows.Reverse();

                //historical data - Volume
                hist_data_in = rawData.Select(arr => arr[5]).ToList();
                hist_volumes = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(dbl => Helpers.CustomParseDouble(dbl)).ToList();
                hist_volumes.Reverse();
                
                //historical data - percent change
                hist_data_in = rawData.Select(arr => arr[6]).ToList();
                hist_daily_change = hist_data_in.Skip(DATA_FILE_HEADER_ROWS).Select(dbl => Math.Round(double.Parse(dbl.Replace("%", "")), 8)).ToList();
                hist_daily_change.Reverse();
                
                index1 = dataFileName.LastIndexOf("\\") + 1;
                index2 = dataFileName.LastIndexOf(".csv");
                if(index1 > 0 && index2 > 0)
                {
                    Name = dataFileName.Substring(index1, index2 - index1);
                }

                //IMPORTANT: ValidateHistoricalData() must be called before updateHistPublicProperties() to ensure the private
                // "hist_" data lists are finalized with any duplicate values removed.
                if (ValidateHistoricalData())
                {
                    this.ContainsHistData = true;
                }

                GetProfile();
                updateHistPublicProperties();

                //Compute technical indicators
                CalculatePctChange();
                CalculateAvgPrice();
                CalculateVolatility();
                ComputeTrend();
                ComputeTechnicalIndicators();

                

                //set return status successful
                success = true;
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  GetProfile
         *  Description:    Get the company profile if it exists in the expected location
         *                  The expected location is a subfolder within the same folder as
         *                  the equity source .csv file with the word "Profile" in the name.
         *                  
         *                  This function will search only the first subfolder it finds 
         *                  matching this pattern
         *  Parameters: 
         *****************************************************************************/
        private void GetProfile()
        {
            List<string> searchNames = new List<string>();
            string root = "", pf = "";
            bool profileFound = false;
            string[] profiles, pf_lines;
            List<string[]> profile_content;

            try
            {
                root = this.dataFileName.Substring(0, dataFileName.LastIndexOf("\\"));
                string[] dirs = Directory.GetDirectories(root, "*Profile*", SearchOption.AllDirectories);

                if(dirs.Length > 0)
                {
                    //Define search keys
                    searchNames.Add(Name);
                    searchNames.Add(Name.Replace(" ", "-").ToLower());

                    //Find the first file name matching one of the search keys and suffixed with "_profile"
                    foreach(string sName in searchNames)
                    {
                        profiles = Directory.GetFiles(dirs[0], string.Concat(sName, "*_profile.txt"), SearchOption.TopDirectoryOnly);
                        if(profiles.Length > 0)
                        {
                            pf = profiles[0];
                            break;
                        }
                    }

                    //Read the profile text file
                    // For data to be read properly, a value must immediately follow the corresponding key on the same line 
                    // and separated by a comma.
                    //
                    // ex. "Symbol,AC" or "Industry,Airline"
                    if (pf != "")
                    {
                        profile_content = CSVParser.CSVtoListArray(pf, 20);

                        //Symbol = profile_content.Where(arg => arg[0] == "Symbol").FirstOrDefault()[1];
                        pf_lines = profile_content.Where(arg => arg.Contains("Symbol")).FirstOrDefault();
                        Symbol = pf_lines[pf_lines.ToList().IndexOf("Symbol") + 1];

                        pf_lines = profile_content.Where(arg => arg.Contains("Industry")).FirstOrDefault();
                        Industry = pf_lines[pf_lines.ToList().IndexOf("Industry") + 1];

                        pf_lines = profile_content.Where(arg => arg.Contains("Sector")).FirstOrDefault();
                        Sector = pf_lines[pf_lines.ToList().IndexOf("Sector") + 1];

                        profileFound = true;
                    }
                }
            }
            catch(Exception)
            {

            }

            if(!profileFound)
            {
                SetDefaultSymbol();
            }
        }

        /*****************************************************************************
         *  FUNCTION:  SetDefaultSymbol
         *  
         *  Description:    If no ticker symbol is found for this equity, this function
         *                  creates a default one using the first alpha-numeric value 
         *                  in each word of the Name field.
         *                  
         *****************************************************************************/
        private void SetDefaultSymbol()
        {
            string[] words = Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            Symbol = "";

            if (words != null && words.Length > 0)
            {
                foreach(string str in words)
                {
                    Symbol += str.Substring(0, 1).ToUpper();
                }
                Symbol = Helpers.RemoveNonAlphanumeric(Symbol) + "?";
            }
        }

        public void UpdateSymbol(string newSymbol)
        {
            Symbol = newSymbol;
            if(Symbol.Contains("?"))
            {
                Symbol = Symbol.Replace("?", "") + "?";
            }
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
                GetProfile();
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
            catch(Exception)
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
            catch(Exception)
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
                catch(Exception)
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
            int index = 0;

            //Check for duplicate data entries
            List<DateTime> duplicates = hist_dates.GroupBy(x => x)
                                         .Where(g => g.Count() > 1)
                                         .Select(g => g.Key)
                                         .ToList();

            //If duplicates exist, remove all but the last occurence of the value from each vector
            foreach (DateTime elt in duplicates)
            {
                string tempstr = this.Name;
                index = hist_dates.IndexOf(elt);

                while(index != hist_dates.LastIndexOf(elt))
                {
                    hist_dates.RemoveAt(index);
                    hist_price.RemoveAt(index);
                    hist_price_opens.RemoveAt(index);
                    hist_price_highs.RemoveAt(index);
                    hist_price_lows.RemoveAt(index);
                    hist_volumes.RemoveAt(index);
                    hist_daily_change.RemoveAt(index);
                    index = hist_dates.IndexOf(elt);
                }
            }

            validated &= (hist_dates.Count() == hist_price.Count());
            validated &= (hist_dates.Count() == hist_price_opens.Count());
            validated &= (hist_dates.Count() == hist_price_highs.Count());
            validated &= (hist_dates.Count() == hist_price_lows.Count());
            validated &= (hist_dates.Count() == hist_volumes.Count());

            this.end_date_index = hist_dates.Count - 1;

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
            //validated &= (this.live_chg != null);
            //validated &= (this.live_chg_pct != null);
            //validated &= (this.live_high != null);
            //validated &= (this.live_low != null);
            //validated &= (this.live_volume != null);

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
                pctChange = Math.Round(((HistoricalPrice.Last() - HistoricalPrice.First()) / HistoricalPrice.First()) * 100, 2);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  CalculateAvgPrice
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CalculateAvgPrice()
        {
            if (HistoricalPrice.Count() > 0)
            {
                avgPrice = Math.Round(HistoricalPrice.Average(), 2);
                avgDailyPctChange = HistoricalPctChange.Average();
                avgDailyVolume = (long)HistoricalVolumes.Average();
            }
        }

        /*****************************************************************************
         *  FUNCTION:  CalculateVolatility
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CalculateVolatility()
        {
            Volatility = Helpers.StdDev(HistoricalPctChange);
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
            AccumDistrIndex = Helpers.AccumulationDistributionIndex(hist_price, hist_price_highs, hist_price_lows, hist_volumes).GetRange(start_date_index, end_date_index);

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
            scaled_volumes = HistoricalVolumes.Select(x => (double)((double)x / (double)HistoricalVolumes.Max())).ToList();


            //Trend will be one of {-1, 0, +1}
            Trend = HistoricalPctChange.Select(sign => (Math.Abs(sign) < pct_change_zero) ? 0 : (int)(sign / Math.Abs(sign))).ToList();
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
                weight = Math.Pow((scaled_volumes[i] / emAvg), Math.Abs(HistoricalPctChange[i]));

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
            int window_size, rDecimals;
            bool transformApplied = false;

            rDecimals = 2;
            switch (pType)
            {
                case Transformation.GAUSS:
                    try
                    {
                        pSigma = Double.Parse(pParams["pSigma"]);
                        hist_price = Algorithms.GaussianBlur(hist_price, pSigma);
                        transformApplied = true;
                    }
                    catch(Exception){ }
                    break;
                case Transformation.MEAN:
                    try
                    {
                        window_size = int.Parse(pParams["Window"]);
                        hist_price = Algorithms.MeanFilter(hist_price, window_size);
                        transformApplied = true;
                    }
                    catch (Exception)
                    {
                        window_size = 1;
                    }
                    break;
                case Transformation.NORMALIZE:
                    try
                    {
                        hist_price = Algorithms.Normalize(hist_price, 0);
                        rDecimals = 3;
                        transformApplied = true;
                    }
                    catch(Exception){ }
                    break;
                default:
                    break;
            }

            for (int i = 0; i < hist_price.Count; i++)
            {
                hist_price[i] = Math.Round(hist_price[i], rDecimals);
            }

            if (transformApplied)
            {
                updateHistPublicProperties();
                CalculateAvgPrice();
            }
        }
        #endregion

        public void GetSentimentScore()
        {
            //Get source text to analyze
            // - need predetermined list of possible searchable sources
            // - 
        }

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

            this.HistoricalPriceDate.Clear();
            this.HistoricalPrice.Clear();
            this.HistoricalPctChange.Clear();
            this.HistoricalOpens.Clear();
            this.HistoricalHighs.Clear();
            this.HistoricalLows.Clear();
            this.HistoricalVolumes.Clear();

            this.live_price.Clear();
            this.live_timestamps.Clear();
        }
    }
}
