using System;

namespace TagPortal.Domain.Aggregated.ReportData
{
    public class ArticlePriceChangeReportData : PagedModel
    {
        public long SupplierArticleId { get; set; }
        public long ArticleId { get; set; }
        public string ArticleNo { get; set; }
        public string Description { get; set; }
        public string TechnicalDescription { get; set; }
        public string ClientName { get; set; }
        public string SupplierName { get; set; }
        public string SupplierArticleNo { get; set; }
        //public string SupplierEanNo { get; set; }
        public string SupplierArticleDescription { get; set; }
        public string SupplierArticleTechnicalDescription { get; set; }
        public long UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public float MinQuantity { get; set; }
        public float CurrentPrice { get; set; }
        public float PriceChange { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
    }
}
