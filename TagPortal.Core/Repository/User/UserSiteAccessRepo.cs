using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request.User;
using TagPortal.Domain;
using TagPortal.Domain.Aggregated.User;

namespace TagPortal.Core.Repository.User
{
    internal class UserSiteAccessRepo : BaseRepository
    {
        public UserSiteAccessRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(UserSiteAccessRequest req = null, bool usePaging = false)
        {
            var sql = $@"
                SELECT
                        u.UserId, u.Username, u.FirstName, u.LastName, r.RoleId, r.Name as 'RoleName'
                FROM USER_TAB u
                    INNER JOIN ROLE_TAB r on r.RoleId = u.RoleId
                    INNER JOIN ROLE_PERMISSION_TAB rp on rp.RoleId = r.RoleId";

            if (req == null) throw new ArgumentException("Missing Request");

            var searchParams = new List<string>();
            if (req.UserId > 0) searchParams.Add("u.UserId = @UserId");
            if (req.RoleId > 0) searchParams.Add("r.RoleId = @RoleId");
            if (!string.IsNullOrWhiteSpace(req.RoleName)) searchParams.Add("r.Name like '%' + @RoleName + '%'");
            if (!string.IsNullOrWhiteSpace(req.Username)) searchParams.Add("u.Username like '%' + @Username + '%'");
            if (!string.IsNullOrWhiteSpace(req.UserFullName)) searchParams.Add("CONCAT(u.FirstName, u.LastName) LIKE '%' + @Username + '%'");
            if (req.PermissionType.HasValue) searchParams.Add("rp.PermissionType = @PermissionType");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += " group by u.UserId, u.Username, u.FirstName, u.LastName, r.RoleId, r.Name ";

            sql += $@" ORDER BY 
                    CASE WHEN @OrderBy = 2 THEN r.Name END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 3 THEN u.Username END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 4 THEN u.FirstName END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 4 THEN u.LastName END {EnumOrderByDirectionToSql(req.OrderByDirection)}";

            sql += UsePagingOffset(usePaging);
            return sql;
        }

        public PagedModel<UserSiteAccess> GetPagedList(UserSiteAccessRequest request)
        {
            var p = new PagedModel<UserSiteAccess>()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };
            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");
            request.CurrentPage -= 1;
            var sql = GetSql(request, true);
            p.Items = DbGetList<UserSiteAccess>(sql, request);
            p.TotalNumberOfItems = p.Items.Count > 0 ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }

        public List<UserSiteAccess> GetList(UserSiteAccessRequest request)
        {
            return DbGetList<UserSiteAccess>(GetSql(request), request);
        }
    }
}
