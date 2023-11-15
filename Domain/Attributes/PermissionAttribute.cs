using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enum.Permission;

namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PermissionAttribute : BaseEnumAttribute
    {
        public EnumPermissionGroup PermissionGroup { get; set; }
        public bool IsVisible { get; set; }

        public PermissionAttribute(EnumPermissionGroup permissionGroup, bool isVisible = true)
        {
            PermissionGroup = permissionGroup;
            IsVisible = isVisible;
        }
    }
}
