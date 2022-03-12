namespace TagPortal.Domain.Aggregated.ReportData
{
    public class ArticleTurnoverReportData : PagedModel
    {
        public long ArticleId { get; set; }
        public string ArticleNo { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleTechnicalDescription { get; set; }
        public long ArticleGroupId { get; set; }
        public string ArticleGroupName { get; set; }

        public long SupplierArticleId { get; set; }
        public string SupplierArticleNo { get; set; }
        public string SupplierArticleDescription { get; set; }
        public string SupplierArticleTechnicalDescription { get; set; }
        public long SupplierArticleGroupId { get; set; }
        public string SupplierArticleGroupName { get; set; }

        public long UnitId { get; set; }
        public string UnitName { get; set; }
        public string UnitCode { get; set; }
        public float CurrentPrice { get; set; }
        public float UnitsThisYear { get; set; }
        public float TurnoverThisYear { get; set; }
        public float UnitsLastYear { get; set; }
        public float TurnoverLastYear { get; set; }
        public float UnitsTwoYearsAgo { get; set; }
        public float TurnoverTwoYearsAgo { get; set; }
    }
}
