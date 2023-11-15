using System;
using Web0.Dto;

namespace Web.Dto.Payment
{
    public class PaymentDto : BaseDto
    {
        public long PaymentId { get; set; }
        public string Title { get; set; }
        public long PaymentPriorityId { get; set; }
        public string PaymentPriorityName { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool IsDeposit { get; set; }
        public bool IsPaidToPerson { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public DateTime ApprovedAt { get; set; }
        public string PriceFormatted { get { return Price.ToString("## ##0.00"); } }
        public string DateFormatted { get { return Date.ToString("dd/MM/yyyy"); } }
    }
}
