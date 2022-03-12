using System;

namespace TagPortal.DataMigrator.Dto
{
    internal class PurchaseOrderLineDto
    {
        public string NextPendingNo { get; set; }
        public int OrderLineNo { get; set; }
        public double Amount { get; set; }
        public long Unit { get; set; } // FK UNIT
        public string Unit_name { get; set; }
        public decimal Price { get; set; }
        public string Project { get; set; } //FK PROJECT
        public long Project_id { get; set; }
        public string Project_no { get; set; }
        public string Project_name { get; set; }
        public bool Project_active { get; set; }
        public bool Project_is_return_project { get; set; }
        public bool Project_is_delivered { get; set; }
        public string Sfi { get; set; } //FK SFI
        public string Sfi_name { get; set; }
        public string Account { get; set; } //FK LEDGER ACCOUNT
        public string Department { get; set; }
        public string Description { get; set; }
        public DateTime ResponseDeadline { get; set; }
        public DateTime PlannedDeliveryDate { get; set; }
        public string Destination { get; set; } //FK DESTINATION
        public bool IsTurnkey { get; set; }
        public long ItemRepairId { get; set; }
        public string ActivityNo { get; set; } //FK ACTIVITY
        public long ProjectId { get; set; }
        public string TagNumber { get; set; }
        public bool IsIHMConfirmedBySupplier { get; set; }
        public bool IsExtraDaysIHMRequired { get; set; }
        public bool ShowOrderLineForTagging { get; set; }
        public bool IsTaggingCompleted { get; set; }
        public long ClientId { get; set; }

    }
}
