using Domain.Attributes;

namespace Domain.Enum.Permission
{
    public enum EnumPermission
    {
        CONTROLLER_ENDPOINT_NOT_USED = -1,

        [Permission(EnumPermissionGroup.System)] AdminAccount = 1,
        [Permission(EnumPermissionGroup.Log)] Log = 2,
        [Permission(EnumPermissionGroup.System)] Users = 3,
    }

    public static class EnumPermissionText
    {
        public static string Text(this EnumPermission val)
        {
            switch (val)
            {
                case EnumPermission.CONTROLLER_ENDPOINT_NOT_USED:
                    return "";
                case EnumPermission.AdminAccount:
                    return "Admin";
                case EnumPermission.Log:
                    return "Log";
                case EnumPermission.Users:
                    return "Users";
                default:
                    return "";
            }
        }
    }
}
