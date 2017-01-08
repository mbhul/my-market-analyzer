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
            STD_DEV = 0x42
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
                                                      PrototypeFunction.AVERAGE, PrototypeFunction.STD_DEV, PrototypeFunction.VARIANCE
                                                  };

        private double[,] PopulationToClassify;
        private int[] PopulationClassLabels;

        DataManager DataSrc;
        
        public AlgorithmDesignForm(ref DataManager data_src)
        {
            InitializeComponent();
            DataSrc = data_src;
            InitializeDefaultValues();
            LoadData();
        }

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

        private void LoadData()
        {
            if (DataSrc.HistoricalData != null &&
                DataSrc.HistoricalData.Constituents.Count > 0)
            {
                this.dataTableMain.BindMarketData(DataSrc.HistoricalData);
            }
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

        private void ConfigureParameters(int pAlgListIndex)
        {
            this.gbParameters.Visible = true;
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
                    this.gbParameters.Visible = false;
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
                    switch (keyX)
                    {
                        case ClassificationKey.CLOSE:
                            eqMemberX = DataSrc.HistoricalData.Constituents[i].HistoricalPrice;
                            break;
                        case ClassificationKey.HIGH:
                            eqMemberX = DataSrc.HistoricalData.Constituents[i].HistoricalHighs;
                            break;
                        case ClassificationKey.LOW:
                            eqMemberX = DataSrc.HistoricalData.Constituents[i].HistoricalLows;
                            break;
                        case ClassificationKey.OPEN:
                            eqMemberX = DataSrc.HistoricalData.Constituents[i].HistoricalOpens;
                            break;
                        case ClassificationKey.PCT_CHANGE:
                            eqMemberX = DataSrc.HistoricalData.Constituents[i].HistoricalPctChange;
                            break;
                        case ClassificationKey.VOLUME:
                            eqMemberX = Algorithms.IncrementalPercentChange(DataSrc.HistoricalData.Constituents[i].HistoricalVolumes, 0);
                            break;
                        default:
                            break;
                    }

                    switch (keyY)
                    {
                        case ClassificationKey.CLOSE:
                            eqMemberY = DataSrc.HistoricalData.Constituents[i].HistoricalPrice;
                            break;
                        case ClassificationKey.HIGH:
                            eqMemberY = DataSrc.HistoricalData.Constituents[i].HistoricalHighs;
                            break;
                        case ClassificationKey.LOW:
                            eqMemberY = DataSrc.HistoricalData.Constituents[i].HistoricalLows;
                            break;
                        case ClassificationKey.OPEN:
                            eqMemberY = DataSrc.HistoricalData.Constituents[i].HistoricalOpens;
                            break;
                        case ClassificationKey.PCT_CHANGE:
                            eqMemberY = DataSrc.HistoricalData.Constituents[i].HistoricalPctChange;
                            break;
                        case ClassificationKey.VOLUME:
                            eqMemberY = Algorithms.IncrementalPercentChange(DataSrc.HistoricalData.Constituents[i].HistoricalVolumes, 0);
                            break;
                        default:
                            break;
                    }

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



    }
}
