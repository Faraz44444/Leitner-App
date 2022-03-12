namespace TagPortal.Domain.Aggregated.ReportData
{
    public class ArticleTurnoverYearlySummaryReportData
    {
        public long ArticleId { get; set; }
        public float Units { get; set; }
        public float Turnover { get; set; }
        public int Year { get; set; }
    }
}
