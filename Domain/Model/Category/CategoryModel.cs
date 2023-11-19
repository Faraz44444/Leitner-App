using Domain.Model.BaseModels;
using System;

namespace Domain.Model.Batch
{
    public class CategoryModel : PagedBaseModel
    {
        public long CategoryId { get; set; }

        public string Name { get; set; }

        public CategoryModel() { }

        public CategoryModel(string name, long createdByUserId) : base(createdByUserId)
        {
            Name = name;
            CreatedByUserId = createdByUserId;
        }

        public new void Validate()
        {
            if (Name.Empty()) throw new ArgumentException("Name is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}