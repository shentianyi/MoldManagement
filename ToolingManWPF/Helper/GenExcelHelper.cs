using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolingManWPF.Utilities;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace ToolingManWPF.Helper
{
    public class GenExcelHelper
    {
        public static bool GenExcel<T>(string filepath, string sheetName, string[] headers, string[] pathes, List<T> items)
        {
            Application excel = new Application();

            excel.Application.Workbooks.Add(true);
            excel.Visible = false;
            excel.DisplayAlerts = false;

            Workbooks books = excel.Workbooks;
            _Workbook book = books.Add(XlWBATemplate.xlWBATWorksheet);
            _Worksheet sheet = book.ActiveSheet;
            try
            {
                System.Reflection.Missing miss = System.Reflection.Missing.Value;

                sheet.Name = sheetName;

                for (int i = 0; i < headers.Length; i++)
                {
                    excel.Cells[1, i + 1] = headers[i];
                }
                Type t = typeof(T);
                for (int i = 0; i < items.Count; i++)
                {
                    for (int j = 0; j < pathes.Length; j++)
                    {
                        object v = t.GetProperty(pathes[j]).GetValue(items[i], null);
                        excel.Cells[2 + i, j + 1] = v == null ? "" : v.ToString();
                    }
                }
                Range range = sheet.Range[sheet.Cells[1, 1], sheet.Cells[items.Count + 1, headers.Length + 1]];
                range.HorizontalAlignment = XlHAlign.xlHAlignLeft;
                sheet.SaveAs(filepath, miss, miss, miss, miss, miss, XlSaveAsAccessMode.xlNoChange, miss, miss, miss);
                book.Close(false, miss, miss);
                books.Close();
                excel.Quit();
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                Marshal.ReleaseComObject(sheet);
                Marshal.ReleaseComObject(book);
                Marshal.ReleaseComObject(books);
                Marshal.ReleaseComObject(excel);
                GC.Collect();
            }
            return true;
        }
    }
}
