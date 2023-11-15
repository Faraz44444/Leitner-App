using System;
using Domain.Model.BaseModels;

namespace Domain.Model.Payment
{
    public class PaymentRecommendationModel : PagedBaseModel
    {
        public string Title { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool IsDeposit { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public float AveragePrice { get; set; }
        public DateTime Date { get; set; }
        public long Repetition { get; set; }
        private PaymentRecommendationModel() { }

    }
}