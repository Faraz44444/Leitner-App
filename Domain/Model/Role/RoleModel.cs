using System;
using Domain.Model.BaseModels;

namespace Domain.Model.Role
{
    public class RoleModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long RoleId { get; set; }
        [IsTableColumn(true)]
        public string Name { get; set; }

        public RoleModel() { }

        public RoleModel(string name, long clientId, long createdByUserId, string createdByFirstName, string createdByLastName, bool deleted): base(createdByUserId, createdByFirstName, createdByLastName)
        {
            Name = name;
            ClientId = clientId;
            CreatedAt = DateTime.Now;
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