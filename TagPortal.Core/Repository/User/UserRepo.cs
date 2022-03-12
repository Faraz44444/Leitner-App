    using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Repository.User
{
    internal class UserRepo : BaseRepository
    {
        public UserRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(UserRequest req = null, bool usePaging = false, long id = 0, string usernameOrEmail = "")
        {
            var sql = $@"
                select 
                    u.UserId, u.RoleId, u.Username, u.Email, u.PasswordHash, u.FirstName, u.LastName, u.UserType, u.LastLoginAt, u.PhoneNumber,
                    U.Active, u.CssFontSize , u.ForcePasswordReset, u.ForceUserInformationUpdate, u.UserInitials
					{UsePagingColumn(usePaging)}
                From USER_TAB u";

            if (id > 0)
            {
                sql += " WHERE u.UserId = @id ";
                return sql;
            }
            else if (!string.IsNullOrWhiteSpace(usernameOrEmail))
            {
                sql += " WHERE (u.Username = @usernameOrEmail or u.Email = @usernameOrEmail)";
                return sql;
            }

            if (req == null) throw new ArgumentException("Missing Request");

            var searchParams = new List<string>();

            if (req.UserType.HasValue) searchParams.Add("u.UserType = @UserType");
            if (!string.IsNullOrWhiteSpace(req.Username)) searchParams.Add("u.Username like '%' + @Username + '%'");
            if (!string.IsNullOrWhiteSpace(req.FullName)) searchParams.Add("CONCAT(u.FirstName, u.LastName) LIKE '%' + @FullName + '%'");
            if (!string.IsNullOrWhiteSpace(req.Email)) searchParams.Add("u.Email like '%' + @Email + '%'");
            if (!string.IsNullOrWhiteSpace(req.UserInitials)) searchParams.Add("u.UserInitials like '%' + @UserInitials + '%'");
            

            if (req.Active.HasValue) searchParams.Add("u.Active = @Active");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@" ORDER BY 
                    CASE WHEN @OrderBy = 1 THEN u.UserType END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 2 THEN u.Username END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 3 THEN u.Email END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 4 THEN u.FirstName END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 4 THEN u.LastName END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 6 THEN u.Active END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 7 THEN u.LastLoginAt END {EnumOrderByDirectionToSql(req.OrderByDirection)}";

            sql += UsePagingOffset(usePaging);
            return sql;
        }

        public PagedModel<UserModel> GetPagedList(UserRequest request)
        {
            var p = new PagedModel<UserModel>()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };
            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");
            request.CurrentPage -= 1;
            var sql = GetSql(request, true);
            p.Items = DbGetList<UserModel>(sql, request);
            p.TotalNumberOfItems = p.Items.Count > 0 ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }

        public List<UserModel> GetList(UserRequest request)
        {
            return DbGetList<UserModel>(GetSql(request), request);
        }

        public UserModel GetById(long id)
        {
            return DbGetFirstOrDefault<UserModel>(GetSql(id: id), new { id });
        }
        public UserModel GetByUsernameOrEmail(string usernameOrEmail)
        {
            return DBGetSingleOrDefault<UserModel>(GetSql(usernameOrEmail: usernameOrEmail), new { usernameOrEmail });
        }
        public long Insert(UserModel model, string temporaryPasswordHash, Guid? passwordResetGuid = null)
        {
            var sql = @"
                INSERT INTO USER_TAB (
                    UserType, Username, UserInitials, Email, FirstName, LastName, ForcePasswordReset,
                    ForceUserInformationUpdate, Active, CssFontSize, LastLoginAt, PasswordHash, PhoneNumber)
                OUTPUT inserted.UserId VALUES (
                    @UserType, @Username, @UserInitials, @Email, @FirstName, @LastName, @ForcePasswordReset, 
                    @ForceUserInformationUpdate, @Active, @CssFontSize, @LastLoginAt, @PasswordHash, @PhoneNumber)";

            var p = new DynamicParameters(model);
            p.Add("UserType", model.UserType);
            p.Add("Username", model.Username);
            p.Add("Email", model.Email);
            p.Add("FirstName", model.FirstName);
            p.Add("LastName", model.LastName);
            p.Add("ForcePasswordReset", model.ForcePasswordReset);
            p.Add("ForceUserInformationUpdate", model.ForceUserInformationUpdate);
            p.Add("Active", model.Active);
            p.Add("CssFontSize", model.CssFontSize);
            p.Add("LastLoginAt", model.LastLoginAt);
            p.Add("PasswordHash", temporaryPasswordHash);
            p.Add("UserInitials", model.UserInitials);
            p.Add("PasswordResetGuid", passwordResetGuid ?? (object)null);
            p.Add("PhoneNumber", model.PhoneNumber);
            return DbInsert<long>(sql, p);
        }

        public bool Update(UserModel model)
        {
            var sql = @"
                UPDATE USER_TAB set 
                        Email = @Email, FirstName = @FirstName, LastName = @LastName, ForcePasswordReset = @ForcePasswordReset, LastLoginAt = @LastLoginAt, UserType=@UserType,
                        ForceUserInformationUpdate = @ForceUserInformationUpdate, Active = @Active, CssFontSize = @CssFontSize,
                        PhoneNumber =@PhoneNumber 
                WHERE 
                        UserId = @UserId";
            var p = new DynamicParameters(model);
            p.Add("Email", model.Email);
            p.Add("FirstName", model.FirstName);
            p.Add("LastName", model.LastName);
            p.Add("ForcePasswordReset", model.ForcePasswordReset);
            p.Add("ForceUserInformationUpdate", model.ForceUserInformationUpdate);
            p.Add("Active", model.Active);
            p.Add("CssFontSize", model.CssFontSize);
            p.Add("LastLoginAt", model.LastLoginAt);
            p.Add("PhoneNumber", model.PhoneNumber);
            return DbUpdate(sql, p);
        }

        public bool UpdateLeftNavigationMinified(long userId, bool leftNavigationMinified)
        {
            var sql = @"
                UPDATE USER_TAB set 
                    LeftNavigationMinified = @leftNavigationMinified                        
                WHERE
                    UserId = @userId";
            return DbUpdate(sql, new { userId, leftNavigationMinified });
        }

        public string GetUserInitials(long userId)
        {
            var sql = @"
                SELECT 
'                   UserInitials 
                FROM USER_TAB
                WHERE 
                    UserId = @userId";
            return DbGetFirstOrDefault<string>(sql, new { userId });
        }

        public int GetNextTagNo(long userId)
        {
            var sql = @"
                SELECT 
                    NextTagNo 
                FROM USER_TAB
                WHERE 
                    UserId = @userId";
            return DbGetFirstOrDefault<int>(sql, new { userId });
        }

        public bool UpdateNextTagNo(long userId, int nextTagNo)
        {
            var sql = @"
                UPDATE USER_TAB set 
                    NextTagNo = @NextTagNo                        
                WHERE 
                    UserId = @userId";
            return DbUpdate(sql, new { userId, nextTagNo });
        }

        public string GetTagSearchFilterJson(long userId)
        {
            var sql = @"
                SELECT 
                    TagSearchFilterJson 
                FROM USER_TAB 
                WHERE 
                    UserId = @UserId";

            return DbGetFirstOrDefault<string>(sql, new { userId });
        }

        public string SaveTagSearchFilterJson(long userId, string tagSearchFilterJson)
        {
            var sql = @"
                UPDATE USER_TAB SET 
                    TagSearchFilterJson = @tagSearchFilterJson 
                WHERE 
                    UserId = @UserId";

            return DbGetFirstOrDefault<string>(sql, new { userId, tagSearchFilterJson });
        }
    }
}
