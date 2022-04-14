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

namespace MyMarketAnalyzer
{
    public partial class CustomChart : Chart
    {
        private DataPoint lastDataPoint = null;
        private Boolean lastDataPointIsHovered = false;
        private System.Windows.Forms.Cursor outCursor = null;
        private Boolean customToolTipEnabled = false;

        private Color ptColor;
        private MarkerStyle ptStyle;
        private Size ptSize;
        private int MarkerSize;
        private int caCount_Prev;
        private int seriesCount_Prev;
        private double[,] AxesRange = new double[2, 2];

        private Boolean firstMouseEntry = true;
        private Boolean _mousePressed = false;
        private int[] _lastPoint = new int[2];
        private int _csIndex;

        /*** PUBLIC PROPERTIES ***/
        public int CurrentSeriesIndex
        {
            get => _csIndex;
            set
            {
                _csIndex = value;
                SetVisibleCandlestick();
            }
        }

        public bool CandleStickEnabled{ get; set; }

        public DataPoint CurrentPoint { get; private set; }
        public int CurrentPointIndex { get; private set; }
        public double Zoom { get; private set; }
        public Boolean AllowZoom { get; set; }
            
        public CustomChart() : base()
        {
            InitializeComponent();
            InitOtherProperties();
        }

        private void InitOtherProperties()
        {
            MarkerSize = -1;
            Zoom = 0;
            caCount_Prev = 0;
            seriesCount_Prev = 0;
            AllowZoom = false;
            this.DataBindings.CollectionChanged += new CollectionChangeEventHandler(this.customChart_OnCollectionChanged);
        }

        /*********************************************************************
         * BEGIN CUSTOM EVENTS
         **********************************************************************/
        protected virtual void OnSelectedPointChanged(EventArgs e)
        {
            EventHandler handler = SelectedPointChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler SelectedPointChanged;
        /*********************************************************************
         * END CUSTOM EVENTS
         **********************************************************************/

        private void setAxisCursorStyle()
        {
            this.ChartAreas[0].CursorX.LineColor = Color.Black;
            this.ChartAreas[0].CursorX.LineWidth = 1;
            this.ChartAreas[0].CursorX.LineDashStyle = ChartDashStyle.Dot;
            this.ChartAreas[0].CursorX.Interval = 0;
            this.ChartAreas[0].CursorY.LineColor = Color.Black;
            this.ChartAreas[0].CursorY.LineWidth = 1;
            this.ChartAreas[0].CursorY.LineDashStyle = ChartDashStyle.Dot;
            this.ChartAreas[0].CursorY.Interval = 0;   
        }

        private void custom_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_mousePressed && Zoom > 1.0)
            {
                DragChart(e.X, e.Y);
            }
            MoveCursor(e.X, e.Y);

            _lastPoint[0] = e.X;
            _lastPoint[1] = e.Y;
        }

        private void DragChart(int X, int Y)
        {
            double Xchange = 0, Ychange = 0;

            //X an Y distance the mouse was dragged since the last time the event was processed
            Xchange = (double)(X - _lastPoint[0]) * (0.75 / Zoom);
            Ychange = (double)(Y - _lastPoint[1]) * (-0.05 / Zoom);

            //If the distance is positive, this means the mouse was dragged up or right.
            // In either case, this means we are moving towards the axis minimum (adjust displayed range in the -ve direction)
            // Otherwise, movement is towards the axis maximum (adjust displayed range in the +ve direction)
            if (Xchange > 0)
            {
                Xchange = ((this.ChartAreas[0].AxisX.Minimum - Xchange) < AxesRange[0, 0]) ?
                        (this.ChartAreas[0].AxisX.Minimum - AxesRange[0, 0]) : Xchange;
            }
            else
            {
                Xchange = ((this.ChartAreas[0].AxisX.Maximum - Xchange) > AxesRange[0, 1]) ?
                        (this.ChartAreas[0].AxisX.Maximum - AxesRange[0, 1]) : Xchange;
            }

            if (Ychange > 0)
            {
                Ychange = ((this.ChartAreas[0].AxisY.Minimum - Ychange) < AxesRange[1, 0]) ?
                        (this.ChartAreas[0].AxisY.Minimum - AxesRange[1, 0]) : Ychange;
            }
            else
            {
                Ychange = ((this.ChartAreas[0].AxisY.Maximum - Ychange) > AxesRange[1, 1]) ?
                        (this.ChartAreas[0].AxisY.Maximum - AxesRange[1, 1]) : Ychange;
            }

            //Adjust the axes ranges
            this.ChartAreas[0].AxisX.Minimum -= Xchange;
            this.ChartAreas[0].AxisX.Maximum -= Xchange;
            this.ChartAreas[0].AxisY.Minimum -= Ychange;
            this.ChartAreas[0].AxisY.Maximum -= Ychange;
        }

