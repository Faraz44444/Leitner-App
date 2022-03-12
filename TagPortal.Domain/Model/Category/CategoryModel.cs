using System;
using TagPortal.Domain.Enum;

namespace TagPortal.Domain.Model.Category
{
    public class CategoryModel : PagedModel
    {

        public long CategoryId { get; set; }
        public EnumCategoryPriority CategoryPriority { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
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
}


