﻿using System;
using System.Data;

namespace TagPortal.Core.Repository.Security
{
    internal class SecurityRepo : BaseRepository
    {
        public SecurityRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        public string GetPasswordHashByUserId(long userId)
        {
            var sql = @"SELECT PasswordHash FROM USER_TAB WHERE UserId = @userId";

            return DbGetFirstOrDefault<string>(sql, new { userId });
        }

        public bool SetPasswordProperties(long userId, string passwordHash, DateTime passwordLastSet, bool passwordIsAutogenerated, Guid? passwordResetGuid)
        {
            var sql = @"update USER_TAB
                        SET PasswordHash = @passwordHash, PasswordLastSetAt = @passwordLastSet, PasswordIsAutogenerated = @passwordIsAutogenerated, PasswordResetGuid = @passwordResetGuid
                        WHERE UserId = @userId";
            return DbUpdate(sql, new { userId, passwordHash, passwordLastSet, passwordIsAutogenerated, passwordResetGuid });
        }

        public bool GetPasswordIsAutogenerated(long userId)
        {
            var sql = "SELECT PasswordIsAutogenerated FROM USER_TAB WHERE UserId = @userId";
            return DbGetFirstOrDefault<bool>(sql, new { userId });
        }

        public DateTime? GetPasswordLastSetAt(long userId)
        {
            var sql = "SELECT PasswordLastSetAt FROM USER_TAB WHERE UserId = @userId";
            return DbGetFirstOrDefault<DateTime?>(sql, new { userId });
        }

        public long GetUserIdByPasswordResetGuid(Guid passwordResetGuid)
        {
            var sql = "SELECT UserId FROM USER_TAB WHERE PasswordResetGuid = @passwordResetGuid";
            return DBGetSingleOrDefault<long>(sql, new { passwordResetGuid });
        }
    }
}
