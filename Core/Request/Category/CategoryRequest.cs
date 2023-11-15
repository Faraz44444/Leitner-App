using Core.Request.BaseRequests;
using Domain.Enum.Category;
using System;
using System.Collections.Generic;

namespace Core.Request.Category
{
    public class CategoryRequest : BaseRequestPaged
    {
        public long CategoryId { get; set; }
        public string Name { get; set; }
        public EnumCategoryPriority Priority { get; set; }
        public float WeeklyLimit { get; set; }
        public float MonthlyLimit { get; set; }
        public override string TableAlias { get; set; }
        public bool? HasMonthlyLimit { get; set; }
        public override string WhereSql()
        {
            if (CategoryId > 0)
            {
                return "WHERE ca.CategoryId = @CategoryId";
            }

            var searchParams = new List<string>();
            if (!Name.Empty()) searchParams.Add("ca.Name like '%' + @Name + '%'");
            if (Priority > 0 &&
                Array.IndexOf(Enum.GetValues(typeof(EnumCategoryPriority)), Priority) != -1)
                searchParams.Add("ca.Priority = @Priority");
            if (WeeklyLimit > 0) searchParams.Add("ca.WeeklyLimit like '%' + @WeeklyLimit + '%'");
            if (MonthlyLimit > 0) searchParams.Add("ca.MonthlyLimit like '%' + @MonthlyLimit + '%'");
            if(HasMonthlyLimit.HasValue)
            {
                if (HasMonthlyLimit.Value)
                    searchParams.Add("ca.MonthlyLimit IS NOT NULL");
                else
                    searchParams.Add("ca.MonthlyLimit IS NULL");
            }
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumCategoryRequest
    {
        Name = 1,
        Priority = 2,
        WeeklyLimit = 3,
        MonthlyLimit = 4,
        Deleted = 5,
        DeletedAt = 6,
        CreatedAt = 7,
        CreatedByFullName = 8
    }
}