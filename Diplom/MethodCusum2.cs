using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    class MethodCusum2 : IMethod
    {
        //count of elem in sequence
        private int n;

        //for store elem-s
        private SortedDictionary<DateTime, double> valuesX;

        //statistic (based on logarithm relation of likelihood)
        private double L;

        //threshold of detection (>0)
        private double h;

        private double minSum;
        private double t;

        //average time before false alarm
        private double E0;

        //average time delay in the detection
        private double E1;

        //
        private double M0;

        //
        private double M1;

        public MethodCusum2(int N, double H)
        {
            n = N;
            h = H;
            valuesX = new SortedDictionary<DateTime, double>();

            //M0 = E
            //E0 = Math.Abs(Math.Pow(Math.E, h) - h - 1) / Math.Abs(M0);
            //E0 = (Math.Pow(Math.E, -h) + h - 1) / (M1);
        }

        //density before discord
        public double F0(double val)
        {
            return val;
        }

        //density after discord
        public double F1(double val)
        {
            return val;
        }

        public SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> allReadValues)
        {
            if(valuesX.Count < n)
            {
                //ignore
                return null;
            }
            else if(valuesX.Count == n)
            {
                //copy read values
                foreach (var item in allReadValues)
                    valuesX.Add(item.Key, item.Value);

                //initialize
                minSum = valuesX.Values.First();

                return null;
            }
            else
            {
                //calculate CUSUM
                              
                L = valuesX.Sum(x => Math.Log(F1(x.Value)/F0(x.Value)));

                //minSum
                for (int i = 0; i < valuesX.Count; i++)
                {
                    double res = valuesX.Reverse().Skip(i).Sum(x => Math.Log(F1(x.Value) / F0(x.Value)));

                    //update minSum if less
                    if (res < minSum)
                        minSum = res;
                }

                //check on StopCondition
                if(L - minSum >= h)
                {
                    t = L - minSum;
                }
                else
                {
                    //offset values (may only n count)
                    valuesX.Remove(valuesX.Keys.First());
                    valuesX.Add(allReadValues.Keys.Last(), allReadValues.Values.Last());
                }               

                return null;
            }
        }

        public bool StopCondition => throw new NotImplementedException();

        public string StopMessage => throw new NotImplementedException();

        public string NameForSeries => throw new NotImplementedException();

        public string AdditionallyNameForSeries => throw new NotImplementedException();

        public SortedDictionary<DateTime, double> GetCalculationsA()
        {
            throw new NotImplementedException();
        }

        public SortedDictionary<DateTime, double> GetCalculationsB()
        {
            throw new NotImplementedException();
        }

        public string SetAdditionallyNameForSeriesA()
        {
            throw new NotImplementedException();
        }

        public string SetAdditionallyNameForSeriesB()
        {
            throw new NotImplementedException();
        }
    }
}
