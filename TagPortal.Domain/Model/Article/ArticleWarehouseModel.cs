using System;

namespace TagPortal.Domain.Model.Article
{
    public class ArticleWarehouseModel : PagedModel
    {
        public long ArticleId { get; set; }
        public long SiteId { get; set; }
        public long WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseCode { get; set; }
        public decimal DefaultPurchaseAmount { get; set; }
        //LOG STUFF
        public long CreatedByUserId { get; set; }
        public string CreatedByFullName { get; set; }
        public string CreatedByEmail { get; set; }
        public DateTime CreatedDate { get; set; }
        public long UpdatedByUserId { get; set; }
        public string UpdatedByFullName { get; set; }
        public string UpdatedByEmail { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}
