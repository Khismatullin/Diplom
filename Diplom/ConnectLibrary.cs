using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Diplom
{
    public class ConnectLibrary
    {
        public ConnectLibrary()
        {
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
        }

        System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (args.Name.Contains("OxyPlot"))
            {
                return Assembly.Load(Diplom.Properties.Resources.OxyPlot);
            }

            if (args.Name.Contains("OxyPlot_WindowsForms"))
            {
                return Assembly.Load(Diplom.Properties.Resources.OxyPlot_WindowsForms);
            }

            return null;
        }
    }
}
