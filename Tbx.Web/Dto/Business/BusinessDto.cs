using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TbxPortal.Web.Dto.Business
{
    public class BusinessDto
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
    }
}