using System;

namespace TbxPortal.Web.Dto.Payment.PaymentTotal
{
    public class PaymentTotalDto
    {
        public long PaymentTotalId { get; set; }
        public string Title { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public double Price { get; set; }
        public DateTime Date { get; set; }
        public bool IsDeposit { get; set; }
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

    }
}