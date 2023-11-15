using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Report
{
    public class DashboardSumsRequest
    {
        public bool? IsDeposit { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public long ClientId { get; set; }
    }
}
