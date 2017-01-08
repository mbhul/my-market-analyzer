using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;

namespace MyMarketAnalyzer
{
    class HeatMap : System.Windows.Forms.DataGridView
    {
        private DataTable mapValues = new DataTable();
        private int lastIndex = 0;

        private bool IsValid = false;

        private const int DEFAULT_COL_WIDTH = 50;

        public HeatMap() : base()
        {
            
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // HeatMap
            // 
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }

        public void AddRow(List<DateTime> pDates, List<Double> pHeatValues)
        {
            bool validParams = false;
            int i;
            DataRow row;
            DateTime prev_start, prev_end;

            if(pDates.Count == pHeatValues.Count)
            {
                validParams = true;
            }

            if (validParams == true)
            {
                if(mapValues.Rows.Count <= 0)
                {
                    CreateMapTable(pDates);
                }

                prev_start = DateTime.Parse(mapValues.Columns[0].Caption);
                prev_end = DateTime.Parse(mapValues.Columns[mapValues.Columns.Count - 1].Caption);

                //Create new row
                row = mapValues.NewRow();

                for (i = 0; i < pDates.Count; i++)
                {

                    if (pDates[i] < prev_start)
                    {

                    }
                    else if (pDates[i] > prev_end)
                    {

                    }
                    else
                    {
                        row[i.ToString()] = Math.Round(pHeatValues[i], 2);
                    }
                }

                //Add row to DataTable
                mapValues.Rows.Add(row);

                //Bind DataTable to the data grid view
                BindTableSource();
            }
        }

        private void CreateMapTable(List<DateTime> pDates)
        {
            int i;
            DataColumn column;

            mapValues.Clear();
            mapValues.Constraints.Clear();
            mapValues.Rows.Clear();
            mapValues.Columns.Clear();
            lastIndex = 0;

            for(i = 0; i < pDates.Count; i++)
            {
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Double");
                column.ColumnName = i.ToString();
                column.Caption = pDates[i].ToString();
                column.ReadOnly = true;
                column.Unique = false;
                // Add the Column to the DataColumnCollection.
                mapValues.Columns.Add(column);
                lastIndex++;
            }
        }

        private void AddColumn(DateTime pDate, int position)
        {
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Double");
            column.ColumnName = (lastIndex++).ToString();
            column.Caption = pDate.ToString();
            column.ReadOnly = true;
            column.Unique = false;
            // Add the Column to the DataColumnCollection.
            mapValues.Columns.Add(column);
            column.SetOrdinal(position);
        }

        private void BindTableSource()
        {
            int i;

            //bindingSource1.DataSource = tableSource;
            this.DataSource = mapValues;

            for (i = 0; i < this.Columns.Count; i++)
            {
                this.Columns[i].Width = DEFAULT_COL_WIDTH;
                this.Columns[i].SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            }

            
        }

        public void Redraw()
        {
            int i, j, colorValue;
            double heatvalue;

            if (this.Visible == true && this.IsValid == false)
            {
                for (i = 0; i < this.Rows.Count - 1; i++)
                {
                    for (j = 0; j < this.Columns.Count; j++)
                    {
                        heatvalue = (double)this.Rows[i].Cells[j].Value;
                        colorValue = 255 - (int)(255 * Math.Abs(heatvalue));
                        if (heatvalue > 0)
                        {
                            this.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(colorValue, 255, colorValue);
                        }
                        else if (heatvalue < 0)
                        {
                            this.Rows[i].Cells[j].Style.BackColor = Color.FromArgb(255, colorValue, colorValue);
                        }
                    }
                }

                //this.IsValid = true;
            }
        }

    }
}
