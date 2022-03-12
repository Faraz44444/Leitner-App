using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Repository.User
{
    internal class UserSiteRepo : BaseRepository
    {
        public UserSiteRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(UserSiteRequest req = null, bool usePaging = false)
        {
            var sql = $@"SELECT us.UserId, us.SiteId, us.RoleId, us.ClientId ,
	                        u.Email as 'UserEmail', u.FirstName as 'UserFirstName', u.LastName as 'UserLastName',
	                        s.Name as 'SiteName', r.Name as 'RoleName'  {UsePagingColumn(usePaging)}
                        FROM USER_SITE_TAB us
                        inner join USER_TAB u on u.UserId = us.UserId
                        inner join SITE_TAB s on s.SiteId = us.SiteId
                        inner join ROLE_TAB r on r.RoleId = us.RoleId";

            if (req == null) throw new ArgumentException("Missing Request");

            var searchParams = new List<string>();
            if (req.UserId > 0) searchParams.Add("us.UserId = @UserId");
            if (req.SiteId > 0) searchParams.Add("us.SiteId = @SiteId");
            if (req.RoleId > 0) searchParams.Add("us.RoleId = @RoleId");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@" ORDER BY 
                    CASE WHEN @OrderBy = 1 THEN u.Email END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 2 THEN u.FirstName END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 2 THEN u.LastName END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 3 THEN s.Name END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 4 THEN r.Name END {EnumOrderByDirectionToSql(req.OrderByDirection)}";

            sql += UsePagingOffset(usePaging);
            return sql;
        }

        public PagedModel<UserSiteModel> GetPagedList(UserSiteRequest request)
        {
            var p = new PagedModel<UserSiteModel>()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };
            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");
            request.CurrentPage -= 1;
            var sql = GetSql(request, true);
            p.Items = DbGetList<UserSiteModel>(sql, request);
            p.TotalNumberOfItems = p.Items.Count > 0 ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }

        public List<UserSiteModel> GetList(UserSiteRequest request)
        {
            return DbGetList<UserSiteModel>(GetSql(request), request);
        }

        public bool Insert(UserSiteModel model)
        {
            var sql = @"INSERT INTO USER_SITE_TAB (UserId, SiteId, RoleId, ClientId)                        
                        VALUES (@UserId, @SiteId, @RoleId, @ClientId)";
            return DbInsert(sql, model);
        }

        public bool Update(UserSiteModel model)
        {
            var sql = @"UPDATE USER_SITE_TAB set 
                        RoleId = @RoleId
                        where UserId = @UserId and SiteId = @SiteId";

            return DbUpdate(sql, model);
        }

        public bool Delete(UserSiteModel model)
        {
            var sql = @"DELETE FROM USER_SITE_TAB 
                        where UserId = @UserId and SiteId = @SiteId";

            return DbDelete(sql, model);
        }

        public bool DeleteByUserId(long userId)
        {
            var sql = @"DELETE FROM USER_SITE_TAB 
                        where UserId = @UserId";

            return DbDelete(sql, new { userId });
        }
    }
}
