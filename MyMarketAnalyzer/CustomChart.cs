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
        private DataPoint lastPoint = null;
        private Boolean lastPointIsHovered = false;
        private System.Windows.Forms.Cursor outCursor = null;
        private Boolean customToolTipEnabled = false;

        private Color ptColor;
        private MarkerStyle ptStyle;
        private Size ptSize;
        private int MarkerSize;

        private Boolean firstMouseEntry = true;

        /*** PUBLIC PROPERTIES ***/
        public int CurrentSeriesIndex { get; set; }
        public DataPoint CurrentPoint { get; private set; }
        public int CurrentPointIndex { get; private set; }
            
        public CustomChart() : base()
        {
            InitializeComponent();
            InitOtherProperties();
        }

        private void InitOtherProperties()
        {
            MarkerSize = -1;
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
            Point mousePoint;
            double yPosition = 0, xPosition = 0, chartAreaYSpan = 0;
            int seriesIndex = 0;

            if(CurrentSeriesIndex >= 0 && CurrentSeriesIndex < this.Series.Count)
            {
                seriesIndex = CurrentSeriesIndex;
            }
            
            // Call HitTest
            HitTestResult result = this.HitTest(e.X, e.Y);
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
                    double testposition = e.X - chartMainArea.AxisX.ValueToPixelPosition(chartMainArea.AxisX.Minimum);

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

                        //Gets the array index of the point (in the chart series) at the relative x-position of the cursor
                        double pt_index = (testposition / chartAreaWidth) * (double)this.Series[seriesIndex].Points.Count;

                        //make sure the array index is in bounds
                        if (pt_index >= (double)this.Series[seriesIndex].Points.Count)
                        {
                            pt_index = (double)this.Series[seriesIndex].Points.Count - 1;
                        }

                        if(pt_index >= 0)
                        {
                            //Set cursor to display as cross
                            if (this.Cursor != Cursors.WaitCursor)
                            {
                                this.Cursor = Cursors.Cross;
                            }

                            //Get the data point at the point index
                            point = this.Series[seriesIndex].Points[(int)pt_index];

                            if (lastPoint != null)
                            {
                                lastPoint.MarkerStyle = MarkerStyle.None;
                                if (point != lastPoint)
                                {
                                    CurrentPoint = point;
                                    CurrentPointIndex = (int)pt_index;
                                    OnSelectedPointChanged(EventArgs.Empty);
                                }
                            }

                            //set the last point
                            lastPoint = point;

                            //Highlight the point at the current x-position
                            point.BackSecondaryColor = Color.White;
                            point.BackHatchStyle = ChartHatchStyle.Percent25;

                            point.MarkerStyle = MarkerStyle.Diamond;
                            point.MarkerSize = 5;

                            //Get the y-position (in pixels) of the highlighted data point
                            chartAreaYSpan = chartMainArea.AxisY.Maximum - chartMainArea.AxisY.Minimum;
                            yPosition = chartMainArea.AxisY.ValueToPixelPosition(point.YValues[0]);
                            xPosition = e.X;
                        }
                    }
                }
                else if (this.Series[seriesIndex].ChartType == SeriesChartType.Point)
                {
                    xPosition = (double)e.X;
                    yPosition = (double)e.Y;

                    if (result.PointIndex > 0)
                    {
                        //point = (DataPoint)this.Series[seriesIndex].Points[result.PointIndex];

                        //if (lastPoint != null)
                        //{
                        //    lastPoint.MarkerStyle = MarkerStyle.None;
                        //}

                        ////set the last point
                        //lastPoint = point;
                        //lastPointIsHovered = true;

                        ////Highlight the point at the current x-position
                        //point.BackSecondaryColor = Color.White;
                        //point.BackHatchStyle = ChartHatchStyle.Percent25;

                        //point.MarkerStyle = MarkerStyle.Diamond;
                        //point.MarkerColor = Color.Black;
                        //point.MarkerSize = 10;  
                    }
                    else
                    {
                        lastPointIsHovered = false;
                    }

                }

                // Move the crosshair cursor
                mousePoint = new Point((int)xPosition, (int)yPosition);

                chartMainArea.CursorX.SetCursorPixelPosition(mousePoint, true);
                chartMainArea.CursorY.SetCursorPixelPosition(mousePoint, true);
            }

            //this.Invalidate();
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
            
        }

        private void customchart_PostPaint(object sender, ChartPaintEventArgs e)
        {
            int x, y;
            if (customToolTipEnabled == true && this.Series.Count > 0 && lastPointIsHovered == true)
            {
                x = (int)this.ChartAreas[0].AxisX.ValueToPixelPosition(lastPoint.XValue);
                y = (int)this.ChartAreas[0].AxisY.ValueToPixelPosition(lastPoint.YValues[0]);

                DrawCustomToolTip(new Point(x, y), e.ChartGraphics.Graphics);
            }
        }

        private void DrawCustomToolTip(Point pAtPt, Graphics pChartGraphics)
        {
            Pen lPen = new System.Drawing.Pen(System.Drawing.Color.Blue);
            Rectangle ttRect = new Rectangle(pAtPt.X, pAtPt.Y, 30, 25);

            pChartGraphics.DrawRectangle(lPen, ttRect);
            lPen.Dispose();
        }

        public void SetChartPointSize(int pSize)
        {
            if(pSize > 0)
            {
                this.MarkerSize = pSize;
            }
        }

        private void customChart_OnCollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            
        }
    }
}
