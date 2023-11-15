using Core.Request.BaseRequests;
using Domain.Enum.Category;
using System;
using System.Collections.Generic;

namespace Core.Request.Payment
{
    public class PaymentPriorityRequest : BaseRequestPaged
    {
        public long PaymentPriorityId { get; set; }
        public string Name { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            if (PaymentPriorityId > 0)
            {
                return "WHERE pp.PaymentPriorityId = @PaymentPriorityId";
            }

            var searchParams = new List<string>();
            if (!Name.Empty()) searchParams.Add("pp.Name like '%' + @Name + '%'");
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumPaymentPriorityRequest
    {
        Name = 1,
        Deleted = 2,
        DeletedAt = 3,
        CreatedAt = 4,
        CreatedByFullName = 5
    }
}