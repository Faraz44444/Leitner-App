using Core.Request.BaseRequests;
using System.Collections.Generic;

namespace Core.Request.Payment
{
    public class MaterialRequest : BaseRequestPaged
    {
        public long MaterialId { get; set; }
        public long BatchId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            if (MaterialId > 0)
            {
                return " WHERE p.PaymentId = @PaymentId";
            }

            var searchParams = new List<string>();
            if (!Question.Empty()) searchParams.Add($"{TableAlias}.Question like '%' + @Question + '%'");
            if (!Answer.Empty()) searchParams.Add($"{TableAlias}.Answer like '%' + @Answer + '%'");
            if (CategoryId > 0) searchParams.Add($"{TableAlias}.CategoryId = @CategoryId");
            if (BatchId > 0) searchParams.Add($"{TableAlias}.BatchId = @BatchId");
            if (!CategoryName.Empty()) searchParams.Add("ca.Name like '%' + @CategoryName+ '%'");

            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumPaymentRequest
    {
        Question = 1,
        Answer = 2,
        Category = 3,
    }
}