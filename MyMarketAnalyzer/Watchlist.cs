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
    public partial class Watchlist : UserControl
    {
        //Constants

        //Private members
        private List<Equity> Equities;
        private List<WatchlistItem> Items;
        private int hovered_index = -1;
        private bool HasPainted = false;

        //private Timer ItemUpdateTimer = new Timer();
        private System.Timers.Timer ItemUpdateTimer = new System.Timers.Timer();

        //Public members
        public int UpdateInterval = 60000;

        /*****************************************************************************
         *  CONSTRUCTOR:       Watchlist
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        public Watchlist()
        {
            InitializeComponent();
            this.Equities = new List<Equity>();
            this.Items = new List<WatchlistItem>();

            this.ItemUpdateTimer.Interval = UpdateInterval;
            //this.ItemUpdateTimer.Tick += ItemUpdateTimer_Tick;
            this.ItemUpdateTimer.Elapsed += ItemUpdateTimer_Elapsed;
        }

        /*****************************************************************************
         *  FUNCTION:       AddMouseMoveHandler
         *  Description:    Set the Mouse Move event handler of all child controls to 
         *                  watchlist_MouseMove. Recursive.
         *  Parameters:     
         *          c    -  The control to bind the event handler to
         *****************************************************************************/
        private void AddMouseMoveHandler(Control c)
        {
            c.MouseMove += watchlist_MouseMove;
            c.MouseLeave += watchlist_MouseLeave;
            if (c.Controls.Count > 0)
            {
                foreach (Control ct in c.Controls)
                {
                    AddMouseMoveHandler(ct);
                }  
            }
        }

        /*****************************************************************************
         *  FUNCTION:       Add
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        public void Add(Equity pEquity, String pMarket = "")
        {
            if(!this.ExistsItem(pEquity))
            {
                WatchlistItem wlItem = new WatchlistItem(pEquity);
                Items.Add(wlItem);
                PaintItem(Items.Count - 1);
                AddMouseMoveHandler(wlItem);
                wlItem.OnWatchlistUpdate += new WatchlistItem.WatchlistEventHandler(WatchList_OnWatchlistUpdate);

                this.Equities.Add(pEquity);
            }

            ManageUpdateTimer();
        }

        /*****************************************************************************
         *  FUNCTION:       Remove
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        public void Remove(int pIndex)
        {
            if (this.Items.Count > 0 && pIndex >= 0 && pIndex < this.Items.Count)
            {
                this.Items[pIndex].Dispose();
                this.Items.RemoveAt(pIndex);
                this.Equities.RemoveAt(pIndex);
            }
            ManageUpdateTimer();
            RePaint();
        }

        /*****************************************************************************
         *  FUNCTION:       RePaint
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void RePaint()
        {
            int index = 0;
            
            if(this.Items.Count > 0)
            {
                while(index < this.Items.Count)
                {
                    PaintItem(index++);
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       PaintItem
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void PaintItem(int pIndex)
        {
            int item_count = Items.Count();
            int y = (Items[pIndex].Height * pIndex) + (pIndex * 2);

            if(this.VerticalScroll.Value > 0)
            {
                this.VerticalScroll.Value = 0;
            }

            if(item_count > 0)
            {
                if (!this.Controls.Contains(Items[pIndex]))
                {
                    this.Controls.Add(Items[pIndex]);
                }
                Items[pIndex].Location = new Point(0, y);
                Items[pIndex].Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;

                if (this.VScroll && HasPainted)
                {
                    Items[pIndex].Width = this.Width - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                }
                else
                {
                    Items[pIndex].Width = this.Width;
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:       ExistsItem
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private Boolean ExistsItem(Equity pItem)
        {
            Boolean exists = false;
            List<Equity> searchResult = null;

            searchResult = this.Equities.Where(x => x.Name == pItem.Name).ToList();
            if (searchResult != null && searchResult.Count > 0)
            {
                if(searchResult[0].ListedMarket == pItem.ListedMarket)
                {
                    exists = true;
                }
            }

            return exists;
        }

        /*****************************************************************************
         *  FUNCTION:       ManageUpdateTimer
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void ManageUpdateTimer()
        {
            if(!this.ItemUpdateTimer.Enabled && this.Equities.Count > 0)
            {
                this.ItemUpdateTimer.Start();
            }
            else if (this.Equities.Count == 0)
            {
                this.ItemUpdateTimer.Stop();
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  ItemLiveUpdate
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        void ItemUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (this.Equities.Count == this.Items.Count)
            {
                foreach (Equity eq in this.Equities)
                {
                    eq.UpdateLiveData();
                }
                ApplyUpdates();
            }
        }

        void ItemUpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (this.Equities.Count == this.Items.Count)
            {
                foreach (Equity eq in this.Equities)
                {
                    eq.UpdateLiveData();
                }
                
                //Invoke UI updates on the UI thread
                this.Invoke((MethodInvoker)delegate
                {
                    ApplyUpdates();
                });
            }
        }

        /*****************************************************************************
         *  FUNCTION:       ApplyUpdates
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        public void ApplyUpdates()
        {
            int index = 0;
            if (this.Equities.Count == this.Items.Count)
            {
                foreach (WatchlistItem item in this.Items)
                {
                    item.UpdateItem(this.Equities[index++]);
                }
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  watchlist_MouseMove
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void watchlist_MouseMove(object sender, MouseEventArgs e)
        {
            int x, y, hid;
            Control hoveredControl;

            //Get mouse point
            x = this.PointToClient(Control.MousePosition).X;
            y = this.PointToClient(Control.MousePosition).Y;

            //Check for child control (ie. WatchlistItem) at the mouse point
            hoveredControl = this.GetChildAtPoint(new Point(x, y));

            if(hoveredControl != null)
            {
                //index of the item curently hovered
                hid = this.Items.IndexOf(((WatchlistItem)hoveredControl));

                //highlight current and unhighlight previous hovered controls
                if (hid != hovered_index && hovered_index < this.Items.Count)
                {
                    ((WatchlistItem)hoveredControl).BackColor = Color.Gainsboro;
                    if(hovered_index >= 0)
                    {
                        this.Items[hovered_index].BackColor = Color.White;
                    }
                }

                //update the previous hovered index
                hovered_index = hid;
            }
            else
            {
                if(hovered_index < this.Items.Count &&
                   hovered_index >= 0)
                {
                    this.Items[hovered_index].BackColor = Color.White;
                }
                hovered_index = -1;
            }
            
        }

        /*****************************************************************************
         *  EVENT HANDLER:  watchlist_MouseLeave
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void watchlist_MouseLeave(object sender, EventArgs e)
        {
            watchlist_MouseMove(sender, new MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, 0, 0, 0));
        }

        /*****************************************************************************
         *  EVENT HANDLER:  WatchList_OnWatchlistUpdate
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void WatchList_OnWatchlistUpdate(object sender, WatchlistEventArgs e)
        {
            int index;
            if(e.EventType == WatchlistEventType.REM_ITEM)
            {
                index = this.Items.IndexOf((WatchlistItem)sender);
                this.Remove(index);
            }
        }

        /*****************************************************************************
         *  EVENT HANDLER:  watchlist_OnPaint
         *  Description:    
         *  Parameters:     
         *****************************************************************************/
        private void watchlist_OnPaint(object sender, PaintEventArgs e)
        {
            HasPainted = true;
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
                          IntPtr hWnd,      // handle to destination window
                          UInt32 Msg,       // message
                          IntPtr wParam,  // first message parameter
                          IntPtr lParam   // second message parameter
                          );

        
    }

    
}
