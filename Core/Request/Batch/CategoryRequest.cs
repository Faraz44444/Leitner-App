using Core.Request.BaseRequests;
using System.Collections.Generic;

namespace Core.Request.Batch
{
    public class BatchRequest : BaseRequestPaged
    {
        public long BatchId { get; set; }
        public string BatchNo { get; set; }
        public override string WhereSql()
        {
            if (BatchId > 0)
            {
                return "WHERE b.BatchId = @BatchId";
            }

            var searchParams = new List<string>();
            if (!BatchNo.Empty()) searchParams.Add("b.BatchNo like '%' + @BatchNo + '%'");
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumBatchRequest
    {
        BatchNo = 1,
        CreatedAt = 7,
        CreatedByFullName = 8
    }
}