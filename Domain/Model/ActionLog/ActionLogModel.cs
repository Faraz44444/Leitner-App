using Domain.Enum.EntityType;
using Domain.Enum.OperationType;
using Domain.Interfaces;
using Domain.Model.BaseModels;
using Extensions;
using System;

namespace Domain.Model.ActionLog
{
    public class ActionLogModel : PagedBaseModel
    {
        public long ActionLogId { get; set; }
        public string Name { get; set; }
        public string EntityJson { get; set; }
        public EnumEntityType EntityType { get; set; }
        public EnumOperationType OperationType { get; set; }
        public long EntityId { get; set; }
        public long AlternateEntityId { get; set; }

        public ActionLogModel() { }

        public ActionLogModel(ICurrentUser createdBy, string name, string entityJson, EnumEntityType entityType, EnumOperationType operationType,
            long entityId, long alternateEntityId = 0) : base(createdBy)
        {
            Name = name;
            EntityJson = entityJson;
            EntityType = entityType;
            OperationType = operationType;
            EntityId = entityId;
        }

        public new void Validate()
        {
            if (EntityType.Empty()) throw new ArgumentException("EntityType is not set");
            if (OperationType.Empty()) throw new ArgumentException("OperationType is not set");
            if (EntityId < 1) throw new ArgumentException("EntityId is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}
