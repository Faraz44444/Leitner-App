using TagPortal.Domain.Enum;

namespace TagPortal.Domain.Model.Role
{
    public class RolePermissionModel : PagedModel
    {
        public EnumPermissionType PermissionType { get; set; }
        public string PermissionGroup { get; set; }
        public string PermissionName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsLocked { get; set; }
    }
}
