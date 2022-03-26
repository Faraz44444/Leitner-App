using System;

namespace TagPortal.Domain.Model.Payment
{
    public class PaymentModel : PagedModel
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
        public double Price { get; set; }
        public string FormattedPrice
        {
            get
            {
                return String.Format("{0:N}", Price);
            }
        }
        public DateTime Date { get; set; }
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
        public DateTime ApprovedAt { get; set; }
    }
}


