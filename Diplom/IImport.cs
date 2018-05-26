using System;
using System.Collections.Generic;

namespace Diplom
{
    interface IImport
    {
        SortedDictionary<DateTime, double> LoadData(int value);
        SortedDictionary<DateTime, double> GetValues();
        int GetCount();
        void DisposeResource();
    }
}
