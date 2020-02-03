using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Net;

namespace MyMarketAnalyzer
{
    public class ExchangeMarket
    {
        private String downloadedDataPath = "";
        private String liveDataUrl = "";
        private List<Equity> constituents;
        private Double pct_download;

        public String Name { get; private set; }

        public DateTime DateStart { get; private set; }
        public DateTime DateEnd { get; private set; }
        public int MaxDataPoints { get; private set; }
        public Boolean IsDataAligned { get; private set; }

        public Double[] CorrelationCoefficients { get; private set; }

        /*****************************************************************************
         *  CONSTRUCTOR:  ExchangeMarket
         *  Description:    
         *  Parameters: 
         *          path - 
         *****************************************************************************/
        public ExchangeMarket(String path)
        {
            downloadedDataPath = path;
            constituents = null;
            pct_download = 0;
            this.Name = "";
            this.IsDataAligned = true;
        }

        /*****************************************************************************
         *  CONSTRUCTOR:  ExchangeMarket
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public ExchangeMarket()
        {
            pct_download = 0;
            this.Name = "";
            this.IsDataAligned = true;
        }

        #region Class Properties
        public List<Equity> Constituents
        {
            get { return constituents; }
        }

        public String GetCurrentRepoPath()
        {
            return downloadedDataPath;
        }

        public void SetCurrentRepoPath(String path)
        {
            downloadedDataPath = path;
        }

        public Double DownloadPercentage
        {
            get { return pct_download; }
        }
        #endregion

        /*****************************************************************************
         *  FUNCTION:  parseCsvHistoricalData
         *  Description:    
         *  Parameters: 
         *          pContainingFolder - 
         *****************************************************************************/
        public Boolean parseCsvHistoricalData(String pContainingFolder = "")
        {
            Boolean success = false;
            DirectoryInfo rootDir;
            FileInfo[] files = null;
            int itemCount, index;
            Equity cEQ = null;
            List<Equity> searchResult;

            //Get the root folder
            if(pContainingFolder == "")
            {
                rootDir = new DirectoryInfo(downloadedDataPath);
            }
            else
            {
                rootDir = new DirectoryInfo(pContainingFolder);
            }

            if(this.Name == "")
            {
                this.Name = rootDir.Name;
            }
            
            //Get the number of CSV files in the selected folder
            files = rootDir.GetFiles("*.csv");
            itemCount = files.Count();
            this.IsDataAligned = true;

            if (files != null)
            {
                index = 0;
                pct_download = 0;
                //Loop through all CSV files one by one
                foreach (FileInfo fi in files)
                {
                    try
                    {
                        if (this.constituents == null)
                        {
                            this.constituents = new List<Equity>();
                        }

                        //If the constituents list already contains an item with the same name as the file, then update the existing Equity class
                        searchResult = constituents.Where(p => Helpers.RemoveNonAlphanumeric(p.Name) == Helpers.RemoveNonAlphanumeric(fi.Name.Replace(".csv",""))).ToList();

                        if (searchResult != null && searchResult.Count > 0)
                        {
                            searchResult[0].DataFileName = fi.FullName;
                            searchResult[0].ReadDataFile();
                            cEQ = searchResult[0];
                        }
                        //Otherwise create the new Equity instance by parsing the file
                        else
                        {
                            cEQ = new Equity();
                            cEQ.DataFileName = fi.FullName;
                            cEQ.ListedMarket = this.Name;

                            if (cEQ.ReadDataFile() == true)
                            {
                                constituents.Add(cEQ);
                            }
                        }

                        //Set IsDataAligned to True if all Equities in this market instance have data points for all of the dates in the data set
                        if (this.IsDataAligned && this.Constituents.Count > 0)
                        {
                            this.IsDataAligned = cEQ.HistoricalPriceDate.SequenceEqual(this.Constituents[0].HistoricalPriceDate);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    index++;
                    pct_download = (Double)index / (Double)files.Count();
                }

                CalculateCorrelationCoefficients();
                UpdateTimeline();
            }
            
            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  parseHtmlLiveData
         *  Description:    
         *  Parameters: 
         *          pHtmlData - 
         *****************************************************************************/
        public Boolean parseHtmlLiveData(MSHTML.IHTMLDocument2 pHtmlData, String pUrl, String pName)
        {
            Boolean success = true;
            MSHTML.IHTMLElement2 docBody;
            MSHTML.IHTMLElementCollection searchCollection;
            MSHTML.IHTMLElementCollection classProperties;
            MSHTML.IHTMLElementCollection hrefCollection;
            List<String> columnHeaders;
            List<String> equityData;
            string eq_url = "";

            try
            {
                //Update configuration data
                this.liveDataUrl = pUrl;
                if (this.Name == "")
                {
                    this.Name = pName;
                }

                docBody = (MSHTML.IHTMLElement2)pHtmlData.body;

                //build the array of column headers
                searchCollection = (MSHTML.IHTMLElementCollection)docBody.getElementsByTagName("th");
                columnHeaders = Helpers.ParseIHTMLElementCollection(searchCollection);

                //build each equity class
                docBody = (MSHTML.IHTMLElement2)docBody.getElementsByTagName("tbody").item(0);
                searchCollection = (MSHTML.IHTMLElementCollection)docBody.getElementsByTagName("tr");

                if (this.constituents == null)
                {
                    this.constituents = new List<Equity>();
                }

                foreach (MSHTML.IHTMLElement2 item in searchCollection)
                {
                    classProperties = item.getElementsByTagName("td");
                    hrefCollection = item.getElementsByTagName("a");

                    //Get the full URL address for the particular equity
                    foreach (MSHTML.IHTMLElement href in hrefCollection)
                    {
                        eq_url = (String)href.getAttribute("href");
                        if(eq_url.Contains("/equities/"))
                        {
                            eq_url = eq_url.Replace("about:","");
                            break;
                        }
                        else
                        {
                            eq_url = "";
                        }
                    }
                    eq_url = (new Uri(pUrl)).DnsSafeHost + eq_url;

                    equityData = Helpers.ParseIHTMLElementCollection(classProperties);
                    UpdateConstituentData(columnHeaders, equityData, eq_url);
                }

                success = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                success = false;
            }

            return success;
        }

        private void ValidateConstituentSymbols()
        {
            List<string> symbols;
            string symbol_temp = "";

            try
            {
                symbols = this.Constituents.Select(a => a.Symbol).ToList();
                var duplicates = symbols.GroupBy(x => x).Where(g => g.Count() > 1)
                                          .Select(y => new { Element = y.Key, Count = y.Count() })
                                          .ToList();

                foreach(var dp in duplicates)
                {
                    symbol_temp = dp.Element;

                    for (int i = 0; i < dp.Count; i++)
                    {
                        if(i > 0)
                        {
                            this.Constituents.Where(a => a.Symbol == dp.Element).FirstOrDefault().UpdateSymbol(string.Concat(symbol_temp, i.ToString()));
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateConstituentData
         *  Description:    
         *  Parameters: 
         *          pDataLabels - 
         *          pDataValues - 
         *****************************************************************************/
        private void UpdateConstituentData(List<String> pDataLabels, List<String> pDataValues, String pEqUrl = "")
        {
            int item_index = 0;
            string item_value = "";
            string cName = "";
            
            List<Equity> searchResult;
            List<String> data_labels = new List<String>(pDataLabels);
            List<String> data_values = new List<String>(pDataValues);
            
            Equity cEq;

            foreach (String label in data_labels)
            {
                item_value = data_values[item_index];

                //ToDo: optimize
                if(label.Contains("Name"))
                {
                    //Find item_value in the 'constituents' List
                    searchResult = constituents.Where(p => Helpers.RemoveNonAlphanumeric(p.Name) == Helpers.RemoveNonAlphanumeric(item_value)).ToList();
                    cName = data_values[item_index];
                    data_labels.RemoveAt(item_index);
                    data_values.RemoveAt(item_index);

                    if(searchResult != null && searchResult.Count > 0)
                    {
                        searchResult[0].UpdateLiveData(data_labels, data_values);
                    }
                    else
                    {
                        cEq = new Equity(cName);
                        cEq.UpdateLiveData(data_labels, data_values, pEqUrl);
                        cEq.ListedMarket = this.Name;
                        this.constituents.Add(cEq);
                    }
                    break;
                }
                item_index++;
            }
        }

        /*****************************************************************************
         *  FUNCTION:    UpdateTimeline
         *  
         *  Description:    Based on all of the Equities loaded in the Constituents list,
         *                  get the first and last dates for which data exists for at least 
         *                  1 Equity, as well as the maximum number of data points in any
         *                  single Equity
         *                  
         *  Parameters:     None
         *****************************************************************************/
        private void UpdateTimeline()
        {
            this.DateStart = this.Constituents.Select(x => x.HistoricalPriceDate.Min()).Min();
            this.DateEnd = this.Constituents.Select(x => x.HistoricalPriceDate.Max()).Max();
            this.MaxDataPoints = this.Constituents.Select(x => x.HistoricalPrice.Count()).Max();
        }

        /*****************************************************************************
         *  FUNCTION:  ExportToXML
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public Boolean ExportToXML()
        {
            Boolean success = false;
            XmlDocument doc = new XmlDocument();
            String filepath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")) + "Config\\" + this.Name + ".xml";

            //(1) the xml declaration is recommended, but not mandatory
            XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            XmlElement root = doc.DocumentElement;
            doc.InsertBefore(xmlDeclaration, root);

            //(2) string.Empty makes cleaner code
            XmlElement elementMain = doc.CreateElement(string.Empty, "ExchangeMarket", string.Empty);
            doc.AppendChild(elementMain);

            XmlElement elementSummary = doc.CreateElement(string.Empty, "Summary", string.Empty);
            elementMain.AppendChild(elementSummary);

            XmlElement elementConstituents = doc.CreateElement(string.Empty, "Consituents", string.Empty);
            elementMain.AppendChild(elementConstituents);

            XmlAttribute attr1 = doc.CreateAttribute("count");
            attr1.Value = constituents.Count().ToString();
            elementConstituents.Attributes.Append(attr1);

            XmlElement usrElement = doc.CreateElement(string.Empty, "LiveDataUrl", string.Empty);
            attr1 = doc.CreateAttribute("href");
            attr1.InnerText = liveDataUrl;
            usrElement.Attributes.Append(attr1);

            XmlNode nodeIn;
            foreach(Equity member in constituents)
            {
                //usrElement = doc.CreateElement(string.Empty, "Equity", string.Empty);
                nodeIn = doc.ImportNode(member.ToXmlElement(),true);
                elementConstituents.AppendChild(nodeIn);
            }
            
            doc.Save(filepath);

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  CalculateCorrelationCoefficients
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CalculateCorrelationCoefficients()
        {
            CorrelationCoefficients = Helpers.PearsonProductCoefficient(this.constituents);
        }

        /*****************************************************************************
         *  FUNCTION:  GetPPCCoefficients
         *  Description:    
         *  Parameters: 
         *          pItemIndex - 
         *****************************************************************************/
        public Dictionary<int, Double> GetPPCCoefficients(int pItemIndex)
        {
            Dictionary<int, Double> return_list = new Dictionary<int, Double>();
            int i, hash_key;

            for (i = 0; i < constituents.Count(); i++)
            {
                if (i != pItemIndex && CorrelationCoefficients != null)
                {
                    hash_key = Helpers.getHash(i+1, pItemIndex+1, this.constituents.Count());
                    return_list.Add(i, CorrelationCoefficients[hash_key]);
                }
            }

            return return_list;
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateCorrelationCoefficients
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void UpdateCorrelationCoefficients()
        {
            this.CalculateCorrelationCoefficients();
        }

        /*****************************************************************************
         *  FUNCTION:       Clear
         *  Description:    Clears all Constituent data to provide a clean starting point
         *  Parameters:     None
         *****************************************************************************/
        public void Clear()
        {
            foreach(Equity eq in this.Constituents)
            {
                eq.Clear();
            }
            this.Constituents.Clear();
            this.pct_download = 0;
            GC.Collect();
        }
    }
}
