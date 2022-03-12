using System;

namespace TagPortal.Core.ExternalApi.Bring.Dto.Tracking
{
    public class BringTrackingEventSetDto
    {
        public string description { get; set; }
        public string status { get; set; }
        public int unitId { get; set; }
        public string unitType { get; set; }
        public int postalCode { get; set; }
        public string city { get; set; }
        public string countryCode { get; set; }
        public string country { get; set; }
        public DateTime dateIso { get; set; }
        public bool consignmentEvent { get; set; }
        public bool insignificant { get; set; }

    }
}
