using Domain.Enum.EntityType;
using Domain.Enum.OperationType;
using Domain.Interfaces;
using Domain.Model.BaseModels;
using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.ActionLog
{
    public class ActionLogModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long ActionLogId { get; set; }
        [IsTableColumn(true)]
        public string Name { get; set; }
        [IsTableColumn(true)]
        public string EntityJson { get; set; }
        [IsTableColumn(true)]
        public EnumEntityType EntityType { get; set; }
        [IsTableColumn(true)]
        public EnumOperationType OperationType { get; set; }
        [IsTableColumn(true)]
        public long EntityId { get; set; }
        [IsTableColumn(true)]
        public long AlternateEntityId { get; set; }
        [IsTableColumn(true)]
        public long ClientId { get; set; }
        
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
            if (ClientId < 1) throw new ArgumentException("ClientId is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}
