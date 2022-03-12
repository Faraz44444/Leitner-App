namespace TagPortal.Core.Request.User
{
    public class UserSupplierRequest : BaseRequestPaged<UserSupplierOrderByEnum>
    {
        public long UserId { get; set; }
        public long SupplierId { get; set; }
        public long RoleId { get; set; }
    }

    public enum UserSupplierOrderByEnum
    {
        UserEmail = 1,
        UserFullName = 2,
        SupplierName = 3,
        RoleName = 4
    }
}
