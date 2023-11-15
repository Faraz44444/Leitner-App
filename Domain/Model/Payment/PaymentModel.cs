using System;
using Domain.Model.BaseModels;

namespace Domain.Model.Payment
{
    public class PaymentModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long PaymentId { get; set; }
        [IsTableColumn(true)]
        public string Title { get; set; }
        [IsTableColumn(true)]
        public long PaymentPriorityId { get; set; }
        public string PaymentPriorityName { get; set; }
        [IsTableColumn(true)]
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        [IsTableColumn(true)]
        public bool IsDeposit { get; set; }
        [IsTableColumn(true)]
        public bool IsPaidToPerson { get; set; }
        [IsTableColumn(true)]
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        [IsTableColumn(true)]
        public float Price { get; set; }
        [IsTableColumn(true)]
        public DateTime Date { get; set; }
        [IsTableColumn(true)]
        public DateTime ApprovedAt { get; set; }

        public PaymentModel() { }

        public PaymentModel(string title, long paymentPriorityId, long businessId, string businessName, bool isDeposit,
            bool isPaidToPerson, long categoryId, float price, DateTime date, long clientId, long createdByUserId,
            string createdByFirstName, string createdByLastName) : base(createdByUserId, createdByFirstName, createdByLastName)
        {
            Title = title;
            PaymentPriorityId = paymentPriorityId;
            BusinessId = businessId;
            BusinessName = businessName;
            IsDeposit = isDeposit;
            IsPaidToPerson = isPaidToPerson;
            CategoryId = categoryId;
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