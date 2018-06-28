using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    class EvaluationPoisson : IEvaluation
    {
        public double evaluation { get; set; }
        private int counterK;

        //average count of events for interval
        private double lymbda;

        //function of probability
        private double pk;

        public EvaluationPoisson()
        {

        }

        public void Evaluate(SortedDictionary<DateTime, double> input)
        {
            if (input != null && input.Count == 300)
            {
                //select initial 300 values for analysis and predict

                //max threshold by initial values
                evaluation = input.Max(i => i.Value);

                //variation
                evaluation = input.Sum(i => Math.Pow(i.Value, 2) * (1.0 / input.Count)) - Math.Pow(input.Sum(i => i.Value), 2);

                //count of values of CUSUM, exceeds threshold (equal 25% by N)
                counterK = 0;
                foreach (var item in input)
                    if (item.Value <= -0.1)
                        counterK++;

                //calculate distribution Poisson - probability that for 300 new values exceeds will be less or equal than "counter"
                lymbda = 300 * counterK/300.0;
                for (int i = 0; i <= counterK; i++)
                    pk += (Math.Pow(lymbda, i) * Math.Pow(Math.E, -lymbda)) / Factorial(i);

                //evaluation as a percentage
                evaluation = Math.Round(pk * 100, 1);
            }
        }

        int Factorial(int x)
        {
            return (x == 0) ? 1 : x * Factorial(x - 1);
        }
    }
}
