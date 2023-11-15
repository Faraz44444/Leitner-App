using System;
using Web0.Dto;

namespace Web.Dto.Report
{
    public class YearlyOverviewDto : BaseDto
    {
        public double Price { get; set; }
        public bool IsDeposit { get; set; }
        public string FormattedPrice
        {
            get
            {
                return String.Format("{0:N}", Price);
            }
        }
        public Int32 Year { get; set; }
        public Int32 Month { get; set; }
        public DateTime Date
        {
            get
            {
                return new DateTime(Year, Month, 1);
            }
        }
        public string FormattedDate
        {
            get
            {
                return Date.ToString("MMMM") + " , " + Date.Year;
            }
        }

    }
}
