using System;
using Web0.Dto;

namespace Web.Dto.Report
{
    public class MonthlyOverviewDto : BaseDto
    {
        public string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public long PaymentPriorityId { get; set; }
        public string PaymentPriorityName { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool IsDeposit { get; set; }
        public bool IsPaidToPerson { get; set; }
        public double Price { get; set; }
        public string FormattedPrice
        {
            get
            {
                return String.Format("{0:N}", Price);
            }
        }
        public double PriceFrom { get; set; }
        public double PriceTo { get; set; }
        public Int32 Year { get; set; }
        public Int32 Month { get; set; }
    }
}
