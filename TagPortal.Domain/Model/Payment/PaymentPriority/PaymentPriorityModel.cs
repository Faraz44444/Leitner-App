using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagPortal.Domain.Model.PaymentPriority
{
    public class PaymentPriorityModel : PagedModel
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
