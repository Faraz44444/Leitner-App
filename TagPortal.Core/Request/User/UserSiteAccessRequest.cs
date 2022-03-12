using TagPortal.Domain.Enum;

namespace TagPortal.Core.Request.User
{
    public class UserSiteAccessRequest : BaseRequestPaged<UserSiteAccessOrderByEnum>
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserFullName { get; set; }
        public EnumPermissionType? PermissionType { get; set; }
    }

    public enum UserSiteAccessOrderByEnum
    {
        SiteName = 1,
        RoleName = 2,
        Username = 3,
        UserFullName = 4
    }
}
