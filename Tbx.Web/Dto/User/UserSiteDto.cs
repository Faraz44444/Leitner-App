using System.Collections.Generic;

namespace TbxPortal.Web.Dto.User
{
    public class UserSiteDto
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long ClientId { get; set; }
    }

    public class UserSiteRequestDto
    {
        public List<UserSiteDto> UserSiteList { get; set; }
        public UserSiteRequestDto()
        {
            UserSiteList = new List<UserSiteDto>();
        }
    }
}