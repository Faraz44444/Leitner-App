namespace TagPortal.Domain.Model.User
{
    public class UserSupplierModel : PagedModel
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
}
