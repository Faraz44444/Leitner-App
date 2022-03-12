using TagPortal.Domain.Enum;

namespace TagPortal.Core.Request.Role
{
    public class RolePermissionsRequest : BaseRequestPaged<RolePermissionsOrderByEnum>
    {
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long SupplierId { get; set; }
    }

    public enum RolePermissionsOrderByEnum
    {
        RoleName = 2
    }
}
