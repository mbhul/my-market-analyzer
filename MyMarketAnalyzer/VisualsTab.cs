using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;

namespace MyMarketAnalyzer
{
    public partial class VisualsTab : UserControl
    {
        public enum TransformType
        {
            [StringValue("Gaussian")]
            TRF_GAUSS = 0xF0,
            [StringValue("Mean")]
            TRF_MEAN = 0xF1,
            [StringValue("Normalize")]
            TRF_NORM = 0xF2,
            [StringValue("Shifted PPC")]
            TRF_SHIFT_PPC = 0xF3
        }

        //***** CONSTANTS *****//
        private const int TAB_X_PAD_CHARS = 10;
        private const UInt16 MAX_INVALIDATION_LEVEL = 10;

        private TransformType[] TransformTList = { 
                                                       TransformType.TRF_GAUSS, TransformType.TRF_MEAN, TransformType.TRF_NORM,
                                                       TransformType.TRF_SHIFT_PPC
                                                   };

        //***** DATA MEMBERS *****//
        private List<Equity> dataSet;
        protected ExchangeMarket statsMarketData;
        private List<String> CorrelationTableSortInstruction;
        private Boolean AllTransformApplied;
        private UInt16 DataIsValid;

        private int trfWorkerSelectedIndex;
        private String[] trfWorkerParamText;

        //***** PROPERTIES *****//
        private int lastDisplayedCorrelation;
        private int lastSelectedTab;
        private Boolean tabControlLocked = false;
        private Boolean isCloseButtonHovered = false;

        //test
        private ToolTip ChartYToolTip = new ToolTip();

        public VisualsTab()
        {
            InitializeComponent();
            InitializePrivateGlobals();
        }

        public VisualsTab(Equity inputData, ExchangeMarket marketData = null)
        {
            InitializeComponent();
            InitializePrivateGlobals();
            dataSet = new List<Equity>();
            dataSet.Add(inputData);
            if (marketData != null)
            {
                statsMarketData = marketData;
            }   
        }

        public VisualsTab(List<Equity> inputData, ExchangeMarket marketData = null)
        {
            InitializeComponent();
            InitializePrivateGlobals();
            //dataSet = inputData;
            dataSet = new List<Equity>(inputData);
            if (marketData != null)
            {
                statsMarketData = marketData;
            }  
        }

        private void SET_DATA_INVALID()
        {
            if (DataIsValid < MAX_INVALIDATION_LEVEL)
            {
                DataIsValid++;
            }
            else if (DataIsValid > MAX_INVALIDATION_LEVEL)
            {
                //Something wrong, reset the validation level
                DataIsValid = 0;
            }
        }

        private void SET_DATA_VALID()
        {
            if (DataIsValid > MAX_INVALIDATION_LEVEL)
            {
                //Something wrong, reset the validation level
                DataIsValid = 0;
            }
            else if (DataIsValid > 0)
            {
                DataIsValid--;
            }
        }

        private void InitializePrivateGlobals()
        {
            dataSet = null;
            statsMarketData = null;
            lastDisplayedCorrelation = -1;
            lastSelectedTab = 0;
            CorrelationTableSortInstruction = new List<string>();
            AllTransformApplied = false;
            DataIsValid = 0;

            for (int i = 0; i < TransformTList.Length; i++)
            {
                this.tsComboTransformation.Items.Add(StringEnum.GetStringValue(TransformTList[i]));
            }
        }

        private void VisualsTab_Load(object sender, EventArgs e)
        {
            SET_DATA_INVALID();
            ClearSummaryDataTabControl();

            splitContainerTabVS.Panel2Collapsed = true;

            if (dataSet != null)
            {
                LoadChartData();

                chartMain.ChartAreas[0].CursorX.LineColor = Color.Black;
                chartMain.ChartAreas[0].CursorX.LineWidth = 1;
                chartMain.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dot;
                chartMain.ChartAreas[0].CursorX.Interval = 0;
                chartMain.ChartAreas[0].CursorY.LineColor = Color.Black;
                chartMain.ChartAreas[0].CursorY.LineWidth = 1;
                chartMain.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dot;
                chartMain.ChartAreas[0].CursorY.Interval = 0;   
            }
            SET_DATA_VALID();
        }

        private void AddChartLegend()
        {
            int i;

            if (chartMain.Series.Count > 1)
            {
                for (i = 0; i < chartMain.Series.Count; i++)
                {
                    // Assign the legend to Series1.
                    if (chartMain.Series[i].ChartArea == chartMain.ChartAreas[0].Name)
                    {
                        chartMain.Series[i].Legend = "chartMainLegend1";
                        chartMain.Series[i].IsVisibleInLegend = true;
                    }
                }
            }
            else
            {
                //Do Nothing
            }
            
        }

