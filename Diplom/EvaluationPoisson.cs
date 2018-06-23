using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Diplom
{
    class EvaluationPoisson : IEvaluation
    {
        public double evaluation { get; set; }

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


                //if distribution Poisson(Puason) may calculate count of often values CUSUM over the limit

                //after calculate function of density predict probability of count values CUSUM over the limit
                evaluation = 72.1;
            }
        }
    }
}
