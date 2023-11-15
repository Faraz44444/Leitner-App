using System;
using System.Collections.Generic;
using Core.Request.BaseRequests;

namespace Core.Request.User
{
    public class UserClientRequest : BaseRequestPaged
    {
        public long UserId { get; set; }
        public long ClientId { get; set; }
        public long RoleId { get; set; }

        public List<string> RequestMembersNames = new()
        {
            "Username",
            "Email",
            "FullName",
            "FullPhone",
            "LastLoginAtFrom",
            "LastLoginAtTo",
            "EmployeeId",
            "EmployeeFullName",
            "EmployeeAlias",
            "EmployeeDisplayName",
            "IsSystem"
        };

        public override string WhereSql()
        {
            var searchParams = new List<string>();
            if (UserId > 0) searchParams.Add("uc.UserId = @UserId");
            if (ClientId > 0) searchParams.Add("uc.ClientId = @ClientId");
            if (RoleId > 0) searchParams.Add("uc.RoleId = @RoleId");

            return $" WHERE {BaseWhereSql(TableAlias)} {(BaseWhereSql(TableAlias).Length > 1 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }

    }

    public enum EnumUserClientRequest
    {
        UserId=1,
        ClientId = 2,
        RoleId = 3

    }
}