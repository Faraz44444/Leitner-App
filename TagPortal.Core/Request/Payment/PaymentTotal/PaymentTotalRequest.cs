using System;

namespace TagPortal.Core.Request.Payment.PaymentTotal
{
    public class PaymentTotalRequest : BaseRequestPaged<PaymentTotalOrderByEnum>
    {
        public long PaymentTotalId { get; set; }
        public string Title { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public double Price { get; set; }
        public bool? IsDeposit { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public long CreatedByUserId { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public string CreatedByFullName
        {
            get
            {
                return CreatedByFirstName + CreatedByLastName;
            }
        }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedFrom { get; set; }
        public DateTime CreatedTo { get; set; }

    }
    public enum PaymentTotalOrderByEnum
    {
        Title = 1,
        Business = 7,
        Price = 2,
        Date = 3,
        IsDeposit = 4,
        CreatedByFullName = 5,
        CreatedAt = 6
    }
}
