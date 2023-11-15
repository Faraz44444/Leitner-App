using System;
using Domain.Enum.Permission;
using Domain.Model.BaseModels;

namespace Domain.Model.Role
{
    public class RolePermissionModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long RoleId { get; set; }
        [IsTableColumn(true)]
        public EnumPermission Permission { get; set; }
        public RolePermissionModel() { }

        public RolePermissionModel(long roleId, EnumPermission permission)
        {
            RoleId = roleId;
            Permission = permission;
        }

        public new void Validate()
        {
            if (RoleId < 1) throw new ArgumentException("RoleId is not set");
            if (!System.Enum.IsDefined(typeof(EnumPermission), Permission)) throw new ArgumentException("Permission is not set");
        }
    }
}
