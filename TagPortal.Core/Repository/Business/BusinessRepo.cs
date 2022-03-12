
using System;
using System.Collections.Generic;
using System.Data;
using TagPortal.Core.Request.Business;
using TagPortal.Domain;
using TagPortal.Domain.Model.Business;

namespace TagPortal.Core.Repository.Business
{
    class BusinessRepo : BaseRepository
    {
        public BusinessRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(BusinessRequest request = null, long id = 0, bool usePaging = false)
        {
            string sql = $@"
                SELECT 
                   B.BusinessId, B.BusinessName , B.CreatedByUserId, u.FirstName AS 'CreatedByFirstName', u.LastName AS 'CreatedByLastName', B.CreatedAt
                    {UsePagingColumn(usePaging)}
                FROM dbo.BUSINESS_TAB B
                    LEFT JOIN [USER_TAB] u on u.UserId = B.CreatedByUserId
            ";

            if (id > 0)
            {
                sql += "WHERE B.BusinessId = @BusinessId";
                return sql;
            }

            var searchParams = new List<string>();

            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("B.BusinessName like '%' + @BusinessName + '%'");
            if (request.CreatedAt > DateTime.MinValue) searchParams.Add("B.CreatedAt <= @CreatedAt");
            if (request.CreatedByUserId > 0) searchParams.Add("B.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParams.Add("B.CreatedAt <= @CreatedFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParams.Add("B.CreatedAt <= @CreatedTo");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@"
                ORDER BY    
                    CASE WHEN @OrderBy = {(int)BusinessOrderByEnum.BusinessName} THEN B.BusinessName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)BusinessOrderByEnum.CreatedAt} THEN B.CreatedAt END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)BusinessOrderByEnum.CreatedByFullName} THEN u.FirstName  END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)BusinessOrderByEnum.CreatedByFullName} THEN u.LastName  END {EnumOrderByDirectionToSql(request.OrderByDirection)}


                {UsePagingOffset(usePaging)}
            ";

            return sql;
        }
        private string GetByNameSql(string name)
        {
            string sql = $@"
                SELECT 
                    B.BusinessId
                FROM dbo.BUSINESS_TAB B
                    LEFT JOIN [USER_TAB] u on u.UserId = B.CreatedByUserId
                WHERE B.BusinessName = @name
            ";
            return sql;
        }

        internal PagedModel<BusinessModel> GetPagedList(BusinessRequest request)
        {
            return DbGetPagedList<BusinessModel>(GetSql(request, usePaging: true), request);
        }

        internal List<BusinessModel> GetList(BusinessRequest request)
        {
            return DbGetList<BusinessModel>(GetSql(request, usePaging: false), request);

        }

        internal BusinessModel GetById(long id)
        {
            return DbGetFirstOrDefault<BusinessModel>(GetSql(id: id), new { id });
        }


        internal int GetByName(string name)
        {
            return DbGetFirstOrDefault<int>(GetByNameSql(name), new { name});
        }


        internal bool Update(BusinessModel model)
        {
            string sql = @"
                UPDATE BUSINESS_TAB SET
                   BusinessName = @BusinessName,
                   CreatedByUserId = @CreatedByUserId,
                   CreatedAt = @CreatedAt,

                WHERE BusinessId = @BusinessId
            ";

            return DbUpdate(sql, GetDynamicParameters(model));
        }

        internal long Insert(BusinessModel model)
        {
            string sql = @"
                INSERT INTO dbo.BUSINESS_TAB (
                     B.BusinessName, B.CreatedByUserId, B.CreatedAt
                )
                OUTPUT INSERTED.BusinessId
                VALUES (
                   @BusinessName, @CreatedByUserId, @CreatedAt
                )
            ";

            return DbInsert<long>(sql, GetDynamicParameters(model));
        }

        internal bool DeleteById(long OrderId)
        {
            string sql = @"
                DELETE FROM dbo.BUSINESS_TAB 
                WHERE BusinessId = @BusinessId
            ";

            return DbDelete(sql, new { OrderId });
        }

    }
}


