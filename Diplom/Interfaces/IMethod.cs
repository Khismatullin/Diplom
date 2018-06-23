using System;
using System.Collections.Generic;

namespace Diplom
{
    interface IMethod
    {
        bool StopCondition { get; }
        string StopMessage { get; }
        string NameForSeries { get; }
        string OtherNameForSeries { get; }
        string SetOtherNameForSeries();
        SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> allValues);
        SortedDictionary<DateTime, double> GetOtherCalculations();
    }
}
