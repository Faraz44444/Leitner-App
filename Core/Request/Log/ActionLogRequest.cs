using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Request.BaseRequests;
using Domain.Enum.EntityType;
using Domain.Enum.OperationType;

namespace Core.Request.Log
{
    public class ActionLogRequest : BaseRequestPaged
    {
        public EnumOperationType? OperationType { get; set; }
        public List<EnumOperationType> OperationTypes { get; set; }
        public string Name { get; set; }
        public EnumEntityType? EntityType { get; set; }
        public List<EnumEntityType> EntityTypes { get; set; }
        public long EntityId { get; set; }
        public long AlternateEntityId { get; set; }
        public bool IncludeEntityJson { get; set; } = false;
    }

    public enum EnumActionLogRequest
    {
        OperationType = 1,
        Name = 2,
        EntityType = 3,
        CreatedAt = 4,
        CreatedByFullName = 5
    }
}
