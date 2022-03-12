using System;

namespace TagPortal.Core.ExternalApi.DnbCurrency.Dto
{
    internal class DnbCurrencyRateResponseDto
    {
        public string baseCurrency { get; set; }
        public string quoteCurrency { get; set; }
        public string Country { get; set; }
        public DateTime updatedDate { get; set; }
        public decimal Amount { get; set; }
        public decimal buyRateTransfer { get; set; }
        public decimal sellRateTransfer { get; set; }
        public decimal midRate { get; set; }
    }
}
