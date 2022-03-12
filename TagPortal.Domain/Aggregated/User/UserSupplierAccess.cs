using System.Collections.Generic;
using TagPortal.Domain.Enum;

namespace TagPortal.Domain.Aggregated.User
{
    public class UserSupplierAccess : PagedModel
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }

        public List<EnumPermissionType> PermissionTypeList { get; set; }

        public UserSupplierAccess()
        {
            PermissionTypeList = new List<EnumPermissionType>();
        }
    }
}
