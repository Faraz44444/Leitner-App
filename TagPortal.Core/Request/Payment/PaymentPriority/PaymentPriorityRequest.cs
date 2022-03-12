using System;

namespace TagPortal.Core.Request.Payment.PaymentPriority
{
    public class PaymentPriorityRequest : BaseRequestPaged<PaymentPriorityOrderByEnum>
    {
        public long PaymentPriorityId { get; set; }
        public string PaymentPriorityName { get; set; }
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
    public enum PaymentPriorityOrderByEnum
    {
        PaymentPriorityName = 1,
        CreatedByFullName = 2,
        CreatedAt = 3,
    }
}

