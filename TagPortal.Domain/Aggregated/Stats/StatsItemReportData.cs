namespace TagPortal.Domain.Aggregated.Stats
{
    public class StatsItemReportData
    {
        public StatsItemDetailsReportData Year { get; set; }
        public StatsItemDetailsReportData Month { get; set; }
        public StatsItemDetailsReportData Week { get; set; }
        public StatsItemDetailsReportData Day { get; set; }
    }
}
