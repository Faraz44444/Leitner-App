using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using TagPortal.Core.Request.Payment;
using TagPortal.Domain;
using TagPortal.Domain.Model.Payment;

namespace TagPortal.Core.Repository.Payment
{
    internal class PaymentRepo : BaseRepository
    {
        public PaymentRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(PaymentRequest request = null, long id = 0, bool usePaging = false)
        {
            string sql = $@"
                SELECT 
                   p.PaymentId, p.Title, P.PaymentPriorityId, p.BusinessId, p.IsDeposit, p.IsPaidToPerson, p.CategoryId, p.Price, p.Date, p.CreatedByUserId, p.CreatedAt, 
                   u.FirstName AS 'CreatedByFirstName', u.LastName AS 'CreatedByLastName',
                   b.BusinessName As 'BusinessName',
                   c.CategoryName As 'CategoryName',
                   pp.PaymentPriorityName
                    {UsePagingColumn(usePaging)}

                FROM dbo.PAYMENT_TAB p
                    LEFT JOIN [USER_TAB] u on u.UserId = p.CreatedByUserId
                    INNER JOIN [CATEGORY_TAB] c on c.CategoryId= p.CategoryId
                    INNER JOIN [PAYMENT_PRIORITY_TAB] pp on pp.PaymentPriorityId = p.PaymentPriorityId
                    LEFT JOIN [BUSINESS_TAB] b on b.BusinessId = p.BusinessId
            ";

            if (id > 0)
            {
                sql += $@"WHERE p.PaymentId = {id}";
                return sql;
            }

            var searchParams = new List<string>();

            if (!String.IsNullOrEmpty(request.Title)) searchParams.Add("p.Title like '%' + @Title + '%'");
            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("b.BusinessName like '%' + @BusinessName + '%'");
            if (request.BusinessId > 0) searchParams.Add("p.BusinessId like '%' + @BusinessId + '%'");
            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit = @IsDeposit");
            if (request.CategoryId > 0) searchParams.Add("p.CategoryId = @CategoryId");
            if (request.PaymentPriorityId > 0) searchParams.Add("p.PaymentPriorityId = @PaymentPriorityId");
            if (!String.IsNullOrEmpty(request.CategoryName)) searchParams.Add(" c.CategoryName like '%' + @CategoryName + '%'");
            if (!String.IsNullOrEmpty(request.PaymentPriorityName)) searchParams.Add(" pp.PaymentPriorityName like '%' + @PaymentPriorityName + '%'");
            if (request.IsPaidToPerson.HasValue) searchParams.Add("p.IsPaidToPerson = @IsPaidToPerson");
            if (request.Price > 0) searchParams.Add("p.Price = @Price");
            if (request.DateFrom > DateTime.MinValue) searchParams.Add("p.Date >= @DateFrom");
            if (request.DateTo > DateTime.MinValue) searchParams.Add("p.Date < @DateTo");
            if (request.CreatedAt > DateTime.MinValue) searchParams.Add("p.Date <= @CreatedAt");
            if (request.CreatedByUserId > 0) searchParams.Add("p.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParams.Add("p.CreatedAt <= @CreatedFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParams.Add("p.CreatedAt < @CreatedTo");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@"
                ORDER BY    
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.Title} THEN p.Title END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.BusinessName} THEN b.BusinessName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.IsPaidToPerson} THEN p.IsPaidToPerson END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.CategoryName} THEN c.CategoryName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.Price} THEN p.Price END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.Date} THEN p.Date END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.CreatedByFullName} THEN p.CreatedByUserId END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.CreatedAt} THEN p.CreatedAt END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.IsDeposit} THEN p.IsDeposit END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentOrderByEnum.PaymentPriorityName} THEN pp.PaymentPriorityName END {EnumOrderByDirectionToSql(request.OrderByDirection)}

                {UsePagingOffset(usePaging)}
            ";

            return sql;
        }

        internal PagedModel<PaymentModel> GetPagedList(PaymentRequest request)
        {
            return DbGetPagedList<PaymentModel>(GetSql(request, usePaging: true), request);
        }

        internal List<PaymentModel> GetList(PaymentRequest request)
        {
            return DbGetList<PaymentModel>(GetSql(request, usePaging: false), request);

        }

        internal PaymentModel GetById(long id)
        {
            return DbGetFirstOrDefault<PaymentModel>(GetSql(id: id), new { id });
        }

        internal bool Update(PaymentModel model)
        {
            string sql = @"
                UPDATE PAYMENT_TAB SET
                   Title = @Title,
                   PaymentPriorityId = @PaymentPriorityId,
                   BusinessId = @BusinessId,
                   IsDeposit = @IsDeposit,
                   IsPaidToPerson = @IsPaidToPerson,
                   CategoryId = @CategoryId,
                   Price = @Price,
                   Date = @Date,
                   CreatedByUserId = @CreatedByUserId,
                   CreatedAt = @CreatedAt,
                   ApprovedAt = @ApprovedAt

                WHERE PaymentId = @PaymentId
            ";

            return DbUpdate(sql, GetDynamicParameters(model));
        }

        internal long Insert(PaymentModel model)
        {
            string sql = @"
                INSERT INTO dbo.PAYMENT_TAB (
                     Title, PaymentPriorityId, BusinessId, IsDeposit, IsPaidToPerson, CategoryId,
                     Price, Date, CreatedByUserId, CreatedAt, ApprovedAt
                )
                OUTPUT INSERTED.PaymentId
                VALUES (@Title, @PaymentPriorityId, @BusinessId, @IsDeposit, @IsPaidToPerson, @CategoryID,
                   @Price, @Date, @CreatedByUserId, @CreatedAt, @ApprovedAt)
            ";

            return DbInsert<long>(sql, GetDynamicParameters(model));
        }

        internal bool DeleteById(long OrderId)
        {
            string sql = @"
                DELETE FROM dbo.PAYMENT_TAB 
                WHERE PaymentId = @PaymentId
            ";

            return DbDelete(sql, new { OrderId });
        }

        private string GetSumSql(PaymentRequest request)
        {
            string sql = $@"

                  SELECT
                         CASE WHEN SUM(p.Price) Is Null Then 0 Else SUM(p.Price) End AS 'Sum'
                  FROM dbo.PAYMENT_TAB p
                  ";

            var searchParams = new List<string>();

            if (!String.IsNullOrEmpty(request.Title)) searchParams.Add("p.Title like '%' + @Title + '%'");
            if (request.BusinessId > 0) searchParams.Add("p.BusinessId like '%' + @BusinessId + '%'");
            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("b.BusinessName like '%' + @BusinessName + '%'");
            if (!String.IsNullOrEmpty(request.CategoryName)) searchParams.Add(" c.CategoryName like '%' + @CategoryName + '%'");
            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit = @IsDeposit");
            if (request.IsPaidToPerson.HasValue) searchParams.Add("p.IsPaidToPerson = @IsPaidToPerson");
            if (request.CategoryId > 0) searchParams.Add("p.CategoryId = @CategoryId");
            if (request.PaymentPriorityId > 0) searchParams.Add("p.PaymentPriorityId = @PaymentPriorityId");
            if (request.Price > 0) searchParams.Add("p.Price = @Price");
            if (request.DateFrom > DateTime.MinValue) searchParams.Add("p.Date >= @DateFrom");
            if (request.DateTo > DateTime.MinValue) searchParams.Add("p.Date < @DateTo");
            if (request.CreatedAt > DateTime.MinValue) searchParams.Add("p.Date <= @DateTo");
            if (request.CreatedByUserId > 0) searchParams.Add("p.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParams.Add("p.CreatedAt <= @CreatedAtFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParams.Add("p.CreatedAt < @CreatedAtTo");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";
            return sql;
        }
        internal PaymentSumModel GetSum(PaymentRequest request)
        {
            return DbGetFirstOrDefault<PaymentSumModel>(GetSumSql(request), request);
        }
        internal RecordDateModel GetFirstRecordDate()
        {
            string sql = $@"

                  SELECT
                        MIN(p.Date) AS 'DATE'
                  FROM dbo.PAYMENT_TAB p
                  ";
            return DbGetFirstOrDefault<RecordDateModel>(sql, null);
        }
        internal RecordDateModel GetLastRecordDate()
        {
            string sql = $@"

                  SELECT
                        MAX(p.Date) AS 'DATE'
                  FROM dbo.PAYMENT_TAB p
                  ";
            return DbGetFirstOrDefault<RecordDateModel>(sql, null);
        }

        private DynamicParameters GetParameters(PaymentModel model)
        {
            var p = GetDynamicParameters(model);
            return p;
        }
    }
}

