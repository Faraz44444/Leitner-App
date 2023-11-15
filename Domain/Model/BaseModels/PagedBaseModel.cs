using System;
using Domain.Interfaces;

namespace Domain.Model.BaseModels
{ 
    public abstract class PagedBaseModel : PagedModel
    {
        [IsTableColumn(true)]
        public DateTime CreatedAt { get; set; }
        [IsTableColumn(true)]
        public long CreatedByUserId { get; set; }
        [IsTableColumn(true)]
        public string CreatedByFirstName { get; set; }
        [IsTableColumn(true)]
        public string CreatedByLastName { get; set; }
        [IsTableColumn(true)]
        public long ClientId { get; set; }
        [IsTableColumn(true)]
        public bool Deleted { get; set; }
        [IsTableColumn(true)]
        public DateTime? DeletedAt { get; set; }

        protected PagedBaseModel() { }

        protected PagedBaseModel(
            long createdByUserId,
            string createdByFirstName,
            string createdByLastName,
            DateTime? createdAt = null,
            bool? deleted = false,
            DateTime? deletedAt = null)
        {
            CreatedAt = createdAt ?? DateTime.Now;
            CreatedByUserId = createdByUserId;
            CreatedByFirstName = createdByFirstName;
            CreatedByLastName = createdByLastName;
            Deleted = deleted ?? false;
            DeletedAt = deletedAt;
        }

        protected PagedBaseModel(
            ICurrentUser createdBy,
            DateTime? createdAt = null,
            bool? deleted = false,
            DateTime? deletedAt = null)
        {
            CreatedByUserId = createdBy.UserId;
            CreatedByFirstName = createdBy.FirstName;
            CreatedByLastName = createdBy.LastName;
            ClientId = createdBy.CurrentClientId;

            CreatedAt = createdAt ?? DateTime.Now;
            Deleted = deleted ?? false;
            DeletedAt = deletedAt;
        }
        public void Validate()
        {
            if (CreatedAt == DateTime.MinValue || CreatedAt == DateTime.MaxValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
            if (CreatedByUserId < 0) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (CreatedByFirstName.Empty()) throw new ArgumentException("CreatedByFirstName Is not Set", "CreatedByFirstName");
            if (CreatedByLastName.Empty()) throw new ArgumentException("CreatedByLastName Is not Set", "CreatedByLastName");
            if (Deleted && (!DeletedAt.HasValue || DeletedAt.Value == DateTime.MinValue || DeletedAt.Value == DateTime.MaxValue))
                throw new ArgumentException("DeletedAt is not Set", "DeletedAt");

        }

    }

}
