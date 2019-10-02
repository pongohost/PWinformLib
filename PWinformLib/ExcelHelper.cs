using System;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;

namespace PWinformLib
{
    public class ExcelHelper
    {
        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true, int sheetNumber = 1)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                ExcelWorksheet ws = pck.Workbook.Worksheets[sheetNumber];
                return GetDTFromExcelWithPos(ws,ws.Dimension.Start.Row,ws.Dimension.Start.Column,hasHeader);
            }
        }

        public static DataTable GetDataTableFromExcel(string path,int startRow,int startCol, bool hasHeader = true, int sheetNumber = 1)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                ExcelWorksheet ws = pck.Workbook.Worksheets[sheetNumber];
                return GetDTFromExcelWithPos(ws, startRow, startCol, hasHeader);
            }
        }

        public static DataSet GetDataSetFromExcel(string path, bool hasHeader = true)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                DataSet ds = new DataSet();
                foreach (ExcelWorksheet itm in pck.Workbook.Worksheets)
                {
                    DataTable dt = GetDTFromExcelWithPos(
                        itm,
                        itm.Dimension.Start.Row,
                        itm.Dimension.Start.Column,
                        hasHeader);
                    ds.Tables.Add(dt);
                }
                return ds;
            }
        }

        public static DataSet GetDataSetFromExcel(string path,int startRow,int startCol, bool hasHeader = true)
        {
            using (var pck = new ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                DataSet ds = new DataSet();
                foreach (ExcelWorksheet itm in pck.Workbook.Worksheets)
                {
                    DataTable dt = GetDTFromExcelWithPos(
                        itm,
                        startRow,
                        startCol,
                        hasHeader);
                    ds.Tables.Add(dt);
                }
                return ds;
            }
        }

        private static DataTable GetDTFromExcelWithPos(ExcelWorksheet ws, int rowNumBer, int colNumber ,bool hasHeader = true,int sheetNumber=1)
        {
            DataTable tbl = new DataTable();

            foreach (var firstRowCell in ws.Cells[rowNumBer,
                colNumber, rowNumBer, ws.Dimension.End.Column])
            {
                tbl.Columns.Add(hasHeader
                    ? firstRowCell.Text
                    : String.Format("Column {0}", firstRowCell.Start.Column));
            }

            var startRow = hasHeader ? rowNumBer + 1 : rowNumBer;
            for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
            {
                var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                DataRow row = tbl.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - ws.Dimension.Start.Column] = cell.Text;
                }
            }
            return tbl;
        }

        public static void ExcelFromDataSet(DataSet ds, string path, bool includeHeader = true,
            string[] header = null)
        {
            var existingFile = new FileInfo(path);
            if (existingFile.Exists)
                existingFile.Delete();

            using (var package = new ExcelPackage(existingFile))
            {
                foreach (DataTable dt in ds.Tables)
                {
                    var ws = package.Workbook.Worksheets.Add(dt.TableName);

                    if (includeHeader && header == null)
                        ws.Cells[1, 1].LoadFromDataTable(dt, true);
                    else if (includeHeader)
                    {
                        int i = 1;
                        foreach (string itm in header)
                        {
                            ws.Cells[1, i].Value = itm;
                            i++;
                        }

                        ws.Cells[2, 1].LoadFromDataTable(dt, false);
                    }
                    else
                        ws.Cells[1, 1].LoadFromDataTable(dt, false);

                    ws.Cells[1, 1, dt.Rows.Count + 1, dt.Columns.Count].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    ws.Cells[1, 1, dt.Rows.Count + 1, dt.Columns.Count].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    ws.Cells[1, 1, dt.Rows.Count + 1, dt.Columns.Count].Style.Border.Right.Style =
                        ExcelBorderStyle.Thin;
                    ws.Cells[1, 1, dt.Rows.Count + 1, dt.Columns.Count].Style.Border.Bottom.Style =
                        ExcelBorderStyle.Thin;
                    if (includeHeader)
                    {
                        ws.Cells[1, 1, 1, dt.Columns.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[1, 1, 1, dt.Columns.Count].Style.Fill.BackgroundColor.SetColor(Color.Gainsboro);
                    }
                }

                package.Save();
            }

        }

        public static void ExcelFromDataTable(DataTable dt, string path, bool includeHeader = true,
            string[] header = null)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dt);
            ExcelFromDataSet(ds,path,includeHeader,header);
        }
        /*public string void ExcelReport()
        {

        }*/
    }
}
