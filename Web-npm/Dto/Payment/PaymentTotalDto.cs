using System;
using Web0.Dto;

namespace Web.Dto.Payment
{
    public class PaymentTotalDto : BaseDto
    {
        public long PaymentTotalId { get; set; }
        public string Title { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool IsDeposit { get; set; }
        public bool IsPaidToPerson { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public string FormattedDate
        {
            get
            {
                return Date.ToString("MMMM") + " , " + Date.Year;
            }
        }
        public string PriceFormatted { get { return Price.ToString("## ##0.00"); } }
    }
}
