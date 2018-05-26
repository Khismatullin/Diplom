using System;
using System.Collections.Generic;

namespace Diplom
{
    interface IDeclineData
    {
        SortedDictionary<DateTime, double> AddDecline(SortedDictionary<DateTime, double> loadData);
    }
}
