using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Report
{
    public class PaymentsOverviewRequest
    {
        public DateTime Date { get; set; }
        public bool? IsDeposit { get; set; }
    }
}
