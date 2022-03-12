using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TbxPortal.Web.Dto.Payment.PaymentPriority
{
    public class PaymentPriorityDto
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
    }
}