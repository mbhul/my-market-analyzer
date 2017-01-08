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
    public partial class WatchlistItem : UserControl
    {
        public UInt32 ID { get; private set; }
        public delegate void WatchlistEventHandler(object sender, WatchlistEventArgs e);
        public event WatchlistEventHandler OnWatchlistUpdate;

        /*****************************************************************************
         *  CONSTRUCTOR:       WatchlistItem
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        public WatchlistItem()
        {
            InitializeComponent();
            ID = Helpers.GetSimpleID();
        }

        /*****************************************************************************
         *  CONSTRUCTOR:       WatchlistItem
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        public WatchlistItem(Equity pEquity)
        {
            InitializeComponent();

            //Set identification
            lblName.Text = pEquity.Name;
            lblMarket.Text = pEquity.ListedMarket;
            ID = Helpers.GetSimpleID();
            
            //Populate visible fields
            UpdateItem(pEquity);

            //Misc initialization
            this.lblRemove.Cursor = Cursors.Hand;
        }

        public void UpdateItem(Equity pEquity)
        {
            int count = 0;
            double change = 0;

            //Populate visible fields
            if (pEquity.ContainsLiveData)
            {
                count = pEquity.DailyLast.Count();
                if (count > 0)
                {
                    change = pEquity.DailyChg;
                    lblDate.Text = pEquity.DailyTime[count - 1].ToString();
                    lblPrice.Text = pEquity.DailyLast[count - 1].ToString();
                    lblChange.Text = pEquity.DailyChg.ToString() + " (" + pEquity.DailyChgPct.ToString() + "%)";
                }
            }
            else if (pEquity.ContainsHistData)
            {
                count = pEquity.HistoricalPrice.Count();
                if (count > 1)
                {
                    change = pEquity.HistoricalPrice[count - 1] - pEquity.HistoricalPrice[count - 2];

                    lblDate.Text = pEquity.HistoricalPriceDate[count - 1].ToString();
                    lblPrice.Text = pEquity.HistoricalPrice[count - 1].ToString();
                    lblChange.Text = change.ToString() + " (" + pEquity.HistoricalPctChange[count - 1].ToString() + "%)";
                }
            }
            else
            {
                /* No existing data found */
            }

            if (change > 0)
            {
                lblChange.ForeColor = Color.Green;
            }
            else if (change < 0)
            {
                lblChange.ForeColor = Color.Red;
            }
            else { }
        }

        private void lblRemove_Click(object sender, EventArgs e)
        {
            WatchlistEventArgs evArgs = new WatchlistEventArgs(WatchlistEventType.REM_ITEM, this.ID);
            OnWatchlistUpdate(this, evArgs);
        }

        private void lblRemove_MouseEnter(object sender, EventArgs e)
        {
            this.lblRemove.Image = MyMarketAnalyzer.Properties.Resources.x_icon_hover;
        }

        private void lblRemove_MouseLeave(object sender, EventArgs e)
        {
            this.lblRemove.Image = MyMarketAnalyzer.Properties.Resources.x_icon_normal;
        }

    }

    //create an enum for easy addition of new events
    public enum WatchlistEventType
    {
        REM_ITEM
    }

    public class WatchlistEventArgs : EventArgs
    {
        public WatchlistEventType EventType { get; private set; }
        public UInt32 WatchlistItemID;

        public WatchlistEventArgs(WatchlistEventType evType, UInt32 ID)
        {
            EventType = evType;
            WatchlistItemID = ID;
        }
    }
}
