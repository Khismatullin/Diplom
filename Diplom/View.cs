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
        private IEvaluation evaluation;
        private IChart chart;
        private IExport dataExport;
        private SortedDictionary<DateTime, double> dataView;

        public View(IImport dl, IDeclineData d, IMethod m, IEvaluation ev, IChart c, IExport e)
        {
            dataImport = dl;
            declineData = d;
            method = m;
            evaluation = ev;
            chart = c;
            dataExport = e;

            dataView = new SortedDictionary<DateTime, double>();
        }

        public SortedDictionary<DateTime, double> Import(int i)
        {
            return dataImport.ImportData(i);
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

        public SortedDictionary<DateTime, double> Evaluate(SortedDictionary<DateTime, double> loadVal)
        {
            if(evaluation != null)
            {
                evaluation.Evaluate(loadVal);
            }

            return loadVal;
        }

        public SortedDictionary<DateTime, double> OtherCalculations()
        {
            if (method != null)
                return method.GetOtherCalculations();
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
                    MessageBox.Show(method.StopMessage + "\nВероятность обнаружения утечки составляет " + evaluation.evaluation + " %.", "Предупреждение!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                line = chart.AddLine(method.SetOtherNameForSeries());

            //read and visualize adding by 1 value
            for (int i = 1; i < dataImport.GetCount(); i++)
            {
                if (Output(Evaluate(UseMethod(Decline(Import(i))))) == false)
                    break;

                //additionally series
                if(method != null)
                    chart.AddPointOnLine(OtherCalculations(), line);
            }
        }

        public void ExportData()
        {
            dataExport.ExportData(dataView);
        }

        public void DisposeView()
        {
            dataImport.DisposeImport();
            chart.DisposeChart();
            dataExport.DisposeExport();
        }
    }
}
