namespace TagPortal.Domain.Aggregated.ReportData
{
    public class SupplierPotentialSalesArticle : PagedModel
    {
        public long SupplierArticleId { get; set; }
        public string SupplierArticleNo { get; set; }
        public string SupplierArticleDescription { get; set; }
        public string SupplierArticleTechnicalDescription { get; set; }
        public decimal PotentialSales { get; set; }
        public decimal ActualSales { get; set; }
    }
}
