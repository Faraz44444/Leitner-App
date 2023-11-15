using Domain.Model.BaseModels;
using System;

namespace Domain.Model.Client
{
    public class ClientModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long ClientId { get; set; }
        [IsTableColumn(true)]
        public string Name { get; set; }
        [IsTableColumn(true)]
        public string OrganizationNo { get; set; }
        [IsTableColumn(true)]
        public string Email { get; set; }

        public ClientModel() { }

        public ClientModel(
            string name,
            string organizationNo,
            long createdByUserId,
            string createdByFirstName,
            string createdByLastName,
            string email = "") : base(createdByUserId, createdByFirstName, createdByLastName)
        {
            this.Name = name;
            this.OrganizationNo = organizationNo;
            this.Email = email;
        }

        public new void Validate()
        {
            if (Name.Empty()) throw new ArgumentException("Name is not set");
            if (OrganizationNo.Empty()) throw new ArgumentException("OrganizationNo is not set");
            if (Email.Empty()) throw new ArgumentException("Email is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }

        public string PhoneCountryCode { get; private set; }
    }
}
