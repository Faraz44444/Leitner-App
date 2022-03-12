using System.Collections.Generic;

namespace TagPortal.Domain.Aggregated.Stats
{
    public class StatsItemDetailsReportData
    {
        public double Total { get; set; }
        public double Growth { get; set; }
        public List<StatsItemDatasetReportData> DataSet { get; set; }
    }
}
