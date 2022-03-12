using System;

namespace TagPortal.Domain.Aggregated.Stats
{
    public class StatsItemDatasetReportData
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Title { get; set; }
        public double Sum { get; set; }
    }
}
