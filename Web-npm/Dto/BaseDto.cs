using System;
namespace Web.Dto
{
    public class BaseDto
    {
        public DateTime? CreatedAt { get; set; }
        public long CreatedByUserId { get; set; }
        public string? CreatedByFirstName { get; set; }
        public string? CreatedByLastName { get; set; }
        public string CreatedByFullName => new[] {CreatedByFirstName, CreatedByLastName}.Join(" ").Replace("  ", " ");
        public bool Deleted { get; set; }
        public string DeletedText => Deleted ? "Deleted" : "Active";
        public DateTime? DeletedAt { get; set; }
    }
}
