using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplom
{
    class View
    {
        private IImport dataImport;
        private IDeclineData declineData;
        private IMethod method;
        private IChart chart;
        private IExport dataExport;
        private SortedDictionary<DateTime, double> dataView;

        public View(IImport dl, IDeclineData d, IMethod m, IChart c, IExport e)
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

                //save data in view
                Task taskAddData = new Task(() =>
                {
                    if (res != null)
                        dataView.Add(res.Keys.Last(), res.Values.Last());
                });
                taskAddData.Start();

                return res;
            }
            else
                return loadVal;
        }

        public SortedDictionary<DateTime, double> OtherCalculationsA()
        {
            if (method != null)
                return method.GetCalculationsA();
            else
                return null;
        }

        public SortedDictionary<DateTime, double> OtherCalculationsB()
        {
            if (method != null)
                return method.GetCalculationsB();
            else
                return null;
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

        public void ShowResults()
        {
            object line = null;
            if (method != null)
                line = chart.AddLine(method.SetAdditionallyNameForSeriesA());

            //read and visualize adding by 1 value
            for (int i = 1; i < dataImport.GetCount(); i++)
            {
                if (Output(UseMethod(Decline(Load(i)))) == false)
                    break;

                //additionally series
                if(method != null)
                    chart.AddPointOnLine(OtherCalculationsA(), line);
            }
        }

        public void ExportData()
        {
            dataExport.Export(dataView);
        }

        public void DisposeMaketResource()
        {
            dataImport.DisposeResource();
            dataExport.DisposeResource();
        }
    }
}
