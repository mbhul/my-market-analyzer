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
    public partial class StatTable : UserControl
    {
        private enum ColumnStyle
        {
            NONE,
            PLUS_MINUS,
            SYMBOL
        }

        private enum ContextMenuEntry
        {
            [StringValue("Show Chart")]
            CHART,
            [StringValue("Add to Watchlist")]
            WATCHLIST,
            [StringValue("Open In New Tab")]
            NEWTAB,
            [StringValue("Add To Chart")]
            ADDCHART
        }

        private int NUM_ENTRIES = 1;
        protected DataTable tableSource = new DataTable();

        private const UInt32 WM_NOTIFY = 0x004E;
        private const UInt32 WM_CELLCLICK = 0xA000;
        private const UInt32 WM_MULTIROWCLICK = 0xA001;
        private const UInt32 WM_ADDWATCHLIST = 0xA007;
        private const UInt32 WM_MULTI_ADDWATCHLIST = 0xA008;
        private const String IndexColumnName = "ItemNum";

        private ContextMenuEntry[] ContextMenuEntries = { ContextMenuEntry.CHART, ContextMenuEntry.WATCHLIST };

        private String FilterExpression = "";
        private int CurrentSelectedRow;
        private List<Tuple<int,int>> SelectedCells;
        private int ScrollIndex = 0;

        private Dictionary<String, Color> HighlightInstructions = new Dictionary<string, Color>();

        List<ColumnStyle> ColumnStyles = new List<ColumnStyle>();
        List<int> PlusMinusColumns = new List<int>();
        int SymbolColumn;

        public List<int> SelectedEntries { get; private set; }
        public StatTableType TableType { get; set; }
        public int NumberOfEntries { get; private set; }

        public StatTable()
        {
            InitializeComponent();

            SelectedEntries = new List<int>();
            SelectedCells = new List<Tuple<int, int>>();
            dataGridView1.ContextMenu = new ContextMenu();
            CurrentSelectedRow = 0;
            NumberOfEntries = 0;
        }

        //TBD: Initialize a table with a fixed number of entries
        public StatTable(int num_rows)
        {
            if (num_rows > 1)
            {
                NUM_ENTRIES = num_rows;
            }
            InitializeComponent();

            SelectedEntries = new List<int>();
            SelectedCells = new List<Tuple<int, int>>();
            dataGridView1.ContextMenu = new ContextMenu();
            CurrentSelectedRow = 0;
            NumberOfEntries = 0;
        }

        /*****************************************************************************
         *  FUNCTION:  DisplayedCount
         *  Description:    Returns the number of rows currently shown in the table
         *  Parameters:     None
         *****************************************************************************/
        public int DisplayedCount()
        {
            return tableSource.Select(FilterExpression).Length;
        }

        /*****************************************************************************
         *  FUNCTION:  ResetRows
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void ResetRows(int num_rows)
        {
            NUM_ENTRIES = num_rows;
        }

        /*****************************************************************************
         *  FUNCTION:  SetFilterExpression
         *  Description:    Set the filter expression to be applied to the DataTable. 
         *                  The filter is applied only once ApplyFilter() is called.
         *                  
         *                  This is to allow a filter to be applied from the parent window
         *                  
         *  Parameters:    
         *              pHandle - 
         *              strFilter - 
         *****************************************************************************/
        public void SetFilterExpression(IntPtr pHandle, String strFilter)
        {
            if(pHandle == this.Parent.Handle)
            {
                FilterExpression = strFilter;
            }
        }

        /*****************************************************************************
         *  FUNCTION:   Clear
         *  Description:    Self explanatory. Clears the table.   
         *  Parameters: None
         *****************************************************************************/
        public void Clear()
        {
            tableSource.Clear();
            tableSource.Constraints.Clear();
            tableSource.Rows.Clear();
            tableSource.Columns.Clear();
            tableSource.DefaultView.Sort = String.Empty;
            tableSource.DefaultView.RowFilter = String.Empty;
            ClearCellStyles();
            dataGridView1.DataSource = null;
        }

        /*****************************************************************************
         *  FUNCTION:  ClearCellStyles
         *  Description:    Clears formatting from the table, and specifically the cells
         *                  containing +/- data which is coloured green/red
         *  Parameters: 
         *****************************************************************************/
        private void ClearCellStyles()
        {
            ColumnStyles.Clear();
            PlusMinusColumns.Clear();
        }

        /*****************************************************************************
         *  FUNCTION:  BindMarketData
         *  Description:    This is the main data binding function. It creates or updates
         *                  the table from the data contained in the passed ExchangeMarket class.
         *  Parameters: 
         *****************************************************************************/
        public void BindMarketData(ExchangeMarket mktData, bool updateExisting = false)
        {
            int i;
            Double NameColWidth = 0.25;
            Dictionary<int, Double> CoefficientList;
            List<int> remSelectedRows = new List<int>(this.SelectedEntries);
            List<Tuple<int,int>> remSelectedCells = new List<Tuple<int,int>>(this.SelectedCells);
            String existingSort = this.GetDataViewSortInstruction();

            ScrollIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex;

            DataRow row;

            //Create a Historical data table
            if (TableType == StatTableType.HIST_STATS)
            {
                CreateNewTable();

                // Create the columns and column headings
                AddColumnToSource(System.Type.GetType("System.String"), TableHeadings.Name, "Name", true, false);
                AddColumnToSource(System.Type.GetType("System.String"), TableHeadings.Symbol, "Sym", true, false, false, ColumnStyle.SYMBOL);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_Last, TableHeadings.Live_Last, true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_High, TableHeadings.Live_High, true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_Low, TableHeadings.Live_Low, true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.PctChange[0], TableHeadings.PctChange[1], true, false, false, ColumnStyle.PLUS_MINUS);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Hist_Avg[0], TableHeadings.Hist_Avg[1], true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Hist_Vlty, "StdDev (Volatility)", true, false);
                AddColumnToSource(System.Type.GetType("System.DateTime"), TableHeadings.Hist_DtStart[0], TableHeadings.Hist_DtStart[1], true, false);
                AddColumnToSource(System.Type.GetType("System.DateTime"), TableHeadings.Hist_DtEnd[0], TableHeadings.Hist_DtEnd[1], true, false);

                //Fixed width for the stock name column is 25% of the full visible table width
                NameColWidth = 0.25;

                //Create the table rows from the ExchangeMarket data
                for (i = 0; i < mktData.Constituents.Count(); i++)
                {
                    row = tableSource.NewRow();
                    row[IndexColumnName] = i + 1;
                    row[TableHeadings.Name] = mktData.Constituents[i].Name;
                    row[TableHeadings.Symbol] = mktData.Constituents[i].Symbol;
                    row[TableHeadings.Live_Last] = mktData.Constituents[i].HistoricalPrice[mktData.Constituents[i].HistoricalPrice.Count - 1];
                    row[TableHeadings.Live_High] = mktData.Constituents[i].HistoricalPrice.Max();
                    row[TableHeadings.Live_Low] = mktData.Constituents[i].HistoricalPrice.Min();
                    row[TableHeadings.PctChange[0]] = mktData.Constituents[i].pctChange;
                    row[TableHeadings.Hist_Avg[0]] = mktData.Constituents[i].avgPrice;
                    row[TableHeadings.Hist_Vlty] = mktData.Constituents[i].Volatility;
                    row[TableHeadings.Hist_DtStart[0]] = mktData.Constituents[i].HistoricalPriceDate[0];
                    row[TableHeadings.Hist_DtEnd[0]] = mktData.Constituents[i].HistoricalPriceDate[mktData.Constituents[i].HistoricalPriceDate.Count() - 1];
                    tableSource.Rows.Add(row);
                }
            }
            //Create a PPC correlation table
            else if (TableType == StatTableType.ANALYSIS_PPC)
            {
                CreateNewTable();

                // Create new columns 
                AddColumnToSource(System.Type.GetType("System.String"), TableHeadings.Name, "Name", true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.PPC_Max[0], TableHeadings.PPC_Max[1], true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.PPC_Min[0], TableHeadings.PPC_Min[1], true, false);  

                NameColWidth = 0.5;
                for (i = 0; i < mktData.Constituents.Count(); i++)
                {
                    row = tableSource.NewRow();
                    row[IndexColumnName] = i + 1;
                    row[TableHeadings.Name] = mktData.Constituents[i].Name;
                    CoefficientList = mktData.GetPPCCoefficients(i);

                    row[TableHeadings.PPC_Max[0]] = CoefficientList.Values.Max();
                    row[TableHeadings.PPC_Min[0]] = CoefficientList.Values.Min();

                    tableSource.Rows.Add(row);
                }
            }
            // Create a Live data table
            else if (TableType == StatTableType.LIVE_STATS)
            {
                CreateNewTable();

                // Create new columns
                AddColumnToSource(System.Type.GetType("System.String"), TableHeadings.Name, "Name", true, false);
                AddColumnToSource(System.Type.GetType("System.String"), TableHeadings.Symbol, "Sym", true, false, false, ColumnStyle.SYMBOL);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_Last, "Last", true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_High, "High", true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_Low, "Low", true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_Chg, "Change", true, false, false, ColumnStyle.PLUS_MINUS);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.PctChange[0], TableHeadings.PctChange[1], true, false, false, ColumnStyle.PLUS_MINUS);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Live_Vol, "Volume", true, false);
                AddColumnToSource(System.Type.GetType("System.DateTime"), TableHeadings.Live_Time, "Time", true, false);

                NameColWidth = 0.25;
                for (i = 0; i < mktData.Constituents.Count(); i++)
                {
                    row = tableSource.NewRow();
                    row[IndexColumnName] = i + 1;
                    row[TableHeadings.Name] = mktData.Constituents[i].Name;
                    row[TableHeadings.Symbol] = mktData.Constituents[i].Symbol;
                    row[TableHeadings.Live_Last] = mktData.Constituents[i].DailyLast[mktData.Constituents[i].DailyLast.Count - 1];
                    row[TableHeadings.Live_High] = mktData.Constituents[i].DailyHigh;
                    row[TableHeadings.Live_Low] = mktData.Constituents[i].DailyLow;
                    row[TableHeadings.Live_Chg] = mktData.Constituents[i].DailyChg;
                    row[TableHeadings.PctChange[0]] = mktData.Constituents[i].DailyChgPct;
                    row[TableHeadings.Live_Vol] = mktData.Constituents[i].DailyVolume;
                    row[TableHeadings.Live_Time] = mktData.Constituents[i].DailyTime[mktData.Constituents[i].DailyTime.Count - 1];
                    tableSource.Rows.Add(row);
                }
            }
            else
            {
                //Invalid table-type. Do nothing.
            }

            NumberOfEntries = mktData.Constituents.Count();

            //Get the column indices of columns where the set style is PLUS_MINUS
            PlusMinusColumns = ColumnStyles.Select((cs, ci) => cs == ColumnStyle.PLUS_MINUS ? ci : -1).Where(ci2 => ci2 >= 0).ToList();
            SymbolColumn = ColumnStyles.Select((cs, ci) => cs == ColumnStyle.SYMBOL ? ci : -1).Where(ci2 => ci2 >= 0).FirstOrDefault();

            //Bind Data from the DataTable to the DataGridView control
            BindTableSource(NameColWidth);

            //Apply existing filter
            ApplyFilter();

            if (updateExisting)
            {
                this.SelectedEntries = new List<int>(remSelectedRows);
                //this.SelectedCells = new List<Tuple<int, int>>(remSelectedCells);
                this.SetDataViewSortInstruction(existingSort);
                this.UpdateSelection(remSelectedCells);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateSelection
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void UpdateSelection(List<Tuple<int, int>> pSelectCells)
        {
            int row_index;

            try
            {
                if (pSelectCells.Count > 0)
                {
                    row_index = this.dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[IndexColumnName].Value.Equals(pSelectCells[0].Item1)).First().Index;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[row_index].Cells[pSelectCells[0].Item2];
                }

                foreach (Tuple<int, int> cellInfo in pSelectCells)
                {
                    row_index = this.dataGridView1.Rows.Cast<DataGridViewRow>().Where(r => r.Cells[IndexColumnName].Value.Equals(cellInfo.Item1)).First().Index;
                    this.dataGridView1.Rows[row_index].Cells[cellInfo.Item2].Selected = true;
                }

                if (this.ScrollIndex > 0 && this.ScrollIndex < dataGridView1.Rows.Count)
                {
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = this.ScrollIndex;
                }     
            }
            catch(Exception)
            {
                this.SelectedEntries.Clear();
                this.SelectedCells.Clear();
                this.ScrollIndex = 0;
            }
            //catch(IndexOutOfRangeException ior)
            //{
            //    this.SelectedEntries.Clear();
            //    this.SelectedCells.Clear();
            //    this.ScrollIndex = 0;
            //}
        }

        /*****************************************************************************
         *  FUNCTION:  BindCorrelationData
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void BindCorrelationData(ref ExchangeMarket mktData, int pIndex)
        {
            int i;
            Dictionary<int, Double> CoefficientList;

            DataRow row;
            
            if(TableType == StatTableType.INDIVIDUAL_PPC && (pIndex < mktData.Constituents.Count && pIndex > 0))
            {
                //CreateSingleCorrelationTable();
                CreateNewTable();

                // Create new columns 
                AddColumnToSource(System.Type.GetType("System.String"), TableHeadings.Name, "Name", true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.PPC_Coeff, "Correlation", true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.PctChange[0], TableHeadings.PctChange[1], true, false);
                AddColumnToSource(System.Type.GetType("System.Double"), TableHeadings.Hist_Avg[0], TableHeadings.Hist_Avg[1], true, false);

                CoefficientList = mktData.GetPPCCoefficients(pIndex);

                for (i = 0; i < mktData.Constituents.Count; i++)
                {
                    if(i != pIndex)
                    {
                        row = tableSource.NewRow();
                        row[IndexColumnName] = i + 1;
                        row[TableHeadings.Name] = mktData.Constituents[i].Name;
                        row[TableHeadings.PctChange[0]] = mktData.Constituents[i].pctChange;
                        row[TableHeadings.Hist_Avg[0]] = mktData.Constituents[i].avgPrice;

                        //Check for availability of PPC coefficients
                        if (i < CoefficientList.Count)
                        {
                            row[TableHeadings.PPC_Coeff] = CoefficientList[i];
                        }
                        else
                        {
                            row[TableHeadings.PPC_Coeff] = 0;
                        }

                        tableSource.Rows.Add(row);
                    }
                }

                //Bind Data
                BindTableSource(0.5);
            }

            NumberOfEntries = mktData.Constituents.Count();

        }

        /*****************************************************************************
         *  FUNCTION:  ApplyFilter
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void ApplyFilter()
        {
            tableSource.DefaultView.RowFilter = FilterExpression;
        }

        /*****************************************************************************
         *  FUNCTION:  SetDataViewSortInstruction
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public bool SetDataViewSortInstruction(String InstrString)
        {
            bool success = false;
            try
            {
                tableSource.DefaultView.Sort = InstrString;
                success = true;
            }
            catch(Exception)
            {
                //handle exception
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  GetDataViewSortInstruction
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public String GetDataViewSortInstruction()
        {
            return tableSource.DefaultView.Sort;
        }

        /*****************************************************************************
         *  FUNCTION:  BindTableSource
         *  Description:    Binds the data from the source DataTable to the DataGridView
         *                  control which is ultimately displayed on the UI. 
         *  Parameters: 
         *****************************************************************************/
        private void BindTableSource(Double NameColWidth = 0.25)
        {
            int i;

            bindingSource1.DataSource = tableSource;
            dataGridView1.DataSource = bindingSource1;

            //Set Item # column invisible (used to recall the original order of sorted data)
            dataGridView1.Columns[0].Visible = false;

            if (this.Visible)
            {
                //Set Column Widths
                dataGridView1.Columns[1].Width = (int)(this.Width * NameColWidth);
                for (i = 2; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = (int)(this.Width * (1 - NameColWidth) / (dataGridView1.Columns.Count - 2));
                }
            }

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.HeaderText = tableSource.Columns[col.HeaderText].Caption;
            }
        }

        /*****************************************************************************
         *  FUNCTION:  AddColumnToSource
         *  Description:    Adds a new column to the global source Table (tableSource)
         *                  based on the passed parameters.
         *  Parameters: 
         *****************************************************************************/
        private void AddColumnToSource(Type pDataType, String pName, String pCaption, Boolean pReadOnly, Boolean pUnique, 
            Boolean pAutoIncrement = false, ColumnStyle pStyle = ColumnStyle.NONE)
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = pDataType;
            column.ColumnName = pName;
            column.AutoIncrement = pAutoIncrement;
            column.Caption = pCaption;
            column.ReadOnly = pReadOnly;
            column.Unique = pUnique;
            // Add the Column to the DataColumnCollection.
            tableSource.Columns.Add(column);
            ColumnStyles.Add(pStyle);
        }

        /*****************************************************************************
         *  FUNCTION:  CreateNewTable
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void CreateNewTable()
        {
            DataColumn column;

            //Clear any existing structure
            this.Clear();

            // Create new DataColumn, set DataType, 
            // ColumnName and add to DataTable.   
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = IndexColumnName;
            column.Caption = "#";
            column.ReadOnly = true;
            column.Unique = true;
            // Add the Column to the DataColumnCollection.
            tableSource.Columns.Add(column);
            tableSource.PrimaryKey = new DataColumn[1] { column };
            ColumnStyles.Add(ColumnStyle.NONE);
        }

        /*****************************************************************************
         *  FUNCTION:       WriteColumn<Type>
         *  Description:    Writes data to an existing column, or inserts a new column
         *                  if it doesn't already exist
         *  Parameters:     
         *          pName   - 
         *          pDataIn -
         *          pIndex  -
         *****************************************************************************/
        public bool WriteColumn<T>(String pName, List<T> pDataIn, int pIndex)
        {
            bool success = false;
            DataRow row;
            int i;

            if(tableSource != null &&
                pDataIn.Count() == tableSource.Rows.Count)
            {
                if (tableSource.Columns.Contains(pName) == false)
                {
                    // Create new DataColumn, set DataType, 
                    // ColumnName and add to DataTable. 
                    AddColumnToSource(typeof(T), pName, pName, false, false);

                    //Update the plus-minus column indeces (these are the indices of columns whose values are to be colored green/red based on the value)
                    ColumnStyle new_item = ColumnStyles[ColumnStyles.Count - 1];
                    ColumnStyles.RemoveAt(ColumnStyles.Count - 1);
                    ColumnStyles.Insert(pIndex > 0 ? pIndex : 1, new_item);
                    PlusMinusColumns = ColumnStyles.Select((cs, ci) => cs == ColumnStyle.PLUS_MINUS ? ci : -1).Where(ci2 => ci2 >= 0).ToList();
                    SymbolColumn = ColumnStyles.Select((cs, ci) => cs == ColumnStyle.SYMBOL ? ci : -1).Where(ci2 => ci2 >= 0).FirstOrDefault();

                    //Set the column at the desired position. Note: column[0] must always be the item index (which is hidden) so don't allow insertion before it
                    if (pIndex > 0)
                    {
                        tableSource.Columns[tableSource.Columns.Count - 1].SetOrdinal(pIndex);
                        dataGridView1.Columns[pName].DisplayIndex = pIndex;
                    }
                    else if (pIndex == 0)
                    {
                        tableSource.Columns[tableSource.Columns.Count - 1].SetOrdinal(1);
                        dataGridView1.Columns[pName].DisplayIndex = 0;
                    }
                }

                tableSource.Columns[pName].ReadOnly = false;
                for(i = 0; i < pDataIn.Count(); i++)
                {
                    row = tableSource.Rows.Find(i+1);
                    if(row != null)
                    {
                        row[pName] = pDataIn[i];
                    }
                }
                tableSource.Columns[pName].ReadOnly = true;

                success = true;
            }

            return success;
        }

        /*****************************************************************************
         *  FUNCTION:  HighlightData
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void HighlightData(String filterExpression, Color pColour)
        {
            DataRow[] rows;
            DataView dv;
            DataTable sortedTable;
            String ColumnName;
            int i, count, index;

            try
            {
                dv = tableSource.DefaultView;
                sortedTable = dv.ToTable();
                rows = sortedTable.Select(filterExpression);
                
                ColumnName = Helpers.GetFirstWord(filterExpression);

                if (rows != null && rows.Count() > 0)
                {
                    count = rows.Count();
                    for (i = 0; i < count; i++)
                    {
                        index = sortedTable.Rows.IndexOf(rows[i]);
                        dataGridView1.Rows[index].Cells[ColumnName].Style.BackColor = pColour;
                    }
                }

                //Add to the dictionary of highlight commands
                filterExpression = filterExpression.Replace(" ", "");
                if (HighlightInstructions.ContainsKey(filterExpression))
                {
                    HighlightInstructions[filterExpression] = pColour;
                }
                else
                {
                    HighlightInstructions.Add(filterExpression, pColour);
                }

            }
            catch(Exception)
            {
                //throw new ArithmeticException();
            }
            
        }

        /*****************************************************************************
         *  FUNCTION:  ClearDataHighlighting
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        public void ClearDataHighlighting(String filterExpression)
        {
            //Remove the instruction from the dictionary of highlight commands
            filterExpression = filterExpression.Replace(" ", "");
            if (HighlightInstructions.ContainsKey(filterExpression))
            {
                HighlightInstructions.Remove(filterExpression);
            }
        }

        /*****************************************************************************
         *  FUNCTION:  ReapplyHighlighting
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void ReapplyHighlighting()
        {
            List<String> keys = HighlightInstructions.Keys.ToList();
            List<Color> colors = HighlightInstructions.Values.ToList();

            for(int i = 0; i < HighlightInstructions.Count; i++)
            {
                HighlightData(keys[i], colors[i]);
            }
        }

        //Cell double-click event handler
        /*****************************************************************************
         *  EVENT:  dataGridView1_CellDoubleClick
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > 0)
            {
                SendMessage(Application.OpenForms[0].Handle, WM_CELLCLICK, (IntPtr)((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value - 1), this.Handle);
            }
        }

        /*****************************************************************************
         *  EVENT:  dataGridView1_ContextMenuItemClick
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_ContextMenuItemClick(object sender, EventArgs e)
        {
            if(((MenuItem)sender).Index == (int)ContextMenuEntry.CHART)
            {
                if (SelectedEntries.Count() > 1)
                {
                    SendMessage(Application.OpenForms[0].Handle, WM_MULTIROWCLICK, IntPtr.Zero, this.Handle);
                }
                else
                {
                    SendMessage(Application.OpenForms[0].Handle, WM_CELLCLICK, (IntPtr)((int)dataGridView1.Rows[CurrentSelectedRow].Cells[0].Value - 1), this.Handle);
                }
            }
            
            if(((MenuItem)sender).Index == (int)ContextMenuEntry.WATCHLIST)
            {
                if (SelectedEntries.Count() > 1)
                {
                    SendMessage(Application.OpenForms[0].Handle, WM_MULTI_ADDWATCHLIST, IntPtr.Zero, this.Handle);
                }
                else
                {
                    SendMessage(Application.OpenForms[0].Handle, WM_ADDWATCHLIST, (IntPtr)((int)dataGridView1.Rows[CurrentSelectedRow].Cells[0].Value - 1), this.Handle);
                }
            }
        }

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int SendMessage(
                          IntPtr hWnd,      // handle to destination window
                          UInt32 Msg,       // message
                          IntPtr wParam,  // first message parameter
                          IntPtr lParam   // second message parameter
                          );

        //This event fired when a row is selected by clicking the row header
        /*****************************************************************************
         *  EVENT:  dataGridView1_RowHeaderClick
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_RowHeaderClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdateRowSelection();
        }

        /*****************************************************************************
         *  EVENT:  dataGridView1_RowEnter
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            UpdateRowSelection();
        }

        /*****************************************************************************
         *  FUNCTION:  UpdateRowSelection
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void UpdateRowSelection()
        {
            int i, selrow, selcol;
            SelectedEntries.Clear();
            SelectedCells.Clear();

            //Update row selections
            if (dataGridView1.SelectedRows.Count > 1)
            {
                for (i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    selrow = (int)dataGridView1.SelectedRows[i].Cells[0].Value - 1;
                    SelectedEntries.Add(selrow);
                }
            }
            else if (dataGridView1.SelectedRows.Count == 1 && CurrentSelectedRow < dataGridView1.Rows.Count)
            {
                SelectedEntries.Add((int)dataGridView1.Rows[CurrentSelectedRow].Cells[0].Value - 1);
            }
            else { }

            //Update cell selections
            for(i = 0; i < dataGridView1.SelectedCells.Count; i++)
            {
                selrow = (int)dataGridView1.SelectedCells[i].OwningRow.Cells[0].Value;
                selcol = (int)dataGridView1.SelectedCells[i].ColumnIndex;

                //Ignore header row and hidden id column
                if(selrow > 0 && selcol > 0)
                {
                    SelectedCells.Add(new Tuple<int, int>(selrow, selcol));
                    if(!SelectedEntries.Contains(selrow - 1))
                    {
                        SelectedEntries.Add(selrow - 1);
                    } 
                }
            }
        }

        /*****************************************************************************
         *  EVENT:  dataGridView1_MouseDown
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_MouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewCell click_cell;
            int selrow = 0;

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                selrow = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (this.SelectedCells.Contains(new Tuple<int, int>(selrow, e.ColumnIndex)) == false)
                {
                    dataGridView1.ClearSelection();
                    dataGridView1.CurrentCell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                }

                click_cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                click_cell.Selected = true;
                
                CurrentSelectedRow = e.RowIndex;
                if (e.Button == MouseButtons.Right)
                {
                    ShowContextMenu();

                    Rectangle r = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                    Point p = new Point(r.X + e.X, r.Y + e.Y);
                    dataGridView1.ContextMenu.Show(dataGridView1, p);
                }
            }
        }

        /*****************************************************************************
         *  FUNCTION:  ShowContextMenu
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void ShowContextMenu()
        {
            dataGridView1.ContextMenu.MenuItems.Clear();
            
            for(int i = 0; i < ContextMenuEntries.Count(); i++)
            {
                dataGridView1.ContextMenu.MenuItems.Add(StringEnum.GetStringValue(ContextMenuEntries[i]));
                dataGridView1.ContextMenu.MenuItems[i].Click += new EventHandler(dataGridView1_ContextMenuItemClick);
            }
        }

        /*****************************************************************************
         *  EVENT:  dataGridView1_SelectionChanged
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            UpdateRowSelection();
        }

        /*****************************************************************************
         *  EVENT:  dataGridView1_OnPaint
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void dataGridView1_OnPaint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        /*****************************************************************************
         *  EVENT:  datagridView1_VisibleChanged
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void datagridView1_VisibleChanged(object sender, EventArgs e)
        {
            int i;
            Double NameColWidth = 0.25;

            if(TableType == StatTableType.ANALYSIS_PPC)
            {
                NameColWidth = 0.5;
            }

            if (this.Visible && dataGridView1.Columns.Count > 2 && !this.Disposing)
            {
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = (int)(this.Width * NameColWidth);
                for (i = 2; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = (int)(this.Width * (1 - NameColWidth) / (dataGridView1.Columns.Count - 2));
                }
            }
        }

        /*****************************************************************************
         *  EVENT:  datagridview_DataBindingComplete
         *  Description:    
         *  Parameters: 
         *****************************************************************************/
        private void datagridview_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ReapplyHighlighting();
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {

        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int i;
            double value;
            string symbl;

            //Set Cell Styles
            for (int index = e.RowIndex; index <= e.RowIndex + e.RowCount - 1; index++)
            {
                symbl = dataGridView1.Rows[index].Cells[SymbolColumn].Value.ToString();
                if(symbl.Contains("?"))
                {
                    dataGridView1.Rows[index].Cells[SymbolColumn].Style.ForeColor = Color.MediumBlue;
                }

                for (i = 0; i < PlusMinusColumns.Count; i++)
                {
                    value = (double)dataGridView1.Rows[index].Cells[PlusMinusColumns[i]].Value;
                    if (value > 0)
                    {
                        dataGridView1.Rows[index].Cells[PlusMinusColumns[i]].Style.ForeColor = Color.Green;
                    }
                    else if (value < 0)
                    {
                        dataGridView1.Rows[index].Cells[PlusMinusColumns[i]].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dataGridView1.Rows[index].Cells[PlusMinusColumns[i]].Style.ForeColor = Color.Black;
                    }
                }
            }
        }

        

    }
}
