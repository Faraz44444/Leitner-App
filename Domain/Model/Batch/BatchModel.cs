using Domain.Model.BaseModels;
using System;

namespace Domain.Model.Batch
{
    public class BatchModel : PagedBaseModel
    {
        public long BatchId { get; set; }

        public string BatchNo { get; set; }

        public BatchModel() { }

        public BatchModel(string batchNo, long createdByUserId) : base(createdByUserId)
        {
            BatchNo = batchNo;
            CreatedByUserId = createdByUserId;
        }

        public new void Validate()
        {
            if (BatchNo.Empty()) throw new ArgumentException("Name is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}