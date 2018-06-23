using System;
using System.Collections.Generic;

namespace Diplom
{
    interface IImport
    {
        SortedDictionary<DateTime, double> ImportData(int value);
        int GetCount();
        void DisposeImport();
    }
}
