namespace TagPortal.Core.ExternalApi.Bring.Dto.Tracking
{
    public class BringTrackingAddressDto
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public short postalCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
    }
}