        private void UpdateChartTitles()
        {
            if(chartMain.Titles.Count == 0)
            {
                chartMain.Titles.Add(new Title());
            }

            if (dataSet.Count > 1)
            {
                chartMain.Titles[0].Text = "Selected Equities";
            }
            else
            {
                chartMain.Titles[0].Text = dataSet[0].Name;
            }

            if(chartMain.Titles.IndexOf(TechnicalIndicatorConst.TI_CHARTAREA_NAME) >= 0)
            {
                chartMain.Titles[TechnicalIndicatorConst.TI_CHARTAREA_NAME].Text = dataSet[lastSelectedTab].Name + "Technical Indicators";
            }
        }
        
        public void ReBindExchangeMarket(ExchangeMarket pMarket)
        {
            if(pMarket != null)
            {
                this.statsMarketData = pMarket;
            }
        }

        //Reload with the passed data
        public void ReloadChart(List<Equity> inputData)
        {
            SET_DATA_INVALID();
            dataSet.Clear();
            chartMain.Series.Clear();
            dataSet = inputData;

            ClearSummaryDataTabControl();
            LoadChartData();

            chartMain.Invalidate();
            SET_DATA_VALID();
        }

        private void ReloadChart()
        {
            SET_DATA_INVALID();
            chartMain.Series.Clear();

            ClearSummaryDataTabControl();
            LoadChartData();

            chartMain.Invalidate();
            SET_DATA_VALID();
        }

        public void AddToChart(List<Equity> inputData)
        {
            int i;
            List<Equity> newData = new List<Equity>();

            SET_DATA_INVALID();

            for (i = 0; i < inputData.Count; i++)
            {
                if(!dataSet.Contains(inputData[i]))
                {
                    dataSet.Add(inputData[i]);
                    newData.Add(inputData[i]);
                }
            }

            AppendChartData(newData);

            chartMain.Invalidate();
            SET_DATA_VALID();
        }

        public void RemoveFromChart(int AtIndex)
        {
            TabPage pTab;

            SET_DATA_INVALID();

            //Remove from data set
            dataSet.RemoveAt(AtIndex);

            //Remove tab from tab control
            pTab = tabVisualsData.TabPages[AtIndex];
            this.tabVisualsData.TabPages.Remove(pTab);
            pTab.Controls.Clear();
            pTab.Dispose();
            pTab = null;

            //Update chart control
            // Lock the tab control since we are only removing data and do not want to create any new tabs
            chartMain.Series.Clear();
            tabControlLocked = true;
            LoadChartData();
            tabControlLocked = false;
            chartMain.Invalidate();

            SET_DATA_VALID();
        }

