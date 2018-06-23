using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    interface IEvaluation
    {
        double evaluation { get; }
        void Evaluate(SortedDictionary<DateTime, double> input);
    }
}
