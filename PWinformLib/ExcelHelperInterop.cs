using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PWinformLib.UI;
using Excel = Microsoft.Office.Interop.Excel;

namespace PWinformLib
{
    public class ExcelHelperInterop
    {
        // ExcelHelper.ExcelMultiSheetToDS("filenya.xls","1|2|3")
        public static DataSet ExcelMultiSheetToDS(String filename, String sheet)
        {
            DataSet dataSet = new DataSet();
            int SheetX = 0;
            String[] arrSheet = sheet.Split('|');
            foreach (String sheetnya in arrSheet)
            {
                int.TryParse(sheetnya, out SheetX);
                DataTable dt = ExcelToDt(filename, SheetX);
                dataSet.Tables.Add(dt);
            }
            return dataSet;
        }

        public static DataTable ExcelToDt(String fileName, int sheet = 1)
        {
            DataTable dt = new DataTable();
            Excel.Application xlApp = new Excel.Application();
            //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName, 0, true, 5, "", "", true,
                Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheet];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            object[,] value = xlRange.Value;
            int colCount = value.GetLength(1);
            int rowCount = value.GetLength(0);
            //add column
            String[] judulKolom = new String[colCount + 1];
            for (int x = 1; x <= colCount; x++)
            {
                if (xlRange.Cells[1, x] != null && xlRange.Cells[1, x].Value2 != null)
                {
                    dt.Columns.Add((string)value[1, x], typeof(String));
                    judulKolom[x] = (string)value[1, x];
                }
            }
            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
            DataRow dataRow;
            for (int i = 2; i <= rowCount; i++)
            {
                dataRow = dt.NewRow();
                for (int j = 1; j <= colCount; j++)
                {
                    if (value[i, j] != null)
                        dataRow[judulKolom[j]] = value[i, j];
                }
                dt.Rows.Add(dataRow);
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);
            return dt;
        }

        public static Task AsyncDgvtoExcel(DataGridView dgv)
        {
            return Task.Run(() =>
            {
                DgvtoExcel(dgv);
            });

        }
        
        public static void DgvtoExcel(DataGridView dgv)
        {
            if (dgv.Rows.Count > 0)
            {
                string filename = "";
                SaveFileDialog SV = new SaveFileDialog();
                SV.Filter = "EXCEL FILES|*.xlsx;*.xls";
                DialogResult result = SV.ShowDialog();

                if (result == DialogResult.OK)
                {
                    filename = SV.FileName;
                    bool multiselect = dgv.MultiSelect;
                    dgv.MultiSelect = true;
                    dgv.SelectAll();
                    dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
                    Clipboard.SetDataObject(dgv.GetClipboardContent());
                    var results = System.Convert.ToString(Clipboard.GetData(DataFormats.Text));
                    dgv.ClearSelection();
                    dgv.MultiSelect = multiselect;
                    Excel.Application XCELAPP = null;
                    Excel.Workbook XWORKBOOK = null;
                    Excel.Worksheet XSHEET = null;
                    object misValue = Missing.Value;
                    XCELAPP = new Excel.Application();
                    XWORKBOOK = XCELAPP.Workbooks.Add(misValue);
                    XCELAPP.DisplayAlerts = false;
                    XCELAPP.Visible = false;
                    XSHEET = XWORKBOOK.ActiveSheet;
                    XSHEET.Paste();
                    XWORKBOOK.SaveAs(filename, Excel.XlFileFormat.xlOpenXMLWorkbook);
                    XWORKBOOK.Close(false);
                    XCELAPP.Quit();
                    try
                    {
                        Marshal.ReleaseComObject(XSHEET);
                        Marshal.ReleaseComObject(XWORKBOOK);
                        Marshal.ReleaseComObject(XCELAPP);
                    }
                    catch(Exception ex)
                    {
                        notification.Error("Export Data Gagal",$"{ex.Message}");
                    }
                    notification.Ok("Export Berhasil",$"Data di Simpan di {SV.FileName}");
                }
            }
        }

    }
}
