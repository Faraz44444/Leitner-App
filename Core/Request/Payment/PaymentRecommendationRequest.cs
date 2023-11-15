using Core.Request.BaseRequests;
using System;

namespace Core.Request.Payment
{
    public class PaymentRecommendationRequest : BaseRequestPaged
    {
        public string Title { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool? IsDeposit { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public float AveragePrice { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public long Repetition { get; set; }

    }
    public enum EnumPaymentRecommendationRequest
    {
        Date = 1,
        Title = 2,
        BusinessName = 3,
        IsDeposit = 4,
        CreatedAt = 5,
        CategoryName = 6,
        AveragePrice = 7,
        BusinessId = 8,
        Repetition = 9,
    }
}