using Core.Request.BaseRequests;
using System.Collections.Generic;

namespace Core.Request.Business
{
    public class BusinessRequest : BaseRequestPaged
    {
        public long BusinessId { get; set; }
        public string Name { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            if (BusinessId > 0)
            {
                return "WHERE b.BusinessId = @BusinessId";
            }

            var searchParams = new List<string>();
            if (!Name.Empty()) searchParams.Add("b.Name like '%' + @Name + '%'");
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumBusinessRequest
    {
        Name = 1,
        Deleted = 2,
        DeletedAt = 3,
        CreatedAt = 4,
        CreatedByFullName = 5
    }
}