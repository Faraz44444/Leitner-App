using System;
using TagPortal.Domain.Enum;

namespace TagPortal.Domain.Model.User
{
    public class UserModel : PagedModel
    {
        public long UserId { get; set; }
        public EnumUserType UserType { get; set; }
        public string Username { get; set; }
        public string UserInitials { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ForcePasswordReset { get; set; }
        public bool ForceUserInformationUpdate { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public decimal CssFontSize { get; set; }
        public DateTime LastLoginAt { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
