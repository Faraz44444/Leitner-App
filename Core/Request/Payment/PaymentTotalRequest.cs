using Core.Request.BaseRequests;
using Domain.Enum.Category;
using System;
using System.Collections.Generic;

namespace Core.Request.Payment
{
    public class PaymentTotalRequest : BaseRequestPaged
    {
        public long PaymentTotalId { get; set; }
        public string Title { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool? IsDeposit { get; set; }
        public float Price { get; set; }
        public float PriceFrom { get; set; }
        public float PriceTo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            if (PaymentTotalId > 0)
            {
                return "WHERE tp.PaymentTotalId = @PaymentTotalId";
            }
            
            var searchParams = new List<string>();
            if (!Title.Empty()) searchParams.Add("tp.Title like '%' + @Title + '%'");
            if (IsDeposit.HasValue) searchParams.Add("tp.IsDeposit = @IsDeposit");
            if (BusinessId > 0) searchParams.Add("tp.BusinessId = @BusinessId");
            
            if (Price > 0) searchParams.Add("tp.Price = @Price");
            if (PriceFrom > 0) searchParams.Add("tp.Price >= @PriceFrom");
            if (PriceTo > 0) searchParams.Add("tp.Price <= @PriceTo");
            
            if (Date.HasValue && Date > DateTime.MinValue && Date < DateTime.MaxValue)
                searchParams.Add("tp.Date = @Date");
            if (DateFrom.HasValue && DateFrom > DateTime.MinValue && DateFrom < DateTime.MaxValue) 
                searchParams.Add("CAST(tp.Date AS DATE) >= CAST(@DateFrom As DATE)");
            if (DateTo.HasValue && DateTo > DateTime.MinValue && DateTo < DateTime.MaxValue)
                searchParams.Add("CAST(tp.Date AS DATE) <= CAST(@DateTo As DATE)");
            
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
}