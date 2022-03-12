using TagPortal.Domain.Enum;

namespace TbxPortal.Web.Dto.Role
{
    public class RolePermissionDto
    {
        public EnumPermissionType PermissionType { get; set; }
        public string PermissionGroup { get; set; }
        public string PermissionName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long ClientId { get; set; }
        public bool IsLocked { get; set; }
    }
}