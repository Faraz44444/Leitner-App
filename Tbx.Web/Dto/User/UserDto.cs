using System;
using TagPortal.Domain.Enum;

namespace TbxPortal.Web.Dto.User
{
    public class UserDto
    {
        public long UserId { get; set; }
        public EnumUserType UserType { get; set; }
        public string Username { get; set; }
        public string UserInitials { get; set; }
        public int NextTagNo { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool ForcePasswordReset { get; set; }
        public bool ForceUserInformationUpdate { get; set; }
        public long CurrentSiteId { get; set; }
        public string CurrentSiteName { get; set; }
        public long ClientId { get; set; }
        public bool Active { get; set; }
        public decimal CssFontSize { get; set; }
        public bool LeftNavigationMinified { get; set; }
        public DateTime LastLoginAt { get; set; }
        public long CurrentSupplierId { get; set; }
        public string SupplierName { get; set; }
        public string ExternalId { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}