        private void MoveCursor(int X, int Y)
        {
            Point mousePoint;
            double yPosition = 0, xPosition = 0, chartAreaYSpan = 0;
            int seriesIndex = 0, csindex = 0;

            //Get the selected Series index based on the CurrentSeriesIndex (set externally)
            if (CurrentSeriesIndex >= 0 && CurrentSeriesIndex < this.Series.Count)
            {
                //Ignore Candlestick series
                while (csindex <= CurrentSeriesIndex)
                {
                    if (!Series[seriesIndex].Name.EndsWith("_CS"))
                    {
                        csindex++;
                    }
                    seriesIndex++;
                }
                seriesIndex--;
            }

            HitTestResult result = this.HitTest(X, Y);
            ChartArea chartMainArea;
            DataPoint point;
            double resolution = 1;

            if (this.Series.Count > 0)
            {
                if (firstMouseEntry == true)
                {
                    setAxisCursorStyle();
                    firstMouseEntry = false;
                }

                // Get the chart Area object
                chartMainArea = this.ChartAreas[0];

                //Set Y-axis cursor interval
                resolution = Helpers.MinimumDifference(this.Series[seriesIndex].Points.Select(pt => pt.YValues[0]).ToList());
                if (resolution > 0)
                {
                    chartMainArea.CursorY.Interval = resolution;
                }
                //Set X-axis cursor interval
                resolution = Helpers.MinimumDifference(this.Series[seriesIndex].Points.Select(pt => pt.XValue).ToList());
                if (resolution > 0)
                {
                    chartMainArea.CursorX.Interval = resolution;
                }

                if (this.Series[seriesIndex].ChartType == SeriesChartType.Line)
                {
                    //width of the inner chart area
                    double chartAreaWidth = chartMainArea.AxisX.ValueToPixelPosition(chartMainArea.AxisX.Maximum) -
                        chartMainArea.AxisX.ValueToPixelPosition(chartMainArea.AxisX.Minimum);

                    //height of the inner chart area
                    double chartAreaHeight = chartMainArea.AxisY.ValueToPixelPosition(chartMainArea.AxisY.Minimum) -
                        chartMainArea.AxisY.ValueToPixelPosition(chartMainArea.AxisY.Maximum);

                    //position (in pixels) of the cursor from the minimum inner chart x-position
                    double testposition = X - chartMainArea.AxisX.ValueToPixelPosition(chartMainArea.AxisX.Minimum);

                    if (this.Series.Count > 0)
                    {
                        //limit the cursor position to within the chart area
                        if (testposition < 0)
                        {
                            testposition = 0;
                        }
                        else if (testposition > chartAreaWidth)
                        {
                            testposition = chartAreaWidth;
                        }

                        double pt_index;
                        int x_index = (int)Math.Round(chartMainArea.AxisX.PixelPositionToValue(X));

                        //Gets the array index of the point (in the chart series) at the relative x-position of the cursor
                        //pt_index = (testposition / chartAreaWidth) * (double)this.Series[seriesIndex].Points.Count;
                        pt_index = this.Series[seriesIndex].Points.IndexOf(FindNearest(seriesIndex, x_index));

                        //make sure the array index is in bounds
                        if (pt_index >= (double)this.Series[seriesIndex].Points.Count)
                        {
                            pt_index = (double)this.Series[seriesIndex].Points.Count - 1;
                        }

                        if (pt_index >= 0)
                        {
                            //Set cursor to display as cross
                            if (this.Cursor != Cursors.WaitCursor)
                            {
                                this.Cursor = Cursors.Cross;
                            }

                            //Get the data point at the point index
                            point = this.Series[seriesIndex].Points[(int)pt_index];

                            if (lastDataPoint != null)
                            {
                                lastDataPoint.MarkerStyle = MarkerStyle.None;
                                if (point != lastDataPoint)
                                {
                                    CurrentPoint = point;
                                    CurrentPointIndex = (int)pt_index;
                                    OnSelectedPointChanged(EventArgs.Empty);
                                }
                            }

                            //set the last point
                            lastDataPoint = point;

                            //Highlight the point at the current x-position
                            point.BackSecondaryColor = Color.White;
                            point.BackHatchStyle = ChartHatchStyle.Percent25;

                            point.MarkerStyle = MarkerStyle.Diamond;
                            point.MarkerSize = 5;

                            //Get the y-position (in pixels) of the highlighted data point
                            chartAreaYSpan = chartMainArea.AxisY.Maximum - chartMainArea.AxisY.Minimum;
                            yPosition = chartMainArea.AxisY.ValueToPixelPosition(point.YValues[0]);

                            //xPosition = X;
                            xPosition = chartMainArea.AxisX.ValueToPixelPosition(point.XValue);
                        }
                    }
                }
                else if (this.Series[seriesIndex].ChartType == SeriesChartType.Point)
                {
                    //TBD
                }

                //xPosition = (double)X;
                //yPosition = (double)Y;
                result = this.HitTest((int)xPosition, (int)yPosition);

                if (result.PointIndex > 0)
                {
                    point = (DataPoint)this.Series[seriesIndex].Points[result.PointIndex];

                    if (lastDataPoint != null)
                    {
                        lastDataPoint.MarkerStyle = MarkerStyle.None;
                    }

                    //set the last point
                    lastDataPoint = point;
                    lastDataPointIsHovered = true;

                    //Highlight the point at the current x-position
                    point.BackSecondaryColor = Color.White;
                    point.BackHatchStyle = ChartHatchStyle.Percent25;

                    point.MarkerStyle = MarkerStyle.Diamond;
                    point.MarkerColor = Color.Black;
                    point.MarkerSize = 10;
                }
                else
                {
                    lastDataPointIsHovered = false;
                }

                // Move the crosshair cursor
                mousePoint = new Point((int)xPosition, (int)yPosition);

                chartMainArea.CursorX.SetCursorPixelPosition(mousePoint, true);
                chartMainArea.CursorY.SetCursorPixelPosition(mousePoint, true);
            }
        }