        private void LoadChartData()
        {
            int i;
            TabPage newTab;
            VisualSummaryTabPage vspage;

            for (i = 0; i < dataSet.Count; i++)
            {
                //Update the selected equity tab control
                if (!tabControlLocked)
                {
                    if (i == 0)
                    {
                        tabVisualsData.TabPages[0].Text = dataSet[i].Name.PadRight(dataSet[i].Name.Length + TAB_X_PAD_CHARS, ' ');
                        visualSummaryTabPage1.BindDataSet(dataSet[i]);
                    }
                    else
                    {
                        newTab = new TabPage();
                        vspage = new VisualSummaryTabPage(dataSet[i]);
                        newTab.Controls.Add(vspage);
                        newTab.Text = dataSet[i].Name.PadRight(dataSet[i].Name.Length + TAB_X_PAD_CHARS, ' ');
                        tabVisualsData.TabPages.Add(newTab);
                        vspage.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                        vspage.Width = tabVisualsData.TabPages[0].Controls[0].Width;
                    }
                }

                //Plot the chart data
                chartMain.Series.Add(dataSet[i].Name);
                chartMain.Series[dataSet[i].Name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                if (((VisualSummaryTabPage)tabVisualsData.TabPages[i].Controls[0]).DisplayedData == DisplayedDataSet.HISTORICAL)
                {
                    chartMain.Series[dataSet[i].Name].Points.DataBindXY(dataSet[i].HistoricalPriceDate, dataSet[i].HistoricalPrice);
                }
                else if (((VisualSummaryTabPage)tabVisualsData.TabPages[i].Controls[0]).DisplayedData == DisplayedDataSet.LIVE)
                {
                    chartMain.Series[dataSet[i].Name].Points.DataBindXY(dataSet[i].DailyTime, dataSet[i].DailyLast);
                }
                else { }

                AddChartLegend();
                CorrelationTableSortInstruction.Add(String.Empty);
            }

            UpdateChartTitles();
        }

        private void AppendChartData(List<Equity> appData)
        {
            int i, total_index;
            TabPage newTab;
            VisualSummaryTabPage vspage;

            for (i = 0; i < appData.Count; i++)
            {
                if (!tabControlLocked)
                {
                    //Update tab control
                    newTab = new TabPage();
                    vspage = new VisualSummaryTabPage(appData[i]);
                    newTab.Controls.Add(vspage);
                    newTab.Text = appData[i].Name.PadRight(appData[i].Name.Length + TAB_X_PAD_CHARS, ' ');
                    tabVisualsData.TabPages.Add(newTab);
                    vspage.Anchor = AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    vspage.Width = tabVisualsData.TabPages[0].Controls[0].Width;

                    //Plot chart data
                    chartMain.Series.Add(appData[i].Name);
                    chartMain.Series[appData[i].Name].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;

                    total_index = i + dataSet.Count - 1;
                    if (((VisualSummaryTabPage)tabVisualsData.TabPages[total_index].Controls[0]).DisplayedData == DisplayedDataSet.HISTORICAL)
                    {
                        chartMain.Series[appData[i].Name].Points.DataBindXY(appData[i].HistoricalPriceDate, appData[i].HistoricalPrice);
                    }
                    else if (((VisualSummaryTabPage)tabVisualsData.TabPages[total_index].Controls[0]).DisplayedData == DisplayedDataSet.LIVE)
                    {
                        chartMain.Series[appData[i].Name].Points.DataBindXY(appData[i].DailyTime, appData[i].DailyLast);
                    }
                    else { }

                    AddChartLegend();
                    CorrelationTableSortInstruction.Add(String.Empty);
                } 
            }

            UpdateChartTitles();
        }

        //Clear all tab pages other than the first from the tab control 
        private void ClearSummaryDataTabControl()
        {
            int i, tabcount, selectedTab;
            TabPage removedTab;

            tabcount = tabVisualsData.TabPages.Count;
            CorrelationTableSortInstruction.Clear();

            if(tabcount > 1)
            {
                selectedTab = tabVisualsData.SelectedIndex;
                tabVisualsData.SelectedIndex = 0;
                lastSelectedTab = selectedTab;
            }

            for (i = 0; i < tabcount; i++)
            {
                if(i > 0)
                {
                    removedTab = tabVisualsData.TabPages[1];
                    tabVisualsData.TabPages.Remove(removedTab);
                    removedTab.Controls.Clear();
                    removedTab.Dispose();
                    removedTab = null;
                }
            }
        }

        //Resize
        private void VisualsTab_Resize(object sender, EventArgs e)
        {

        }

        private void chartMain_SelectedPointChanged(object sender, EventArgs e)
        {
            ((VisualSummaryTabPage)tabVisualsData.TabPages[chartMain.CurrentSeriesIndex].Controls[0]).UpdateCurrentPoint(chartMain.CurrentPointIndex);
        }

        private void tabVisualsData_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Bind Corresponding data to the correlation table
            BindCorrelationTable();

            //Re-apply the sorting instruction that was last applied to the current data
            if (tblVisualsTab1.Visible && CorrelationTableSortInstruction.Count > 0)
            {
                tblVisualsTab1.SetDataViewSortInstruction(CorrelationTableSortInstruction[tabVisualsData.SelectedIndex]);
            }

            //Update Selection Indexes
            lastSelectedTab = tabVisualsData.SelectedIndex;
            this.chartMain.CurrentSeriesIndex = lastSelectedTab;

            //Set the initial 'current point' information
            ((VisualSummaryTabPage)tabVisualsData.TabPages[lastSelectedTab].Controls[0]).UpdateCurrentPoint(0);

            //Manage chart area updates
            SET_DATA_INVALID();

            manageTechnicalIndicators();
            UpdateChartTitles();

            SET_DATA_VALID();
        }

        private void tabVisualsData_Deselected(object sender, TabControlEventArgs e)
        {
            lastSelectedTab = tabVisualsData.SelectedIndex;
            if (lastSelectedTab > -1 && lastSelectedTab < CorrelationTableSortInstruction.Count)
            {
                CorrelationTableSortInstruction[lastSelectedTab] = tblVisualsTab1.GetDataViewSortInstruction();
            }
        }

        public void ToggleCorrelationTable(bool setVisible)
        {
            splitContainerTabVS.SplitterDistance = (int)(this.Width * 0.65);
            splitContainerTabVS.Panel2Collapsed = !setVisible;
        }

        private void BindCorrelationTable()
        {
            //Get the index within
            int pIndex = statsMarketData.Constituents.IndexOf(statsMarketData.Constituents.Where(x => x.Name == dataSet[tabVisualsData.SelectedIndex].Name).FirstOrDefault());
            if (statsMarketData != null && pIndex > 0)
            {
                tblVisualsTab1.BindCorrelationData(ref statsMarketData, pIndex);
            }
        }

