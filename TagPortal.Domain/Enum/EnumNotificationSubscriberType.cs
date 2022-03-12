using System.ComponentModel;

namespace TagPortal.Domain.Enum
{
    public enum EnumNotificationSubscriberType
    {
        //TAG
        [Description("Tag It News")]
        Tag_News = 1,
        [Description("Purchase Order Complete Delivery")]
        Tag_PurchaseOrderCompleteDelivery = 2,
        [Description("Order Received")]
        Tag_Supplier_PurchaseOrderReceived = 3,

        //BITS
        [Description("Bits & Pieces News")]
        Bits_News = 1000,
        [Description("Purchase Order Confirmed")]
        Bits_PurchaseOrderConfirmed = 1001,
        [Description("Purchase Order Complete Delivery")]
        Bits_PurchaseOrderCompleteDelivery = 1002,
        [Description("Article Turnover")]
        Bits_ArticleTurnOver = 1003,
        [Description("Price Changes")]
        Bits_PriceChanges = 1004,
        [Description("Order Lines")]
        Bits_OrderLines = 1005,


        [Description("Delivery Rate")]
        Bits_Supplier_DeliveryRate = 1006,
        [Description("Order Received")]
        Bits_Supplier_PurchaseOrderReceived = 1007,
        [Description("Supplier Article Turnover")]
        Bits_SupplierArticleTurnOver = 1008,


        //TBX
        [Description("Toolbox News")]
        Tbx_News = 2000,
    }
}
