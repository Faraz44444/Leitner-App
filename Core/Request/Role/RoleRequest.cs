using Core.Request.BaseRequests;
using System.Collections.Generic;

namespace Core.Request.Role
{
    public class RoleRequest : BaseRequestPaged
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            if(RoleId > 0)
            {
                return "WHERE r.RoleId = @RoleId";
            }

            var searchParams = new List<string>();
            if (!Name.Empty()) searchParams.Add("r.Name like '%' + @Name + '%'");
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumRoleRequest
    {
        Name = 1,
        Deleted = 2,
        DeletedAt = 3,
        CreatedAt = 4,
        CreatedByFullName = 5
    }
}