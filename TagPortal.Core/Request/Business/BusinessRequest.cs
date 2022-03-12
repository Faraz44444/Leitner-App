using System;

namespace TagPortal.Core.Request.Business
{
    public class BusinessRequest : BaseRequestPaged<BusinessOrderByEnum>
    {
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
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
    public enum BusinessOrderByEnum
    {
        BusinessName = 1,
        CreatedByFullName = 7,
        CreatedAt = 8,
    }
}
