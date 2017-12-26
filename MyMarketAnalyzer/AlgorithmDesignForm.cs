using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyMarketAnalyzer
{
    public enum DefaultAlgorithm
    {
        [StringValue("k-means")]
        K_MEANS = 0xAA,
        [StringValue("Naive Bayes")]
        N_BAYES = 0xAB,
        [StringValue("Fuzzy c-means")]
        FUZZY_C_MEANS = 0xAC
    }

    public partial class AlgorithmDesignForm : Form
    {
        private enum ClassificationKey
        {
            NULL = 0xFF,
            [StringValue("Price (Close)")]
            CLOSE = 0x10,
            [StringValue("Price (Open)")]
            OPEN = 0x11,
            [StringValue("Price (High)")]
            HIGH = 0x12,
            [StringValue("Price (Low)")]
            LOW = 0x13,
            [StringValue("% Change (Close)")]
            PCT_CHANGE = 0x14,
            [StringValue("% Change (Volume)")]
            VOLUME = 0x15
        }

        private enum PrototypeFunction
        {
            [StringValue("Average")]
            AVERAGE = 0x40,
            [StringValue("Variance")]
            VARIANCE = 0x41,
            [StringValue("Std. Deviation")]
            STD_DEV = 0x42,
            [StringValue("Daily")]
            DAILY = 0x43
        }

        private const int ONE_HUNDRED_PCT = 100;

        //List of available default algorithms
        private DefaultAlgorithm[] AlgSelectList = { 
                                                       DefaultAlgorithm.K_MEANS, DefaultAlgorithm.N_BAYES, DefaultAlgorithm.FUZZY_C_MEANS
                                                   };
        //List of Equity features
        private ClassificationKey[] ClassKeyList = {
                                                       ClassificationKey.CLOSE, ClassificationKey.HIGH, ClassificationKey.LOW,
                                                       ClassificationKey.OPEN, ClassificationKey.PCT_CHANGE, ClassificationKey.VOLUME
                                                   };
        //List of functions available to generate a single prototype from a list of data for each Equity
        private PrototypeFunction[] ClassFnList = {
                                                      PrototypeFunction.AVERAGE, PrototypeFunction.STD_DEV, PrototypeFunction.VARIANCE,
                                                      PrototypeFunction.DAILY
                                                  };

        private double[,] PopulationToClassify;
        private int[] PopulationClassLabels;

        DataManager DataSrc;

        private List<List<double>> daily_chart_x_values;
        private List<List<double>> daily_chart_y_values;

        private int ConsoleLineCount = 1;
        
        public AlgorithmDesignForm(ref DataManager data_src)
        {
            InitializeComponent();
            DataSrc = data_src;
            InitializeDefaultValues();
            LoadData();

            this.Width = 990;
        }

        /*****************************************************************************
         *  FUNCTION:       InitializeDefaultValues
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void InitializeDefaultValues()
        {
            int i;

            this.numTrainingPct.Value = this.trackBarTrainingPct.Value;
            this.numTestPct.Value = ONE_HUNDRED_PCT - this.numTrainingPct.Value;

            for(i = 0; i < AlgSelectList.Length; i++)
            {
                this.algorithmSelectListBox.Items.Add(StringEnum.GetStringValue(AlgSelectList[i]));
            }

            for(i = 0; i < ClassKeyList.Length; i++)
            {
                this.cbProtoAttributeX.Items.Add(StringEnum.GetStringValue(ClassKeyList[i]));
            }

            for (i = 0; i < ClassKeyList.Length; i++)
            {
                this.cbProtoAttributeY.Items.Add(StringEnum.GetStringValue(ClassKeyList[i]));
            }

            for (i = 0; i < ClassFnList.Length; i++)
            {
                this.cbProtoFunction.Items.Add(StringEnum.GetStringValue(ClassFnList[i]));
            }
        }

        /*****************************************************************************
         *  FUNCTION:       LoadData
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void LoadData()
        {
            string warning_txt = "";
            System.Drawing.Font promptFont;

            if (DataSrc.HistoricalData != null &&
                DataSrc.HistoricalData.Constituents.Count > 0)
            {
                this.dataTableMain.BindMarketData(DataSrc.HistoricalData);
                this.chartSlider.Maximum = this.DataSrc.HistoricalData.MaxDataPoints;
                this.consoleTxt.Clear();

                //Check if data contained in the bound DataSrc is aligned properly
                if(!DataSrc.HistoricalData.IsDataAligned)
                {
                    this.tabConsoleContainer.SelectedTab = this.tabPageConsole;
                    this.autofillCheckBox.Enabled = true;
                    
                    promptFont = new Font(this.consoleTxt.Font, FontStyle.Bold);
                    warning_txt = ">Warning: The loaded Equity data does not contain uniform date points, meaning that data is not correctly aligned. ";
                    this.consolePrompt(warning_txt, promptFont, Color.Red);

                    warning_txt = "Equities which do not contain data for all date values will be ignored from any daily analysis. " +
                        "To include those equities in the analysis anyway, select the 'Auto-Fill Date Gaps' checkbox. " +
                        "If this option is selected, data missing at the ends of a series will be filled in with the average over the existing data. " +
                        "Data missing in the middle of a series will be filled in with the average of the immediately preceding and succeeding points.";
                    promptFont = new Font(this.consoleTxt.Font, FontStyle.Regular);
                    this.consolePrompt(warning_txt, promptFont, Color.Black);
                }
                else
                {
                    this.autofillCheckBox.Checked = false;
                    this.autofillCheckBox.Enabled = false;
                }
            }
        }

        private void LockFormInputs()
        {
            this.panel1.Enabled = false;
        }

        private void UnlockFormInputs()
        {
            this.panel1.Enabled = true;
        }

        private void algDesignForm_FirstShown(object sender, EventArgs e)
        {
            //Ensure data is loaded
            if (DataSrc.HistoricalData != null && 
                this.dataTableMain.NumberOfEntries != DataSrc.HistoricalData.Constituents.Count &&
                DataSrc.HistoricalData.Constituents.Count > 0)
            {
                LoadData();
            }
            
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            int pct_value = trackBarTrainingPct.Value;
            this.numTrainingPct.Value = pct_value;
            this.numTestPct.Value = ONE_HUNDRED_PCT - pct_value;
        }

        private void numTrainingPct_ValueChanged(object sender, EventArgs e)
        {
            int pct_value = (int)numTrainingPct.Value;
            this.trackBarTrainingPct.Value = pct_value;
            this.numTestPct.Value = ONE_HUNDRED_PCT - pct_value;
        }

        private void numTestPct_ValueChanged(object sender, EventArgs e)
        {
            int pct_value = (int)numTestPct.Value;
            this.numTrainingPct.Value = ONE_HUNDRED_PCT - pct_value;
            this.trackBarTrainingPct.Value = ONE_HUNDRED_PCT - pct_value;
        }

        private void algorithm_SelectionChanged(object sender, EventArgs e)
        {
            ConfigureParameters(algorithmSelectListBox.SelectedIndex);
        }

        /*****************************************************************************
         *  FUNCTION:       ConfigureParameters
         *  Description:    
         *  Parameters:     
         *      pAlgListIndex   - 
         *****************************************************************************/
        private void ConfigureParameters(int pAlgListIndex)
        {
            this.gbParameters1.Visible = true;
            this.gbParameters2.Visible = false;
            this.gbParameters3.Visible = false;

            numGroupMin.Enabled = true;
            numGroupMax.Enabled = true;

            switch(AlgSelectList[pAlgListIndex])
            {
                case DefaultAlgorithm.K_MEANS:
                case DefaultAlgorithm.FUZZY_C_MEANS:
                    lblParam1.Text = "k value:";
                    numParam1.Minimum = 1;
                    numParam1.Maximum = 100;
                    lblParam1.Visible = true;
                    numParam1.Visible = true;

                    lblParam2.Visible = false;
                    numParam2.Visible = false;

                    lblParam3.Visible = false;
                    numParam3.Visible = false;

                    numGroupMin.Enabled = false;
                    numGroupMax.Enabled = false;
                    
                    break;
                default:
                    this.gbParameters1.Visible = false;
                    this.gbParameters2.Visible = false;
                    this.gbParameters3.Visible = false;
                    break;
            } 
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            double param1Value, param2Value, param3Value;
            double[,] classprototypes;
            int i, k;

            SET_WAIT_CURSOR();

            param1Value = (double)this.numParam1.Value;
            param2Value = (double)this.numParam2.Value;
            param3Value = (double)this.numParam3.Value;

            if (DataSrc.HistoricalData.Constituents.Count > 0)
            {
                //If the population data is successfully retrieved
                if(BuildPopulationMatrix())
                {
                    //Run selected Algorithm
                    switch (AlgSelectList[algorithmSelectListBox.SelectedIndex])
                    {
                        case DefaultAlgorithm.K_MEANS:
                            if (param1Value > 0)
                            {
                                //Define the initial class prototypes as random points within feature space defined by the population
                                // this function limits the points to within a box defined by the min and max in each dimension plus _%
                                classprototypes = Helpers.XRandPointsInSpace((int)param1Value, PopulationToClassify);
                                k = Algorithms.k_means(PopulationToClassify, classprototypes, (int)param1Value, 5000, out PopulationClassLabels);
                            }
                            BindChartData(PopulationToClassify, PopulationClassLabels);
                            InsertClassColumn(PopulationClassLabels);
                            break;

                        case DefaultAlgorithm.FUZZY_C_MEANS:
                            if (param1Value > 0)
                            {
                                //Define the initial class prototypes as random points within feature space defined by the population
                                // this function limits the points to within a box defined by the min and max in each dimension plus _%
                                classprototypes = Helpers.XRandPointsInSpace((int)param1Value, PopulationToClassify);
                                k = Algorithms.fuzzy_c_means(PopulationToClassify, classprototypes, (int)param1Value, 2, 5000, out PopulationClassLabels);
                            }
                            BindChartData(PopulationToClassify, PopulationClassLabels);
                            InsertClassColumn(PopulationClassLabels);
                            break;

                        default:
                            break;
                    }
                }
            }

            CLEAR_WAIT_CURSOR();
        }

        private void cbProtoAttribute_XIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbProtoAttribute_YIndexChanged(object sender, EventArgs e)
        {

        }

        private bool BuildPopulationMatrix()
        {
            bool success = true;
            int i, nSize;
            List<Double> eqMemberX, eqMemberY;
            ClassificationKey keyX = (cbProtoAttributeX.SelectedIndex < 0) ? ClassificationKey.NULL : ClassKeyList[cbProtoAttributeX.SelectedIndex];
            ClassificationKey keyY = (cbProtoAttributeY.SelectedIndex < 0) ? ClassificationKey.NULL : ClassKeyList[cbProtoAttributeY.SelectedIndex];

            nSize = DataSrc.HistoricalData.Constituents.Count;
            PopulationToClassify = new double[nSize, 2];

            eqMemberX = null;
            eqMemberY = null;

            if (keyX == keyY || keyX == ClassificationKey.NULL || keyY == ClassificationKey.NULL)
            {
                success = false;
            }
            else
            {
                for (i = 0; i < nSize; i++)
                {
                    eqMemberX = SelectDataComponent(keyX, DataSrc.HistoricalData.Constituents[i]);
                    eqMemberY = SelectDataComponent(keyY, DataSrc.HistoricalData.Constituents[i]);

                    if (eqMemberX != null && eqMemberY != null)
                    {
                        switch (ClassFnList[cbProtoFunction.SelectedIndex])
                        {
                            case PrototypeFunction.AVERAGE:
                                PopulationToClassify[i, 0] = eqMemberX.Average();
                                PopulationToClassify[i, 1] = eqMemberY.Average();
                                break;
                            case PrototypeFunction.STD_DEV:
                                PopulationToClassify[i, 0] = Helpers.StdDev(eqMemberX);
                                PopulationToClassify[i, 1] = Helpers.StdDev(eqMemberY);
                                break;
                            case PrototypeFunction.VARIANCE:
                            default:
                                break;
                        }
                    }
                    else
                    {
                        success = false;
                        break;
                    }
                }
            }

            return success;
        }

        private List<double> SelectDataComponent(ClassificationKey pKey, Equity pSrc)
        {
            List<double> return_list;

            switch (pKey)
            {
                case ClassificationKey.CLOSE:
                    return_list = pSrc.HistoricalPrice;
                    break;
                case ClassificationKey.HIGH:
                    return_list = pSrc.HistoricalHighs;
                    break;
                case ClassificationKey.LOW:
                    return_list = pSrc.HistoricalLows;
                    break;
                case ClassificationKey.OPEN:
                    return_list = pSrc.HistoricalOpens;
                    break;
                case ClassificationKey.PCT_CHANGE:
                    return_list = pSrc.HistoricalPctChange;
                    break;
                case ClassificationKey.VOLUME:
                    return_list = Algorithms.IncrementalPercentChange(pSrc.HistoricalVolumes, 0);
                    break;
                default:
                    return_list = new List<double>();
                    break;
            }

            return return_list;
        }

        private void InsertClassColumn(int[]classLabels)
        {
            List<int> UniqueClasses;
            int i;

            if(this.dataTableMain.NumberOfEntries > 0 &&
                this.DataSrc.HistoricalData.Constituents.Count > 0 &&
                classLabels.Length == this.dataTableMain.NumberOfEntries)
            {
                if(this.dataTableMain.WriteColumn<int>("Class_Label", classLabels.ToList(), 0) == false)
                {
                    MessageBox.Show("Class Insert Failed");
                }
                UniqueClasses = new HashSet<int>(classLabels).ToList();

                for(i = 0; i < UniqueClasses.Count(); i++)
                {
                    if (i < this.chartMain.Series.Count)
                    {
                        this.dataTableMain.HighlightData("Class_Label = " + UniqueClasses[i].ToString(), this.chartMain.Series[i].MarkerColor);
                    }
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       BindChartData
         *  Description:    
         *  Parameters:     None
         *****************************************************************************/
        private void BindChartData(double[,] dataIn, int[]classLabels)
        {
            List<double> x_values, y_values;
            List<int> classLabelsList;
            int nSize = dataIn.GetUpperBound(0) + 1;

            x_values = new List<double>();
            y_values = new List<double>();
            classLabelsList = new List<int>();

            if (nSize == classLabels.Length)
            {
                for (int i = 0; i < nSize; i++)
                {
                    x_values.Add(dataIn[i, 0]);
                    y_values.Add(dataIn[i, 1]);
                    classLabelsList.Add(classLabels[i]);
                }

                BindChartData(x_values, y_values, classLabelsList);
            }
        }

        private void BindChartData(List<double> x_values, List<double> y_values, List<int> classLabels)
        {
            List<List<double>> XDataByClass, YDataByClass;
            int num_classes, i;
            string clbl_txt;
            Random randGen = new Random();

            if (x_values.Count == y_values.Count && x_values.Count == classLabels.Count)
            {
                ClearChart();

                num_classes = classLabels.Max() + 1;
                XDataByClass = new List<List<double>>();
                YDataByClass = new List<List<double>>();

                for (i = 0; i < num_classes; i++)
                {
                    XDataByClass.Add(new List<double>());
                    YDataByClass.Add(new List<double>());
                }

                for (i = 0; i < x_values.Count; i++)
                {
                    XDataByClass[classLabels[i]].Add(x_values[i]);
                    YDataByClass[classLabels[i]].Add(y_values[i]);
                }

                //Bind data to chart
                for (i = 0; i < num_classes; i++)
                {
                    clbl_txt = "Class_" + (i + 1).ToString();
                    chartMain.Series.Add(clbl_txt);

                    chartMain.Series[clbl_txt].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
                    chartMain.Series[clbl_txt]["IsXAxisQuantitative"] = "true";
                    chartMain.Series[clbl_txt].Points.Clear();
                    chartMain.Series[clbl_txt].Points.DataBindXY(XDataByClass[i], YDataByClass[i]);
                    chartMain.Series[clbl_txt].MarkerStyle = MarkerStyle.Circle + (i % 10);
                    chartMain.Series[clbl_txt].MarkerSize = 10;
                    chartMain.Series[clbl_txt].MarkerColor = Color.FromArgb(randGen.Next(100, 200), randGen.Next(100, 200), randGen.Next(100, 200));
                }

                //Set chart axes labels
                if (cbProtoAttributeX.SelectedIndex >= 0)
                {
                    chartMain.ChartAreas[0].AxisX.Title = StringEnum.GetStringValue(ClassKeyList[cbProtoAttributeX.SelectedIndex]);
                }
                if (cbProtoAttributeY.SelectedIndex >= 0)
                {
                    chartMain.ChartAreas[0].AxisY.Title = StringEnum.GetStringValue(ClassKeyList[cbProtoAttributeY.SelectedIndex]);
                }
                
            }
            chartMain.Invalidate();
        }

        private void ClearChart()
        {
            chartMain.Series.Clear();
        }

        private void SET_WAIT_CURSOR(bool center = false)
        {
            //if (center == true)
            //{
            //    System.Windows.Forms.Cursor.Position = new Point(this.Width / 2, this.Height / 2);
            //}
            this.Cursor = Cursors.WaitCursor;
            this.chartMain.Cursor = Cursors.WaitCursor;
        }

        private void CLEAR_WAIT_CURSOR()
        {
            this.Cursor = Cursors.Default;
            this.chartMain.Cursor = Cursors.Default;
        }

        public void AddToChart(List<double> x_values, List<double> y_values, List<int> classLabels)
        {

        }

        private void chartSlider_Scroll(object sender, EventArgs e)
        {
            ProcessChartSliderChange();
        }

        private void btnChartSliderLeft_Click(object sender, EventArgs e)
        {
            if (this.chartSlider.Value > 0)
            {
                this.chartSlider.Value--;
            }
            ProcessChartSliderChange();
        }

        private void btnChartSliderRight_Click(object sender, EventArgs e)
        {
            if (this.chartSlider.Value < this.chartSlider.Maximum)
            {
                this.chartSlider.Value++;
            }
            ProcessChartSliderChange();
        }

        private void ProcessChartSliderChange()
        {
            int index = this.chartSlider.Value;
            int n = this.DataSrc.HistoricalData.Constituents.Count;

            if (index >= 0 && index < this.daily_chart_x_values.Count)
            {
                BindChartData(this.daily_chart_x_values[index], this.daily_chart_y_values[index], new List<int>(new int[n]));
            }
        }

        private void cbProtoFunction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(this.DataSrc != null && 
                this.DataSrc.HistoricalData.Constituents.Count > 0 &&
                this.cbProtoFunction.SelectedItem.ToString() == StringEnum.GetStringValue(PrototypeFunction.DAILY))
            {
                BuildMovingChart();
            }
        }

        private void BuildMovingChart()
        {
            int i, n;
            List<double> x_seed;
            List<double> y_seed;

            ClassificationKey keyX = (cbProtoAttributeX.SelectedIndex < 0) ? ClassificationKey.NULL : ClassKeyList[cbProtoAttributeX.SelectedIndex];
            ClassificationKey keyY = (cbProtoAttributeY.SelectedIndex < 0) ? ClassificationKey.NULL : ClassKeyList[cbProtoAttributeY.SelectedIndex];

            this.daily_chart_x_values = new List<List<double>>();
            this.daily_chart_y_values = new List<List<double>>();

            n = this.DataSrc.HistoricalData.Constituents.Count;
            x_seed = new List<double>(new double[n]);
            this.daily_chart_x_values.Add(x_seed);

            //If no feature is selected for the y-axis, treat the data as 1 dimensional
            // To do this, each X-point for a particular equity will be placed at a unique but constant y-value
            if (this.cbProtoAttributeY.SelectedIndex < 0)
            {
                y_seed = Enumerable.Range(1, n).Select(x => (double)x).ToList();
            }
            else
            {
                //Otherwise, start all data points at (0, 0)
                y_seed = new List<double>(new double[n]);
            }
            this.daily_chart_y_values.Add(y_seed);

            //Problem: not guaranteed that the date associated with a data-point-index is the same accross the entire array of Constituents. Need to align first.

            //Create each subsequent series by adding the selected x attribute at the given index to it's previous value for each equity in the list
            for (i = 0; i < this.DataSrc.HistoricalData.Constituents[0].HistoricalPriceDate.Count; i++)
            {
                switch(keyX)
                {
                    case ClassificationKey.PCT_CHANGE:
                        x_seed = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalPctChange[i]).ToList();
                        break;
                    case ClassificationKey.VOLUME:
                        x_seed = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalVolumes[i]).ToList();
                        break;
                    default:
                        x_seed = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalPctChange[i]).ToList();
                        break;
                }

                //If no feature is selected for the y-axis, treat the data as 1 dimensional
                // To do this, each X-point for a particular equity will be placed at a unique but constant y-value
                if (this.cbProtoAttributeY.SelectedIndex < 0)
                {
                    y_seed = Enumerable.Range(1, n).Select(x => (double)x).ToList();
                    this.daily_chart_y_values.Add(y_seed);
                }
                else
                {
                    //Otherwise increment the data from the center point
                    switch (keyY)
                    {
                        case ClassificationKey.PCT_CHANGE:
                            y_seed = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalPctChange[i]).ToList();
                            break;
                        case ClassificationKey.VOLUME:
                            y_seed = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalVolumes[i]).ToList();
                            break;
                        default:
                            y_seed = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalPctChange[i]).ToList();
                            break;
                    }
                    this.daily_chart_y_values.Add(this.daily_chart_y_values[i].Zip(y_seed, (x, y) => x + y).ToList());
                }

                this.daily_chart_x_values.Add(this.daily_chart_x_values[i].Zip(x_seed, (x, y) => x + y).ToList());
            }

            this.chartSlider.Maximum = this.DataSrc.HistoricalData.Constituents.Select(x => x.HistoricalPriceDate.Count).Max();
            BindChartData(this.daily_chart_x_values[0], this.daily_chart_y_values[0], new List<int>(new int[n]));
        }

        /*****************************************************************************
         *  Function:       consolePrompt
         *  Description:    Displays prompt-text in the console rich text box with 
         *                  specified font and colour
         *  Parameters:     
         *****************************************************************************/
        private void consolePrompt(String pPromptText, Font pFont, Color pForeColour)
        {
            int start_index = this.consoleTxt.TextLength;
            this.consoleTxt.AppendText(pPromptText);

            this.consoleTxt.Select(start_index, pPromptText.Length);
            this.consoleTxt.SelectionColor = pForeColour;
            this.consoleTxt.SelectionFont = pFont;
            this.consoleTxt.Select();

            this.consoleTxt.AppendText(Environment.NewLine);
        }

        /*****************************************************************************
         *  EVENT HANDLER:  console_TextChanged
         *  Description:    Inserts a '>' character at the beginning of each new line
         *  Parameters:     
         *****************************************************************************/
        private void console_TextChanged(object sender, EventArgs e)
        {
            if(this.consoleTxt.Lines.Count() > this.ConsoleLineCount)
            {
                this.consoleTxt.AppendText(">");
                this.ConsoleLineCount = this.consoleTxt.Lines.Count();
            }
        }

    }
}
