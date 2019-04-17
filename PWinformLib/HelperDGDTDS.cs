using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using PWinformLib.Preloader;

namespace PWinformLib
{
    public class HelperDGDTDS
    {
        public static void AddToolStripToDgv(DataGridView dgv)
        {
            dgv.MouseUp += new MouseEventHandler(MouseUpHandler);
        }

        private static void MouseUpHandler(object sender,MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                {
                    DataGridView dgvTemp = (DataGridView)sender;
                    /*ContextMenu cm = new ContextMenu();
                    cm.MenuItems.Add("Export Data", MenuStripHandler);
                    cm.Show(dgvTemp, new Point(e.X, e.Y));*/
                    ContextMenuStrip cm = new ContextMenuStrip();
                    cm.Items.Add("Export Data",Properties.Resources.excel,MenuStripHandler);
                    cm.Show(dgvTemp, new Point(e.X, e.Y));
                }
                break;
            }
        }

        private static void MenuStripHandler(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                DataGridView dgv = new DataGridView();
                ToolStripMenuItem mI = (ToolStripMenuItem) sender;
                ContextMenuStrip cmx = mI.Owner as ContextMenuStrip;
                if(cmx != null)
                    dgv = cmx.SourceControl as DataGridView;
                if (mI.Text.Equals("Export Data"))
                {
                    Preloaderani.addSpinnLoad(dgv);
                    ExcelHelperInterop.DgvtoExcel(dgv);
                    Preloaderani.addSpinnLoad(dgv);
                }
            }
        }

        public static void AddToolStripToCMStrip(ContextMenuStrip cmx)
        {
            cmx.Items.Add("Export Data", Properties.Resources.excel, MenuStripHandler);
        }

        //convert datagridview data to datatable
        //DataTable dataTable = ToDataTable(dataGridView1);
        public static DataTable DGVToDataTable(DataGridView dataGridView)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                if (dataGridViewColumn.Visible)
                {
                    dt.Columns.Add();
                }
            }
            var cell = new object[dataGridView.Columns.Count];
            foreach (DataGridViewRow dataGridViewRow in dataGridView.Rows)
            {
                for (int i = 0; i < dataGridViewRow.Cells.Count; i++)
                {
                    cell[i] = dataGridViewRow.Cells[i].Value;
                }
                dt.Rows.Add(cell);
            }
            return dt;
        }

        //convert var to datatable
        //DataTable dtttt = VarToDataTable(detailProdLinq.ToList());
        public static DataTable VarToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                tb.Columns.Add(prop.Name, prop.PropertyType);
            }

            foreach (var item in items)
            {
                var values = new object[props.Length];
                for (var i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        //merubah dari json ke datatable
        public static DataTable JsonToDataTable(String json)
        {
            DataTable hasil = new DataTable();
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable kolom = dataSet.Tables["kolom"];
            DataTable isi = dataSet.Tables["isi"];

            for (int i = 0; i < kolom.Columns.Count; i++)
            {
                hasil.Columns.Add(kolom.Rows[0][i].ToString(), typeof(string));
            }

            for (int ii = 0; ii < isi.Rows.Count; ii++)
            {
                DataRow row = hasil.NewRow();
                for (int i = 0; i < kolom.Columns.Count; i++)
                {
                    row[kolom.Rows[0][i].ToString()] = isi.Rows[ii][i].ToString();
                }
                hasil.Rows.Add(row);
            }
            return hasil;
        }

        //membersihkan datagrid
        public static void clearDatagrid(DataGridView dgv)
        {
            if (dgv.DataSource != null)
            {
                dgv.DataSource = null;
            }
            else
            {
                dgv.Rows.Clear();
                dgv.Refresh();
            }
        }

        public static void clearDataTableRow(DataTable dt)
        {
            /*foreach (DataRow row in dt.Rows)
            {
                row.Delete();
            }*/
            for (int i = dt.Rows.Count -1; i >=0; i--)
            {
                dt.Rows[i].Delete();
            }
            //TableAdapter.Update(dt);
        }
        //
        public static void AddDatePickerDGV(object sender, DataGridViewCellEventArgs e,int ColPos)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.ColumnIndex == ColPos)
            {
                DateTimePicker dtp = new DateTimePicker();
                dgv.Controls.Add(dtp);
                Rectangle tempRect = dgv.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                dtp.Location = tempRect.Location;
                dtp.Width = tempRect.Width;
                dtp.CloseUp += (o, args) => { dtp.Visible = false; };
                dtp.TextChanged += (o, args) => { dgv.CurrentCell.Value = dtp.Text.ToString(); };
                dtp.Visible = true;
                /*Thread.Sleep(500);
                dgv[e.ColumnIndex,e.RowIndex].Open();*/
                //dgv[e.ColumnIndex,e.RowIndex].
            }
        }
        //Set combobox value from dgv
        public static void setComboDgv(DataGridViewColumn dgvC, DataTable dt)
        {
            DataGridViewComboBoxColumn theColumn = (DataGridViewComboBoxColumn)dgvC;
            theColumn.Items.Clear();
            foreach (DataRow item in dt.Rows)
            {
                theColumn.Items.Add(item[0]);
            }
        }

        //private void dgv_Direct_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        //{
        //    bantu.datagrid_OnlyNumber(dgv_Direct, e, new int[] { 1, 2 });
        //}
        public static void datagrid_OnlyNumber(DataGridView dgv, DataGridViewEditingControlShowingEventArgs e, int[] kolom)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Helper.text_OnlyNumberEvent);
            if (kolom.Contains(dgv.CurrentCell.ColumnIndex))
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Helper.text_OnlyNumberEvent);
                }
            }
        }

        //List<String> groupNameList = bantu.getUniqueValuefromDgv(dgv, "dir_operator");
        public static List<string> getUniqueValuefromDgv(DataGridView dgv, String ColumnName)
        {
            var vv = dgv.Rows.Cast<DataGridViewRow>()
                .Where(x => !x.IsNewRow)                   // either..
                .Where(x => x.Cells[ColumnName].Value != null) //..or or both
                .Select(x => x.Cells[ColumnName].Value.ToString())
                .Distinct()
                .ToList();
            return vv;
        }

        public static void MergeRowTextboxColumnEx(DataGridView dgv, int kolom, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleCenter)
        {
            String isi = dgv[kolom, 0].Value.ToString();
            int idxAwal = 0;
            for (int i = 1; i < dgv.RowCount; i++)
            {
                if (!dgv[kolom, i].Value.ToString().Equals(isi))
                {
                    ((DataGridViewTextBoxCellEx)dgv[kolom, idxAwal]).RowSpan = i - idxAwal;
                    ((DataGridViewTextBoxCellEx) dgv[kolom, idxAwal]).Style.Alignment = alignment;
                    idxAwal = i;
                    isi = dgv[kolom, i].Value.ToString();
                }
                else if (i == dgv.RowCount - 1 && idxAwal < i)
                {
                    ((DataGridViewTextBoxCellEx)dgv[kolom, idxAwal]).RowSpan = i - (idxAwal - 1);
                    ((DataGridViewTextBoxCellEx)dgv[kolom, idxAwal]).Style.Alignment = alignment;
                    isi = dgv[kolom, i].Value.ToString();
                }
            }
        }

        public static void MergeRowTextboxColumnEx(DataGridView dgv,int colNumber, int firstRow, int length, DataGridViewContentAlignment alignment = DataGridViewContentAlignment.MiddleCenter)
        {

            ((DataGridViewTextBoxCellEx)dgv[colNumber, firstRow]).RowSpan = length;
            ((DataGridViewTextBoxCellEx)dgv[colNumber, firstRow]).Style.Alignment = alignment;
        }

        //List<String> groupNameList = bantu.getUniqueValuefromDgv(dgv, "dir_operator");
        public static List<string> getUniqueValuefromDataTable(DataTable dt, String ColumnName)
        {
            var vv = dt.AsEnumerable()
                .Where(row => row[ColumnName].ToString().Length > 0)
                .Select(x => x[ColumnName].ToString())
                .Distinct()
                .ToList();
            return vv;
        }

        public static void MergeCellsInRow(DataGridView dgv, int row, int col1, int col2)
        {
            Graphics g = dgv.CreateGraphics();
            Pen p = new Pen(dgv.GridColor);
            Rectangle r1 = dgv.GetCellDisplayRectangle(col1, row, true);
            Rectangle r2 = dgv.GetCellDisplayRectangle(col2, row, true);

            int recWidth = 0;
            string recValue = String.Empty;
            for (int i = col1; i <= col2; i++)
            {
                recWidth += dgv.GetCellDisplayRectangle(i, row, true).Width;
                if (dgv[i, row].Value != null)
                    recValue += dgv[i, row].Value.ToString() + " ";
            }
            Rectangle newCell = new Rectangle(r1.X, r1.Y, recWidth, r1.Height);
            g.FillRectangle(new SolidBrush(dgv.DefaultCellStyle.BackColor), newCell);
            g.DrawRectangle(p, newCell);
            g.DrawString(recValue, dgv.DefaultCellStyle.Font, new SolidBrush(dgv.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
        }

        public static void MergeCellsInColumn(DataGridView dgv, int col, int row1, int row2)
        {
            Graphics g = dgv.CreateGraphics();
            Pen p = new Pen(dgv.GridColor);
            Rectangle r1 = dgv.GetCellDisplayRectangle(col, row1, true);
            Rectangle r2 = dgv.GetCellDisplayRectangle(col, row2, true);

            int recHeight = 0;
            string recValue = String.Empty;
            for (int i = row1; i <= row2; i++)
            {
                recHeight += dgv.GetCellDisplayRectangle(col, i, true).Height;
                if (dgv[col, i].Value != null)
                    recValue += dgv[col, i].Value.ToString() + " ";
            }
            Rectangle newCell = new Rectangle(r1.X, r1.Y, r1.Width, recHeight);
            g.FillRectangle(new SolidBrush(dgv.DefaultCellStyle.BackColor), newCell);
            g.DrawRectangle(p, newCell);
            g.DrawString(recValue, dgv.DefaultCellStyle.Font, new SolidBrush(dgv.DefaultCellStyle.ForeColor), newCell.X + 3, newCell.Y + 3);
        }
    }
}
