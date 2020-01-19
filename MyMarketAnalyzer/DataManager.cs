using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.IO.Compression;
using System.ComponentModel;
using System.Xml;
using System.Net;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace MyMarketAnalyzer
{
    public class DataManager
    {
        //type-defs
        private struct t_ActiveMarket
        {
            public string id, name;
            public string smlId, sid;

            public t_ActiveMarket(string pSmlId, string pSid, string pId, string pName)
            {
                smlId = pSmlId;
                sid = pSid;
                id = pId;
                name = pName;
            }
        }

        //Constants
        private const string DEFAULT_USER_AGENT = "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:41.0) Gecko/20100101 Firefox/41.0";
        private const string DEFAULT_SMLID = "608";
        private const string DEFAULT_SID = "";
        private const UInt32 WM_LIVEUPDATE = 0xA005;
        private const UInt32 WM_UPDATING_DATA = 0xA006;
        private const UInt32 WM_LIVESESSIONCLOSED = 0xA009;

        //Public Members
        public ExchangeMarket HistoricalData { get; private set; }
        public String HistoricalDataSource { get; private set; }
        public Profile UserProfile { get; private set; }
        public Boolean IsLiveSessionOpen { get; private set; }

        //Private Members
        private ApplicationClass oExcel;
        private _Workbook oBook;
        private static string xlsmpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MyMarketAnalyzer.Properties.Resources.ExcelDataDownloaderPath));

        //lock used to prevent race-condtion between the 'update' and 'change active market' processes
        private bool LockedForUpdate = false;

        private BackgroundWorker LiveSessionWorker = new System.ComponentModel.BackgroundWorker();
        private System.Timers.Timer LiveSessionTimer = new System.Timers.Timer();

        //Timer interval
        private double TimerIntervalMinutes = 0.0;

        //Data for building Web Requests
        private t_ActiveMarket ActiveMarket = new t_ActiveMarket(DEFAULT_SMLID, DEFAULT_SID, "25282", "S&P/TSX 60");
        private String TemplateRequestStr = "https://ca.investing.com/equities/StocksFilter";

        //Lists for selecting the source of live data based on market
        private List<MarketRegion> SelectableMarketRegions = new List<MarketRegion>();
        private List<List<t_ActiveMarket>> ListOfMarkets = new List<List<t_ActiveMarket>>();

        //Handle to the object which spawned this instance. Used to send messages back to the main application form.
        private IntPtr ParentFormPtr;

        public DataManager(IntPtr pParent)
        {
            oExcel = null;
            oBook = null;
            HistoricalData = null;
            HistoricalDataSource = "";
            IsLiveSessionOpen = false;

            //this.LiveSessionWorker.WorkerSupportsCancellation = true;
            //this.LiveSessionWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LiveSessionWorker_DoWork);

            LiveSessionTimer.Elapsed += new System.Timers.ElapsedEventHandler(LiveSessionUpdate);
            ParentFormPtr = pParent;

            UserProfile = new Profile();

            //Set the SSL/TLS security protocol to support TLS version 1.0, 1.1, and 1.2 (needed for HTTPS requests we use)
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
        }

        ~DataManager()
        {
            if(oExcel != null)
            {
                try
                {
                    //oBook.Close(false);
                    //System.Runtime.InteropServices.Marshal.ReleaseComObject(oBook);
                    oExcel.Quit();
                }
                catch (Exception) { }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oExcel);
                oBook = null;
                oExcel = null;
            }

            //Garbage collection
            GC.Collect();
        }

        /*****************************************************************************
         *  FUNCTION:       LoadMarketRegionConfig
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public void LoadMarketRegionConfig(MarketRegion[] mktRegions)
        {
            //updateDataManagerConfig();

            foreach(MarketRegion mkt in mktRegions)
            {
                SelectableMarketRegions.Add(mkt);
                ListOfMarkets.Add(new List<t_ActiveMarket>());
            }

            if (SelectableMarketRegions.Count > 0)
            {
                loadDataManagerConfig();
            }
        }

        /*****************************************************************************
         *  FUNCTION:       RunHistoricalDataDownloader
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public void RunHistoricalDataDownloader()
        {
            ManageExcelInstance();

            // Object for missing (or optional) arguments.
            object oMissing = System.Reflection.Missing.Value;

            // Make it visible
            oExcel.Visible = true;

            // Define Workbooks
            Workbooks oBooks = oExcel.Workbooks;
            
            if(oBooks.Count <= 0)
            {
                //Open the file, using the 'path' variable
                oBook = oBooks.Open(xlsmpath);
            }
            
            // Run the macro, "GetHistoricalData"
            oExcel.GetType().InvokeMember("Run", System.Reflection.BindingFlags.Default | System.Reflection.BindingFlags.InvokeMethod, null, oExcel, new Object[] { "GetHistoricalData" });
        }

        /*****************************************************************************
         *  FUNCTION:       ManageExcelInstance
         *  Description:    Ensure only 1 instance of Excel
         *  Parameters:     None
         *****************************************************************************/
        private void ManageExcelInstance()
        {
            if (oExcel == null)
            {
                oExcel = new ApplicationClass();
            }
        }

        /*****************************************************************************
         *  FUNCTION:       ManageDataConnections
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void ManageDataConnections()
        {
            String pUrl = TemplateRequestStr;

            MSHTML.HTMLDocument htmlresponse = new MSHTML.HTMLDocument();
            MSHTML.IHTMLDocument2 webresponse = (MSHTML.IHTMLDocument2)htmlresponse;

            if (LockedForUpdate == false)
            {
                LockedForUpdate = true;

                //Disable timer ticks
                LiveSessionTimer.Stop();
                SendMessage(this.ParentFormPtr, WM_UPDATING_DATA, IntPtr.Zero, IntPtr.Zero);

                /**** Download Data ****/
                pUrl += "?noconstruct=1" + "&smlID=" + ActiveMarket.smlId +
                        "&sid=" + ActiveMarket.sid + "&tabletype=price" + "&index_id=" + ActiveMarket.id;

                webresponse = Helpers.HTMLRequestResponse(pUrl);

                //Parse the downloaded HTML file
                if (HistoricalData == null)
                {
                    HistoricalData = new ExchangeMarket();
                }

                HistoricalData.parseHtmlLiveData(webresponse, pUrl, ActiveMarket.name);

                SendMessage(this.ParentFormPtr, WM_LIVEUPDATE, IntPtr.Zero, IntPtr.Zero);

                LockedForUpdate = false;
                LiveSessionTimer.Start();
            }
        }

        /*****************************************************************************
         *  FUNCTION:       ReadHistoricalData
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public Boolean ReadHistoricalData()
        {
            Boolean success = true;

            if(HistoricalDataSource == "")
            {
                success = false;
            }
            else
            {
                if (HistoricalData == null)
                {
                    HistoricalData = new ExchangeMarket(HistoricalDataSource);
                }
                success = HistoricalData.parseCsvHistoricalData();
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:       ReadHistoricalData
         *  Description:    
         *  Parameters:     
         *          pPath - 
         *****************************************************************************/
        public Boolean ReadHistoricalData(String pPath)
        {
            Boolean success = true;

            success = Helpers.ValidatePath(pPath);
            if(success)
            {
                HistoricalDataSource = pPath;
                success = ReadHistoricalData();
            }
            
            return success;
        }

        public void UnloadHistoricalData()
        {
            if(HistoricalData != null)
            {
                HistoricalData.Clear();
            }
        }

        /*****************************************************************************
         *  FUNCTION:       SetHistoricalDataPath
         *  Description:    
         *  Parameters:     
         *          pPath - 
         *****************************************************************************/
        public Boolean SetHistoricalDataPath(String pPath)
        {
            Boolean success = true;

            HistoricalDataSource = pPath;
            success = Helpers.ValidatePath(HistoricalDataSource);

            if(success)
            {
                if (HistoricalData == null)
                {
                    HistoricalData = new ExchangeMarket(HistoricalDataSource);
                }
                else
                {
                    HistoricalData.SetCurrentRepoPath(HistoricalDataSource);
                }
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:       OpenLiveSession
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public void OpenLiveSession(double pRefreshPeriodMin = 1)
        {
            if (ActiveMarket.id != "")
            {
                TimerIntervalMinutes = pRefreshPeriodMin;
                this.IsLiveSessionOpen = true;

                //Run the download once, before starting the timer
                ManageDataConnections();

                LiveSessionTimer.Stop();
                LiveSessionTimer.Interval = (int)(pRefreshPeriodMin * 60000);
                LiveSessionTimer.Start();
            }
        }

        /*****************************************************************************
         *  FUNCTION:       CloseLiveSession
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public void CloseLiveSession()
        {
            LiveSessionTimer.Stop();
            IsLiveSessionOpen = false;
            SendMessage(this.ParentFormPtr, WM_LIVESESSIONCLOSED, IntPtr.Zero, IntPtr.Zero);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  LiveSessionUpdate
         *  Description:    Timed Event for the LiveSessionTimer
         *  Parameters:     
         *          source - 
         *          e      -
         *****************************************************************************/
        private void LiveSessionUpdate(object source, System.Timers.ElapsedEventArgs e)
        {
            ManageDataConnections();
        }

        /*****************************************************************************
         *  FUNCTION:       getMarketsInRegion
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public string[] getMarketsInRegion(MarketRegion pRegion)
        {
            List<string> retun_list = new List<string>();

            return (retun_list.ToArray<string>());
        }

        /*****************************************************************************
         *  FUNCTION:       GetListOfAvailableMarkets
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public List<String> GetListOfAvailableMarkets(MarketRegion pSelectedRegion)
        {
            List<String> return_list = new List<string>();
            int mkt_index = 0;

            if(SelectableMarketRegions.Contains(pSelectedRegion))
            {
                mkt_index = SelectableMarketRegions.IndexOf(pSelectedRegion);
                return_list = new List<string>(ListOfMarkets[mkt_index].Select(i => i.name).ToList());
            }

            return return_list;
        }

        /*****************************************************************************
         *  FUNCTION:       ChangeActiveMarket
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public bool ChangeActiveMarket(int pRegionIndex, int pMarketIndex)
        {
            bool isRunning = false;
            bool success = true;

            //obtain lock
            if(LockedForUpdate == false)
            {
                LockedForUpdate = true;

                //Temporarily stop timed update
                if (LiveSessionTimer.Enabled)
                {
                    LiveSessionTimer.Stop();
                    isRunning = true;
                }

                //Change the active market index
                if (pRegionIndex >= 0 && pRegionIndex < SelectableMarketRegions.Count)
                {
                    if (pMarketIndex >= 0 && pMarketIndex < ListOfMarkets[pRegionIndex].Count)
                    {
                        ActiveMarket = ListOfMarkets[pRegionIndex][pMarketIndex];
                        this.HistoricalData = new ExchangeMarket();
                    }
                }

                //release lock
                LockedForUpdate = false;

                //Re-start timed update if it was previously enabled
                if (isRunning)
                {
                    TimerIntervalMinutes = (TimerIntervalMinutes > 0) ? TimerIntervalMinutes : 1;
                    this.OpenLiveSession(TimerIntervalMinutes);
                } 
            }
            else
            {
                //Notify caller that lock failed
                success = false;
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:       UpdateUserProfile
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        public void UpdateUserProfile(String filename)
        {
            this.UserProfile = this.UserProfile.Load(filename);
        }

        #region Configuration Data Functions
        /*****************************************************************************
         *  FUNCTION:       loadDataManagerConfig
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void loadDataManagerConfig()
        {
            XmlDocument configFile = new XmlDocument();
            string application_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string config_path = Path.Combine(application_path, MyMarketAnalyzer.Properties.Resources.DataManagerConfigPath);
            config_path = new Uri(config_path).LocalPath;

            string market_name = "", market_query = "", market_smlid = "";
            string option_id, option_name;
            XmlNode marketNode;
            XmlNodeList nodeList;

            List<t_ActiveMarket> tempMktList;

            configFile.Load(config_path);

            foreach(MarketRegion mkt in SelectableMarketRegions)
            {
                market_name = StringEnum.GetStringValue(mkt);
                market_query = string.Format("Datamanager/Live/Markets/Data/Market[@name='{0}']", market_name);
                nodeList = configFile.SelectNodes(market_query);

                if(nodeList.Count > 0)
                {
                    marketNode = nodeList[0];
                    tempMktList = new List<t_ActiveMarket>();
                    market_smlid = marketNode.Attributes.GetNamedItem("smlID").InnerText;

                    nodeList = marketNode.SelectNodes("option");
                    foreach (XmlNode i_mkt in nodeList)
                    {
                        option_id = i_mkt.Attributes.GetNamedItem("id").InnerText;
                        option_name = i_mkt.InnerText;
                        tempMktList.Add(new t_ActiveMarket(market_smlid, DEFAULT_SID, option_id, option_name));
                    }

                    this.ListOfMarkets[SelectableMarketRegions.IndexOf(mkt)] = tempMktList;
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       updateDataManagerConfig
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private bool updateDataManagerConfig()
        {
            bool success = true;
            int index1, index2;

            //Web Request / Response locals
            String baseUrl, subUrl, responseText, parseText;
            WebRequest request;
            WebResponse response;
            MSHTML.HTMLDocument htmlresponse = new MSHTML.HTMLDocument();
            MSHTML.IHTMLDocument2 webresponse = (MSHTML.IHTMLDocument2)htmlresponse;

            //Xml objects
            XmlDocument configFile = new XmlDocument();
            XmlNodeList nodeList, keyNodeList, subNodeList, configMarketsNodeList, configOptionNodeList;
            XmlDocument KeyElement = new XmlDocument();
            XmlNode marketNode;
            StreamReader reader;

            List<String> listOfMarkets;
            List<String> listOfOptions;

            string application_path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            string config_path = Path.Combine(application_path, MyMarketAnalyzer.Properties.Resources.DataManagerConfigPath);
            config_path = new Uri(config_path).LocalPath;

            configFile.Load(config_path);

            nodeList = configFile.SelectNodes("Datamanager/Live/Markets/Schema");

            index1 = -1;
            index2 = -1;
            parseText = "";
            try
            {
                baseUrl = nodeList[0].Attributes.GetNamedItem("url").InnerText;
                request = WebRequest.Create(baseUrl);
                request.Credentials = CredentialCache.DefaultCredentials;
                ((HttpWebRequest)request).Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                ((HttpWebRequest)request).UserAgent = DEFAULT_USER_AGENT;
                ((HttpWebRequest)request).Timeout = 20000;

                response = request.GetResponse();

                reader = new StreamReader(response.GetResponseStream());
                responseText = reader.ReadToEnd();
                reader.Close();
                response.Close();

                //write response to string
                webresponse.write(responseText);

                //begin search for the menu items we want
                keyNodeList = GetConfiguredItems(responseText, configFile, "Datamanager/Live/Markets/Schema/");

                configMarketsNodeList = configFile.SelectNodes("Datamanager/Live/Markets/Data/Market");
                listOfMarkets = new List<string>();

                for(int i = 0; i < configMarketsNodeList.Count; i++)
                {
                    listOfMarkets.Add(configMarketsNodeList[i].Attributes.GetNamedItem("name").InnerText);
                }

                //For each geographic location, get the list of available markets / indexes
                foreach(XmlNode subNode in keyNodeList)
                {
                    if (subNode.InnerXml != "")
                    {
                        index1 = subNode.OuterXml.IndexOf("href=\"", 0) + 6;
                        index2 = subNode.OuterXml.IndexOf("\"", index1);
                        parseText = subNode.OuterXml.Substring(index1, index2 - index1);
                        subUrl = (new Uri(new Uri(baseUrl), parseText)).ToString();

                        if(parseText.Substring(0,1) != "/")
                        {
                            parseText = "/" + parseText;
                        }

                        if(listOfMarkets.Contains(subNode.InnerXml))
                        {
                            marketNode = configMarketsNodeList[listOfMarkets.IndexOf(subNode.InnerXml)];
                        }
                        else
                        {
                            marketNode = configFile.CreateNode("element", "Market", "");

                            XmlAttribute atHref = configFile.CreateAttribute("href");
                            atHref.Value = parseText;

                            XmlAttribute atName = configFile.CreateAttribute("name");
                            atName.Value = subNode.InnerText;

                            marketNode.Attributes.Append(atHref);
                            marketNode.Attributes.Append(atName);

                            configFile.SelectNodes("Datamanager/Live/Markets/Data")[0].InsertAfter(marketNode, configMarketsNodeList[configMarketsNodeList.Count - 1]);
                        }

                        responseText = "";
                        request = WebRequest.Create(subUrl);
                        request.Credentials = CredentialCache.DefaultCredentials;
                        ((HttpWebRequest)request).Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
                        ((HttpWebRequest)request).UserAgent = DEFAULT_USER_AGENT;
                        ((HttpWebRequest)request).Timeout = 20000;

                        response = request.GetResponse();

                        reader = new StreamReader(response.GetResponseStream());
                        responseText = reader.ReadToEnd();
                        reader.Close();
                        response.Close();

                        //write response to string
                        webresponse.write(responseText);

                        subNodeList = GetConfiguredItems(responseText, configFile, "Datamanager/Live/Markets/Data/Schema/");
                        listOfOptions = new List<string>();
                        configOptionNodeList = null;

                        if (marketNode.HasChildNodes)
                        {
                            configOptionNodeList = marketNode.SelectNodes("option");

                            for (int i = 0; i < configOptionNodeList.Count; i++)
                            {
                                listOfOptions.Add(configOptionNodeList[i].Attributes.GetNamedItem("id").InnerText);
                            }
 
                        }

                        foreach (XmlNode optionNode in subNodeList)
                        {
                            //Only add options that aren't already in the list (based on id)
                            if (!listOfOptions.Contains(optionNode.Attributes.GetNamedItem("id").InnerText))
                            {
                                XmlNode newOption = configFile.ImportNode(optionNode, true);
                                if (configOptionNodeList == null)
                                {
                                    marketNode.AppendChild(newOption);
                                }
                                else
                                {
                                    marketNode.InsertAfter(newOption, configOptionNodeList[configOptionNodeList.Count - 1]);
                                }
                            }
                        }
                    }
                }

                configFile.Save(config_path);
            }
            catch(Exception)
            {
                success = false;
            }

            return success;
        }

        private XmlNodeList GetConfiguredItems(String pSource, XmlDocument configFile, String pConfigBaseAddress)
        {
            String searchString, parseText, subName;
            int index1, index2, count1, count2, ignore, i;

            XmlDocument KeyElement = new XmlDocument();
            XmlNodeList keyNodeList;

            if (pConfigBaseAddress.Substring(pConfigBaseAddress.Length - 1, 1) != "/")
            {
                pConfigBaseAddress += "/";
            }

            //begin search for the menu items we want
            searchString = configFile.SelectNodes(pConfigBaseAddress + "SearchStart")[0].InnerXml;
            index1 = pSource.IndexOf(searchString);

            //Get the inner html of the Key Element
            searchString = configFile.SelectNodes(pConfigBaseAddress + "KeyElement")[0].Attributes.GetNamedItem("name").InnerText;
            index1 = pSource.IndexOf("<" + searchString, index1);
            index2 = pSource.IndexOf("</" + searchString, index1);
            parseText = pSource.Substring(index1, index2 - index1 + 2 + searchString.Length);

            //Ensure that we have the complete inner html of the element, and haven't just terminated at the end tag of a nested twin
            count1 = ((parseText.Length) - (parseText.Replace("<" + searchString, "").Length)) / (searchString.Length + 1);
            count2 = ((parseText.Length) - (parseText.Replace("</" + searchString, "").Length)) / (searchString.Length + 2);

            while (count1 != count2)
            {
                index2 = pSource.IndexOf("</" + searchString, index2);
                parseText = pSource.Substring(index1, index2 - index1 + searchString.Length);

                count1 = ((parseText.Length) - (parseText.Replace("<" + searchString, "").Length)) / searchString.Length;
                count2 = ((parseText.Length) - (parseText.Replace("</" + searchString, "").Length)) / searchString.Length;
            }
            parseText += ">";

            parseText = parseText.Replace("&amp;", "&");
            parseText = parseText.Replace("&", "&amp;");

            //Load it into an XML document structure to make searching easier
            KeyElement.LoadXml(parseText);

            //Iterate for each Sub Element
            subName = configFile.SelectNodes(pConfigBaseAddress + "SubElement")[0].Attributes.GetNamedItem("name").InnerText;
            keyNodeList = KeyElement.GetElementsByTagName(subName);

            //remove number of elements from beginning
            try
            {
                ignore = int.Parse(configFile.SelectNodes(pConfigBaseAddress + "SubElement")[0].Attributes.GetNamedItem("ignore").InnerText);
                for(i = 0; i < ignore; i++)
                {
                    keyNodeList[i].RemoveAll();
                }
            }
            catch(Exception) { }

            return keyNodeList;
        }

        #endregion

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
                          IntPtr hWnd,      // handle to destination window
                          UInt32 Msg,       // message
                          IntPtr wParam,  // first message parameter
                          IntPtr lParam   // second message parameter
                          );

    }

}