        private void tblVisualsTab1_OnLayout(object sender, LayoutEventArgs e)
        {
            //Set up the correlation table
            if (tabVisualsData.SelectedIndex != lastDisplayedCorrelation)
            {
                BindCorrelationTable();
                lastDisplayedCorrelation = tabVisualsData.SelectedIndex;
            }
        }

        /*---------------------------------------------------------------------------
         * Override the DrawItem event for the tabVisualsData Tab Control. 
         * Allows drawing of the 'X' close button.
         * ---------------------------------------------------------------------------
         */
        private void tabVisualsData_DrawItem(object sender, DrawItemEventArgs e)
        {
            Rectangle r = tabVisualsData.GetTabRect(e.Index);
            Rectangle closeButton = new Rectangle(r.Right - 30, r.Top + 2, 30, 30);

            Point mousePoint = tabVisualsData.PointToClient(System.Windows.Forms.Control.MousePosition);

            //This code will render a "x" mark at the end of the Tab caption. 
            if (tabVisualsData.SelectedIndex == e.Index)
            {
                if (this.isCloseButtonHovered) //closeButton.Contains(mousePoint))
                {
                    e.Graphics.DrawImage(MyMarketAnalyzer.Properties.Resources.x_icon_hover, new Point(e.Bounds.Right - 30, e.Bounds.Top + 2));
                }
                else
                {
                    e.Graphics.DrawImage(MyMarketAnalyzer.Properties.Resources.x_icon_normal, new Point(e.Bounds.Right - 30, e.Bounds.Top + 2));
                }
            }
            e.Graphics.DrawString(this.tabVisualsData.TabPages[e.Index].Text, e.Font, Brushes.Black, e.Bounds.Left + 4, e.Bounds.Top + 4);
            e.DrawFocusRectangle();
        }

        private void tabVisualsData_MouseDown(object sender, MouseEventArgs e)
        {
            //Looping through the controls.
            for (int i = 0; i < this.tabVisualsData.TabPages.Count; i++)
            {
                Rectangle r = tabVisualsData.GetTabRect(i);
                Rectangle closeButton = new Rectangle(r.Right - 30, r.Top + 2, 30, 30);
                if(tabVisualsData.TabCount == 1)
                {
                    this.InvokeLostFocus(this.Parent, new EventArgs());
                }
                else if (closeButton.Contains(e.Location))
                {
                    this.isCloseButtonHovered = false;
                    RemoveFromChart(i);
                    break;
                }
                else { }
            }
        }

        private bool IsCloseButtonHoverChanged(Point mouse_location)
        {
            bool isChanged = false;
            bool closeButtonHover;

            Rectangle r = tabVisualsData.GetTabRect(tabVisualsData.SelectedIndex);
            Rectangle closeButton = new Rectangle(r.Right - 30, r.Top + 2, 30, 30);
            closeButtonHover = closeButton.Contains(mouse_location);

            isChanged = (closeButtonHover == !this.isCloseButtonHovered);
            this.isCloseButtonHovered = closeButtonHover;

            return isChanged;
        }

