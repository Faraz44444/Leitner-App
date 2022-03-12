using Newtonsoft.Json;
using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Bring.Dto.Tracking
{
    public class BringTrackingDto
    {
        public string apiVersion { get; set; }
        public List<BringTrackingConsignmentSetDto> consignmentSet { get; set; }

        // HELPERS
        public string ConsignmentNo => consignmentSet[0].consignmentId.ToString();
        public string WeightInKg => consignmentSet[0].totalWeightInKgs > 0 ? $"{consignmentSet[0].totalWeightInKgs} Kg" : null;
        public string Measurement
        {
            get
            { // ONLY SHOW THE DIMENSION IF ALL OF THE DIMENSIONS ARE PROVIDED
                var cs = consignmentSet[0];
                if (cs.lengthInCm > 0 && cs.widthInCm > 0 && cs.heightInCm > 0)
                    return $"{cs.lengthInCm} x {cs.widthInCm} x {cs.heightInCm} cm";
                else
                    return null;
            }
        }

        public bool IsDelivered => consignmentSet[0].packageSet[0].eventSet[0].status == "DELIVERED";
        public string SenderName => consignmentSet[0].packageSet[0].senderName;
        public string DeliveryStatus => consignmentSet[0].packageSet[0].eventSet[0].description;

        private BringTrackingDto() { }

        public BringTrackingDto(string jsonStr)
        {
            if (!string.IsNullOrEmpty(jsonStr))
            {
                var data = JsonConvert.DeserializeObject<BringTrackingDto>(jsonStr);

                apiVersion = data.apiVersion;
                consignmentSet = data.consignmentSet;
            }
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
