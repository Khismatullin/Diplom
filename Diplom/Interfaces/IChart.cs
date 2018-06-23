using System;
using System.Collections.Generic;
using System.Drawing;

namespace Diplom
{
    interface IChart
    {
        void VisualizeData(SortedDictionary<DateTime, double> loadData, bool marker);
        object AddLine(string titleName);
        void AddPointOnLine(SortedDictionary<DateTime, double> data, object objLine);
        void DisposeChart();
    }
}
