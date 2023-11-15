using Core.Request.BaseRequests;
using System;

namespace Core.Request.Client
{
    public class ClientRequest : BaseRequestPaged
    {
        public string Name { get; set; }
        public string OrganizationNo { get; set; }
        public string Email { get; set; }
        public override long ClientId
        {
            get => throw new InvalidOperationException("This prop is not active");
            set => throw new InvalidOperationException("This prop is not active");
        }
    }
    public enum EnumClientRequest
    {
        Name = 1,
        OrganizationNo = 2,
        Email = 3,
        CreatedAt = 4,
        CreatedByFullName = 5,
        Deleted = 6,
        DeletedAt = 7
    }
}
