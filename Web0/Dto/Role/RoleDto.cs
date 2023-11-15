namespace Web0.Dto.Role
{
    public class RoleDto : BaseDto
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public long ClientId { get; set; }
    }
}
