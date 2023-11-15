using System;
using Domain.Enum.Category;
using Domain.Model.BaseModels;

namespace Domain.Model.Category
{
    public class CategoryModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long CategoryId { get; set; }
        
        [IsTableColumn(true)]
        public string Name { get; set; }
        
        [IsTableColumn(true)]
        public EnumCategoryPriority Priority { get; set; }
        
        [IsTableColumn(true)]
        public float WeeklyLimit{ get; set; }
        
        [IsTableColumn(true)]
        public float MonthlyLimit{ get; set; }

        public CategoryModel() { }

        public CategoryModel(string name,
                             EnumCategoryPriority priority,
                             float weeklyLimit,
                             float monthlyLimit,
                             long clientId,
                             long createdByUserId,
                             string createdByFirstName,
                             string createdByLastName,
                             bool deleted) : base(createdByUserId, createdByFirstName, createdByLastName)
        {
            Name = name;
            Priority = priority;
            WeeklyLimit = weeklyLimit;
            MonthlyLimit = monthlyLimit;
            ClientId = clientId;
            CreatedByUserId = createdByUserId;
            CreatedByFirstName = createdByFirstName;
            CreatedByLastName = createdByLastName;
            Deleted = deleted;
        }

        public new void Validate()
        {
            if (Name.Empty()) throw new ArgumentException("Name is not set");
            if (ClientId < 1) throw new ArgumentException("ClientId is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}