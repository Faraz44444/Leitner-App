namespace TagPortal.Domain.Aggregated.ReportData
{
    public class SupplierPotentialSales : PagedModel
    {
        public decimal PotentialSales { get; set; }
        public decimal ActualSales { get; set; }
        public int Week { get; set; }
        public int Year { get; set; }
    }
}
