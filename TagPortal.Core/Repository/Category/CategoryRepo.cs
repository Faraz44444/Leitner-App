using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request.Category;
using TagPortal.Domain;
using TagPortal.Domain.Model.Category;
using TagPortal.Extensions;

namespace TagPortal.Core.Repository.Category
{
    class CategoryRepo : BaseRepository
    {
        public CategoryRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }
        private string GetSql(CategoryRequest request, bool usePaging = false)
        {
            string sql = $@"
                SELECT 
                   ct.CategoryId, ct.CategoryName, ct.CategoryPriority, ct.CreatedAt,  ct.CreatedByUserId,
                   ct.WeeklyLimit, ct.MonthlyLimit,
                   u.FirstName AS 'CreatedByFirstName', u.LastName AS 'CreatedByLastName'
                    {UsePagingColumn(usePaging)}
                FROM dbo.CATEGORY_TAB ct
                    LEFT JOIN [USER_TAB] u on u.UserId = ct.CreatedByUserId
            ";

            if (request.CategoryId.HasValue && request.CategoryId > 0)
            {
                sql += "WHERE ct.CategoryId = @CategoryId";
                return sql;
            }

            var searchParam = new List<string>();

            if (!String.IsNullOrEmpty(request.CategoryName)) searchParam.Add("ct.CategoryName like '%' + @CategoryName + '%'");
            if (request.CategoryPriority > 0) searchParam.Add("ct.CategoryPriority = @CategoryPriority");
            if (request.WeeklyLimit > 0) searchParam.Add("ct.WeeklyLimit >= @WeeklyLimit");
            if (request.MonthlyLimit > 0) searchParam.Add("ct.MonthlyLimit >= @MonthlyLimit");
            if (!String.IsNullOrEmpty(request.CreatedByFullName)) searchParam.Add("u.FirstName like '%' + @CreatedByFirstName + '%'");
            if (!String.IsNullOrEmpty(request.CreatedByFullName)) searchParam.Add("u.LastName like '%' + @CreatedByFirstName + '%'");
            if (request.CreatedAt > DateTime.MinValue) searchParam.Add("ct.Date <= @DateTo");
            if (request.CreatedByUserId > 0) searchParam.Add("ct.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParam.Add("ct.CreatedAt <= @CreatedFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParam.Add("ct.CreatedAt <= @CreatedTo");

            if (searchParam.Any()) sql += "WHERE " + searchParam.Join(" AND ");

            sql += $@"
                ORDER BY    
                    CASE WHEN @OrderBy = {(int)CategoryOrderByEnum.CategoryName} THEN ct.CategoryName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)CategoryOrderByEnum.CategoryPriority} THEN ct.CategoryPriority END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)CategoryOrderByEnum.WeeklyLimit} THEN ct.WeeklyLimit END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)CategoryOrderByEnum.MonthlyLimit} THEN ct.MonthlyLimit END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)CategoryOrderByEnum.CreatedAt} THEN ct.CreatedAt END {EnumOrderByDirectionToSql(request.OrderByDirection)}
                {UsePagingOffset(usePaging)}
            ";

            return sql;
        }

        internal PagedModel<CategoryModel> GetPagedList(CategoryRequest request)
        {
            return DbGetPagedList<CategoryModel>(GetSql(request, usePaging: true), request);
        }

        internal List<CategoryModel> GetList(CategoryRequest request)
        {
            return DbGetList<CategoryModel>(GetSql(request, usePaging: false), request);

        }

        internal CategoryModel GetById(CategoryRequest request)
        {
            return DbGetFirstOrDefault<CategoryModel>(GetSql(request, usePaging: false), request);
        }

        internal CategoryModel SingleOrDefault(CategoryRequest request)
        {
            return DBGetSingleOrDefault<CategoryModel>(GetSql(request, usePaging: false), request);
        }

        internal bool Update(CategoryModel model)
        {
            string sql = @"
                UPDATE CATEGORY_TAB SET
                   CategroyName = @CategroyName,
                   CategoryPriority = @CategoryPriority,
                   WeeklyLimit = @WeeklyLimit,
                   MonthlyLimit = @MonthlyLimit,
                WHERE CategoryId = @CategoryId
            ";

            return DbUpdate(sql, GetDynamicParameters(model));
        }

        internal long Insert(CategoryModel model)
        {
            string sql = @"
                INSERT INTO dbo.CATEGORY_TAB (
                     ct.CategoryName, ct.CategoryPriority, ct.WeeklyLimit, ct.MonthlyLimit, ct.CreatedAt, ct.CreatedByUserId
                )
                OUTPUT INSERTED.CategoryId
                VALUES (
                   @CategoryName, @CategoryPriority, @WeeklyLimit, @MonthlyLimit, @CreatedAt, @CreatedByUserId
                )
            ";

            return DbInsert<long>(sql, GetDynamicParameters(model));
        }

        internal bool DeleteById(long OrderId)
        {
            string sql = @"
                DELETE FROM dbo.CATEGROY_TAB 
                WHERE CategoryId = @CategoryId
            ";

            return DbDelete(sql, new { OrderId });
        }
    }
}
