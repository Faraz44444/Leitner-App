using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TagPortal.DataMigrator.Dto;

namespace TagPortal.DataMigrator.Repository
{
    public class PurchaseOrderRepo : IDisposable
    {
        private IDbConnection SqlConnection;

        public PurchaseOrderRepo(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
        }

        public void Dispose()
        {
            SqlConnection.Dispose();
            SqlConnection = null;
        }

        internal List<PurchaseOrderDto> GetPurchaseOrders(int currentPage, int itemsPerPage, long clientId, DateTime fromDate)
        {
            var sql = @"select  b.ClientId, NextPendingNo, OrderNoFromIFS, 
		                        ProfitCenterCode, pc.profit_center_id, pc.name as 'profit_center_name', 
		                        OrderHeaderDescription, OrderHeaderDescriptionDetails, 		
		                        BuyerInitials, 
		                        DeadlineDate, DeliveryDate, OrderGeneratedDate, IsToTagit, IsActive, IsDeleted, IsEmailSent, IsTaggingCompleted, OrderType, 
		                        OrderShippedByText, 
		                        dt.DeliveryTermsId as 'delivery_terms_id', dt.DeliveryTermsName as 'delivery_terms_name',
		                        ShippedById, YourReferenceText, YourOfferText, PaymentTermsText, PaymentTermsId, 
		                        OrderMarkingInstructions, IFSDeliveryAddress, TagReceiverInitials, Currency,  
		                        Project, p.prosjektNavn as 'project_name', p.active as 'project_active', p.IsReturnProject as 'project_is_return_project', p.IHMRequired,
		                        sfi, s.SfiTitle as 'sfi_name',
		                        Account, Department, RepairItemId, OrderLineActivitySequence,
		                        a.NAVN as 'supplier_name', a.ADRESSE1 as 'supplier_address_1', a.ADRESSE2 as 'supplier_address_2', a.POSTNR as 'supplier_zipcode', a.POSTSTED as 'supplier_city', 
		                        a.AVGNR as 'supplier_orgno', a.epost as 'supplier_emal'
                        from CLIENT.BESTILLING_SYNCHED_FROM_TM_TAB b
                        inner join PROFIT_CENTER pc on CONVERT(varchar, pc.profit_center_id) = b.ProfitCenterCode and pc.client_id = b.ClientId 
                        inner join TAGIT.CLIENT_SUPPLIER_LINK_TAB sl on sl.ClientERPSupplierId = b.OrderSupplierIFSId and sl.ClientId = b.ClientId
                        inner join AKTOER a on a.avg_lopenr = sl.TagitSupplierId and a.AVGNR = sl.ClientERPSupplierOrgNo
                        left join DELIVERY_TERMS_TAB dt on dt.DeliveryTermsName = b.DeliveryTermsId and dt.ClientId = b.ClientId
                        left join PROJECT_TAB p on CONVERT(varchar, p.prosjekt) = b.Project and p.client_id = b.ClientId
                        left join SFI_TAB s on s.SFICode = b.SFI and s.ClientId = b.ClientId and s.SfiId <> 3
                        where b.ClientId = @clientId
                        and b.OrderGeneratedDate >= @fromDate
                        order by b.OrderNoFromIFS desc
                        OFFSET ( @CurrentPage * @ItemsPerPage ) ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";

            var p = new DynamicParameters();
            p.Add("CurrentPage", currentPage);
            p.Add("ItemsPerPage", itemsPerPage);
            p.Add("clientId", clientId);
            p.Add("fromDate", fromDate);

            return SqlConnection.Query<PurchaseOrderDto>(sql, p).ToList();
        }

        internal List<PurchaseOrderLineDto> GetPurchaseLineOrders(string nextPendingNo)
        {
            var sql = @"select NextPendingNo, OrderLineNo, Amount, Unit, d.name as 'unit_name', Price,
		                        Project, p.ProjectId as 'project_id', p.prosjekt as 'project_no', p.prosjektNavn as 'project_name', p.active as 'project_active', p.IsReturnProject as 'project_is_return_project', p.IsProjectDelivered as 'project_is_delivered',
		                        sfi, s.SfiTitle as 'sfi_name',
		                        Account, Department, Description, ResponseDeadline, PlannedDeliveryDate,
		                        Destination,
		                        IsTurnkey, ItemRepairId, ActivityNo,
		                        l.ProjectId, TagNumber, IsIHMConfirmedBySupplier, IsExtraDaysIHMRequired, ShowOrderLineForTagging,
		                        IsTaggingCompleted, l.ClientId
                        From client.BESTILLING_LINJER_SYNCHED_FROM_TM_TAB l
                        left join PROJECT_TAB p on p.ProjectId = l.ProjectId and p.client_id = l.ClientId
                        left join SFI_TAB s on s.SFICode = l.SFI and s.ClientId = l.ClientId and s.SfiId <> 3
                        left join DIMENSIONS d on d.value = l.Unit and d.client_id = l.ClientId and d.dimension_type = 1
                        where NextPendingNo = @nextPendingNo
                        and l.Amount > 0";

            var p = new DynamicParameters();
            p.Add("nextPendingNo", nextPendingNo);

            return SqlConnection.Query<PurchaseOrderLineDto>(sql, p).ToList();
        }
    }
}
