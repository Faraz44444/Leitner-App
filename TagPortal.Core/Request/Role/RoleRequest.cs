namespace TagPortal.Core.Request.Role
{
    public class RoleRequest : BaseRequestPaged<RoleOrderByEnum>
    {
        public string Name { get; set; }
        public bool? IsLocked { get; set; }
    }

    public enum RoleOrderByEnum
    {
        Name = 1,
        IsLocked = 2
    }
}
