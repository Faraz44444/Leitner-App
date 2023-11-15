using System.Collections.Generic;

namespace Web.Dto.Category
{
    public class CategoryPriorityDto
    {
        public Dictionary<int, string> Priorities { get; set; } = new();
    }
}
