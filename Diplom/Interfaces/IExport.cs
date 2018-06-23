using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    interface IExport
    {
        void ExportData(SortedDictionary<DateTime, double> d);
        void DisposeExport();
    }
}
