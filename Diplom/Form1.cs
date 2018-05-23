using OxyPlot.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using OxyPlot.Annotations;
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Diplom
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(int hWnd, ref int lpdwProcessId);

        //declaration here for use their in events
        View viewCusum;
        View viewPressure;
        Task[] tasksView;

        //for include all .dll in one .exe
        ConnectLibrary connectLibrary = new ConnectLibrary();

        public Form1()
        {           
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int x, y, w, z;
            ThreadPool.GetMinThreads(out y, out x);
            ThreadPool.SetMinThreads(100000, x);
            ThreadPool.GetMaxThreads(out w, out z);
            ThreadPool.SetMaxThreads(300000, z);

            //handler of hot keys (need set propreties Form.KeyPreview as "true")
            this.KeyUp += new KeyEventHandler(HotKeys);
        }

        private void HotKeys(object sender, KeyEventArgs e)
        {
            //exit button (ESC)
            if (e.KeyCode.ToString() == "Escape")
                Application.Exit();
        }

        interface IDataImport
        {
            SortedDictionary<DateTime, double> LoadData(int value);
            SortedDictionary<DateTime, double> GetValues();
            int GetCount();
            void DisposeResource();
        }

        class ImportExcel : IDataImport
        {
            private ExcelObj.Application excelApp;
            private ExcelObj.Range ShtRange;
            private SortedDictionary<DateTime, double> allDatesPressures;
            private string dir;

            public ImportExcel()
            {
                Settings();
            }

            public ImportExcel(string d)
            {
                dir = d;
                Settings();
            }

            public void Settings()
            {
                excelApp = new ExcelObj.Application();
                allDatesPressures = new SortedDictionary<DateTime, double>();

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.DefaultExt = "*.xls;*.xlsx";
                ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
                ofd.Title = "Выберите документ для загрузки данных";

                if (dir == null)
                {
                    //user selected FileName
                    if (ofd.ShowDialog() != DialogResult.OK)
                        Application.Exit();
                }
                else
                    ofd.FileName = dir;

                ExcelObj.Workbook workbook;
                ExcelObj.Worksheet NwSheet;

                //open your excel file
                workbook = excelApp.Workbooks.Open(ofd.FileName);

                //choose the first sheet(page)
                NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);

                //select only used range(all strings)
                ShtRange = NwSheet.UsedRange;
            }

            public int GetCount()
            {
                return ShtRange.Rows.Count;
            }

            public SortedDictionary<DateTime, double> GetValues()
            {
                return allDatesPressures;
            }

            public SortedDictionary<DateTime, double> LoadData(int Rnum)
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

                allDatesPressures.Add(fromDoubleToDate, readDatePressure[1]);

                return allDatesPressures;
            }

            public void DisposeResource()
            {
                excelApp.Quit();
                excelApp.Quit();
            }
        }

        interface IChart
        {
            void VisualizeData(SortedDictionary<DateTime, double> loadData, bool marker);
        }

        public class ChartOxiPlot : IChart
        {
            private PlotView Plot = new PlotView();
            private LineSeries lineSeries;
            private LineSeries markerSeries;
            private SortedDictionary<DateTime, double> limitValues;
            private int counterVal;

            public ChartOxiPlot(Form1 linkForm1, Point locPlot, Point sizePlot, string titleChart, Color color)
            {
                linkForm1.Controls.Add(Plot);
                Plot.Location = new System.Drawing.Point(locPlot.X, locPlot.Y);
                Plot.Size = new System.Drawing.Size(sizePlot.X, sizePlot.Y);

                Plot.Model = new PlotModel
                {
                    PlotType = PlotType.XY,
                    Background = OxyColor.FromRgb(255, 255, 255),
                    TextColor = OxyColor.FromRgb(0, 0, 0)
                };

                DateTimeAxis xAxis = new DateTimeAxis
                {
                    Position = AxisPosition.Bottom,
                    StringFormat = "HH:mm",
                    Title = "Время",
                    MinorIntervalType = DateTimeIntervalType.Days,
                    IntervalType = DateTimeIntervalType.Days,
                    MajorGridlineStyle = LineStyle.Solid,
                    MinorGridlineStyle = LineStyle.None,
                };
                LinearAxis yAxis = new LinearAxis
                {
                    Position = AxisPosition.Left,
                };
                Plot.Model.Axes.Add(xAxis);
                Plot.Model.Axes.Add(yAxis);

                //by default
                lineSeries = new LineSeries
                {
                    Title = titleChart,
                    StrokeThickness = 1,
                    Color = OxyColor.FromRgb(color.R, color.G, color.B)//pink
                };
                Plot.Model.Series.Add(lineSeries);

                markerSeries = new LineSeries
                {
                    Title = "",
                    StrokeThickness = 1,
                    MarkerFill = OxyColors.Red,
                    MarkerType = MarkerType.Circle,
                    MarkerSize = 5,
                    Color = OxyColor.FromRgb(255, 255, 255)
                };
                Plot.Model.Series.Add(markerSeries);

                limitValues = new SortedDictionary<DateTime, double>();
                counterVal = 0;
            }

            public void VisualizeData(SortedDictionary<DateTime, double> loadData, bool marker)
            {
                DataPoint dp = new DataPoint(DateTimeAxis.ToDouble(loadData.Keys.Last()), loadData.Values.Last());

                //save values for further delete their
                counterVal++;
                limitValues.Add(loadData.Keys.Last(), loadData.Values.Last());

                //show only 1200 values
                if (counterVal >= 1200)
                {
                    lineSeries.Points.Remove(new DataPoint(DateTimeAxis.ToDouble(limitValues.Keys.First()), limitValues.Values.First()));
                    limitValues.Remove(limitValues.Keys.First());
                }

                lineSeries.Points.Add(dp);

                //marker 1 value
                if (marker == true)
                    markerSeries.Points.Add(dp);

                //for update chart
                Plot.InvalidatePlot(true);
            }
        }

        interface IMethod
        {
            SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> allValues);
            bool StopCondition { get; }
            string StopMessage { get; }
        }

        class MethodCusum : IMethod
        {
            private double N;
            private double b;
            private int k;
            private double[] x;
            private double average;
            private double estKsi;
            private double ksi;
            private SortedDictionary<DateTime, double> timeY;
            public bool StopCondition { get; set; }
            public string StopMessage { get; set; }

            public MethodCusum()
            {
                //critical value
                N = -10.01;

                //max measurement error
                b = 0.025;

                //count measurement
                k = 20;

                //x
                x = new double[k];

                //y(t)
                timeY = new SortedDictionary<DateTime, double>();

                StopCondition = false;
            }

            public SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> allDatesPressures)
            {
                //at first X = {P(ts), P(ts+1), ... P(tf)}
                if (allDatesPressures.Count == k)
                {
                    int ind = 0;
                    foreach (var item in allDatesPressures)
                    {
                        x[ind] = item.Value;
                        ind++;
                    }

                    //y[0] = 0
                    timeY.Add(allDatesPressures.Keys.Last(), 0);

                    return null;
                }
                //trend, CUSUM
                else if (allDatesPressures.Count > k)
                {
                    //average(TREND) from set X
                    average = x.Average();

                    //remove trend E^(tf + 1) = P(tf + 1) - f(tf + 1)                     
                    estKsi = allDatesPressures.Values.Last() - average;

                    //find E[tf + 1] = E^[tf + 1] + b
                    ksi = estKsi + b;

                    //CUSUM -- y[tf + 1] = y[tf] + ksi
                    timeY.Add(allDatesPressures.Keys.Last(), Math.Min(0, timeY.Values.Last() + ksi));

                    //check on d(y[t]) = I(y[t] < N), if d(y[t]) = 1 then STOP
                    if (timeY.Values.Last() < N)
                    {
                        StopCondition = true;

                        //save message
                        DateTime showDate = allDatesPressures.Keys.Last();
                        StopMessage = showDate.ToShortDateString() + " в " + showDate.TimeOfDay + " был обнаружен момент разладки!";

                        return timeY;
                    }
                    else
                    {
                        //replace X = {P(ts), P(ts+1), ... P(tf)}
                        int ind = 0;
                        while (ind < x.Length - 1)
                        {
                            x[ind] = x[ind + 1];
                            ind++;
                        }
                        if (ind == k - 1)
                            x[ind] = allDatesPressures.Values.Last();
                    }

                    return timeY;
                }
                else
                    return null;
            }
        }

        interface IDeclineData
        {
            SortedDictionary<DateTime, double> AddDecline(SortedDictionary<DateTime, double> loadData);
        }

        class NoiseDecline : IDeclineData
        {
            private Random randOne;
            private Random randTwo;
            private double v1;
            private double v2;
            private double r;
            private double f;
            private SortedDictionary<DateTime, double> noiseData;

            public NoiseDecline()
            {
                randOne = new Random();
                randTwo = new Random();
                noiseData = new SortedDictionary<DateTime, double>();
            }

            public SortedDictionary<DateTime, double> AddDecline(SortedDictionary<DateTime, double> loadData)
            {
                //random noise with normal distribution
                r = 1;
                while(r >= 1)
                {
                    v1 = randOne.NextDouble();
                    v2 = randTwo.NextDouble();
                    r = v1 * v1 + v2 * v2;
                }
                f = Math.Sqrt(-2 * Math.Log(r)/2);
                var noise = loadData.Values.Last() + v1*f;

                noiseData.Add(loadData.Keys.Last(), noise);
                return noiseData;
            }
        }

        interface IDataExport
        {
            void Export(SortedDictionary<DateTime, double> d);
        }

        class ExportExcel : IDataExport
        {
            private ExcelObj.Application excelApp;
            private ExcelObj.Workbook workBook;
            private ExcelObj.Worksheet worksheet;
            private string dirExport;

            public ExportExcel(string d)
            {
                dirExport = d;
                excelApp = new ExcelObj.Application();
                workBook = excelApp.Workbooks.Open(dirExport);
                worksheet = workBook.ActiveSheet as ExcelObj.Worksheet;
            }

            public void Export(SortedDictionary<DateTime, double> inputData)
            {
                int i = 1;
                foreach(KeyValuePair<DateTime, double> item in inputData)
                {
                    worksheet.Cells[i, 1].Value = item.Key;
                    worksheet.Cells[i, 2].Value = item.Value;
                    i++;
                }

                workBook.SaveAs(dirExport);
                CloseExcel();
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
        }

        class View
        {
            private IDataImport dataImport;
            private IDeclineData declineData;
            private IMethod method;
            private IChart chart;
            private IDataExport dataExport;
            private SortedDictionary<DateTime, double> dataView;

            public View(IDataImport dl, IDeclineData d, IMethod m, IChart c, IDataExport e)
            {
                dataImport = dl;
                declineData = d;
                method = m;
                chart = c;
                dataExport = e;

                dataView = new SortedDictionary<DateTime, double>();
            }

            public SortedDictionary<DateTime, double> Load(int i)
            {
                return dataImport.LoadData(i);
            }

            public SortedDictionary<DateTime, double> Decline(SortedDictionary<DateTime, double> loadData)
            {
                //because consistently transfer result along chain
                if (declineData != null)
                    return declineData.AddDecline(loadData);
                else
                    return loadData;
            }

            public SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> loadVal)
            {
                //because consistently transfer result along chain
                if (method != null)
                {
                    var res = method.UseMethod(loadVal);
                    
                        Task task = new Task(() => 
                        {
                            if (res != null)
                                dataView.Add(res.Keys.Last(), res.Values.Last());
                        });
                        task.Start();

                    return res;
                }
                else
                    return loadVal;
            }

            public bool Output(SortedDictionary<DateTime, double> procVal)
            {
                if (procVal != null)
                {
                    if (method != null && method.StopCondition)
                    {
                        chart.VisualizeData(procVal, true);
                        MessageBox.Show(method.StopMessage, "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else
                        chart.VisualizeData(procVal, false);
                }

                return true;
            }

            public void Show()
            {
                //read and visualize adding by 1 value
                for (int i = 1; i < dataImport.GetCount(); i++)
                {
                    if (Output(UseMethod(Decline(Load(i)))) == false)
                        break;
                }
            }

            public void ExportData()
            {
                dataExport.Export(dataView);
            }

            public void DisposeMaketResource()
            {
                dataImport.DisposeResource();
            }
        }

        private void информацияОСоздателеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Тема ВКР: Программное обеспечение для обнаружения утечек нефтепродуктов в трубопроводе\n" +
                "Выполнил:  Хисматуллин А.И. ПРО-409\n" +
                "Руководитель:  Ризванов Д.А.\n" +
                "Рецензент:  Aблеев В.Р.\n"
                , "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information
           );
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonImportData_Click(object sender, EventArgs e)
        {
            //protection from fool
            checkBoxNoise.Enabled = false;
            buttonImportData.Enabled = false;
            buttonExportData.Enabled = false;

            string importDir = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\data2.xlsx";
            string exportDir = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\results.xlsx";
            IDeclineData declineDataC = null;
            IDeclineData declineDataP = null;

            //add noise if need
            if (checkBoxNoise.Checked)
            {
                declineDataC = new NoiseDecline();
                declineDataP = new NoiseDecline();
            }

            viewCusum = new View(new ImportExcel(importDir), declineDataC, new MethodCusum(), new ChartOxiPlot(this, new Point(10, 40), new Point(500, 400), "y(t)", Color.DarkMagenta), new ExportExcel(exportDir));
            viewPressure = new View(new ImportExcel(importDir), declineDataP, null, new ChartOxiPlot(this, new Point(530, 40), new Point(500, 400), "P(t)", Color.Blue), new ExportExcel(exportDir));

            //for not blocking UI-thread
            tasksView = new Task[2]
            {
                new Task(()=>{ viewPressure.Show(); }),
                 new Task(()=>{ viewCusum.Show(); }),
            };
            foreach (var item in tasksView)
                item.Start();

            //wait ending work tasks
            Task taskWait = new Task(()=>
            {
                Task.WaitAll(tasksView);
                checkBoxNoise.Enabled = true;
                buttonImportData.Enabled = true;
                buttonExportData.Enabled = true;
            });
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (viewCusum != null)
                viewCusum.DisposeMaketResource();

            if (viewPressure != null)
                viewPressure.DisposeMaketResource();
        }

        private void buttonExportData_Click(object sender, EventArgs e)
        {
            buttonExportData.Enabled = false;
            if (viewCusum != null)
                viewCusum.ExportData();

            buttonExportData.Enabled = true;
        }
    }
}