        private DataPoint FindNearest(int seriesIndex, int x_value)
        {
            DataPoint nearestPoint;
            List<DataPoint> pointList;
            int x_diff1, x_diff2;

            //pt_index = this.Series[seriesIndex].Points.IndexOf(this.Series[seriesIndex].Points.Where(p => p.XValue == x_index).First());
            pointList = this.Series[seriesIndex].Points.Where(p => p.XValue == x_value).ToList();
            if (pointList != null && pointList.Count() > 0)
            {
                nearestPoint = pointList[0];
            }
            else
            {
                pointList = new List<DataPoint>();
                pointList.Add(this.Series[seriesIndex].Points.Where(p => p.XValue > x_value).FirstOrDefault());
                pointList.Add(this.Series[seriesIndex].Points.Where(p => p.XValue < x_value).LastOrDefault());

                if(pointList[0] == null)
                {
                    nearestPoint = pointList[1];
                }
                else if(pointList[1] == null)
                {
                    nearestPoint = pointList[0];
                }
                else
                {
                    x_diff1 = Math.Abs(x_value - (int)pointList[0].XValue);
                    x_diff2 = Math.Abs(x_value - (int)pointList[1].XValue);
                    nearestPoint = (x_diff1 < x_diff2) ? pointList[0] : pointList[1];
                }
            }

            return nearestPoint;
        }

