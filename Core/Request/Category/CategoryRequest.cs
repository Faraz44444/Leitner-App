using Core.Request.BaseRequests;
using System.Collections.Generic;

namespace Core.Request.Batch
{
    public class CategoryRequest : BaseRequestPaged
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public override string WhereSql()
        {
            if (CategoryId > 0)
            {
                return "WHERE ca.CategoryId = @CategoryId";
            }

            var searchParams = new List<string>();
            if (!Name.Empty()) searchParams.Add("ca.Name like '%' + @Name + '%'");
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumCategoryRequest
    {
        Name = 1,
        CreatedAt = 7,
        CreatedByFullName = 8
    }
}