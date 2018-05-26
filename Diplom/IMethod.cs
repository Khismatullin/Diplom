using System;
using System.Collections.Generic;

namespace Diplom
{
    interface IMethod
    {
        bool StopCondition { get; }
        string StopMessage { get; }
        string NameForSeries { get; }
        string AdditionallyNameForSeries { get; }
        string SetAdditionallyNameForSeriesA();
        string SetAdditionallyNameForSeriesB();
        SortedDictionary<DateTime, double> UseMethod(SortedDictionary<DateTime, double> allValues);
        SortedDictionary<DateTime, double> GetCalculationsA();
        SortedDictionary<DateTime, double> GetCalculationsB();
    }
}