        private void custom_OnMouseLeave(object sender, EventArgs e)
        {
            this.Cursor = this.outCursor;
        }

        private void custom_OnMouseEnter(object sender, EventArgs e)
        {
            if (this.Cursor != Cursors.WaitCursor)
            {
                this.outCursor = this.Cursor;
            }
        }

        private void customchart_OnPaint(object sender, PaintEventArgs e)
        {
            //Set the initial grid lines when a chart area has been added
            if(this.ChartAreas.Count > caCount_Prev)
            {
                RedrawGridLines(caCount_Prev);
            }
            caCount_Prev = this.ChartAreas.Count;

            //If a series is added or removed, update the stored global axes min/max values
            if (this.Series.Count != seriesCount_Prev)
            {
                UpdateAxesRange();
                seriesCount_Prev = this.Series.Count;
            }
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateAxesRange
         *  Description:    Get the min/max X and Y value out of all Series currently 
         *                  bound to the chart
         *  Parameters:     None
         *****************************************************************************/
        private void UpdateAxesRange()
        {
            double x_min, x_max, y_min, y_max;

            for(int i = 0; i < this.Series.Count; i++)
            {
                //Only check the series which belong to the main chart area
                if(Series[i].ChartType != SeriesChartType.Candlestick && 
                    Series[i].ChartArea == this.ChartAreas[0].Name)
                {
                    //Get the range of X and Y values for the data series
                    x_min = Series[i].Points.First().XValue;
                    x_max = Series[i].Points.Last().XValue;
                    y_min = Series[i].Points.FindMinByValue().YValues[0];
                    y_max = Series[i].Points.FindMaxByValue().YValues[0];

                    //Set initial range
                    if(i == 0)
                    {   
                        AxesRange[0, 0] = x_min;
                        AxesRange[0, 1] = x_max;
                        AxesRange[1, 0] = y_min;
                        AxesRange[1, 1] = y_max;
                    }
                    else
                    {
                        //Update the global axes min/max values if necessary
                        AxesRange[0, 0] = (x_min < AxesRange[0, 0]) ? x_min : AxesRange[0, 0];
                        AxesRange[0, 1] = (x_max > AxesRange[0, 1]) ? x_max : AxesRange[0, 1];
                        AxesRange[1, 0] = (y_min < AxesRange[1, 0]) ? y_min : AxesRange[1, 0];
                        AxesRange[1, 1] = (y_max > AxesRange[1, 1]) ? y_max : AxesRange[1, 1];
                    }
                }
            }

            this.ChartAreas[0].AxisX.Minimum = AxesRange[0, 0];
            this.ChartAreas[0].AxisX.Maximum = AxesRange[0, 1];
            this.ChartAreas[0].AxisY.Minimum = AxesRange[1, 0];
            this.ChartAreas[0].AxisY.Maximum = AxesRange[1, 1];
        }

        /*****************************************************************************
         *  FUNCTION:  RedrawGridLines
         *  Description:    Re-draw the chart grid lines at a fixed interval (% of 
         *                  total chart area)
         *  Parameters:     
         *          pChartArea  - the chart area to update
         *          gridsize    - the interval as a % of the displayed range (default 5%)
         *****************************************************************************/
        private void RedrawGridLines(int pChartArea, double gridsize = 0.05)
        {
            if(pChartArea < this.ChartAreas.Count && Series.Count > 0)
            {
                double xRange = this.ChartAreas[pChartArea].AxisX.Maximum -
                    this.ChartAreas[pChartArea].AxisX.Minimum;

                double yRange = this.ChartAreas[pChartArea].AxisY.Maximum -
                    this.ChartAreas[pChartArea].AxisY.Minimum;

                this.ChartAreas[pChartArea].AxisX.MinorGrid.LineColor = Color.Gray;
                this.ChartAreas[pChartArea].AxisX.MinorGrid.LineDashStyle = ChartDashStyle.DashDot;
                this.ChartAreas[pChartArea].AxisX.MajorGrid.Enabled = false;
                this.ChartAreas[pChartArea].AxisX.MinorGrid.Enabled = true;
                this.ChartAreas[pChartArea].AxisX.MinorGrid.Interval = xRange * gridsize;

                this.ChartAreas[pChartArea].AxisY.MinorGrid.LineColor = Color.Gray;
                this.ChartAreas[pChartArea].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dash;
                this.ChartAreas[pChartArea].AxisY.MajorGrid.Enabled = false;
                this.ChartAreas[pChartArea].AxisY.MinorGrid.Enabled = true;
                this.ChartAreas[pChartArea].AxisY.MinorGrid.Interval = yRange * gridsize;
            }
        }

        private void customchart_PostPaint(object sender, ChartPaintEventArgs e)
        {
            int x, y;
            if (customToolTipEnabled == true && this.Series.Count > 0 && lastDataPointIsHovered == true)
            {
                x = (int)this.ChartAreas[0].AxisX.ValueToPixelPosition(lastDataPoint.XValue);
                y = (int)this.ChartAreas[0].AxisY.ValueToPixelPosition(lastDataPoint.YValues[0]);

                DrawCustomToolTip(new Point(x, y), e.ChartGraphics.Graphics);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  DrawCustomToolTip
         *  Description:    
         *                 
         *  Parameters:     
         *          pAtPt  - 
         *          pChartGraphics  - 
         *****************************************************************************/
        private void DrawCustomToolTip(Point pAtPt, Graphics pChartGraphics)
        {
            Pen lPen = new System.Drawing.Pen(System.Drawing.Color.Blue);
            Rectangle ttRect = new Rectangle(pAtPt.X, pAtPt.Y, 30, 25);

            pChartGraphics.DrawRectangle(lPen, ttRect);
            lPen.Dispose();
        }

        /*****************************************************************************
         *  FUNCTION:  SetChartPointSize
         *  Description:    
         *                 
         *  Parameters:     
         *          pSize  - 
         *****************************************************************************/
        public void SetChartPointSize(int pSize)
        {
            if(pSize > 0)
            {
                this.MarkerSize = pSize;
            }
        }

        private void customChart_OnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (this.Series != null && this.Series.Count > 0)
            {
                
            }
        }

        private void custom_OnMouseWheel(object sender, MouseEventArgs e)
        {
            int detent = e.Delta;
            int deltaZoom = (detent / Math.Abs(detent)) * 5;
            double x, y, pctX, pctY;
            double xRange, yRange;
            double xRange2, yRange2;
            double x1, x2, y1, y2;

            if (!AllowZoom) { return; }

            xRange = this.ChartAreas[0].AxisX.Maximum - this.ChartAreas[0].AxisX.Minimum;
            yRange = this.ChartAreas[0].AxisY.Maximum - this.ChartAreas[0].AxisY.Minimum;

            x = CurrentPoint.XValue;
            y = CurrentPoint.YValues[0];

            //if the point has scrolled off the screen, limit to the available chart area to prevent min/max inversion
            x = (x > this.ChartAreas[0].AxisX.Maximum) ? this.ChartAreas[0].AxisX.Maximum : x;
            x = (x < this.ChartAreas[0].AxisX.Minimum) ? this.ChartAreas[0].AxisX.Minimum : x;
            y = (y > this.ChartAreas[0].AxisY.Maximum) ? this.ChartAreas[0].AxisY.Maximum : y;
            y = (y < this.ChartAreas[0].AxisY.Minimum) ? this.ChartAreas[0].AxisY.Minimum : y;

            //data point to zoom in on, as a percentage of the visible axes
            pctX = (x - this.ChartAreas[0].AxisX.Minimum) / xRange;
            pctY = (y - this.ChartAreas[0].AxisY.Minimum) / yRange;

            //Get new size of the axes based on zoom %
            xRange2 = xRange * (1 - (deltaZoom / 100.0));
            yRange2 = yRange * (1 - (deltaZoom / 100.0));

            //Find new axes start and end points
            x2 = (xRange2 * (1 - pctX)) + x;
            x1 = x2 - xRange2;
            y2 = (yRange2 * (1 - pctY)) + y;
            y1 = y2 - yRange2;

            //Limit the window to the available data range
            x1 = (x1 < AxesRange[0, 0]) ? AxesRange[0, 0] : x1;
            x2 = (x2 > AxesRange[0, 1]) ? AxesRange[0, 1] : x2;
            y1 = (y1 < AxesRange[1, 0]) ? AxesRange[1, 0] : y1;
            y2 = (y2 > AxesRange[1, 1]) ? AxesRange[1, 1] : y2;

            //Re-size the axis
            this.ChartAreas[0].AxisX.Minimum = x1;
            this.ChartAreas[0].AxisX.Maximum = x2;
            this.ChartAreas[0].AxisY.Minimum = y1;
            this.ChartAreas[0].AxisY.Maximum = y2;

            Zoom = (AxesRange[0, 1] - AxesRange[0, 0]) / (x2 - x1);
            
            RedrawGridLines(0);
        }

        /*****************************************************************************
         *  FUNCTION:  CreateCandleStick
         *  Description:    Creates a new candlestick series with the data from the passed
         *                  Equity class and with the given name
         *  Parameters:     
         *          pEquity  - Object containing price data (high,low,open,close)
         *          pSeriesName - Name of the parent series
         *****************************************************************************/
        public void CreateCandleStick(Equity pEquity, String pSeriesName, Boolean pReplaceExisting = false)
        {
            String csName = "", seriesName = "";

            //Before attempting to add a new candlestick chart, first ensure any others are hidden
            // and remember the name of the last one that was shown
            foreach (Series cs in this.Series.ToList())
            {
                if (cs.ChartType == SeriesChartType.Candlestick)
                {
                    if (pReplaceExisting)
                    {
                        Series.Remove(cs);
                    }
                    else
                    {
                        if (cs.Enabled)
                        {
                            csName = cs.Name;
                        }
                        cs.Enabled = false;
                    }
                }
            }

            seriesName = pSeriesName + "_CS";
            if (!Series.IsUniqueName(seriesName)) { return; }

            Series.Add(seriesName);
            Series[seriesName].ChartType = SeriesChartType.Candlestick;

            try
            {
                if(this.Series[seriesName].ChartType == SeriesChartType.Candlestick)
                {
                    this.Series[seriesName].CustomProperties = "PriceDownColor=Red, PriceUpColor=Green";
                    this.Series[seriesName].YValuesPerPoint = 4;

                    for (int i = 0; i < pEquity.HistoricalPriceDate.Count; i++)
                    {
                        this.Series[seriesName].Points.AddXY(pEquity.HistoricalPriceDate[i],
                            new object[] { pEquity.HistoricalHighs[i], pEquity.HistoricalLows[i],
                                        pEquity.HistoricalOpens[i], pEquity.HistoricalPrice[i]});
                    }
                }
            }
            catch (Exception)
            {
                //If the new candlestick failed to generate, show the last one that was enabled
                if (csName != "")
                {
                    Series[csName].Enabled = true;
                }
            }

            UpdateAxesRange();
        }

        /*****************************************************************************
         *  FUNCTION:  SetVisibleCandlestick
         *  Description:    Enables the candlestick pattern for the currently selected 
         *                  series. 
         *  Parameters: None
         *****************************************************************************/
        private void SetVisibleCandlestick()
        {
            int csIndex = 0;

            foreach (Series cs in this.Series)
            {
                if (cs.ChartType == SeriesChartType.Candlestick)
                {
                    if (csIndex == CurrentSeriesIndex && this.CandleStickEnabled)
                    {
                        cs.Enabled = true;
                    }
                    else
                    {
                        cs.Enabled = false;
                    }
                    csIndex++;
                }
            }
        }

        /*****************************************************************************
         *  Mouse click event handlers
         *****************************************************************************/
        private void custom_OnMouseDown(object sender, MouseEventArgs e)
        {
            _mousePressed = true;
        }

        private void custom_OnMouseUp(object sender, MouseEventArgs e)
        {
            _mousePressed = false;
        }
    }
}
