using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Bring.Dto.Tracking
{
    public class BringTrackingConsignmentSetDto
    {
        public long consignmentId { get; set; }
        public long previousConsignmentId { get; set; }
        public List<BringTrackingPackageSetDto> packageSet { get; set; }
        public BringTrackingAddressDto recipientAddress { get; set; }
        public BringTrackingAddressDto recipientHandlingAddress { get; set; }
        public BringTrackingAddressDto senderAddress { get; set; }
        public string senderReference { get; set; }
        public string senderCustomerNumber { get; set; }
        public string senderCustomerMasterNumber { get; set; }
        public string senderName { get; set; }
        public bool senderIsSyspedCustomer { get; set; }
        public bool isPickupNoticeAvailable { get; set; }
        public string senderCustomerType { get; set; }
        public float totalWeightInKgs { get; set; }
        public float totalVolumeInDm3 { get; set; }
        public float widthInCm { get; set; }
        public float lengthInCm { get; set; }
        public float heightInCm { get; set; }

        public BringTrackingErrorDto error { get; set; }
    }
}
