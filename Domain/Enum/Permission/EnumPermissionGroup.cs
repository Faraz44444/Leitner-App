using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum.Permission
{
    public enum EnumPermissionGroup
    {
        System,
        Log
    }

    public static partial class EnumTextExtensions
    {
        public static string Text(this EnumPermissionGroup val)
        {
            switch (val)
            {
                case EnumPermissionGroup.System: return "System";
                case EnumPermissionGroup.Log: return "Log";
                default: throw new NotImplementedException();
            }
        }
    }
}
