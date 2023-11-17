using System;
using Domain.Interfaces;

namespace Domain.Model.BaseModels
{
    public abstract class PagedBaseModel : PagedModel
    {
        public DateTime CreatedAt { get; set; }
        public long CreatedByUserId { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }

        protected PagedBaseModel() { }

        protected PagedBaseModel(
            long createdByUserId,
            DateTime? createdAt = null,
            bool? deleted = false,
            DateTime? deletedAt = null)
        {
            CreatedAt = createdAt ?? DateTime.Now;
            CreatedByUserId = createdByUserId;
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
            CreatedAt = createdAt ?? DateTime.Now;
            Deleted = deleted ?? false;
            DeletedAt = deletedAt;
        }
        public void Validate()
        {
            if (CreatedAt == DateTime.MinValue || CreatedAt == DateTime.MaxValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
            if (CreatedByUserId < 0) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (Deleted && (!DeletedAt.HasValue || DeletedAt.Value == DateTime.MinValue || DeletedAt.Value == DateTime.MaxValue))
                throw new ArgumentException("DeletedAt is not Set", "DeletedAt");
        }
    }
}
