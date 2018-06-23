using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathNet.Numerics;

namespace Diplom
{
    class MethodModifyCUSUM : IMethod
    {
        private double N;
        private double b;
        private int k;
        private SortedDictionary<DateTime, double> x;
        private SortedDictionary<DateTime, double> trend;
        private SortedDictionary<DateTime,double> estKsi;
        private SortedDictionary<DateTime, double> ksi;
        private SortedDictionary<DateTime, double> yt;
        private double[] keysXInDouble;
        public string NameForSeries { get; set; }
        public string OtherNameForSeries { get; set; }
        public bool StopCondition { get; set; }
        public string StopMessage { get; set; }

        public MethodModifyCUSUM(double valN, double valB, int valK)
        {
            //critical value (-10.01)
            N = valN;

            //max measurement error (0.025)
            b = valB;

            //count measurement (20)
            k = valK;

            //x
            x = new SortedDictionary<DateTime, double>();

            //f(t)
            trend = new SortedDictionary<DateTime, double>();

            //E^(t)
            estKsi = new SortedDictionary<DateTime, double>();

            //E(t)
            ksi = new SortedDictionary<DateTime, double>();

            //y(t)
            yt = new SortedDictionary<DateTime, double>();

            StopCondition = false;
            NameForSeries = "y(t)";
            keysXInDouble = new double[k];
        }
        
        public SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> allDatesPressures)
        {
            //at first X = {P(ts), P(ts+1), ... P(tf)}
            if (allDatesPressures.Count == k)
            {
                foreach (var item in allDatesPressures)
                    x.Add(item.Key, item.Value);

                //y[0] = 0
                yt.Add(allDatesPressures.Keys.Last(), 0);

                return null;
            }
            //trend, CUSUM (every time we calculate trend by set X)
            else if (allDatesPressures.Count > k)
            {
                //crutch
                int i = 0;
                foreach (var item in x)
                {
                    keysXInDouble[i] = item.Key.ToOADate();
                    i++;
                }

                //find "a" and "b" of linear trend by equation a + bx using lib-ry MathNet.Numerics (trend of set X)
                Tuple<double, double> ab = Fit.Line(keysXInDouble, x.Values.ToArray());
               
                //find and add trend using calulated "a" and "b"
                trend.Add(allDatesPressures.Keys.Last(), ab.Item1 + ab.Item2 * x.Keys.Last().ToOADate());

                //remove trend E^(tf + 1) = P(tf + 1) - f(tf + 1)                     
                estKsi.Add(allDatesPressures.Keys.Last(), allDatesPressures.Values.Last() - trend.Values.Last());

                //find E[tf + 1] = E^[tf + 1] + b
                ksi.Add(allDatesPressures.Keys.Last(), estKsi.Values.Last() + b);

                //CUSUM -- y[tf + 1] = y[tf] + ksi
                yt.Add(allDatesPressures.Keys.Last(), Math.Min(0, yt.Values.Last() + ksi.Values.Last()));
                
                //check on d(y[t]) = I(y[t] < N), if d(y[t]) = 1 then STOP
                if (yt.Values.Last() < N)
                {
                    StopCondition = true;

                    //save message
                    DateTime showDate = allDatesPressures.Keys.Last();
                    StopMessage = showDate.ToShortDateString() + " в " + showDate.TimeOfDay + " был обнаружен момент разладки.";
                }
                else
                {
                    //replace X = {P(ts), P(ts+1), ... P(tf)}
                    x.Remove(x.First().Key);
                    x.Add(allDatesPressures.Keys.Last(), allDatesPressures.Values.Last());
                }

                return yt;
            }
            else
                return null;
        }

        public string SetOtherNameForSeries()
        {
            return OtherNameForSeries = "E(t)";
        }

        public SortedDictionary<DateTime, double> GetOtherCalculations()
        {
            return ksi;
        }
    }
}
