using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MyMarketAnalyzer
{
    public partial class VisualSummaryTabPage : UserControl
    {
        private enum TI_Series_Action
        {
            ADD = 0,
            REMOVE = 1
        }

        //***** CONSTANTS *****//
        private const UInt32 WM_SHOWBTNCLICK = 0xA002;
        private const UInt32 WM_SHOWNEWWINDCLICK = 0xA003;
        private const UInt32 WM_PROCESS_TI = 0xA004;

        private const string LBL_PCTCHANGE = "Daily % Change: ";
        private const string LBL_CLOSE = "Closing Price: ";
        private const string LBL_VOLUME = "Volume: ";
        private const string LBL_LIVEPRICE = "Live Price: ";
        private const string LBL_TIME = "Time: ";

        //***** DATA MEMBERS *****//
        private int BtnShowState = 0;
        private Equity dataSet;
        private String[] TechnicalIndicatorStrings = { TechnicalIndicatorConst.MACD, TechnicalIndicatorConst.ACC_DIST };
        private Boolean[] TechnicalIndicatorsUsed = { false, false };
        private Boolean[] TISeriesAdded = { false, false, false };
        private String[] TISeriesSelections = { "", "", "" };
        private UInt16 TIAnchorIndex;
        private DisplayedDataSet displayed_data = DisplayedDataSet.NONE;

        public bool CandlestickChecked { get; private set; }

        /*****************************************************************************
         *  CONSTRUCTOR:  VisualSummaryTabPage
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public VisualSummaryTabPage()
        {
            InitializeComponent();
            dataSet = new Equity();
            InitTechnicalIndicators();
        }

        /*****************************************************************************
         *  CONSTRUCTOR:  VisualSummaryTabPage
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public VisualSummaryTabPage(Equity pData)
        {
            InitializeComponent();
            dataSet = pData;
            UpdateCurrentPoint(0);
            InitTechnicalIndicators();
            SetEnabledDataCheckboxes();
        }

        /*****************************************************************************
         *  FUNCTION:  InitTechnicalIndicators
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void InitTechnicalIndicators()
        {
            TIAnchorIndex = 0;

            comboTi1.Items.Clear();
            comboTi2.Items.Clear();
            comboTi3.Items.Clear();

            for(int i = 0; i < TechnicalIndicatorStrings.Length; i++)
            {
                comboTi1.Items.Add(TechnicalIndicatorStrings[i]);
                comboTi2.Items.Add(TechnicalIndicatorStrings[i]);
                comboTi3.Items.Add(TechnicalIndicatorStrings[i]);
            }
        }

        #region Public Properties
        public String[] SelectedTechnicalIndicators
        {
            get { return TISeriesSelections; }
        }

        public DisplayedDataSet DisplayedData
        {
            get { return this.displayed_data; }
        }
        #endregion

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
                          IntPtr hWnd,      // handle to destination window
                          UInt32 Msg,       // message
                          IntPtr wParam,  // first message parameter
                          IntPtr lParam   // second message parameter
                          );

        /*****************************************************************************
         *  EVENT HANDLER:  btnShowCorrelation_Click
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void btnShowCorrelation_Click(object sender, EventArgs e)
        {
            BtnShowState = Math.Abs(BtnShowState - 1);
            SendMessage(Application.OpenForms[0].Handle, WM_SHOWBTNCLICK, (IntPtr)BtnShowState, IntPtr.Zero);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  btnNewWind_Click
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void btnNewWind_Click(object sender, EventArgs e)
        {
            SendMessage(Application.OpenForms[0].Handle, WM_SHOWNEWWINDCLICK, IntPtr.Zero, IntPtr.Zero);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  visualsummarypg_Layout
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void visualsummarypg_Layout(object sender, LayoutEventArgs e)
        {

        }

        /*****************************************************************************
         *  EVENT HANDLER:  vssummaryPg_Validated
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void vssummaryPg_Validated(object sender, EventArgs e)
        {

        }

        /*****************************************************************************
         *  FUNCTION:  BindDataSet
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void BindDataSet(Equity pData)
        {
            if(pData != null)
            {
                dataSet = pData;
                SetEnabledDataCheckboxes();
                UpdateCurrentPoint(0);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateCurrentPoint
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void UpdateCurrentPoint(int pIndex)
        {
            if (displayed_data == DisplayedDataSet.HISTORICAL)
            {
                if (pIndex >= 0 && pIndex < dataSet.HistoricalPrice.Count)
                {
                    lblAvgPrice.Text = LBL_CLOSE + dataSet.HistoricalPrice[pIndex].ToString();
                    lblAvgChange.Text = LBL_PCTCHANGE + dataSet.HistoricalPctChange[pIndex].ToString();
                    lblAvgVolume.Text = LBL_VOLUME + dataSet.HistoricalVolumes[pIndex].ToString();

                    UpdateTechnicalIndicatorLabels();
                }
            }
            else if (displayed_data == DisplayedDataSet.LIVE)
            {
                if (pIndex >= 0 && pIndex < dataSet.DailyLast.Count)
                {
                    lblAvgPrice.Text = LBL_LIVEPRICE + dataSet.DailyLast[pIndex].ToString();
                    lblAvgChange.Text = LBL_TIME + dataSet.DailyTime[pIndex].ToString();
                    lblAvgVolume.Text = "";

                    UpdateTechnicalIndicatorLabels();
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateTechnicalIndicatorLabels
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void UpdateTechnicalIndicatorLabels()
        {
            double volume;
            string volUnit = "";

            volume = dataSet.avgDailyVolume;
            if (volume > 1000000.0)
            {
                volume /= 1000000.0;
                volume = Math.Round(volume, 3);
                volUnit = "M";
            }

            lblVariable1.Text = "Price Range: " + dataSet.HistoricalLows.Min().ToString() +
                " - " + dataSet.HistoricalHighs.Max().ToString();

            lblVariable2.Text = "Avg Volume: " + volume.ToString() + volUnit;
            lblVariable3.Text = "Volatility (σ): " + dataSet.Volatility.ToString();

            lblVariable1.Visible = true;
            lblVariable2.Visible = true;
            lblVariable3.Visible = true;
            lblVariable4.Visible = false;
            lblVariable5.Visible = false;
            lblVariable6.Visible = false;
        }

        private void btnTiAdd1_Click(object sender, EventArgs e)
        {
            manageTISelections(0);
        }

        private void btnTiAdd2_Click(object sender, EventArgs e)
        {
            manageTISelections(1);
        }

        private void btnTiAdd3_Click(object sender, EventArgs e)
        {
            manageTISelections(2);
        }

        /*****************************************************************************
         *  FUNCTION:  manageTISelections
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void manageTISelections(UInt16 pIndexSender)
        {
            int i, j, selIndex;
            bool makevisible;
            Button[] tiButtons = { btnTiAdd1, btnTiAdd2, btnTiAdd3 };
            ComboBox[] tiCombos = { comboTi1, comboTi2, comboTi3 };
            String tempSelection;

            //Ensure sender index is valid
            if (pIndexSender <= TISeriesSelections.Length && TISeriesSelections.Length == TISeriesAdded.Length &&
                TechnicalIndicatorsUsed.Length == TechnicalIndicatorStrings.Length)
            {
                TISeriesAdded[pIndexSender] = !TISeriesAdded[pIndexSender];             //Invert the bool that dictates whether the selection has been 'added' or 'removed'
                tempSelection = TISeriesSelections[pIndexSender];                       //Get the selected text of the corresponding combo box
                selIndex = TechnicalIndicatorStrings.ToList().IndexOf(tempSelection);   //Get the index of the selected text in the list of possible technical indicator strings

                //If the selection is being 'removed'
                if (TISeriesAdded[pIndexSender] == false)
                {
                    //Clear the tracked selected text for the item
                    TISeriesSelections[pIndexSender] = "";

                    //Shift any existing selections below the one being removed to fill the blank space left by the removal
                    for (i = pIndexSender; i < TISeriesAdded.Length - 1; i++)
                    {
                        TISeriesSelections[i] = TISeriesSelections[i + 1];
                        TISeriesAdded[i] = TISeriesAdded[i + 1];
                    }

                    //Add the removed text back to the end of the series (ie. the last combo box)
                    TISeriesSelections[TISeriesSelections.Length - 1] = "";
                    TISeriesAdded[TISeriesAdded.Length - 1] = false;

                    //Indicate that the technical indicator (removed text) is again available for assignment
                    TechnicalIndicatorsUsed[selIndex] = false;
                }
                else
                {
                    //Indicate that the technical indicator (selected text) has been claimed 
                    // (the control won't allow the same text to be selected by more than one of the combo boxes)
                    TechnicalIndicatorsUsed[selIndex] = true;
                }

                //Manage the visibility of the buttons and combo boxes
                makevisible = true;

                //Add all of the defined technical indicator strings to each combo box 
                // (the following process will remove the ones that are claimed, except from the combo box that has claimed it)
                InitTechnicalIndicators();
                for (i = 0; i < TISeriesSelections.Length; i++)
                {
                    if (TISeriesSelections[i] == "")
                    {
                        tiCombos[i].SelectedIndex = -1;
                    }
                    else
                    {
                        tiCombos[i].SelectedItem = TISeriesSelections[i];
                    }

                    //Set the visibility of the particular button/combo box
                    tiCombos[i].Visible = makevisible;
                    tiButtons[i].Visible = makevisible;

                    //Set the visibility of the next row (if the current selection is being 'added' then the next row is made visible)
                    makevisible = (makevisible && TISeriesAdded[i]);

                    //Set the button text based on whether the corresponding combo box selection has been locked in
                    if (makevisible == true)
                    {
                        tiButtons[i].Text = "Remove";
                        tiCombos[i].Enabled = false;
                    }
                    else
                    {
                        tiButtons[i].Text = "Add";
                        tiCombos[i].Enabled = true;
                        if (TISeriesSelections[i] == "")
                        {
                            tiButtons[i].Enabled = false;
                        }
                    }

                    //Remove technical indicator strings that are locked in, from all other combo boxes other than the one that has claimed it
                    for (j = 0; j < TechnicalIndicatorsUsed.Length; j++)
                    {
                        if (TISeriesSelections[i] != TechnicalIndicatorStrings[j] &&
                            TechnicalIndicatorsUsed[j] == true)
                        {
                            tiCombos[i].Items.Remove(TechnicalIndicatorStrings[j]);
                        }
                    }
                }

                //Send Windows Message to the main form to invoke the method that displays the technical indicator on the chart
                SendMessage(Application.OpenForms[0].Handle, WM_PROCESS_TI, IntPtr.Zero, IntPtr.Zero);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  SetEnabledDataCheckboxes
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void SetEnabledDataCheckboxes()
        {
            if (this.dataSet != null)
            {
                if(dataSet.ContainsHistData)
                {
                    this.chkHistData.Enabled = true;
                    this.chkHistData.Checked = true;
                }
                else
                {
                    this.chkHistData.Enabled = false;
                    this.chkHistData.Checked = false;
                }

                if(dataSet.ContainsLiveData)
                {
                    this.chkLiveData.Enabled = true;
                    this.chkLiveData.Checked = true;
                }
                else
                {
                    this.chkLiveData.Enabled = false;
                    this.chkLiveData.Checked = false;
                }
            }
        }

        private void comboTi1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTiAdd1.Enabled = true;
            TISeriesSelections[0] = (string)comboTi1.SelectedItem; 
        }

        private void comboTi2_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTiAdd2.Enabled = true;
            TISeriesSelections[1] = (string)comboTi2.SelectedItem; 
        }

        private void comboTi3_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTiAdd3.Enabled = true;
            TISeriesSelections[2] = (string)comboTi3.SelectedItem;
        }

        private void chkLiveData_CheckedChanged(object sender, EventArgs e)
        {
            SetDisplayedDataSet();
        }

        private void chkHistData_CheckedChanged(object sender, EventArgs e)
        {
            SetDisplayedDataSet();
        }

        private void SetDisplayedDataSet()
        {
            if(this.chkLiveData.Checked && this.chkHistData.Checked)
            {
                this.displayed_data = DisplayedDataSet.BOTH;
            }
            else if (this.chkLiveData.Checked)
            {
                this.displayed_data = DisplayedDataSet.LIVE;
            }
            else if (this.chkHistData.Checked)
            {
                this.displayed_data = DisplayedDataSet.HISTORICAL;
            }
            else
            {
                this.displayed_data = DisplayedDataSet.NONE;
            }
        }

        private void ChkCandlestick_CheckedChanged(object sender, EventArgs e)
        {
            this.CandlestickChecked = this.chkCandlestick.Checked;
            this.Invalidate();
        }
    }
}
