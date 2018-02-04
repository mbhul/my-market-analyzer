using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using MyMarketAnalyzer.Utilities;

namespace MyMarketAnalyzer
{
    public enum MarketRegion
    {
        [StringValue("Canada")]
        CA,
        [StringValue("United States")]
        US,
        [StringValue("Europe")]
        EU,
        [StringValue("Asia/Pacific")]
        AP,
        [StringValue("Americas")]
        AM,
        [StringValue("Middle East")]
        ME
    }

    public partial class MainForm : Form
    {   
        /******* These Lists must be the same size ********/
        private MarketRegion[] RegionDropDownList = { 
                                                        MarketRegion.CA, MarketRegion.US, MarketRegion.EU, MarketRegion.AP
                                                    };

        private Image[] RegionImageList = {
                                              MyMarketAnalyzer.Properties.Resources.fl_CA, MyMarketAnalyzer.Properties.Resources.fl_US,
                                              MyMarketAnalyzer.Properties.Resources.fl_EU, MyMarketAnalyzer.Properties.Resources.fl_AP
                                          };
        /**************************************************/

        private String[,] HistoricalFilterKey = { {TableHeadings.PctChange[0], TableHeadings.PctChange[1]},
                                                  {TableHeadings.Hist_Avg[0], TableHeadings.Hist_Avg[1]}, 
                                                  {TableHeadings.Hist_Vlty, TableHeadings.Hist_Vlty} };
        
        private String[,] LiveFilterKey = { { TableHeadings.PctChange[0], TableHeadings.PctChange[1] }, 
                                          { TableHeadings.Live_Last, TableHeadings.Live_Last }, 
                                          { TableHeadings.Live_Vol, TableHeadings.Live_Vol } };
        private int[] LiveSessionIntervalsSec = { 15, 30, 60, 300 };

        //***** CONSTANTS *****//
        private const UInt32 WM_NOTIFY = 0x004E;
        private const UInt32 WM_CELLCLICK = 0xA000;
        private const UInt32 WM_MULTIROWCLICK = 0xA001;
        private const UInt32 WM_SHOWBTNCLICK = 0xA002;
        private const UInt32 WM_SHOWNEWWINDCLICK = 0xA003;
        private const UInt32 WM_PROCESS_TI = 0xA004;
        private const UInt32 WM_LIVEUPDATE = 0xA005;
        private const UInt32 WM_UPDATING_DATA = 0xA006;
        private const UInt32 WM_ADDWATCHLIST = 0xA007;
        private const UInt32 WM_MULTI_ADDWATCHLIST = 0xA008;
        private const UInt32 WM_LIVESESSIONCLOSED = 0xA009;
        private const UInt32 WM_ANALYSISCOMPLETE = 0xA00A;
        private const UInt32 WM_ANALYSISFUNCSELECT = 0xA00B;
        private const UInt32 WM_ANALYSISVARSELECT = 0xA00C;

        private const String DEFAULT_UNITS_SPECIFIER = "[U%100]";

        //***** PRIVATE GLOBALS *****//
        private TabPage tabVisuals;
        private VisualsTab tabVisualsControl;

        private DataManager MyDataManager;
        private RuleParser TestRuleParser;
        private AlgorithmDesignForm AlgDesignForm = null;

        private int AnalysisDisplayIndex = 0;
        private Rectangle InitialSize = new Rectangle();
        private bool DataMenuPanelVisible = false;

        private bool AnalysisBuyRuleActive = false;
        private bool AnalysisSellRuleActive = false;

        private HeatMap heatMapControl = new HeatMap();
        private Form heatMapForm = new Form();

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
                          IntPtr hWnd,      // handle to destination window
                          UInt32 Msg,       // message
                          IntPtr wParam,  // first message parameter
                          IntPtr lParam   // second message parameter
                          );

        /*****************************************************************************
         *  CONSTRUCTOR: MainForm
         *  Description: Creates the main Windows Form for the application
         *****************************************************************************/
        public MainForm()
        {
            InitializeComponent();

            tblStatTableMain.ResetRows(1);
            tabControl1.SelectedIndex = 1;

            MyDataManager = new DataManager(this.Handle);
            TestRuleParser = new RuleParser();

            tabVisuals = null;
            tabVisualsControl = null;
            InitialSize = this.analysisSplitContainer.Panel1.ClientRectangle;
            tabControl1.TabPages.Remove(this.tabPage1);
            AlgDesignForm = new AlgorithmDesignForm(ref MyDataManager);

            InitializeMainForm();
            InitializeAnalysisForm();
            CollapseMenuPanel();

            //debug
            //Helpers.TestPOSTRequest();
        }

        /*****************************************************************************
         *  FUNCTION:       InitializeMainForm
         *  Description:    Initializes / configures general controls and parameters on
         *                  the MainForm window
         *  Parameters:     None
         *****************************************************************************/
        private void InitializeMainForm()
        {
            int i;

            MyDataManager.LoadMarketRegionConfig(RegionDropDownList);

            if (RegionDropDownList.Length > 0)
            {
                for (i = 0; i < RegionDropDownList.Length; i++)
                {
                    this.cbRegionSelect.Items.Add(StringEnum.GetStringValue(RegionDropDownList[i]));
                }
                this.cbRegionSelect.SelectedIndex = 0;
            }
            SetLiveDataIntervalOptions();
        }

        /*****************************************************************************
         *  FUNCTION:       InitializeAnalysisForm
         *  Description:    Initializes / configures controls on the Analysis tab page
         *  Parameters:     None
         *****************************************************************************/
        private void InitializeAnalysisForm()
        {
            int i;

            for(i = 0; i < Analysis.ChartTypes.Length; i++)
            {
                cbAnalysisType.Items.Add(Analysis.ChartTypes[i]);
            }

            for (i = 0; i < Analysis.ChartFeatures.Length; i++)
            {
                cbAnalysisIndicatorX.Items.Add(Analysis.ChartFeatures[i]);
                cbAnalysisIndicatorY.Items.Add(Analysis.ChartFeatures[i]);
            }

            //Additional Control initialization
            this.btnBuyRuleExpandCollapse.FlatStyle = FlatStyle.Flat;
            this.btnBuyRuleExpandCollapse.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);

