using System.Collections.Generic;

namespace TbxPortal.Web.Dto.Role
{
    public class RoleDto
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// This property is only editable by system provder and it indicates that this role is locked, predefined, not editable by clients.
        /// </summary>
        public bool IsLocked { get; set; }
        public long ClientId { get; set; }
        public long SupplierId { get; set; }
        public List<RolePermissionDto> PermissionList { get; set; }

        public RoleDto()
        {
            PermissionList = new List<RolePermissionDto>();
        }
    }
}