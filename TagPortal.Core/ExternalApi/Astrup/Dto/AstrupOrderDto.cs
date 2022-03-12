using System;
using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.Astrup.Dto
{

    public class AstrupOrderDto
    {
        public int Id { get; set; }
        public int Status { get; set; }
        public string PurchaseOrderNo { get; set; }
        public string ErpOrderNo { get; set; }
        public DateTime? OrderDate { get; set; }
        public string CustomerRef { get; set; }
        public List<AstrupOrderLineDto> OrderLines { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VAT { get; set; }
        public decimal TotalGrossPrice { get; set; }
        public int CustomerNo { get; set; }
        public string Note { get; set; }
    }

    public class AstrupOrderLineDto
    {
        public string Number { get; set; }
        public string CustomerArticleNumber { get; set; }
        public decimal Quantity { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public decimal Sum { get; set; }
        public Dictionary<string, decimal> AdditionalUnits { get; set; }
        public decimal Stock { get; set; }
        public Dictionary<string, decimal> StockAdditionalUnits { get; set; }

    }

    public class AstrupOrderLineAdditionalUnitDto
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }

    }
}
