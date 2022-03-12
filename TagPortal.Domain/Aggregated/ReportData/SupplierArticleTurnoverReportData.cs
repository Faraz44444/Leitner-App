namespace TagPortal.Domain.Aggregated.ReportData
{
    public class SupplierArticleTurnoverReportData : PagedModel
    {
        public long ArticleId { get; set; }
        public string ArticleNo { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleTechnicalDescription { get; set; }
        public long ArticleGroupId { get; set; }
        public string ArticleGroupName { get; set; }

        public string SupplierName { get; set; }

        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitCode { get; set; }
        public float AveragePrice { get; set; }
        public float Units { get; set; }
        public float Sum { get; set; }

    }
}
