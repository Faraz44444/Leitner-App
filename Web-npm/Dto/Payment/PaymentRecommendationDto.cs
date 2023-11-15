using System;
using Web0.Dto;

namespace Web.Dto.Payment
{
    public class PaymentRecommendationDto : BaseDto
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
        public string AveragePriceFormatted { get { return AveragePrice.ToString("## ##0.00"); } }
        public string DateFormatted { get { return Date.ToString("dd/MM/yyyy HH:mm"); } }

    }
}
