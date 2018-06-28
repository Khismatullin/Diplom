using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace Diplom
{
    public partial class FormWrapper : Form
    {
        //declaration here for use their in events
        View viewModifyCUSUM;
        View viewPressure;

        //for include all .dll in one .exe
        ConnectLibrary connectLibrary = new ConnectLibrary();

        public FormWrapper()
        {           
            InitializeComponent();
        }

        private void FormWrapper_Load(object sender, EventArgs e)
        {
            int x, y, w, z;
            ThreadPool.GetMinThreads(out y, out x);
            ThreadPool.SetMinThreads(1000, x);
            ThreadPool.GetMaxThreads(out w, out z);
            ThreadPool.SetMaxThreads(3000, z);

            //handler of hot keys (need set propreties Form.KeyPreview as "true")
            KeyDown += new KeyEventHandler(HotKeys);

            ChangeLabels();
        }

        private void HotKeys(object sender, KeyEventArgs e)
        {
            //exit button (ESC)
            if (e.KeyCode.ToString() == "Escape")
                Application.Exit();

            //import data from file (CTRL + O)
            if (e.Control && e.KeyCode == Keys.O)
                импортироватьДанныеИзФайлаToolStripMenuItem_Click(sender, e);

            //export data from file (CTRL + S)
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.S)
                экспортироватьРезультатыВФайлToolStripMenuItem_Click(sender, e);
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

        string CheckDirOnExist(string s, Control c)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.FileName = s;

            if (!File.Exists(ofd.FileName))
            {
                MessageBox.Show("Файл " + ofd.SafeFileName + " не найден в данной директории!", "Информирование пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (ofd.ShowDialog() != DialogResult.OK)
                    ofd.ShowDialog();

                c.Text = ofd.SafeFileName;
                return ofd.FileName;
            }
            else
                return s;            
        }

        static int Factorial(int x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }

        private void buttonImportData_Click(object sender, EventArgs e)
        {
            //clear past plots if there is
            Clear();

            //protection from fool
            EnableControls(false);

            string importDir = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\" + textBoxImportFileName.Text;
            string exportDir = "D:\\MyYandexDisk\\YandexDisk\\CUSUM\\Practice2017\\" + textBoxExportFileName.Text;

            //check ahead
            importDir = CheckDirOnExist(importDir, textBoxImportFileName);
            exportDir = CheckDirOnExist(exportDir, textBoxExportFileName);

            IDeclineData declineDataC = null;
            IDeclineData declineDataP = null;
            bool IsOpenExportFile = checkBoxOpenExportFile.Checked;
            double N = Convert.ToDouble(labelN.Text);
            double b = Convert.ToDouble(labelB.Text);
            int k = Convert.ToInt32(textBoxParamK.Text);
            IMethod methodModifyCUSUM = new MethodModifyCUSUM(N, b, k);

            //add noise if need
            if (checkBoxNoise.Checked)
            {
                declineDataC = new DeclineNoise();
                declineDataP = new DeclineNoise();
            }
            
            viewModifyCUSUM = new View(new ImportExcel(importDir), declineDataC, methodModifyCUSUM, new EvaluationPoisson(), new ChartOxiPlot(this, new Point(520, 40), new Point(500, 400), methodModifyCUSUM.NameForSeries), new ExportExcel(exportDir, IsOpenExportFile));
            viewPressure = new View(new ImportExcel(importDir), declineDataP, null, null, new ChartOxiPlot(this, new Point(10, 40), new Point(500, 400), null), new ExportExcel(exportDir, IsOpenExportFile));

            //for not blocking UI-thread
            Task[] tasksView = new Task[2]
            {
                new Task(()=>{ viewPressure.ShowResults(); }),
                 new Task(()=>{ viewModifyCUSUM.ShowResults(); }),
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
                    if (viewModifyCUSUM != null)
                        viewModifyCUSUM.ExportData();
                    else
                        MessageBox.Show("Невозможно экспортировать результаты в файл, так как не был выполнен импорт исходных данных !", "Информирование пользователя", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if(viewModifyCUSUM != null)
                viewModifyCUSUM.DisposeView();

            if(viewPressure != null)
                viewPressure.DisposeView();
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

                buttonChooseImportFile.Enabled = true;
                buttonChooseExportFile.Enabled = true;
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

                buttonChooseImportFile.Enabled = false;
                buttonChooseExportFile.Enabled = false;
            }
        }

        private void ChangeLabels()
        {
            labelN.Text = (Convert.ToDouble(trackBarMethodCusumParamN.Value) / 100).ToString();
            labelB.Text = (Convert.ToDouble(trackBarMethodCusumParamB.Value) / 1000).ToString();
        }

        private void trackBarParamN_ValueChanged(object sender, EventArgs e)
        {
            ChangeLabels();
        }

        private void trackBarParamB_ValueChanged(object sender, EventArgs e)
        {
            ChangeLabels();
        }

        private void buttonChooseImportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xls;*.xlsx";
            ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            ofd.Title = "Выберите документ для импорта данных";

            //user selected FileName
            if (ofd.ShowDialog() == DialogResult.OK)
                textBoxImportFileName.Text = ofd.SafeFileName;
        }

        private void buttonChooseExportFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "*.xls;*.xlsx";
            ofd.Filter = "Excel Sheet(*.xlsx)|*.xlsx";
            ofd.Title = "Выберите документ для экспорта данных";

            //user selected FileName
            if (ofd.ShowDialog() == DialogResult.OK)
                textBoxExportFileName.Text = ofd.SafeFileName;
        }

        private void импортироватьДанныеИзФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonChooseImportFile_Click(sender, e);
            buttonImportData_Click(sender, e);
        }

        private void экспортироватьРезультатыВФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            buttonChooseExportFile_Click(sender, e);
            buttonExportData_Click(sender, e);
        }
    }
}