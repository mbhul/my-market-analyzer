using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMarketAnalyzer
{
    public partial class HeatMap : UserControl
    {
        Bitmap heatMapImage;

        public HeatMap()
        {
            InitializeComponent();
            heatMapImage = new Bitmap(this.Width, this.Height);
            this.heatMapPicBox.Image = heatMapImage;
        }

        private void heatMap_OnPaint(object sender, PaintEventArgs e)
        {
            
        }

        public void BindMarketData(ExchangeMarket pData)
        {
            const int PCT_MAX_COLOR = 10;

            int xfactor = 0, yfactor = 0;
            int remainder = 0;
            double r, g, b;
            int xcount = 0, ycount = 0;
            Color eqColor = new Color();

            if(pData.Constituents != null)
            {
                if(this.Height < pData.Constituents.Count)
                {
                    this.Height = pData.Constituents.Count;
                }
                if(this.Width < pData.Constituents[0].HistoricalPctChange.Count)
                {
                    this.Width = pData.Constituents[0].HistoricalPctChange.Count;
                }

                if (this.Height > pData.Constituents.Count)
                {
                    yfactor = (int)Math.Floor((double)this.Height / (double)pData.Constituents.Count);
                    remainder = this.Height % pData.Constituents.Count;
                    this.Height -= remainder;
                }

                if (this.Width > pData.Constituents[0].HistoricalPctChange.Count)
                {
                    xfactor = (int)Math.Floor((double)this.Width / (double)pData.Constituents[0].HistoricalPctChange.Count);
                    remainder = this.Width % pData.Constituents[0].HistoricalPctChange.Count;
                    this.Width -= remainder;
                }

                foreach(Equity eq in pData.Constituents)
                {
                    ycount++;
                    xcount = 0;
                    foreach(double pct in eq.HistoricalPctChange)
                    {
                        xcount++;

                        b = 0;
                        if (eq.HistoricalPctChange[xcount - 1] <= 0)
                        {
                            r = 255;
                        }
                        else
                        {
                            r = (-255 * eq.HistoricalPctChange[xcount - 1] / PCT_MAX_COLOR) + 255;
                            r = (r < 0) ? 0 : r;
                        }

                        if (eq.HistoricalPctChange[xcount - 1] >= 0)
                        {
                            g = 255;
                        }
                        else
                        {
                            g = (255 * eq.HistoricalPctChange[xcount - 1] / PCT_MAX_COLOR) + 255;
                            g = (g < 0) ? 0 : g;
                            b = g;
                        }

                        eqColor = Color.FromArgb((int)r, (int)g, (int)b);

                        for (int x = 0; x < xfactor; x++)
                        {
                            for (int y = 0; y < yfactor; y++)
                            {
                                this.heatMapImage.SetPixel((xcount * xfactor) + x, (ycount * yfactor) + y, eqColor);
                            }
                        }
                    }
                }

                this.Invalidate();
            }
        }

        private void heatMap_OnResize(object sender, EventArgs e)
        {

        }

    }
}
