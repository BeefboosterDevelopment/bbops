using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beefbooster.Operations.ReportingServices
{
    public class HerdProfileReport : IHerdProfileReport
    {
        public int Generate(int year, int herdSN)
        {
            var proc = new HerdProfileGenerator();
            return proc.Generate(year, herdSN);
        }
    }
}
