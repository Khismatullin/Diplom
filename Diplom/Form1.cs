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
using OxyPlot.WindowsForms;
using System.Reflection;
using ExcelObj = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Threading;

namespace Diplom
{
    public partial class Form1 : Form
    {
        Maket maketCusum;
        Maket maketPressure;

        public Form1()
        {
            InitializeComponent();
        }

        public void AddControlPlot(Control control)
        {
            Controls.Add(control);
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

        interface IDataLoader
        {
            Dictionary<DateTime, double> LoadData(int value);
            int GetCount();
            void DisposeResource();
        }

        class LoaderExcel : IDataLoader
        {
            private ExcelObj.Application excelApp;
            private ExcelObj.Range ShtRange;
            private Dictionary<DateTime, double> allDatesPressures;
            private string dir;

            public LoaderExcel()
            {
                Settings();
            }

            public LoaderExcel(string d)
            {
                dir = d;
                Settings();
            }

            public void Settings()
            {
                excelApp = new ExcelObj.Application();
                allDatesPressures = new Dictionary<DateTime, double>();

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
                workbook = excelApp.Workbooks.Open(ofd.FileName, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value);

                //choose the first sheet(page)
                NwSheet = (ExcelObj.Worksheet)workbook.Sheets.get_Item(1);

                //select only used range(all strings)
                ShtRange = NwSheet.UsedRange;
            }

            public int GetCount()
            {
                return ShtRange.Rows.Count;
            }

            public Dictionary<DateTime, double> LoadData(int Rnum)
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
            void VisualizeData(Dictionary<DateTime, double> loadData, bool marker);
        }

        public class ChartOxiPlot : IChart
        {
            public PlotView Plot = new PlotView();
            public LineSeries lineSeries;
            public LineSeries markerSeries;
            private Dictionary<DateTime, double> limitValues;
            private int counterVal;

            public ChartOxiPlot(Form1 link, Point locPlot, Point sizePlot, string titleChart, Color color)
            {
                link.AddControlPlot(Plot);

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

                limitValues = new Dictionary<DateTime, double>();
                counterVal = 0;
            }

            public void VisualizeData(Dictionary<DateTime, double> loadData, bool marker)
            {
                DataPoint dp = new DataPoint(DateTimeAxis.ToDouble(loadData.Keys.Last()), loadData.Values.Last());

                //save values for further delete their
                counterVal++;
                limitValues.Add(loadData.Keys.Last(), loadData.Values.Last());

                //show only 50 values
                if (counterVal >= 50)
                    lineSeries.Points.Remove(new DataPoint(DateTimeAxis.ToDouble(limitValues.Keys.First()), limitValues.Values.First()));

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
            Dictionary<DateTime, double> UseMethod(Dictionary<DateTime, double> allValues);
            bool StopCondition { get; }
            string StopMessage { get; }
        }

        class MethodCusum : IMethod
        {
            private double N;
            double b;
            private int k;
            private double[] x;
            private Dictionary<DateTime, double> timeY;
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
                timeY = new Dictionary<DateTime, double>();

                StopCondition = false;
            }

            public Dictionary<DateTime, double> UseMethod(Dictionary<DateTime, double> allDatesPressures)
            {
                double average = 0.0;
                double estKsi = 0.0;
                double ksi = 0.0;

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

        class Maket
        {
            private IDataLoader dataLoader;
            private IMethod method;
            private IChart graphics;
            public int Count;

            public Maket(IDataLoader dl, IMethod m, IChart gr)
            {
                dataLoader = dl;
                method = m;
                graphics = gr;

                Count = dataLoader.GetCount();
            }

            public Dictionary<DateTime, double> Load(int i)
            {
                return dataLoader.LoadData(i);
            }

            public Dictionary<DateTime, double> UseMethod(Dictionary<DateTime, double> loadVal)
            {
                return method.UseMethod(loadVal);
            }

            public bool Output(Dictionary<DateTime, double> procVal)
            {
                if (procVal != null)
                {
                    if (method.StopCondition)
                    {
                        graphics.VisualizeData(procVal, true);
                        MessageBox.Show(method.StopMessage, "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }
                    else
                        graphics.VisualizeData(procVal, false);
                }

                return true;
            }

            public void DisposeMaketResource()
            {
                dataLoader.DisposeResource();
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

        private void buttonLoadData_Click(object sender, EventArgs e)
        {
            string directory = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\data3.xlsx";
            IDataLoader loaderExcel = new LoaderExcel(directory);

            maketCusum = new Maket(loaderExcel, new MethodCusum(), new ChartOxiPlot(this, new Point(10, 40), new Point(500, 400), "y(t)", Color.DarkMagenta));
            maketPressure = new Maket(new LoaderExcel(directory), new MethodCusum(), new ChartOxiPlot(this, new Point(530, 40), new Point(500, 400), "P(t)", Color.Blue));

            Task task = new Task(() =>
            {
                //read and visualize adding by 1 value
                for (int i = 1; i < maketCusum.Count; i++)
                {
                    var loadPressure = maketCusum.Load(i);
                    if (maketCusum.Output(maketCusum.UseMethod(loadPressure)) == false)
                        break;

                    maketPressure.Output(loadPressure);
                }
            });
            task.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (maketCusum != null)
                maketCusum.DisposeMaketResource();

            if (maketPressure != null)
                maketPressure.DisposeMaketResource();
        }
    }
}