using System.Collections.Generic;

namespace TbxPortal.Web.Dto.User
{
    public class UserSupplierDto
    {
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class UserSupplierRequestDto
    {
        public List<UserSupplierDto> UserSupplierList { get; set; }
        public UserSupplierRequestDto()
        {
            UserSupplierList = new List<UserSupplierDto>();
        }
    }
}