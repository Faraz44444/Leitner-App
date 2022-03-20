using System;
using TagPortal.Domain.Enum;

namespace TagPortal.Core.Request.Category
{
    public class CategoryRequest : BaseRequestPaged<CategoryOrderByEnum>
    {

        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public EnumCategoryPriority CategoryPriority { get; set; }
        public float WeeklyLimit { get; set; }
        public float MonthlyLimit { get; set; }
        public bool? HasWeeklyLimit { get; set; }
        public bool? HasMonthlyLimit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime CreatedFrom { get; set; }
        public DateTime CreatedTo { get; set; }
        public long CreatedByUserId { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }
        public string CreatedByFullName
        {
            get
            {
                return CreatedByFirstName + CreatedByLastName;
            }
        }
    }
    public enum CategoryOrderByEnum
    {
        CategoryName = 1,
        CategoryPriority = 2,
        CreatedAt = 3,
        WeeklyLimit = 4,
        MonthlyLimit = 5
    }
}
