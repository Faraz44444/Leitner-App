using System;
using System.Collections.Generic;
using Core.Request.BaseRequests;

namespace Core.Request.User
{
    public class UserRequest : BaseRequestPaged
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string FullPhone { get; set; }
        public DateTime? LastLoginAtFrom { get; set; }
        public DateTime? LastLoginAtTo { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeFullName { get; set; }
        public string EmployeeAlias { get; set; }
        public string EmployeeDisplayName { get; set; }
        public bool? IsSystemUser { get; set; }
        public string PasswordSalt { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? PasswordLastSetAt { get; set; }
        public bool PasswordIsAutogenereted { get; set; }
        public string PasswordResetToken { get; set; }
        public DateTime? PasswordResetTokenGeneratedAt { get; set; }
        public override string TableAlias { get; set; }

        public override string WhereSql()
        {
            var searchParams = new List<string>();
            if (UserId > 0) searchParams.Add("u.UserId = @UserId");
            if (!Email.Empty()) searchParams.Add("u.Email like '%' + @Email + '%'");
            if (!FullName.Empty()) searchParams.Add("REPLACE(CONCAT(u.FirstName, u.LastName), ' ', '') like '%' + REPLACE(@FullName, ' ', '') + '%'");
            if (!FullPhone.Empty()) searchParams.Add("REPLACE(CONCAT(pc.PhoneCode, u.Phone), ' ', '') like '%' + REPLACE(@FullPhone, ' ', '') + '%'");
            if (!Username.Empty()) searchParams.Add("u.Username like '%' + @Username + '%'");
            if (EmployeeId > 0) searchParams.Add("u.EmployeeId = @EmployeeId");
            if (!EmployeeFullName.Empty()) searchParams.Add("REPLACE(CONCAT(e.FirstName, e.LastName), ' ', '') like '%' + REPLACE(@EmployeeFullName, ' ', '') + '%'");
            if (!EmployeeAlias.Empty()) searchParams.Add("e.Alias like '%' + @EmployeeAlias + '%'");
            if (IsSystemUser.HasValue) searchParams.Add("u.IsSystemUser = @IsSystemUser");
            if (!PasswordHash.Empty()) searchParams.Add("u.PasswordResetToken = @PasswordResetToken");
            if (!PasswordHash.Empty()) searchParams.Add("u.PasswordHash = @PasswordHash");
            if (ClientId > 0) searchParams.Add("u.CurrentClientId = @ClientId");
            var baseWhereSql = BaseWhereSql(TableAlias, ignoreClientId: true);
            return $" WHERE {baseWhereSql} {(baseWhereSql.Length > 1 ? " AND " : "")} {string.Join(" AND ", searchParams)}";
        }

    }

    public enum EnumUserRequest
    {
        Username = 1,
        Email = 2,
        FullName = 3,
        FullPhone = 4,
        Deleted = 5,
        DeletedAt = 6,
        CreatedAt = 7,
        CreatedByFullName = 8,
        LastLoginAt = 9,
        EmployeeFullName = 10,
        EmployeeAlias = 11,
        EmployeeDisplayName = 12
    }
}