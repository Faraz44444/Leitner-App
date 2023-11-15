using Core.Request.BaseRequests;
using System.Collections.Generic;

namespace Core.Request.Role
{
    public class RolePermissionRequest : BaseRequestPaged
    {
        public long RoleId { get; set; }
        public int Permission { get; set; }
        public override string TableAlias { get; set; }
        public override string WhereSql()
        {
            var searchParams = new List<string>();
            if (RoleId > 0) searchParams.Add("rp.RoleId = @RoleId");
            if (Permission > 0) searchParams.Add("rp.Permission = @Permission");
            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 && searchParams.Count > 0 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }
    }
    public enum EnumRolePermissionRequest
    {
        RoleId = 1,
        Permission = 2,
        Deleted = 3,
        DeletedAt = 4,
        CreatedAt = 5,
        CreatedByFullName = 6
    }
}