        private void tabVisualsData_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCloseButtonHoverChanged(e.Location))
            {
                tabVisualsData.Invalidate();
            }
        }

        private void tabVisualsData_MouseLeave(object sender, EventArgs e)
        {
            if (this.isCloseButtonHovered)
            {
                tabVisualsData.Invalidate();
            }

            this.isCloseButtonHovered = false;
        }

        private void tabVisualsData_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsCloseButtonHoverChanged(e.Location))
            {
                tabVisualsData.Invalidate();
            }
        }

        public void displayNewWindow()
        {

        }

        #region Series Transformation Functions
        private void tsComboTransform_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Pre-processing: set-up the parameter input labels and text boxes
            if (tsComboTransformation.SelectedIndex >= 0)
            {
                tsBtnUpdate.Enabled = true;
                tsBtnTransformAll.Enabled = true;
                //switch (tsComboTransformation.SelectedItem.ToString())
                switch(TransformTList[tsComboTransformation.SelectedIndex])
                {
                    case TransformType.TRF_GAUSS:
                        //Set Visible Parameters
                        setVisibleParams("Sigma:", "Recursion:");
                        //Set Default Value for empty parameters
                        if (tsParam1Txt.Text == "")
                        {
                            tsParam1Txt.Text = "1.0";
                        }
                        if (tsParam2Txt.Text == "")
                        {
                            tsParam2Txt.Text = "1";
                        }
                        break;
                    case TransformType.TRF_MEAN:
                        setVisibleParams("Window:", "Recursion:");
                        //Set Default Value for empty parameters
                        if (tsParam1Txt.Text == "")
                        {
                            tsParam1Txt.Text = "2";
                        }
                        if (tsParam2Txt.Text == "")
                        {
                            tsParam2Txt.Text = "1";
                        }
                        break;
                    case TransformType.TRF_NORM:
                        setVisibleParams();
                        break;

                    case TransformType.TRF_SHIFT_PPC:
                        setVisibleParams("Shift Value:");
                        //Set Default Value for empty parameters
                        if (tsParam1Txt.Text == "")
                        {
                            tsParam1Txt.Text = "1";
                        }
                        break;

                    default:
                        tsBtnUpdate.Enabled = false;
                        tsBtnTransformAll.Enabled = false;
                        break;
                }
            }

            //Set focus on the tool strip (lose focus from the combobox)
            vsTabToolStrip.Focus();
        }

        private void SET_WAIT_CURSOR(bool center = false)
        {
            if(center == true)
            {
                System.Windows.Forms.Cursor.Position = new Point(this.Width / 2, this.Height / 2);
            }
            this.Cursor = Cursors.WaitCursor;
            this.chartMain.Cursor = Cursors.WaitCursor;
            vsTabToolStrip.Enabled = false;
        }

        private void CLEAR_WAIT_CURSOR()
        {
            this.Cursor = Cursors.Default;
            this.chartMain.Cursor = Cursors.Default;
            vsTabToolStrip.Enabled = true;
        }

        private void ApplyTransformation()
        {
            //Set Cursor to the wait cursor
            SET_WAIT_CURSOR();

            //Populate the required worker thread parameters
            trfWorkerSelectedIndex = tsComboTransformation.SelectedIndex;
            trfWorkerParamText = new String[4] {tsComboTransformation.SelectedItem.ToString(), tsParam1Txt.Text, tsParam2Txt.Text, tsParam3Txt.Text};

            this.dataTransformationThread.RunWorkerAsync();
        }

        private void dataTransformationThread_DoWork(object sender, DoWorkEventArgs e)
        {
            int i, r;
            String StrParam1, StrParam2, StrParam3;
            int recursion = 1;
            Dictionary<String, String> tParams = new Dictionary<string, string>();
            List<Equity> DataToTransform;

            SET_DATA_INVALID();

            //Point to the data set to be transformed
            if (AllTransformApplied == true)
            {
                DataToTransform = statsMarketData.Constituents;
            }
            else
            {
                DataToTransform = dataSet;
            }

            //Validate the required control parameters
            if (trfWorkerParamText.Length != 4)
            {
                trfWorkerSelectedIndex = -1;
            }

            //Validate the selected transformation combobox index
            if (trfWorkerSelectedIndex >= 0)
            {
                switch (TransformTList[trfWorkerSelectedIndex])
                {
                    case TransformType.TRF_GAUSS:
                        StrParam1 = trfWorkerParamText[1];
                        StrParam2 = trfWorkerParamText[2];
                        StrParam3 = trfWorkerParamText[3];

                        //If input parameter is valid, perform the transformation
                        if (Helpers.ValidateNumeric(StrParam1) && Helpers.ValidateNumeric(StrParam2))
                        {
                            recursion = int.Parse(StrParam2);
                            for (r = 0; r < recursion; r++)
                            {
                                tParams.Add("pSigma", StrParam1);
                                for (i = 0; i < DataToTransform.Count(); i++)
                                {
                                    //dataSet[i].TransformData(Transformation.GAUSS, tParams);
                                    DataToTransform[i].TransformData(Transformation.GAUSS, tParams);
                                }
                                tParams.Clear();
                            }
                        }
                        break;
                    case TransformType.TRF_MEAN:
                        StrParam1 = trfWorkerParamText[1];
                        StrParam2 = trfWorkerParamText[2];
                        StrParam3 = trfWorkerParamText[3];

                        //If input parameter is valid, perform the transformation
                        if (Helpers.ValidateNumeric(StrParam1) && Helpers.ValidateNumeric(StrParam2))
                        {
                            recursion = int.Parse(StrParam2);
                            for (r = 0; r < recursion; r++)
                            {
                                tParams.Add("Window", StrParam1);
                                for (i = 0; i < DataToTransform.Count(); i++)
                                {
                                    DataToTransform[i].TransformData(Transformation.MEAN, tParams);
                                }
                                tParams.Clear();
                            }
                        }

                        break;
                    case TransformType.TRF_NORM:
                        tParams.Clear();
                        for (i = 0; i < DataToTransform.Count(); i++)
                        {
                            DataToTransform[i].TransformData(Transformation.NORMALIZE, tParams);
                        }
                        break;

                    case TransformType.TRF_SHIFT_PPC:
                        StrParam1 = trfWorkerParamText[1];

                        if (lastSelectedTab >= 0 && lastSelectedTab < DataToTransform.Count() && Helpers.ValidateNumeric(StrParam1))
                        {
                            for (i = 0; i < statsMarketData.Constituents.Count; i++)
                            {
                                if(statsMarketData.Constituents[i].Name == DataToTransform[lastSelectedTab].Name)
                                {
                                    statsMarketData.Constituents[i].TrimDataLeft(int.Parse(StrParam1));
                                }
                                else
                                {
                                    statsMarketData.Constituents[i].TrimDataRight(int.Parse(StrParam1));
                                }
                            }
                        }
                        
                        break;

                    default:
                        break;
                }
            }

            SET_DATA_VALID();
        }

        private void dataTransformationThread_Completed(object sender, RunWorkerCompletedEventArgs e)
        {
            //Save the current sort (if any) in the correlation table
            if (lastSelectedTab > -1 && lastSelectedTab < CorrelationTableSortInstruction.Count)
            {
                CorrelationTableSortInstruction[lastSelectedTab] = tblVisualsTab1.GetDataViewSortInstruction();
            }

            //Bind Corresponding data to the StatTable
            statsMarketData.UpdateCorrelationCoefficients();
            BindCorrelationTable();

            //Re-apply the sorting instruction that was last applied to the current data
            if (tblVisualsTab1.Visible && CorrelationTableSortInstruction.Count > 0)
            {
                tblVisualsTab1.SetDataViewSortInstruction(CorrelationTableSortInstruction[tabVisualsData.SelectedIndex]);
            }

            //update cursor and re-load the chart
            this.Enabled = true;
            CLEAR_WAIT_CURSOR();

            this.ReloadChart();
            chartMain.Focus();
        }

        private void tsBtnUpdate_Click(object sender, EventArgs e)
        {
            AllTransformApplied = false;
            ApplyTransformation();
        }

        private void tsBtnTransformAll_Click(object sender, EventArgs e)
        {
            AllTransformApplied = true;
            ApplyTransformation();
        }

        private void tsBtnClear_Click(object sender, EventArgs e)
        {
            List<Equity> DataToClear;

            SET_WAIT_CURSOR();
            if (AllTransformApplied == true)
            {
                DataToClear = statsMarketData.Constituents;
            }
            else
            {
                DataToClear = dataSet;
            }

            for (int i = 0; i < DataToClear.Count(); i++)
            {
                DataToClear[i].ClearTransformation();
            }

            tsBtnUpdate.Enabled = false;
            tsBtnTransformAll.Enabled = false;

            tsComboTransformation.SelectedIndex = -1;
            setVisibleParams();
            AllTransformApplied = false;
            this.ReloadChart();
            CLEAR_WAIT_CURSOR();
        }

        private void setVisibleParams(String param1_lbl = "", String param2_lbl = "", String param3_lbl = "")
        {
            //Parameter 1
            if(param1_lbl == "")
            {
                tsParam1Lbl.Visible = false;
                tsParam1Txt.Visible = false;
                tsParam1Separator.Visible = false;
            }
            else
            {
                tsParam1Lbl.Visible = true;
                tsParam1Txt.Visible = true;
                tsParam1Separator.Visible = true;
                tsParam1Lbl.Text = param1_lbl;
            }

            //Parameter 2
            if (param2_lbl == "")
            {
                tsParam2Lbl.Visible = false;
                tsParam2Txt.Visible = false;
                tsParam2Separator.Visible = false;
            }
            else
            {
                tsParam2Lbl.Visible = true;
                tsParam2Txt.Visible = true;
                tsParam2Separator.Visible = true;
                tsParam2Lbl.Text = param2_lbl;
            }

            //Parameter 3
            if (param3_lbl == "")
            {
                tsParam3Lbl.Visible = false;
                tsParam3Txt.Visible = false;
                tsParam3Separator.Visible = false;
            }
            else
            {
                tsParam3Lbl.Visible = true;
                tsParam3Txt.Visible = true;
                tsParam3Separator.Visible = true;
                tsParam3Lbl.Text = param3_lbl;
            }

            //Clear parameter input text boxes
            tsParam1Txt.Text = "";
            tsParam2Txt.Text = "";
            tsParam3Txt.Text = "";
        }
        #endregion

        #region Technical Indicator Management Functions
        public void manageTechnicalIndicators()
        {
            int curr_index, NumTIsApplied;
            bool tiAreaEmpty = true;
            String[] SelectedTechnicalIndicators;
            Legend tiChartAreaLegend;
            Title tiChartAreaTitle;
            curr_index = tabVisualsData.SelectedIndex;

            SelectedTechnicalIndicators = ((VisualSummaryTabPage)tabVisualsData.TabPages[curr_index].Controls[0]).SelectedTechnicalIndicators;
            NumTIsApplied = SelectedTechnicalIndicators.Length;

            if (SelectedTechnicalIndicators.Aggregate((i, j) => i + j).ToString() != "")
            {
                //Add the chart area for the technical indicator series' if it doesn't already exist
                if (chartMain.ChartAreas.IndexOf(TechnicalIndicatorConst.TI_CHARTAREA_NAME) < 0)
                {
                    chartMain.ChartAreas.Add(new ChartArea(TechnicalIndicatorConst.TI_CHARTAREA_NAME));
                    tiChartAreaLegend = new Legend(TechnicalIndicatorConst.TI_CHARTLEGEND_NAME);
                    tiChartAreaLegend.DockedToChartArea = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
                    tiChartAreaLegend.IsDockedInsideChartArea = false;
                    tiChartAreaLegend.Docking = Docking.Bottom;
                    tiChartAreaLegend.Alignment = StringAlignment.Center;
                    chartMain.Legends.Add(tiChartAreaLegend);

                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorX.LineColor = Color.Black;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorX.LineWidth = 1;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorX.LineDashStyle = ChartDashStyle.Dot;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorX.Interval = 0;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorY.LineColor = Color.Black;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorY.LineWidth = 1;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorY.LineDashStyle = ChartDashStyle.Dot;
                    chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME].CursorY.Interval = 0;   

                    tiChartAreaTitle = new Title();
                    tiChartAreaTitle.Name = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
                    tiChartAreaTitle.DockedToChartArea = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
                    tiChartAreaTitle.IsDockedInsideChartArea = false;
                    tiChartAreaTitle.Docking = Docking.Top;
                    tiChartAreaTitle.Alignment = ContentAlignment.MiddleCenter;
                    chartMain.Titles.Add(tiChartAreaTitle);

                    //Manages Text value of each Chart Title
                    UpdateChartTitles();
                }
            }

            //If MACD is one of the selected technical indicator series to display
            if (SelectedTechnicalIndicators.Contains(TechnicalIndicatorConst.MACD))
            {
                if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD) < 0 || this.DataIsValid != 0)
                {
                    this.DisplaySelectedMACD(curr_index);
                }
            }
            else if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD) >= 0)
            {
                this.RemoveSelectedMACD();
            }

            //If Accumulation / Distribution is one of the selected technical indicator series to display
            if (SelectedTechnicalIndicators.Contains(TechnicalIndicatorConst.ACC_DIST))
            {
                if (chartMain.Series.IndexOf(TechnicalIndicatorConst.ACC_DIST) < 0 || this.DataIsValid != 0)
                {
                    this.DisplayAccumDistSeries(curr_index);
                }
            }
            else if (chartMain.Series.IndexOf(TechnicalIndicatorConst.ACC_DIST) >= 0)
            {
                this.RemoveAccumDistSeries();
            }

            //Check if any data series is bound to the technical indicators chart area
            foreach (Series srs in chartMain.Series)
            {
                if (srs.ChartArea == TechnicalIndicatorConst.TI_CHARTAREA_NAME)
                {
                    tiAreaEmpty = false;
                    break;
                }
            }

            //Remove empty chart area
            if (tiAreaEmpty == true)
            {
                ClearTechnicalIndicatorChart();
            }
        }

        private void ClearTechnicalIndicatorChart()
        {
            //Remove empty chart area
            if (chartMain.ChartAreas.IndexOf(TechnicalIndicatorConst.TI_CHARTAREA_NAME) >= 0)
            {
                this.RemoveAccumDistSeries();
                this.RemoveSelectedMACD();
                chartMain.Legends.Remove(chartMain.Legends[TechnicalIndicatorConst.TI_CHARTLEGEND_NAME]);
                chartMain.Titles.Remove(chartMain.Titles[TechnicalIndicatorConst.TI_CHARTAREA_NAME]);
                chartMain.ChartAreas.Remove(chartMain.ChartAreas[TechnicalIndicatorConst.TI_CHARTAREA_NAME]);
            }
        }
        #endregion

        #region MACD Add/Remove Functions
        private void DisplaySelectedMACD(int pIndex)
        {
            List<DateTime> x_axis;
            int skip, take;

            //Create the Series objects if they don't already exist
            if(chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD_DIF) < 0)
            {
                chartMain.Series.Add(TechnicalIndicatorConst.MACD_DIF);
            }
            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD) < 0)
            {
                chartMain.Series.Add(TechnicalIndicatorConst.MACD);
            }
            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD_SIG) < 0)
            {
                chartMain.Series.Add(TechnicalIndicatorConst.MACD_SIG);
            }

            //MACD Diff Series - first so that it shows behind the main lines
            skip = dataSet[pIndex].HistoricalPriceDate.Count - dataSet[pIndex].MACD_C.Count;
            take = dataSet[pIndex].MACD_C.Count;
            x_axis = new List<DateTime>(dataSet[pIndex].HistoricalPriceDate.Skip(skip).Take(take).ToList());
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].Color = System.Drawing.Color.DimGray;
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].BorderColor = System.Drawing.Color.White;
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].BorderWidth = 1;
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].ChartArea = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].Legend = TechnicalIndicatorConst.TI_CHARTLEGEND_NAME;
            chartMain.Series[TechnicalIndicatorConst.MACD_DIF].Points.DataBindXY(x_axis, dataSet[pIndex].MACD_C);

            //MACD 12/26 Series
            skip = dataSet[pIndex].HistoricalPriceDate.Count - dataSet[pIndex].MACD_A.Count;
            take = dataSet[pIndex].MACD_A.Count;
            x_axis = new List<DateTime>(dataSet[pIndex].HistoricalPriceDate.Skip(skip).Take(take).ToList());
            chartMain.Series[TechnicalIndicatorConst.MACD].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartMain.Series[TechnicalIndicatorConst.MACD].Color = System.Drawing.Color.Blue;
            chartMain.Series[TechnicalIndicatorConst.MACD].ChartArea = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
            chartMain.Series[TechnicalIndicatorConst.MACD].Legend = TechnicalIndicatorConst.TI_CHARTLEGEND_NAME;
            chartMain.Series[TechnicalIndicatorConst.MACD].Points.DataBindXY(x_axis, dataSet[pIndex].MACD_A);

            //MACD Signal Series
            skip = dataSet[pIndex].HistoricalPriceDate.Count - dataSet[pIndex].MACD_B.Count;
            take = dataSet[pIndex].MACD_B.Count;
            x_axis = new List<DateTime>(dataSet[pIndex].HistoricalPriceDate.Skip(skip).Take(take).ToList());
            chartMain.Series[TechnicalIndicatorConst.MACD_SIG].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartMain.Series[TechnicalIndicatorConst.MACD_SIG].Color = System.Drawing.Color.Red;
            chartMain.Series[TechnicalIndicatorConst.MACD_SIG].ChartArea = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
            chartMain.Series[TechnicalIndicatorConst.MACD_SIG].Legend = TechnicalIndicatorConst.TI_CHARTLEGEND_NAME;
            chartMain.Series[TechnicalIndicatorConst.MACD_SIG].Points.DataBindXY(x_axis, dataSet[pIndex].MACD_B);   
        }

        private void RemoveSelectedMACD()
        {
            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD) >= 0)
            {
                chartMain.Series.Remove(chartMain.Series[TechnicalIndicatorConst.MACD]);
            }

            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD_SIG) >= 0)
            {
                chartMain.Series.Remove(chartMain.Series[TechnicalIndicatorConst.MACD_SIG]);
            }

            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.MACD_DIF) >= 0)
            {
                chartMain.Series.Remove(chartMain.Series[TechnicalIndicatorConst.MACD_DIF]);
            }
        }
        #endregion

        #region Accum/Dist Add/Remove Functions

        private void DisplayAccumDistSeries(int pIndex)
        {
            List<DateTime> x_axis;
            int skip, take;

            //Create the Series object if it doesn't already exist
            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.ACC_DIST) < 0)
            {
                chartMain.Series.Add(TechnicalIndicatorConst.ACC_DIST);
            }

            //MACD Diff Series - first so that it shows behind the main lines
            skip = dataSet[pIndex].HistoricalPriceDate.Count - dataSet[pIndex].AccumDistrIndex.Count;
            take = dataSet[pIndex].AccumDistrIndex.Count;
            x_axis = new List<DateTime>(dataSet[pIndex].HistoricalPriceDate.Skip(skip).Take(take).ToList());
            chartMain.Series[TechnicalIndicatorConst.ACC_DIST].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chartMain.Series[TechnicalIndicatorConst.ACC_DIST].Color = System.Drawing.Color.Black;
            chartMain.Series[TechnicalIndicatorConst.ACC_DIST].ChartArea = TechnicalIndicatorConst.TI_CHARTAREA_NAME;
            chartMain.Series[TechnicalIndicatorConst.ACC_DIST].Legend = TechnicalIndicatorConst.TI_CHARTLEGEND_NAME;
            chartMain.Series[TechnicalIndicatorConst.ACC_DIST].YAxisType = AxisType.Secondary;
            chartMain.Series[TechnicalIndicatorConst.ACC_DIST].Points.DataBindXY(x_axis, dataSet[pIndex].AccumDistrIndex);
        }

        private void RemoveAccumDistSeries()
        {
            if (chartMain.Series.IndexOf(TechnicalIndicatorConst.ACC_DIST) >= 0)
            {
                chartMain.Series.Remove(chartMain.Series[TechnicalIndicatorConst.ACC_DIST]);
            }
        }

        #endregion

        
    }
}
