using TagPortal.Domain.Enum;

namespace TagPortal.Core.Request.User
{
    public class UserRequest : BaseRequestPaged<UserOrderByEnum>
    {
        public EnumUserType? UserType { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool? Active { get; set; }
        public string UserInitials { get; set; }
    }

    public enum UserOrderByEnum
    {
        UserType = 1,
        Username = 2,
        Email = 3,
        UserFullName = 4,
        Active = 6,
        LastLoginAt = 7
    }
}
