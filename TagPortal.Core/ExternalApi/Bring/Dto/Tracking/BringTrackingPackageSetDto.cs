using System;
using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Bring.Dto.Tracking
{
    public class BringTrackingPackageSetDto
    {
        public string statusDescription { get; set; }
        public string previousPackageNumber { get; set; }
        public string productName { get; set; }
        public int productCode { get; set; }
        public string brand { get; set; }
        public float lengthInCm { get; set; }
        public float widthInCm { get; set; }
        public float heightInCm { get; set; }
        public float volumeInDm3 { get; set; }
        public float weightInKgs { get; set; }
        public DateTime dateOfReturn { get; set; }
        public string senderName { get; set; }
        public BringTrackingAddressDto senderAddress { get; set; }
        public BringTrackingAddressDto recipientAddress { get; set; }
        public BringTrackingAddressDto recipientHandlingAddress { get; set; }

        public List<BringTrackingEventSetDto> eventSet { get; set; }
    }
}
