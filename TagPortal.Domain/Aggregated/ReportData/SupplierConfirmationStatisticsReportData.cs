namespace TagPortal.Domain.Aggregated.ReportData
{
    public class SupplierConfirmationStatisticsReportData : PagedModel
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int NumberOfOrders { get; set; }
        public decimal DeliveryRate { get; set; }
        public decimal AverageConfirmDelay { get; set; }

    }
}
