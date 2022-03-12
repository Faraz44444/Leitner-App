namespace TagPortal.Core.Request.User
{
    public class UserSiteRequest : BaseRequestPaged<UserSiteOrderByEnum>
    {
        public long UserId { get; set; }
        public long SiteId { get; set; }
        public long RoleId { get; set; }
    }

    public enum UserSiteOrderByEnum
    {
        UserEmail = 1,
        UserFullName = 2,
        SiteName = 3,
        RoleName = 4
    }
}
