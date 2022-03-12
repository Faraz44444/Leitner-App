using TbxPortal.Web.Dto.User;
using System.Collections.Generic;
using TagPortal.Domain.Enum;

namespace TbxPortal.Web.Dto.Account
{
    public class AccountDto
    {
        public EnumUserType UserType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CurrentSiteId { get; set; }
        public string CurrentSiteName { get; set; }
        public long CurrentSupplierId { get; set; }
        public string SupplierName { get; set; }
        public decimal CssFontSize { get; set; }
        public bool ForceUserInformationUpdate { get; set; }

        public List<UserSiteAccessDto> UserSiteAccessList { get; set; }
        public List<UserSupplierAccessDto> UserSupplierAccessList { get; set; }
        public AccountDto()
        {
            UserSiteAccessList = new List<UserSiteAccessDto>();
            UserSupplierAccessList = new List<UserSupplierAccessDto>();
        }
    }
}