using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Diplom
{
    public partial class FormWrapper : Form
    {
        //declaration here for use their in events
        View viewCusum;
        View viewPressure;

        //for include all .dll in one .exe
        ConnectLibrary connectLibrary = new ConnectLibrary();

        public FormWrapper()
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

            ChangeLabels();
        }

        private void HotKeys(object sender, KeyEventArgs e)
        {
            //exit button (ESC)
            if (e.KeyCode.ToString() == "Escape")
                Application.Exit();
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
            //clear past plots if there is
            Clear();

            //protection from fool
            EnableControls(false);

            string importDir = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\" + textBoxImportFileName.Text;
            string exportDir = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\" + textBoxExportFileName.Text;
            IDeclineData declineDataC = null;
            IDeclineData declineDataP = null;
            bool IsOpenExportFile = checkBoxOpenExportFile.Checked;
            double N = Convert.ToDouble(labelN.Text);
            double b = Convert.ToDouble(labelB.Text);
            int k = Convert.ToInt32(textBoxParamK.Text);
            IMethod methodCusum = new MethodCusum(N, b, k);

            //add noise if need
            if (checkBoxNoise.Checked)
            {
                declineDataC = new NoiseDecline();
                declineDataP = new NoiseDecline();
            }

            viewCusum = new View(new ImportExcel(importDir), declineDataC, methodCusum, new ChartOxiPlot(this, new Point(520, 40), new Point(500, 400), methodCusum.NameForSeries), new ExportExcel(exportDir, IsOpenExportFile));
            viewPressure = new View(new ImportExcel(importDir), declineDataP, null, new ChartOxiPlot(this, new Point(10, 40), new Point(500, 400), null), new ExportExcel(exportDir, IsOpenExportFile));

            //for not blocking UI-thread
            Task[] tasksView = new Task[2]
            {
                new Task(()=>{ viewPressure.ShowResults(); }),
                 new Task(()=>{ viewCusum.ShowResults(); }),
            };
            foreach (var item in tasksView)
                item.Start();

            //wait ending work tasks
            Task taskWait = new Task(() =>
            {
                Task.WaitAll(tasksView);
                EnableControls(true);
            });
            taskWait.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Clear();
        }

        private void buttonExportData_Click(object sender, EventArgs e)
        {
            //protection for fools
            EnableControls(false);

            Task[] taskExport = new Task[1]
            {
                new Task(()=>
                {
                    if (viewCusum != null)
                        viewCusum.ExportData();
                })
            };
            foreach (var item in taskExport)
                item.Start();

            Task taskWait = new Task(() =>
            {
                Task.WaitAll(taskExport);
                EnableControls(true);
            });
            taskWait.Start();
        }

        private void Clear()
        {
            if(viewCusum != null)
                viewCusum.DisposeMaketResource();

            if(viewPressure != null)
                viewPressure.DisposeMaketResource();
        }

        private void EnableControls(bool enable)
        {
            if (enable)
            {
                checkBoxNoise.Enabled = true;
                textBoxImportFileName.Enabled = true;
                buttonImportData.Enabled = true;

                checkBoxOpenExportFile.Enabled = true;
                textBoxExportFileName.Enabled = true;
                buttonExportData.Enabled = true;

                trackBarMethodCusumParamN.Enabled = true;
                trackBarMethodCusumParamB.Enabled = true;
                textBoxParamK.Enabled = true;
            }
            else
            {
                checkBoxNoise.Enabled = false;
                textBoxImportFileName.Enabled = false;
                buttonImportData.Enabled = false;

                checkBoxOpenExportFile.Enabled = false;
                textBoxExportFileName.Enabled = false;
                buttonExportData.Enabled = false;

                trackBarMethodCusumParamN.Enabled = false;
                trackBarMethodCusumParamB.Enabled = false;
                textBoxParamK.Enabled = false;
            }
        }

        private void ChangeLabels()
        {
            labelN.Text = (Convert.ToDouble(trackBarMethodCusumParamN.Value) / 100).ToString();
            labelB.Text = (Convert.ToDouble(trackBarMethodCusumParamB.Value) / 1000).ToString();
        }

        private void trackBarMethodCusumParamN_ValueChanged(object sender, EventArgs e)
        {
            ChangeLabels();
        }

        private void trackBarMethodCusumParamB_ValueChanged(object sender, EventArgs e)
        {
            ChangeLabels();
        }
    }
}