using System;
using Domain.Interfaces;

namespace Domain.Model.BaseModels
{
    public abstract class BaseModel
    {
        public DateTime CreatedAt { get; set; }
        public long CreatedByUserId { get; set; }
        public string CreatedByFirstName { get; set; }
        public string CreatedByLastName { get; set; }

        protected BaseModel() { }

        protected BaseModel(long createdByUserId,
                            string createdByFirstName,
                            string createdByLastName)
        {
            CreatedAt = DateTime.Now;
            CreatedByUserId = createdByUserId;
            CreatedByFirstName = createdByFirstName;
            CreatedByLastName = createdByLastName;
        }

        protected BaseModel(ICurrentUser createdBy)
        {
            CreatedAt = DateTime.Now;
            CreatedByUserId = createdBy.UserId;
            CreatedByFirstName = createdBy.FirstName;
            CreatedByLastName = createdBy.LastName;
        }

        public void Validate()
        {
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
            if (CreatedByUserId < 1) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (CreatedByFirstName.Empty()) throw new ArgumentException("CreatedByFirstName is not set", "CreatedByFirstName");
            if (CreatedByLastName.Empty()) throw new ArgumentException("CreatedByLastName is not set", "CreatedByLastName");
        }
    }

    public class IsNotTableColumnAttribute : Attribute
    {
        public IsNotTableColumnAttribute(bool isNotTableColumn)
        {
            IsNotTableColumn = isNotTableColumn;
        }
        public bool IsNotTableColumn { get; set; }
    }

}
