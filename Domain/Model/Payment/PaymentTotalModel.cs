using System;
using Domain.Model.BaseModels;

namespace Domain.Model.Payment
{
    public class PaymentTotalModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long PaymentTotalId { get; set; }
        [IsTableColumn(true)]
        public string Title { get; set; }
        [IsTableColumn(true)]
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        [IsTableColumn(true)]
        public bool IsDeposit { get; set; }
        [IsTableColumn(true)]
        public float Price { get; set; }
        [IsTableColumn(true)]
        public DateTime Date { get; set; }

        public PaymentTotalModel() { }

        public PaymentTotalModel(string title, long businessId, string businessName, bool isDeposit,
            float price, DateTime date, long clientId, long createdByUserId,
            string createdByFirstName, string createdByLastName) : base(createdByUserId, createdByFirstName, createdByLastName)
        {
            Title = title;
            BusinessId = businessId;
            BusinessName = businessName;
            IsDeposit = isDeposit;
            Price = price;
            Date = date;
            ClientId = clientId;
            CreatedByUserId = createdByUserId;
            CreatedByFirstName = createdByFirstName;
            CreatedByLastName = createdByLastName;
            Deleted = false;
        }

        public new void Validate()
        {
            if (Title.Empty()) throw new ArgumentException("Title is not set");
            if (ClientId < 1) throw new ArgumentException("ClientId is not set");
            if (CreatedAt == DateTime.MinValue) throw new ArgumentException("CreatedAt is not set");
        }
    }
}