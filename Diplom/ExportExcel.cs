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
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        private ExcelObj.Application excelApp;
        private ExcelObj.Workbook workBook;
        private ExcelObj.Worksheet worksheet;
        private string dirExport;
        private bool IsOpenExportFile = false;

        public ExportExcel(string d, bool o)
        {
            dirExport = d;
            IsOpenExportFile = o;
            Settings();
        }

        public ExportExcel()
        {
            dirExport = null;
        }

        public void Settings()
        {
            if(excelApp == null)
                excelApp = new ExcelObj.Application();
        }

        public void Export(SortedDictionary<DateTime, double> inputData)
        {            
            workBook = excelApp.Workbooks.Open(dirExport);
            excelApp.Columns.ColumnWidth = 15;

            //need be here
            worksheet = workBook.ActiveSheet as ExcelObj.Worksheet;         

            //initialize here, if not selected dirExport
            Task taskDirExport = new Task(() =>
            {
                if (dirExport == null)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
                    ofd.Title = "Выберите документ для экспорта данных";

                    if (ofd.ShowDialog() != DialogResult.OK)
                        Application.Exit();
                    else
                        dirExport = ofd.FileName;

                    Settings();
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

            workBook.SaveAs(dirExport);

            //show user
            if(IsOpenExportFile)
                excelApp.Visible = true;
        }

        private void CloseExcel()
        {
            if (excelApp != null)
            {
                int excelProcessId = -1;
                GetWindowThreadProcessId(excelApp.Hwnd, ref excelProcessId);

                Marshal.ReleaseComObject(worksheet);
                workBook.Close();
                Marshal.ReleaseComObject(workBook);
                excelApp.Quit();
                Marshal.ReleaseComObject(excelApp);

                excelApp = null;
                try
                {
                    Process process = Process.GetProcessById(excelProcessId);
                    process.Kill();
                }
                finally { }
            }
        }

        public void DisposeResource()
        {
            if(excelApp != null)
                excelApp.Quit();
        }
    }
}
