using System;

namespace Domain.Aggregated
{
    public class PaymentsOverviewData
    {
        public int Month { get; set; }
        public float Price { get; set; }
        public string MonthFormatted
        {
            get
            {
                return new DateTime(DateTime.Now.Year, Month, 1).ToString("MMMM");
            }
        }
        public string PriceFormatted
        {
            get
            {
                return Price.ToString("## ##0.00");
            }
        }
    }
}
