using System;

namespace TagPortal.DataMigrator.Dto
{
    internal class PurchaseOrderDto
    {

        public int ClientId { get; set; } // FK T_CLIENT

        public string NextPendingNo { get; set; }
        public string OrderNoFromIFS { get; set; }
        public string ProfitCenterCode { get; set; } // FK T_SITE
        public long Profit_center_id { get; set; }
        public string Profit_center_name { get; set; }
        public string OrderHeaderDescription { get; set; }
        public string OrderHeaderDescriptionDetails { get; set; }
        public string BuyerInitials { get; set; } // FK T_USER
        public DateTime DeadlineDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public DateTime OrderGeneratedDate { get; set; }
        public bool IsToTagit { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsTaggingCompleted { get; set; }
        public string OrderType { get; set; }
        public string OrderShippedByText { get; set; }
        public long Delivery_terms_id { get; set; }
        public string Delivery_terms_name { get; set; }

        public string ShippedById { get; set; }
        public string YourReferenceText { get; set; }
        public string YourOfferText { get; set; }
        public string PaymentTermsText { get; set; }
        public long PaymentTermsId { get; set; } // FK T_PAYMENT_TERM
        public string OrderMarkingInstructions { get; set; }
        public string IFSDeliveryAddress { get; set; }
        public string TagReceiverInitials { get; set; }
        public string Currency { get; set; } // FK T_CURRENCY
        public string Project { get; set; } // FK T_PROJECT
        public string Project_name { get; set; }

        public bool Project_active { get; set; }

        public bool Project_is_return_project { get; set; }

        public bool IHMRequired { get; set; }
        public string Sfi { get; set; } // FK T_SFI
        public string Sfi_name { get; set; }

        public string Account { get; set; } // FK T_ACCOUNT
        public string Department { get; set; }
        public long RepairItemId { get; set; }
        public string OrderLineActivitySequence { get; set; } // FK T_ACTIVITY

        public string OrderSupplierIFSId { get; set; } // FK T_SUPPLIER
        public string Supplier_name { get; set; }
        public string Supplier_country_name { get; set; }
        public string Supplier_address_1 { get; set; }
        public string Supplier_address_2 { get; set; }
        public string Supplier_zipcode { get; set; }
        public string Supplier_city { get; set; }
        public string Supplier_orgno { get; set; }
        public string Supplier_emal { get; set; }
    }
}
