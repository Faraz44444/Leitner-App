using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request.Role;
using TagPortal.Domain;
using TagPortal.Domain.Model.Role;

namespace TagPortal.Core.Repository.Role
{
    internal class RoleRepo : BaseRepository
    {
        public RoleRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(RoleRequest req = null, bool usePaging = false, long id = 0)
        {
            var sql = $@"SELECT RoleId, Name, IsLocked  {UsePagingColumn(usePaging)}
                        FROM ROLE_TAB";

            if (id > 0)
            {
                sql += " WHERE RoleId = @id ";
                return sql;
            }

            if (req == null) throw new ArgumentException("Missing Request");

            var searchParams = new List<string>();
            if (!string.IsNullOrWhiteSpace(req.Name)) searchParams.Add("Name like '%' + @Name + '%'");
            if (req.IsLocked.HasValue) searchParams.Add("IsLocked = IsLocked");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@" ORDER BY 
                    CASE WHEN @OrderBy = 1 THEN Name END {EnumOrderByDirectionToSql(req.OrderByDirection)},
                    CASE WHEN @OrderBy = 2 THEN IsLocked END {EnumOrderByDirectionToSql(req.OrderByDirection)}";

            sql += UsePagingOffset(usePaging);
            return sql;
        }

        public PagedModel<RoleModel> GetPagedList(RoleRequest request)
        {
            var p = new PagedModel<RoleModel>()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };
            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");
            request.CurrentPage -= 1;
            var sql = GetSql(request, true);
            p.Items = DbGetList<RoleModel>(sql, request);
            p.TotalNumberOfItems = p.Items.Count > 0 ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }

        public List<RoleModel> GetList(RoleRequest request)
        {
            return DbGetList<RoleModel>(GetSql(request), request);
        }

        public RoleModel GetById(long id)
        {
            return DbGetFirstOrDefault<RoleModel>(GetSql(id: id), new { id });
        }

        public long Insert(RoleModel model)
        {
            var sql = @"INSERT INTO ROLE_TAB (Name, IsLocked)
                        OUTPUT inserted.RoleId
                        VALUES (@Name, @IsLocked)";
            return DbInsert<long>(sql, GetDynamicParameters(model));
        }

        public bool Update(RoleModel model)
        {
            var sql = @"UPDATE ROLE_TAB 
                        set Name = @Name
                        where RoleId = @RoleId";
            return DbUpdate(sql, model);
        }
    }
}
