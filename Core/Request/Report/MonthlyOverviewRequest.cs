using Core.Request.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Report
{
    public class MonthlyOverviewRequest : BaseRequestPaged
    {
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool? IsDeposit { get; set; }
        public bool? IsPaidToPerson { get; set; }
        public double Price { get; set; }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public DateTime Year { get; set; }
        public DateTime Month { get; set; }
    }
    public enum EnumMonthlyOverviewRequest
    {
        CategoryName = 1,
        Price = 2,
        PaymentPriorityName = 3,
        Year = 4,
        Month = 5,
    }
}