            this.btnSellRuleExpandCollapse.FlatStyle = FlatStyle.Flat;
            this.btnSellRuleExpandCollapse.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
        }

        /*****************************************************************************
         *  FUNCTION:       CollapseMenuPanel
         *  Description:    Collapses the side toolbar menu
         *  Parameters:     None
         *****************************************************************************/
        private void CollapseMenuPanel()
        {
            this.dataMenuPanel.Size = this.dataMenuArrow.Size;
            this.dataMenuPanel.Location = new Point(this.tabStats.Width - this.dataMenuArrow.Width - 4, 0);
            DataMenuPanelVisible = false;
            this.dataMenuArrow.Image = MyMarketAnalyzer.Properties.Resources.arrow_icon_normal;
        }

        /*****************************************************************************
         *  FUNCTION:       ExpandMenuPanel
         *  Description:    Expands the side toolbar menu
         *  Parameters:     None
         *****************************************************************************/
        private void ExpandMenuPanel()
        {
            this.dataMenuPanel.Size = new Size(120, this.tabStats.Height);
            this.dataMenuPanel.Location = new Point(this.tabStats.Width - 120, 0);
            DataMenuPanelVisible = true;
            this.dataMenuArrow.Image = MyMarketAnalyzer.Properties.Resources.arrow_icon_normal_rev;
        }

        /*****************************************************************************
         *  FUNCTION:       SetDisplayHistoricalDataStatus
         *  Description:    Updates the displayed Historical Data load status
         *  Parameters:     
         *          pIsLoaded - True if historical data set has been loaded, False otherwise
         *****************************************************************************/
        private void SetDisplayHistoricalDataStatus(bool pIsLoaded)
        {
            int i;
            Label[] HistStatusLabels = { this.lblHistDataStatus1, this.lblHistDataStatus2 };
            ToolStripTextBox[] HistSourceDir = { this.tsHistSourceDir1, this.tsHistSourceDir2 };

            if (pIsLoaded)
            {
                for (i = 0; i < HistStatusLabels.Length; i++)
                {
                    HistStatusLabels[i].Text = "LOADED";
                    HistStatusLabels[i].ForeColor = Color.Green;
                    HistSourceDir[i].Text = MyDataManager.HistoricalDataSource;
                }
            }
            else
            {
                for (i = 0; i < HistStatusLabels.Length; i++)
                {
                    HistStatusLabels[i].Text = "UNAVAILABLE";
                    HistStatusLabels[i].ForeColor = Color.Red;
                    HistSourceDir[i].Text = "";
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       SetDisplayLiveDataStatus
         *  Description:    Updates the displayed Live Data Session Connection status
         *  Parameters:     
         *          pIsLoaded - True if live data connection is open, False otherwise
         *****************************************************************************/
        private void SetDisplayLiveDataStatus(bool pIsLoaded)
        {
            int i;
            Label[] LiveStatusLabels = { this.lblLiveDataStatus1, this.lblLiveDataStatus2 };

            if (pIsLoaded)
            {
                for (i = 0; i < LiveStatusLabels.Length; i++)
                {
                    LiveStatusLabels[i].Text = "OPEN";
                    LiveStatusLabels[i].ForeColor = Color.Green;
                }
            }
            else
            {
                for (i = 0; i < LiveStatusLabels.Length; i++)
                {
                    LiveStatusLabels[i].Text = "CLOSED";
                    LiveStatusLabels[i].ForeColor = Color.Red;
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       SetLiveDataIntervalOptions
         *  Description:    Populates the drop-down menu for selecting the interval at
         *                  which to request new live data
         *  Parameters:     None
         *****************************************************************************/
        private void SetLiveDataIntervalOptions()
        {
            int i;
            string option_temp;

            this.cbLiveDataInterval.Items.Clear();
            for(i = 0; i < this.LiveSessionIntervalsSec.Count(); i++)
            {
                //Display options greater than 60 in terms of minutes instead of seconds
                if(LiveSessionIntervalsSec[i] >= 60)
                {
                    option_temp = (LiveSessionIntervalsSec[i] / 60).ToString() + " min";
                }
                else
                {
                    option_temp = LiveSessionIntervalsSec[i].ToString() + " sec";
                }

                //Pad option text for cleaner-looking alignment in the drop-down menu
                if ((LiveSessionIntervalsSec[i] >= 10 && LiveSessionIntervalsSec[i] < 60) ||
                    (LiveSessionIntervalsSec[i] >= 600 && LiveSessionIntervalsSec[i] < 6000))
                {
                    option_temp = " " + option_temp;
                }
                else if (LiveSessionIntervalsSec[i] < 10 || LiveSessionIntervalsSec[i] >= 60)
                {
                    option_temp = "   " + option_temp;
                }
                else { }

                this.cbLiveDataInterval.Items.Add(option_temp);
            }
        }

        /*****************************************************************************
         *  FUNCTION:       ClearFilter
         *  Description:    Clears the drop-down filter input
         *  Parameters:     None
         *****************************************************************************/
        private void ClearFilter()
        {
            tblStatTableMain.SetFilterExpression(tabStats.Handle, "");

            if (MyDataManager.HistoricalData.Constituents != null)
            {
                UpdateStatResultCount(MyDataManager.HistoricalData.Constituents.Count());
            }
        }

        /*****************************************************************************
         *  FUNCTION:       SetHistoricalDataFilter
         *  Description:    Populates the filter combo box for historical data
         *  Parameters:     None
         *****************************************************************************/
        private void SetHistoricalDataFilter()
        {
            //Update Filter Drop Down list
            comboBoxStatFilter.Items.Clear();
            comboBoxStatFilter.Items.Add("");

            for(int i = 0; i <= HistoricalFilterKey.GetUpperBound(0); i++)
            {
                comboBoxStatFilter.Items.Add(HistoricalFilterKey[i,1]);
            }
        }

        /*****************************************************************************
         *  FUNCTION:       SetLiveDataFilter
         *  Description:    Populates the filter combo box for live data
         *  Parameters:     None
         *****************************************************************************/
        private void SetLiveDataFilter()
        {
            comboBoxStatFilter.Items.Clear();
            comboBoxStatFilter.Items.Add("");

            for (int i = 0; i <= LiveFilterKey.GetUpperBound(0); i++)
            {
                comboBoxStatFilter.Items.Add(LiveFilterKey[i,1]);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  tsBtnLoadHistorical_Click
         *  Description:    Enables selection of a file containing historical data to be
         *                  parsed and loaded.
         *  Parameters:     
         *          sender - the parent object from which the event was triggered
         *          e      - the event arguments
         *****************************************************************************/
        private void tsBtnLoadHistorical_Click(object sender, EventArgs e)
        {
            String folderName;
            FolderBrowserDialog2 dlgStatFolder;
            
            dlgStatFolder = new FolderBrowserDialog2();
            //dlgStatFolder.Description = "Select folder containing historical data";
            dlgStatFolder.DirectoryPath = Path.GetDirectoryName(Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, MyMarketAnalyzer.Properties.Resources.ExcelDataDownloaderPath)));

            DialogResult result = dlgStatFolder.ShowDialog(null);

            if(result == DialogResult.OK)
            {
                folderName = dlgStatFolder.DirectoryPath;
                MyDataManager.UnloadHistoricalData();
                MyDataManager.SetHistoricalDataPath(folderName);
                this.backgroundWorkerStat.RunWorkerAsync();
                this.backgroundWorkerProgress.RunWorkerAsync();
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  unloadToolStripMenuItem1_Click
         *  Description:    Unloads historical data from the Data Manager and stat
         *                  table binding
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void unloadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MyDataManager.UnloadHistoricalData();
            DisplayStatData();
            SetDisplayHistoricalDataStatus(false);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  backgroundWorkerStat_DoWork
         *  Description:    Worker thread function to load historical data. 
         *                  The main thread is used to update the UI.
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void backgroundWorkerStat_DoWork(object sender, DoWorkEventArgs e)
        {
            Boolean success = MyDataManager.ReadHistoricalData();
        }

        /*****************************************************************************
         *  EVENT HANDLER:  backgroundWorkerProgress_DoWork
         *  Description:    Worker thread which polls the data manager for historical
         *                  data load progress and reports it back to the UI thread.
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void backgroundWorkerProgress_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int[] pct_progress = {0, 0};
            const long timeout = 10000;
            long time_passed = 0;
            int exit_persistence = 0;

            while(MyDataManager.HistoricalData.DownloadPercentage < 1 && time_passed < timeout)
            {
                System.Threading.Thread.Sleep(100);
                time_passed += 100;
                pct_progress[1] = (int)(MyDataManager.HistoricalData.DownloadPercentage * 100);
                if (pct_progress[1] == pct_progress[0])
                {
                    exit_persistence += 1;
                }
                else
                {
                    exit_persistence = 0;
                }

                //if the progress hasn't changed in 5 update periods, exit
                if (exit_persistence >= 5)
                {
                    break;
                }
                pct_progress[0] = pct_progress[1];
                worker.ReportProgress(pct_progress[1]);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  backgroundWorkerProgress_ProgressChanged
         *  Description:    Handles the "ProgressChanged" event invoked when the progress
         *                  reported by the backgroundWorkerProgress_DoWork function has changed.
         *                  This function runs on the main UI thread.
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void backgroundWorkerProgress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressStats.Visible = true;
            progressStats.Value = e.ProgressPercentage;
            if(e.ProgressPercentage == 100)
            {
                tblStatTableMain.TableType = StatTableType.HIST_STATS;
                progressStats.Visible = false;
                DisplayStatData();
                DisplayAnalysisData();
                SetDisplayHistoricalDataStatus(true);
                ClearFilter();
                UpdateAnalysisComboBox();
                ShowVisualizationTab(0, false, false);
            }
        }

        /*****************************************************************************
         *  FUNCTION:       DisplayStatData
         *  Description:    Displays the loaded historical data on the Data Manager tab page.
         *  Parameters:     None
         *****************************************************************************/
        private void DisplayStatData()
        {
            tblStatTableMain.BindMarketData(MyDataManager.HistoricalData, true);
            UpdateStatResultCount(tblStatTableMain.DisplayedCount());

            if (this.tblStatTableMain.TableType == StatTableType.HIST_STATS)
            {
                SetHistoricalDataFilter();
            }
            else if (this.tblStatTableMain.TableType == StatTableType.LIVE_STATS)
            {
                SetLiveDataFilter();
            }
            else { }

            if (this.tabVisuals != null && tabControl1.TabPages.Contains(this.tabVisuals))
            {
                this.tabVisualsControl.ReBindExchangeMarket(MyDataManager.HistoricalData);
            }
        }

        /*****************************************************************************
         *  FUNCTION:       DisplayAnalysisData
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void DisplayAnalysisData()
        {
            
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnExport_Click
         *  Description:    Exports the loaded historical data into an XML file
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (MyDataManager.HistoricalData != null)
            {
                MyDataManager.HistoricalData.ExportToXML();
            }
            else
            {
                const string message = "No Data has been loaded. Export Failed.";
                const string caption = "No Data";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Exclamation);
            }
        }

        /*****************************************************************************
         *  FUNCTION:       ShowVisualizationTab (overloaded)
         *  Description:    Initiates a request to display the Chart tab which
         *                  contains the chart(s) and other performance indicators for
         *                  the selected table item.
         *  Parameters:     
         *          data_index   - 
         *          createNewTab - 
         *****************************************************************************/
        private void ShowVisualizationTab(int data_index, Boolean createNewChart, Boolean selectTab = true)
        {
            List<int> pDataIndex = new List<int>();
            pDataIndex.Add(data_index);
            ShowVisualizationTab(pDataIndex, createNewChart, selectTab);
        }

        /*****************************************************************************
         *  FUNCTION:       ShowVisualizationTab (overloaded)
         *  Description:    Initiates a request to display the Chart tab which
         *                  contains the chart(s) and other performance indicators for
         *                  the selected table items.
         *  Parameters:     
         *          data_index   - the list of indexes of the historical data constituents, 
         *                         within the historical data 'constituents' array, to display
         *                         visual data for.
         *          createNewTab - 'True' to create a new tab, 'False' to add selected data
         *                          to the existing visualization tab
         *****************************************************************************/
        private void ShowVisualizationTab(List<int> data_index, Boolean createNewChart, Boolean selectTab = true)
        {
            List<Equity> pEquities = new List<Equity>();
            int i;

            //Collect the equities to be displayed
            for (i = 0; i < data_index.Count; i++)
            {
                pEquities.Add(MyDataManager.HistoricalData.Constituents[data_index[i]]);
            }

            //Create new display instance if none exists
            if (tabVisuals == null)
            {
                tabVisuals = new TabPage();
                tabVisuals.Text = "Chart";
                tabVisuals.Leave += new EventHandler(this.tabVisuals_OnLoseFocus);

                if (tabVisualsControl == null)
                {
                    tabVisualsControl = new VisualsTab(pEquities, MyDataManager.HistoricalData);
                    tabVisuals.Controls.Add(tabVisualsControl);
                    tabVisualsControl.Dock = DockStyle.Fill;
                }

                tabControl1.TabPages.Add(tabVisuals);

                if (selectTab)
                {
                    tabControl1.SelectedIndex = tabControl1.TabPages.Count - 1;
                }
            }
            else if (!tabControl1.TabPages.Contains(tabVisuals))
            {
                tabVisualsControl.ReloadChart(pEquities);
                tabControl1.TabPages.Add(tabVisuals);

                if (selectTab)
                {
                    tabControl1.SelectedIndex = tabControl1.TabPages.IndexOf(tabVisuals);
                }
            }
            else if (createNewChart)
            {
                tabVisualsControl.ReloadChart(pEquities);
                if (selectTab)
                {
                    tabControl1.SelectedIndex = tabControl1.TabPages.IndexOf(tabVisuals);
                }
            }
            else
            {
                tabVisualsControl.AddToChart(pEquities);
            }
            
        }

        /*****************************************************************************
         *  EVENT HANDLER:  tabVisuals_OnLoseFocus
         *  Description:    
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void tabVisuals_OnLoseFocus(object sender, EventArgs e)
        {
            //if(tabVisuals != null)
            //{
            //    tabControl1.TabPages.Remove(tabVisuals);
            //}
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnExcelDowloader_Click
         *  Description:    Opens an instance of Microsoft Excel with the historical
         *                  data downloader file
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void btnExcelDowloader_Click(object sender, EventArgs e)
        {
            MyDataManager.RunHistoricalDataDownloader();
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnShowChart_Click
         *  Description:    
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void btnShowChart_OnClick(object sender, EventArgs e)
        {
            if(tblStatTableMain.SelectedEntries.Count > 0)
            {
                ShowVisualizationTab(tblStatTableMain.SelectedEntries, false);
            }
        }

        /*****************************************************************************
         *  FUNCTION:       WndProc (Windows Method)
         *  Description:    Intercepts all Windows messages directed at the MainForm window
         *  Parameters:     
         *          m   -   The Windows Forms Message that was sent with the MainForm handle
         *                  as the destination
         *****************************************************************************/
        protected override void WndProc(ref Message m)
        {
            Boolean createNewChart = false;
            if(m.LParam == tblStatTableMain.Handle)
            {
                createNewChart = true;
            }
            switch (m.Msg)
            {
                //*** Message routers for the main stat table ***//
                case (int)WM_CELLCLICK:
                    ShowVisualizationTab((int)m.WParam, createNewChart);
                    break;
                case (int)WM_MULTIROWCLICK:
                    ShowVisualizationTab(((StatTable)StatTable.FromHandle(m.LParam)).SelectedEntries, createNewChart);
                    break;
                case (int)WM_SHOWBTNCLICK:
                    if ((int)m.WParam == 0)
                        tabVisualsControl.ToggleCorrelationTable(false);
                    else
                        tabVisualsControl.ToggleCorrelationTable(true);
                    break;

                //*** Message routers for the Visualization/Chart tab ***//
                case (int)WM_PROCESS_TI:
                    tabVisualsControl.manageTechnicalIndicators();
                    break;
                case (int)WM_SHOWNEWWINDCLICK:
                    tabVisualsControl.displayNewWindow();
                    break;

                //*** Message routers for live data events ***//
                case (int)WM_LIVEUPDATE:
                    lblUpdate.Visible = false;
                    SetDisplayLiveDataStatus(true);
                    tblStatTableMain.TableType = StatTableType.LIVE_STATS;
                    DisplayStatData();
                    UpdateAnalysisComboBox();
                    break;
                case (int)WM_LIVESESSIONCLOSED:
                    SetDisplayLiveDataStatus(false);
                    break;
                case (int)WM_UPDATING_DATA:
                    lblUpdate.Visible = true;
                    break;

                //*** Message routers for watchlist events ***//
                case (int)WM_ADDWATCHLIST:
                    AddToWatchlist((int)m.WParam);
                    break;
                case (int)WM_MULTI_ADDWATCHLIST:
                    AddToWatchlist(((StatTable)StatTable.FromHandle(m.LParam)).SelectedEntries);
                    break;

                //*** Message routers for the Analysis tab ***//
                case (int)WM_ANALYSISCOMPLETE:
                    this.analysisSummaryPage1.DisplayResult();
                    break;
                case (int)WM_ANALYSISFUNCSELECT:
                    this.updateActiveAnalysisRule(typeof(Fn), m.WParam);
                    break;
                case (int)WM_ANALYSISVARSELECT:
                    this.updateActiveAnalysisRule(typeof(Variable), m.WParam);
                    break;
                
                //*** DEFAULT CASE ***//
                default:
                    break;
            }
            base.WndProc(ref m);
        }

        /*****************************************************************************
         *  FUNCTION:       AddToWatchlist
         *  Description:    Adds an item from the loaded data manager to the watchlist
         *                  based on index value.
         *  Parameters:     
         *      pIndex -    the index of the loaded data manager item (Equity) to add
         *                  to the watchlist.
         *****************************************************************************/
        private void AddToWatchlist(int pIndex)
        {
            List<int> index_list = new List<int>();
            index_list.Add(pIndex);
            AddToWatchlist(index_list);
        }

        /*****************************************************************************
         *  FUNCTION:       AddToWatchlist
         *  Description:    Adds a range of items from the loaded data manager to the 
         *                  watchlist, based on index value.
         *  Parameters:     
         *      pIndex -    The list of indexes identifying the data manger items (Equities)
         *                  to add to the watchlist
         *****************************************************************************/
        private void AddToWatchlist(List<int> pIndex)
        {
            foreach(int index in pIndex)
            {
                if(index >= 0 && index < MyDataManager.HistoricalData.Constituents.Count)
                {
                    this.watchlist1.Add(MyDataManager.HistoricalData.Constituents[index], MyDataManager.HistoricalData.Name);
                    this.MyDataManager.UserProfile.WatchlistItems.Add(MyDataManager.HistoricalData.Constituents[index]);
                }
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  statFilter_SelectionChanged
         *  Description:    
         *  Parameters:     
         *          sender - 
         *          e      -
         *****************************************************************************/
        private void statFilter_SelectionChanged(object sender, EventArgs e)
        {
            switch(comboBoxStatFilter.SelectedIndex)
            {
                case 1:     //% Change Selected

                case 2:     //Avg. Price Selected
 
                case 3:     //Volatility Selected
                    tblStatTableMain.Invalidate();
                    groupBoxStatFilter.Visible = true;
                    break;

                default:
                    groupBoxStatFilter.Visible = false;
                    break;
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnRefreshStatTable_Click
         *  Description:    Event handler for the 'From' and 'To' filter text box inputs.
         *                  Refreshes the displayed table data based on the updated filter
         *                  criteria.
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnRefreshStatTable_Click(object sender, EventArgs e)
        {
            String[,] filterKey;
            String filterString = "";
            Boolean isValid = Helpers.ValidateNumeric(txtStatFrom.Text) || Helpers.ValidateNumeric(txtStatTo.Text);

            if (!(this.tblStatTableMain.TableType == StatTableType.LIVE_STATS && this.MyDataManager.IsLiveSessionOpen))
            {
                filterKey = this.HistoricalFilterKey;
            }
            else
            {
                filterKey = this.LiveFilterKey;
            }

            if (isValid)
            {
                if (Helpers.ValidateNumeric(txtStatFrom.Text))
                {
                    filterString += (filterKey[comboBoxStatFilter.SelectedIndex - 1, 0] + " >= " + txtStatFrom.Text);
                }
                if (Helpers.ValidateNumeric(txtStatTo.Text))
                {
                    if (filterString != "")
                    {
                        filterString += " And ";
                    }
                    filterString += (filterKey[comboBoxStatFilter.SelectedIndex - 1, 0] + " <= " + txtStatTo.Text); ;
                }
                tblStatTableMain.SetFilterExpression(this.toolStripContainer1.ContentPanel.Handle, filterString);
                tblStatTableMain.ApplyFilter();
                UpdateStatResultCount(tblStatTableMain.DisplayedCount());
            }
        }

        /*****************************************************************************
         *  FUNCTION:       UpdateStatResultCount
         *  Description:    Updates the stat table 'result count' label text
         *  Parameters:     
         *          resultCount   - # of results
         *****************************************************************************/
        private void UpdateStatResultCount(int resultCount)
        {
            lblStatResultNum.Text = resultCount.ToString() + " Results";
        }

        /*****************************************************************************
         *  FUNCTION:       UpdateAnalysisComboBox
         *  Description:    Updates the drop-down in the Analysis tab page containing
         *                  the list of loaded Equities available for analysis
         *  Parameters:     None
         *****************************************************************************/
        private void UpdateAnalysisComboBox()
        {
            if(MyDataManager.HistoricalData.Constituents != null &&
                MyDataManager.HistoricalData.Constituents.Count > 0)
            {
                this.analysisSelectBox.Items.Clear();
                foreach(Equity eq in MyDataManager.HistoricalData.Constituents)
                {
                    this.analysisSelectBox.Items.Add(eq.Name);
                }
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysisSelectBox_SelectedIndexChanged
         *  Description:    Handles updates to the Analysis tab page when the selected
         *                  equity to be analyzed is changed. 
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysisSelectBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            List<Equity> data = MyDataManager.HistoricalData.Constituents;

            index = this.analysisSelectBox.SelectedIndex;

            if(MyDataManager.HistoricalData.Constituents != null &&
                this.analysisSelectBox.Items.Count == MyDataManager.HistoricalData.Constituents.Count)
            {
                this.AnalysisChartBindData<DateTime>(data[index].HistoricalPriceDate, data[index].HistoricalPrice);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnAnalysisShowChart_Click
         *  Description:    Handles the button click event for the 'Run Analysis' button
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnAnalysisShowChart_Click(object sender, EventArgs e)
        {
            RunAnalysisAndDisplayChart();
        }

        /*****************************************************************************
         *  FUNCTION:       RunAnalysisAndDisplayChart
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void RunAnalysisAndDisplayChart()
        {
            List<double> x_values = new List<double>();
            List<double> y_values = new List<double>();
            List<Equity> hist_data = new List<Equity>();
            //int i, x_index, y_index;
            //double data_value_x, data_value_y;

            if (MyDataManager.HistoricalData != null && 
                cbAnalysisIndicatorX.SelectedIndex >= 0 && 
                cbAnalysisIndicatorY.SelectedIndex >= 0 &&
                cbAnalysisType.SelectedIndex >= 0)
            {
                if (MyDataManager.HistoricalData.Constituents.Count > 0)
                {
                    hist_data = new List<Equity>(MyDataManager.HistoricalData.Constituents);
                    AnalysisChartTypeSpec(hist_data, (AnalysisChartType)cbAnalysisType.SelectedIndex, out x_values, out y_values);
                    AnalysisChartBindData(x_values, y_values);
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       AnalysisChartTypeSpec
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void AnalysisChartTypeSpec(List<Equity> hist_data, AnalysisChartType chart_type, out List<double> x_values, out List<double> y_values)
        {
            int i, x_index, y_index;
            double data_value_x, data_value_y;

            x_values = new List<double>();
            y_values = new List<double>();

            x_index = cbAnalysisIndicatorX.SelectedIndex;
            y_index = cbAnalysisIndicatorY.SelectedIndex;
            for (i = 0; i < hist_data.Count; i++)
            {
                data_value_x = 0;
                data_value_y = 0;
                switch (chart_type)
                {
                    case AnalysisChartType.AVERAGE:
                        data_value_x = AnalysisChartFeatureValue(hist_data[i], x_index, chart_type).Average();
                        data_value_y = AnalysisChartFeatureValue(hist_data[i], y_index, chart_type).Average();
                        this.panelAnalysis1.Visible = false;
                        break;
                    case AnalysisChartType.ANIMATED:
                        if (AnalysisDisplayIndex >= 0 && AnalysisDisplayIndex < hist_data.Count)
                        {
                            data_value_x = AnalysisChartFeatureValue(hist_data[i], x_index, chart_type)[AnalysisDisplayIndex];
                            data_value_y = AnalysisChartFeatureValue(hist_data[i], y_index, chart_type)[AnalysisDisplayIndex];
                            this.lblChartDate.Text = hist_data[0].HistoricalPriceDate[AnalysisDisplayIndex].ToShortDateString();
                            this.panelAnalysis1.Visible = true;
                        }
                        
                        break;
                    default:
                        break;
                }
                x_values.Add(data_value_x);
                y_values.Add(data_value_y);
            }
 
        }

        /*****************************************************************************
         *  FUNCTION:       AnalysisChartFeatureValue
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private List<double> AnalysisChartFeatureValue(Equity pInputData, int pFeatureIndex, AnalysisChartType pChartType)
        {
            List<double> temp_value = new List<double>();

            switch (pFeatureIndex)
            {
                //Daily Spread
                case 0:
                    temp_value = Helpers.ListDoubleOperation(ListOperator.DIFF, pInputData.HistoricalHighs, pInputData.HistoricalLows);
                    temp_value = Helpers.ListDoubleOperation(ListOperator.DIVIDE, temp_value, pInputData.HistoricalOpens);
                    temp_value = (new ListDouble(temp_value) * 100.0).ToList();
                    break;
                //Daily % Change
                case 1:
                    temp_value = (new ListDouble(pInputData.HistoricalPctChange)).ToList();
                    break;
                default:
                    break;
            }

            return temp_value;
        }

        /*****************************************************************************
         *  FUNCTION:       AnalysisChartBindData
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void AnalysisChartBindData<T>(List<T> x_values, List<double> y_values)
        {
            //Bind data to chart
            if (chartAnalysis.Series.Count <= 0)
            {
                chartAnalysis.Series.Add("Analysis");  
            }
            else if(chartAnalysis.Series.IndexOf("Analysis") < 0)
            {
                chartAnalysis.Series.Add("Analysis");  
            }
            chartAnalysis.Series["Analysis"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            chartAnalysis.Series["Analysis"].Points.Clear();
            chartAnalysis.Series["Analysis"].Points.DataBindXY(x_values, y_values);

            //Set chart axes labels
            if (cbAnalysisIndicatorX.SelectedIndex >= 0 && cbAnalysisIndicatorY.SelectedIndex >= 0)
            {
                chartAnalysis.ChartAreas[0].AxisX.Title = Analysis.ChartFeatures[cbAnalysisIndicatorX.SelectedIndex];
                chartAnalysis.ChartAreas[0].AxisY.Title = Analysis.ChartFeatures[cbAnalysisIndicatorY.SelectedIndex];
            }

            chartAnalysis.Invalidate();
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnChartNext_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnChartNext_Click(object sender, EventArgs e)
        {
            if (this.chartAnalysis.Series.Count > 0 &&
                this.AnalysisDisplayIndex < this.chartAnalysis.Series[0].Points.Count - 1)
            {
                 this.AnalysisDisplayIndex++;
                 RunAnalysisAndDisplayChart();
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnChartPrev_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnChartPrev_Click(object sender, EventArgs e)
        {
            if (this.chartAnalysis.Series.Count > 0 &&
                this.AnalysisDisplayIndex > 0)
            {
                this.AnalysisDisplayIndex--;
                RunAnalysisAndDisplayChart();
            }
        }

        private void tabAnalysis_Layout(object sender, LayoutEventArgs e)
        {
            //this.heatMap1.Redraw();
        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysis_nestedSplitPanelRightOnPaint
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysis_nestedSplitPanelRightOnPaint(object sender, PaintEventArgs e)
        {
            var control = sender as SplitContainer;
            //paint the three dots'
            Point[] points = new Point[3];
            var w = control.Width;
            var h = control.Height;
            var d = control.SplitterDistance;
            var sW = control.SplitterWidth;

            //calculate the position of the points'
            if (control.Orientation == Orientation.Horizontal)
            {
                points[0] = new Point((w / 2), d + (sW / 2));
                points[1] = new Point(points[0].X - 10, points[0].Y);
                points[2] = new Point(points[0].X + 10, points[0].Y);
            }
            else
            {
                points[0] = new Point(d + (sW / 2), (h / 2));
                points[1] = new Point(points[0].X, points[0].Y - 10);
                points[2] = new Point(points[0].X, points[0].Y + 10);
            }

            foreach (Point p in points)
            {
                p.Offset(-2, -2);
                e.Graphics.FillRectangle(SystemBrushes.ControlDark,
                    new Rectangle(p, new Size(4, 2)));

                p.Offset(1, 1);
                e.Graphics.FillRectangle(SystemBrushes.ControlLight,
                    new Rectangle(p, new Size(4, 2)));
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnLoadPatternForm_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnLoadPatternForm_Click(object sender, EventArgs e)
        {
            if (AlgDesignForm.IsDisposed || AlgDesignForm == null)
            {
                AlgDesignForm = new AlgorithmDesignForm(ref MyDataManager);
            }
            AlgDesignForm.Show(this);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  random10ToolStripMenuItem_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void random10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = 0;
            int[] indexList = new int[10];
            Random randGen = new Random();

            if (MyDataManager.HistoricalData != null && 
                MyDataManager.HistoricalData.Constituents.Count > 0)
            {
                for(int i = 0; i < 10; i++)
                {
                    do
                    {
                        index = randGen.Next(MyDataManager.HistoricalData.Constituents.Count - 1);
                    }
                    while (indexList.Contains(index));

                    indexList[i] = index;
                }

                ShowVisualizationTab(indexList.ToList(), true);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  cbRegionSelect_SelectedIndexChanged
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void cbRegionSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<String> listOfMarkets;

            if(cbRegionSelect.SelectedIndex >= 0 && cbRegionSelect.SelectedIndex < RegionImageList.Length)
            {
                imgRegionFlag.Image = RegionImageList[cbRegionSelect.SelectedIndex];
            }

            listOfMarkets = MyDataManager.GetListOfAvailableMarkets(RegionDropDownList[cbRegionSelect.SelectedIndex]);
            this.cbMarketSelect.Items.Clear();
            this.cbMarketSelect.Items.AddRange(listOfMarkets.ToArray());

            toolStripStat.Focus();
        }

        /*****************************************************************************
         *  EVENT HANDLER:  openToolStripMenuItem_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double dwnld_period;

            if(this.cbLiveDataInterval.SelectedIndex < 0)
            {
                dwnld_period = (double)this.LiveSessionIntervalsSec[0] / 60.0;
                this.cbLiveDataInterval.SelectedIndex = 0;
            }
            else
            {
                dwnld_period = (double)this.LiveSessionIntervalsSec[this.cbLiveDataInterval.SelectedIndex] / 60.0;
            }
            
            //Validate selected market

            //Start the download / periodic update
            MyDataManager.OpenLiveSession(dwnld_period);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  openToolStripMenuItem_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MyDataManager.CloseLiveSession();
            SetDisplayLiveDataStatus(false);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  cbMarket_IndexChanged
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void cbMarket_IndexChanged(object sender, EventArgs e)
        {
            int i, j;

            i = cbRegionSelect.SelectedIndex;
            j = cbMarketSelect.SelectedIndex;

            MyDataManager.ChangeActiveMarket(i, j);
            
            toolStripStat.Focus();
        }

        /*****************************************************************************
         *  EVENT HANDLER:  dataMenuArrow_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void dataMenuArrow_Click(object sender, EventArgs e)
        {
            if(this.DataMenuPanelVisible)
            {
                CollapseMenuPanel();
            }
            else
            {
                ExpandMenuPanel();
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  dataMenuArrow_MouseEnter
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void dataMenuArrow_MouseEnter(object sender, EventArgs e)
        {
            if (this.DataMenuPanelVisible)
            {
                this.dataMenuArrow.Image = MyMarketAnalyzer.Properties.Resources.arrow_icon_hover_rev;
            }
            else
            {
                this.dataMenuArrow.Image = MyMarketAnalyzer.Properties.Resources.arrow_icon_hover;
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  dataMenuArrow_MouseLeave
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void dataMenuArrow_MouseLeave(object sender, EventArgs e)
        {
            if (this.DataMenuPanelVisible)
            {
                this.dataMenuArrow.Image = MyMarketAnalyzer.Properties.Resources.arrow_icon_normal_rev;
            }
            else
            {
                this.dataMenuArrow.Image = MyMarketAnalyzer.Properties.Resources.arrow_icon_normal;
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  loadProfileToolStripMenuItem_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void loadProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "MMA Profiles (*.mma)|*.mma";
            ofd.FilterIndex = 1;
            ofd.InitialDirectory = this.MyDataManager.UserProfile.CurrentLocation;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                this.MyDataManager.UpdateUserProfile(ofd.FileName);

                this.watchlist1.Visible = false;
                foreach (Equity eq in this.MyDataManager.UserProfile.WatchlistItems)
                {
                    this.watchlist1.Add(eq, eq.ListedMarket);
                }
                this.watchlist1.Visible = true;
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  saveProfileToolStripMenuItem_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "MMA Profiles (*.mma)|*.mma";
            sfd.FilterIndex = 1;
            sfd.InitialDirectory = this.MyDataManager.UserProfile.CurrentLocation;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                this.MyDataManager.UserProfile.Save(sfd.FileName);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnBuyRuleExpandCollapse_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnBuyRuleExpandCollapse_Click(object sender, EventArgs e)
        {
            int x, y;
            TableLayoutRowStyleCollection styles = this.tableLayoutPanel1.RowStyles;
            TableLayoutRowStyleCollection styles2 = this.tblPanelBuyRule.RowStyles;

            //Collapse 'Buy Rule' text box
            if (this.analysisBuy_RTxtBox.Dock == DockStyle.Fill)
            {
                styles[0].SizeType = SizeType.Absolute;
                styles[1].SizeType = SizeType.Absolute;
                styles[2].SizeType = SizeType.AutoSize;

                this.analysisBuy_RTxtBox.Dock = DockStyle.None;
                this.analysisBuy_RTxtBox.Width = this.analysisBuy_RTxtBox.Parent.Width - 25;
                this.analysisBuy_RTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                //Reset the table layout panel containing the 'buy' and 'sell' rule containers
                styles[0].Height = 64;
                styles[1].Height = 64;

                //Reset the table layout panel containing only the buy rule container
                styles2[0].SizeType = SizeType.Absolute;
                styles2[0].Height = 32;
                styles2[1].SizeType = SizeType.AutoSize;

                //Move the expand/collapse button back to its initial position
                x = this.btnBuyRuleExpandCollapse.Parent.Width - this.btnBuyRuleExpandCollapse.Width;
                y = 4;

                this.btnBuyRuleExpandCollapse.Location = new Point(x, y);
                this.btnBuyRuleExpandCollapse.BringToFront();

                this.btnBuyRuleExpandCollapse.Image = MyMarketAnalyzer.Properties.Resources.expand_icon;
            }
            //Expand Buy Rule text box
            else
            {
                styles[0].SizeType = SizeType.Percent;
                styles[1].SizeType = SizeType.Percent;
                styles[2].SizeType = SizeType.Percent;

                //Expand the row containing the buy rule table layout panel to fill the entire buy/sell rule area
                styles[0].Height = 100;
                styles[1].Height = 0;
                styles[2].Height = 0;

                //Resize the buy rule table layout panel to fill as much of the area as necessary
                styles2[0].SizeType = SizeType.Percent;
                styles2[0].Height = 100;
                styles2[1].SizeType = SizeType.Percent;
                styles2[1].Height = 0;

                this.analysisBuy_RTxtBox.Dock = DockStyle.Fill;

                x = this.btnBuyRuleExpandCollapse.Parent.Width - this.btnBuyRuleExpandCollapse.Width - 2;
                y = 0;

                this.btnBuyRuleExpandCollapse.Location = new Point(x, y);
                this.btnBuyRuleExpandCollapse.BringToFront();

                this.btnBuyRuleExpandCollapse.Image = MyMarketAnalyzer.Properties.Resources.collapse_icon;
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnSellRuleExpandCollapse_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnSellRuleExpandCollapse_Click(object sender, EventArgs e)
        {
            int x, y;
            TableLayoutRowStyleCollection styles = this.tableLayoutPanel1.RowStyles;
            TableLayoutRowStyleCollection styles2 = this.tblPanelSellRule.RowStyles;

            if (this.analysisSell_RTxtBox.Dock == DockStyle.Fill)
            {
                styles[0].SizeType = SizeType.Absolute;
                styles[1].SizeType = SizeType.Absolute;
                styles[2].SizeType = SizeType.AutoSize;

                this.analysisSell_RTxtBox.Dock = DockStyle.None;
                this.analysisSell_RTxtBox.Width = this.analysisSell_RTxtBox.Parent.Width - 25;
                this.analysisSell_RTxtBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

                styles[0].Height = 64;
                styles[1].Height = 64;

                styles2[0].SizeType = SizeType.Absolute;
                styles2[0].Height = 32;
                styles2[1].SizeType = SizeType.AutoSize;

                x = this.btnSellRuleExpandCollapse.Parent.Width - this.btnSellRuleExpandCollapse.Width;
                y = 4;

                this.btnSellRuleExpandCollapse.Location = new Point(x, y);
                this.btnSellRuleExpandCollapse.BringToFront();

                this.btnSellRuleExpandCollapse.Image = MyMarketAnalyzer.Properties.Resources.expand_icon;
            }
            else
            {
                styles[0].SizeType = SizeType.Percent;
                styles[1].SizeType = SizeType.Percent;
                styles[2].SizeType = SizeType.Percent;

                styles[0].Height = 0;
                styles[1].Height = 100;
                styles[2].Height = 0;

                styles2[0].SizeType = SizeType.Percent;
                styles2[0].Height = 100;
                styles2[1].SizeType = SizeType.Percent;
                styles2[1].Height = 0;

                this.analysisSell_RTxtBox.Dock = DockStyle.Fill;

                x = this.btnSellRuleExpandCollapse.Parent.Width - this.btnSellRuleExpandCollapse.Width - 2;
                y = 0;

                this.btnSellRuleExpandCollapse.Location = new Point(x, y);
                this.btnSellRuleExpandCollapse.BringToFront();

                this.btnSellRuleExpandCollapse.Image = MyMarketAnalyzer.Properties.Resources.collapse_icon;
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnRunAnalysis_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void btnRunAnalysis_Click(object sender, EventArgs e)
        {
            int index;
            double principal_amt = 0.0;
            List<Equity> data = MyDataManager.HistoricalData.Constituents;

            index = this.analysisSelectBox.SelectedIndex;

            if(MyDataManager.HistoricalData.Constituents != null &&
                this.analysisSelectBox.Items.Count == MyDataManager.HistoricalData.Constituents.Count)
            {
                if (Helpers.ValidateNumeric(this.analysisAmtTxt.Text))
                {
                    principal_amt = Double.Parse(this.analysisAmtTxt.Text);
                    this.TestRuleParser.SetInputParams(data[index], this.analysisBuy_RTxtBox.Text, this.analysisSell_RTxtBox.Text, principal_amt);

                    backgroundWorkerAnalysis.RunWorkerAsync();
                    backgroundWorkerAnalysisProgress.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Please enter a valid Principal Amount!", "Invalid Principal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysisAmtHelp_MouseEnter
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysisAmtHelp_MouseEnter(object sender, EventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.analysisAmtHelpBtn, "Maximum amount (excluding returns) to invest.");

            this.analysisAmtHelpBtn.BackColor = Color.LightCyan;
        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysisAmtHelp_MouseDown
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysisAmtHelp_MouseDown(object sender, MouseEventArgs e)
        {
            ToolTip tt = new ToolTip();
            tt.SetToolTip(this.analysisAmtHelpBtn, "Maximum amount (excluding returns) to invest.");

            this.analysisAmtHelpBtn.BackColor = Color.LightCyan;
        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysisAmtHelp_MouseLeave
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysisAmtHelp_MouseLeave(object sender, EventArgs e)
        {
            this.analysisAmtHelpBtn.BackColor = Color.Transparent;
        }

        /*****************************************************************************
         *  EVENT HANDLER:  backgroundWorkerAnalysisProgress_DoWork
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void backgroundWorkerAnalysisProgress_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int[] pct_progress = { 0, 0 };
            const long timeout = 20000;
            long time_passed = 0;
            int exit_persistence = 0;

            while (this.TestRuleParser.PercentComplete < 1 && time_passed < timeout)
            {
                System.Threading.Thread.Sleep(100);
                time_passed += 100;
                pct_progress[1] = (int)(this.TestRuleParser.PercentComplete * 100);
                if (pct_progress[1] == pct_progress[0])
                {
                    exit_persistence += 1;
                }
                else
                {
                    exit_persistence = 0;
                }

                //if the progress hasn't changed in 5 update periods, exit
                if (exit_persistence >= 5)
                {
                    break;
                }
                pct_progress[0] = pct_progress[1];
                worker.ReportProgress(pct_progress[1]);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  backgroundWorkerAnalysisProgress_ProgressChanged
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void backgroundWorkerAnalysisProgress_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            analysisSummaryPage1.UpdateProgress(e.ProgressPercentage);
            if(e.ProgressPercentage == 100)
            {
                this.analysisSummaryPage1.DisplayResult();
            }
        }

        /*****************************************************************************
         *  FUNCTION:       updateActiveAnalysisRule
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void updateActiveAnalysisRule(Type pType, object pInParam)
        {
            String newText = "";

            if(pType == typeof(Fn))
            {
                newText = StringEnum.GetStringValue((Fn)Enum.Parse(typeof(Fn),pInParam.ToString()));
                newText += "(";
            }
            else if(pType == typeof(Variable))
            {
                newText = StringEnum.GetStringValue((Variable)Enum.Parse(typeof(Variable), pInParam.ToString()));
            }
            else { }

            if(this.AnalysisBuyRuleActive)
            {
                this.analysisBuy_RTxtBox.Text += newText;
            }
            else if (this.AnalysisSellRuleActive)
            {
                this.analysisSell_RTxtBox.Text += newText;
            }
            else { }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  backgroundWorkerAnalysis_DoWork
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void backgroundWorkerAnalysis_DoWork(object sender, DoWorkEventArgs e)
        {
            AnalysisResult _result;
            _result = this.TestRuleParser.RunAnalysis();
            this.analysisSummaryPage1.SetResult(_result);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysisBuy_RTxtBox_TextChanged
         *  Description:    Event fired when the contents of the 'buy rule' text box
         *                  is changed. Enables intelisense / autocomplete.
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysisBuy_RTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        /*****************************************************************************
         *  EVENT HANDLER:  analysisSell_RTxtBox_TextChanged
         *  Description:    Event fired when the contents of the 'sell rule' text box
         *                  is changed. Enables intelisense / autocomplete.
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void analysisSell_RTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

        /*****************************************************************************
         *  EVENT HANDLER:  buyRTBox_OnFocus
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void buyRTBox_OnFocus(object sender, EventArgs e)
        {
            Regex reg;
            int buyRuleLines = this.analysisBuy_RTxtBox.Lines.Count();
            int sellRuleLines = this.analysisSell_RTxtBox.Lines.Count();

            this.AnalysisBuyRuleActive = true;
            this.AnalysisSellRuleActive = false;

            this.tblPanelBuyRule.BackColor = Color.LightSteelBlue;
            this.tblPanelSellRule.BackColor = Color.Transparent;

            if(buyRuleLines == 0)
            {
                if (this.analysisBuy_RTxtBox.Text.Trim() == String.Empty)
                {
                    this.analysisBuy_RTxtBox.Text += DEFAULT_UNITS_SPECIFIER;
                }
            }
            else
            {
                if (this.analysisBuy_RTxtBox.Lines[buyRuleLines - 1].Trim() == String.Empty)
                {
                    this.analysisBuy_RTxtBox.Lines[buyRuleLines - 1] += DEFAULT_UNITS_SPECIFIER;
                }
            }
            
            reg = new Regex("\\[U.*\\]");
            if (sellRuleLines == 0)
            {
                if (reg.Replace(this.analysisSell_RTxtBox.Text, "", 1).Trim() == String.Empty)
                {
                    this.analysisSell_RTxtBox.Text = "";
                }
            }
            else
            {
                if (reg.Replace(this.analysisSell_RTxtBox.Lines[sellRuleLines - 1], "", 1).Trim().Length == 0)
                {
                    this.analysisSell_RTxtBox.Lines[sellRuleLines - 1] = "";
                }
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  sellRTBox_OnFocus
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void sellRTBox_OnFocus(object sender, EventArgs e)
        {
            Regex reg;
            int buyRuleLines = this.analysisBuy_RTxtBox.Lines.Count();
            int sellRuleLines = this.analysisSell_RTxtBox.Lines.Count();

            this.AnalysisBuyRuleActive = false;
            this.AnalysisSellRuleActive = true;

            this.tblPanelBuyRule.BackColor = Color.Transparent;
            this.tblPanelSellRule.BackColor = Color.LightSteelBlue;

            if(sellRuleLines == 0)
            {
                if (this.analysisSell_RTxtBox.Text.Trim() == String.Empty)
                {
                    this.analysisSell_RTxtBox.Text += DEFAULT_UNITS_SPECIFIER;
                }
            }
            else
            {
                if (this.analysisSell_RTxtBox.Lines[sellRuleLines - 1].Trim() == String.Empty)
                {
                    this.analysisSell_RTxtBox.Lines[sellRuleLines - 1] += DEFAULT_UNITS_SPECIFIER;
                }
            }

            reg = new Regex("\\[U.*\\]");
            if (buyRuleLines == 0)
            {
                if (reg.Replace(this.analysisBuy_RTxtBox.Text, "", 1).Trim() == String.Empty)
                {
                    this.analysisBuy_RTxtBox.Text = "";
                }
            }
            else
            {
                if (reg.Replace(this.analysisBuy_RTxtBox.Lines[buyRuleLines - 1], "", 1).Trim().Length == 0)
                {
                    //this doesn't work?
                    this.analysisBuy_RTxtBox.Lines[buyRuleLines - 1] = "";
                }
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  heatMapToolStripMenuItem_Click
         *  Description:    
         *  Parameters:     
         *          sender - handle to the item which fired the event
         *          e      - default event args
         *****************************************************************************/
        private void heatMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            heatMapControl.BindMarketData(this.MyDataManager.HistoricalData);

            heatMapForm.Size = new Size(heatMapControl.Width, heatMapControl.Height);
            heatMapForm.Controls.Add(heatMapControl);
            heatMapControl.Dock = DockStyle.Fill;
            heatMapForm.Text = "Heat Map";

            heatMapForm.Show();
        }

    }
}
