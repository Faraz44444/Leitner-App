namespace TagPortal.Domain.Aggregated.ReportData
{
    public class SiteTurnoverReportData : PagedModel
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public decimal TurnoverAmount { get; set; }
    }
}
