using Core.Infrastructure.Database;
using Core.Request.BaseRequests;
using System;
using System.Collections.Generic;

namespace Core.Request.Payment
{
    public class PaymentRequest : BaseRequestPaged
    {
        public long PaymentId { get; set; }
        public string Title { get; set; }
        public long PaymentPriorityId { get; set; }
        public string PaymentPriorityName { get; set; }
        public long BusinessId { get; set; }
        public string BusinessName { get; set; }
        public bool? IsDeposit { get; set; }
        public bool? IsPaidToPerson { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public float Price { get; set; }
        public float PriceFrom { get; set; }
        public float PriceTo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? ApprovedFrom { get; set; }
        public DateTime? ApprovedTo { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            if (PaymentId > 0)
            {
                return " WHERE p.PaymentId = @PaymentId";
            }

            var searchParams = new List<string>();
            if (!Title.Empty()) searchParams.Add("p.Title like '%' + @Title + '%'");
            if (IsDeposit.HasValue) searchParams.Add("p.IsDeposit = @IsDeposit");
            if (IsPaidToPerson.HasValue) searchParams.Add("p.IsPaidToPerson = @IsPaidToPerson");
            if (PaymentPriorityId > 0) searchParams.Add("p.PaymentPriorityId = @PaymentPriorityId");
            if (!PaymentPriorityName.Empty()) searchParams.Add("pp.Name like '%' + @PaymentPriorityName + '%'");
            if (BusinessId > 0) searchParams.Add("p.BusinessId = @BusinessId");
            if (!BusinessName.Empty()) searchParams.Add("b.Name like '%' + @BusinessName+ '%'");
            if (CategoryId > 0) searchParams.Add("p.CategoryId = @CategoryId");
            if (!CategoryName.Empty()) searchParams.Add("ca.Name like '%' + @CategoryName+ '%'");

            if (Price > 0) searchParams.Add("p.Price = @Price");
            if (PriceFrom > 0) searchParams.Add("p.Price >= @PriceFrom");
            if (PriceTo > 0) searchParams.Add("p.Price <= @PriceTo");

            if (Date.HasValue && Date > DateTime.MinValue && Date < DateTime.MaxValue)
                searchParams.Add("p.Date = @Date");
            if (DateFrom.HasValue && DateFrom > DateTime.MinValue && DateFrom < DateTime.MaxValue)
                searchParams.Add("CAST(p.Date AS DATE) >= CAST(@DateFrom As DATE)");
            if (DateTo.HasValue && DateTo > DateTime.MinValue && DateTo < DateTime.MaxValue)
                searchParams.Add("CAST(p.Date AS DATE) <= CAST(@DateTo As DATE)");

            if (ApprovedAt.HasValue && ApprovedAt > DateTime.MinValue && ApprovedAt < DateTime.MaxValue)
                searchParams.Add("p.ApprovedAt = @ApprovedAt");
            if (ApprovedFrom.HasValue && DateFrom > DateTime.MinValue && DateFrom < DateTime.MaxValue)
                searchParams.Add("CAST(p.ApprovedAt AS ApprovedAt) >= CAST(@ApprovedAtFrom As DATE)");
            if (ApprovedTo.HasValue && DateTo > DateTime.MinValue && DateTo < DateTime.MaxValue)
                searchParams.Add("CAST(p.ApprovedAt AS ApprovedAt) <= CAST(@ApprovedAtTo As DATE)");


            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumPaymentRequest
    {
        Date = 1,
        Title = 2,
        Deleted = 3,
        DeletedAt = 4,
        CreatedAt = 5,
        CreatedByFullName = 6,
        PriorityId = 7,
        BusinessId = 8,
        IsDeposit = 9,
        IsPaidByPerson = 10,
        CategoryId = 11,
        Price = 12,
    }
}