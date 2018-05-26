using System;
using System.Collections.Generic;
using System.Linq;

namespace Diplom
{
    class MethodCusum : IMethod
    {
        private double N;
        private double b;
        private int k;
        private double[] x;
        private SortedDictionary<DateTime, double> average;
        private SortedDictionary<DateTime,double> estKsi;
        private SortedDictionary<DateTime, double> ksi;
        private SortedDictionary<DateTime, double> timeY;
        public string NameForSeries { get; set; }
        public string AdditionallyNameForSeries { get; set; }
        public bool StopCondition { get; set; }
        public string StopMessage { get; set; }

        public MethodCusum(double valN, double valB, int valK)
        {
            //critical value (-10.01)
            N = -10.01;

            //max measurement error (0.025)
            b = valB;

            //count measurement (20)
            k = valK;

            //x
            x = new double[k];

            //f(t)
            average = new SortedDictionary<DateTime, double>();

            //E^(t)
            estKsi = new SortedDictionary<DateTime, double>();

            //E(t)
            ksi = new SortedDictionary<DateTime, double>();

            //y(t)
            timeY = new SortedDictionary<DateTime, double>();

            StopCondition = false;
            NameForSeries = "y(t)";
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
                average.Add(allDatesPressures.Keys.Last(), x.Average());

                //remove trend E^(tf + 1) = P(tf + 1) - f(tf + 1)                     
                estKsi.Add(allDatesPressures.Keys.Last(), allDatesPressures.Values.Last() - average.Values.Last());

                //find E[tf + 1] = E^[tf + 1] + b
                ksi.Add(allDatesPressures.Keys.Last(), estKsi.Values.Last() + b);

                //CUSUM -- y[tf + 1] = y[tf] + ksi
                timeY.Add(allDatesPressures.Keys.Last(), Math.Min(0, timeY.Values.Last() + ksi.Values.Last()));

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

        public string SetAdditionallyNameForSeriesA()
        {
            return AdditionallyNameForSeries = "E(t)";
        }

        public string SetAdditionallyNameForSeriesB()
        {
            return AdditionallyNameForSeries = "f(t)";
        }

        public SortedDictionary<DateTime, double> GetCalculationsA()
        {
            return ksi;
        }

        public SortedDictionary<DateTime, double> GetCalculationsB()
        {
            return average;
        }
    }
}
