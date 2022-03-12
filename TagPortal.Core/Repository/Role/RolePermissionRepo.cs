using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request.Role;
using TagPortal.Domain;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.Role;

namespace TagPortal.Core.Repository.Role
{
    internal class RolePermissionRepo : BaseRepository
    {
        public RolePermissionRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(RolePermissionsRequest req = null, bool usePaging = false, long id = 0)
        {
            var sql = $@"select tr. PermissionType, tr.RoleId, tr.IsLocked, r.Name as 'RoleName'  {UsePagingColumn(usePaging)}
                        From ROLE_PERMISSION_TAB tr
                        inner join ROLE_TAB r on r.RoleId = tr.RoleId";

            if (id > 0)
            {
                sql += " WHERE tr.RoleId = @id ";
                return sql;
            }

            if (req == null) throw new ArgumentException("Missing Request");

            var searchParams = new List<string>();
            if (req.RoleId > 0) searchParams.Add("tr.RoleId = @RoleId");
            if (!string.IsNullOrWhiteSpace(req.RoleName)) searchParams.Add("r.Name like '%' + @Name + '%'");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@" ORDER BY 
                    CASE WHEN @OrderBy = 2 THEN r.Name END {EnumOrderByDirectionToSql(req.OrderByDirection)}";

            sql += UsePagingOffset(usePaging);
            return sql;
        }

        public PagedModel<RolePermissionModel> GetPagedList(RolePermissionsRequest request)
        {
            var p = new PagedModel<RolePermissionModel>()
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };
            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");
            request.CurrentPage -= 1;
            var sql = GetSql(request, true);
            p.Items = DbGetList<RolePermissionModel>(sql, request);
            p.TotalNumberOfItems = p.Items.Count > 0 ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }

        public List<RolePermissionModel> GetList(RolePermissionsRequest request)
        {
            return DbGetList<RolePermissionModel>(GetSql(request), request);
        }

        public bool Insert(RolePermissionModel model)
        {
            var sql = @"INSERT INTO ROLE_PERMISSION_TAB (RoleId, PermissionType, IsLocked)
                        VALUES (@RoleId, @PermissionType, @IsLocked)";
            return DbInsert(sql, model);
        }

        public bool Delete(RolePermissionModel model)
        {
            var sql = @"DELETE FROM ROLE_PERMISSION_TAB
                        WHERE RoleId = @RoleId AND PermissionType = @PermissionType AND IsLocked = 0";
            return DbUpdate(sql, model);
        }

        public bool DeleteByRoleId(long roleId)
        {
            var sql = @"DELETE FROM ROLE_PERMISSION_TAB
                        WHERE RoleId = @RoleId AND IsLocked = 0";
            return DbUpdate(sql, new { roleId});
        }

    }
}
