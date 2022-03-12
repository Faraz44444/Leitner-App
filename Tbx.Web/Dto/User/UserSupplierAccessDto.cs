namespace TbxPortal.Web.Dto.User
{
    public class UserSupplierAccessDto
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public long RoleId { get; set; }
        public string RoleName { get; set; }
    }
}