using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dto.User
{
    public class UserDto : BaseDto
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long PhoneCountryId { get; set; }
        public string Phone { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public long EmployeeId { get; set; }
        public bool IsSystemUser { get; set; }
        public long CurrentClientId { get; set; }
        public DateTime LastUpdateAt { get; set; }
    }
}
