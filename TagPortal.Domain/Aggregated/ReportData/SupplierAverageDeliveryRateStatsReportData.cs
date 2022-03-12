using System;

namespace TagPortal.Domain.Aggregated.ReportData
{
    public class SupplierAverageDeliveryRateStatsReportData
    {
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public DateTime Date { get; set; }
        public decimal DeliveryRate { get; set; }
    }
}
