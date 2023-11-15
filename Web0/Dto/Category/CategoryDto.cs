using Domain.Enum.Category;
using System;
using System.Linq;
using Web0.Dto;

namespace Web.Dto.Category
{
    public class CategoryDto : BaseDto
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public EnumCategoryPriority Priority { get; set; }
        public string PriorityName
        {
            get
            {
                var Name = Enum.GetName(typeof(EnumCategoryPriority), Priority);
                return string.Concat(Name.Select((x, index) => Char.IsUpper(x) && index > 1 ? " " + x : x.ToString())).TrimStart(' ');
            }
        }
        public float WeeklyLimit { get; set; }
        public float MonthlyLimit { get; set; }
    }
}
