using TagPortal.Domain.Enum;

namespace TagPortal.Core.Request.User
{
    public class UserSupplierAccessRequest : BaseRequestPaged<UserSiteAccessOrderByEnum>
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserFullName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public EnumPermissionType? PermissionType { get; set; }
    }

    public enum UserSupplierAccessOrderByEnum
    {
        SiteName = 1,
        Username = 2,
        UserFullName = 3,
        RoleName = 4
    }
}
