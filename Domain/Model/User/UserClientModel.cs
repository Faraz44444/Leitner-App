using System;
using Domain.Model.BaseModels;

namespace Domain.Model.User
{
    public class UserClientModel : PagedBaseModel
    {
        [IsNotTableColumn(true)]
        public long UserId { get; set; }
        [IsNotTableColumn(true)]
        public long ClientId { get; set; }
        [IsNotTableColumn(true)]
        public long RoleId { get; set; }

        public UserClientModel() { }

        public UserClientModel(long userId, long clientId, long roleId)
        {
            UserId = userId;
            ClientId = clientId;
            RoleId = roleId;
        }

        public new void Validate()
        {
            if (UserId < 1) throw new ArgumentException("UserId is not set");
            if (ClientId < 1) throw new ArgumentException("ClientId is not set");
            if (RoleId < 1) throw new ArgumentException("RoleId is not set");
        }
    }
}