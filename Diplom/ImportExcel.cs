using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ExcelObj = Microsoft.Office.Interop.Excel;

namespace Diplom
{
    class ImportExcel : IImport
    {
        private ExcelObj.Application excelApp;
        private ExcelObj.Range ShtRange;
        private SortedDictionary<DateTime, double> allReadDatesPressures;
        private string dir;

        public ImportExcel()
        {
            SettingsImportExcel();
        }

        public ImportExcel(string d)
        {
            dir = d;
            SettingsImportExcel();
        }

        public void SettingsImportExcel()
        {
            if(excelApp == null)
                excelApp = new ExcelObj.Application();

            allReadDatesPressures = new SortedDictionary<DateTime, double>();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xls;*.xlsx";
            ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            ofd.Title = "Выберите документ для импорта данных";

            if (dir == null)
            {
                //user selected FileName
                if (ofd.ShowDialog() != DialogResult.OK)
                    Application.Exit();
            }
            else
                ofd.FileName = dir;
            
            //open your excel file
            ExcelObj.Workbook workbook = excelApp.Workbooks.Open(ofd.FileName);

            //choose the first sheet(page)
            ExcelObj.Worksheet NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);

            //select only used range(all strings)
            ShtRange = NwSheet.UsedRange;
        }

        public int GetCount()
        {
            return ShtRange.Rows.Count;
        }

        public SortedDictionary<DateTime, double> ImportData(int Rnum)
        {
            //for store read values([0] - date, [1] - pressure)
            double[] readDatePressure = new double[2];

            for (int Cnum = 1; Cnum <= ShtRange.Columns.Count; Cnum++)
            {
                if ((ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2 != null)
                    readDatePressure[Cnum - 1] = Convert.ToDouble((ShtRange.Cells[Rnum, Cnum] as ExcelObj.Range).Value2.ToString());
            }

            //specifics Excel
            DateTime fromDoubleToDate = new DateTime(1899, 12, 30).AddDays(Convert.ToDouble(readDatePressure[0]));

            allReadDatesPressures.Add(fromDoubleToDate, readDatePressure[1]);

            return allReadDatesPressures;
        }

        public void DisposeImport()
        {
            if (excelApp != null)
                excelApp.Quit();
        }
    }
}
