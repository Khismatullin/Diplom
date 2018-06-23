using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelObj = Microsoft.Office.Interop.Excel;

namespace Diplom
{
    class ExportExcel : IExport
    {
        private ExcelObj.Application excelApp;
        private string dir;
        private bool IsOpenExportFile = false;

        public ExportExcel(string d, bool o)
        {
            dir = d;
            IsOpenExportFile = o;
            SettingsExportExcel();
        }

        public ExportExcel()
        {
            dir = null;
        }

        public void SettingsExportExcel()
        {
            if(excelApp == null)
                excelApp = new ExcelObj.Application();
        }

        public void ExportData(SortedDictionary<DateTime, double> inputData)
        {
            ExcelObj.Workbook workBook = excelApp.Workbooks.Open(dir);
            excelApp.Columns.ColumnWidth = 15;

            //need be here
            ExcelObj.Worksheet worksheet = workBook.ActiveSheet as ExcelObj.Worksheet;         

            //initialize here, if not selected dirExport
            Task taskDirExport = new Task(() =>
            {
                if (dir == null)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
                    ofd.Title = "Выберите документ для экспорта данных";

                    if (ofd.ShowDialog() != DialogResult.OK)
                        Application.Exit();
                    else
                        dir = ofd.FileName;

                    SettingsExportExcel();
                }
            });
            taskDirExport.Start();

            Task.WaitAll(taskDirExport);
            int i = 1;
            foreach (KeyValuePair<DateTime, double> item in inputData)
            {
                worksheet.Cells[i, 1].Value = item.Key;
                worksheet.Cells[i, 2].Value = item.Value;
                i++;
            }

            workBook.SaveAs(dir);

            //show user
            if(IsOpenExportFile)
                excelApp.Visible = true;
        }

        public void DisposeExport()
        {
            if(excelApp != null)
                excelApp.Quit();
        }
    }
}
