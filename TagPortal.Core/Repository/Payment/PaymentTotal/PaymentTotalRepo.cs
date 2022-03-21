using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using TagPortal.Core.Request.Payment.PaymentTotal;
using TagPortal.Domain;
using TagPortal.Domain.Model.Payment;
using TagPortal.Domain.Model.Payment.PaymentTotal;

namespace TagPortal.Core.Repository.Payment.PaymentTotal
{
    internal class PaymentTotalRepo : BaseRepository
    {
        public PaymentTotalRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(PaymentTotalRequest request = null, long id = 0, bool usePaging = false)
        {
            string sql = $@"
                SELECT 
                   pt.PaymentTotalId, pt.Title, pt.Price, pt.BusinessId, pt.Date, pt.IsDeposit,  pt.CreatedAt, pt.CreatedByUserId,
                   u.FirstName AS 'CreatedByFirstName', u.LastName AS 'CreatedByLastName',
                   b.BusinessName
                    {UsePagingColumn(usePaging)}

                FROM dbo.PAYMENT_TOTAL_TAB pt
                    LEFT JOIN [USER_TAB] u on u.UserId = pt.CreatedByUserId
                    LEFT JOIN [BUSINESS_TAB] b on b.BusinessId = pt.BusinessId
            ";

            if (id > 0)
            {
                sql += $@"WHERE pt.PaymentTotalId = {id}";
                return sql;
            }

            var searchParams = new List<string>();

            if (!String.IsNullOrEmpty(request.Title)) searchParams.Add("pt.Title like '%' + @Title + '%'");
            if (request.Price > 0) searchParams.Add("pt.Price = @Price");
            if (request.BusinessId > 0) searchParams.Add("pt.BusinessId = @BusinessId");
            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("b.BusinessName like '%' + @BusinessName + '%'");
            if (request.DateFrom > DateTime.MinValue) searchParams.Add("pt.Date >= @DateFrom");
            if (request.DateTo > DateTime.MinValue) searchParams.Add("pt.Date <= @DateTo");
            if (request.Date > DateTime.MinValue) searchParams.Add("pt.Date = @Date");
            if (request.IsDeposit.HasValue) searchParams.Add("pt.IsDeposit = @IsDeposit");
            if (request.CreatedAt > DateTime.MinValue) searchParams.Add("pt.Date <= @DateTo");
            if (request.CreatedByUserId > 0) searchParams.Add("pt.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParams.Add("pt.CreatedAt <= @CreatedAtFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParams.Add("pt.CreatedAt <= @CreatedAtTo");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@"
                ORDER BY    
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.Title} THEN pt.Title END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.Title} THEN b.BusinessName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.Price} THEN pt.Price END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.Date} THEN pt.Date END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.IsDeposit} THEN pt.IsDeposit END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.CreatedByFullName} THEN pt.CreatedByUserId END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentTotalOrderByEnum.CreatedAt} THEN pt.CreatedAt END {EnumOrderByDirectionToSql(request.OrderByDirection)}

                {UsePagingOffset(usePaging)}
            ";

            return sql;
        }

        internal PagedModel<PaymentTotalModel> GetPagedList(PaymentTotalRequest request)
        {
            return DbGetPagedList<PaymentTotalModel>(GetSql(request, usePaging: true), request);
        }

        internal List<PaymentTotalModel> GetList(PaymentTotalRequest request)
        {
            return DbGetList<PaymentTotalModel>(GetSql(request, usePaging: false), request);

        }

        internal PaymentTotalModel GetById(long id)
        {
            return DbGetFirstOrDefault<PaymentTotalModel>(GetSql(id: id), new { id });
        }

        internal bool Update(PaymentTotalModel model)
        {
            string sql = @"
                UPDATE PAYMENT_TOTAL_TAB SET
                   Title = @Title,
                   BusinessId = @BusinessId,
                   Price = @Price,
                   Date = @Date,
                   IsDeposit = @IsDeposit,
                   CreatedByUserId = @CreatedByUserId,
                   CreatedAt = @CreatedAt,

                WHERE PaymentTotalId = @PaymentTotalId
            ";

            return DbUpdate(sql, GetDynamicParameters(model));
        }

        internal long Insert(PaymentTotalModel model)
        {
            string sql = @"
                INSERT INTO dbo.PAYMENT_TOTAL_TAB (
                     Title, BusinessId, Price, Date, IsDeposit, CreatedAt, CreatedByUserId
                )
                OUTPUT INSERTED.PaymentTotalId
                VALUES (@Title, @BusinessId, @Price, @Date, @IsDeposit, @CreatedAt, @CreatedByUserId)
            ";

            return DbInsert<long>(sql, GetDynamicParameters(model));
        }

        internal bool DeleteById(long OrderId)
        {
            string sql = @"
                DELETE FROM dbo.PAYMENT_TOTAL_TAB 
                WHERE PaymentTotalId = @PaymentTotalId
            ";

            return DbDelete(sql, new { OrderId });
        }

        private string GetSumSql(PaymentTotalRequest request)
        {
            string sql = $@"

                  SELECT
                         SUM(pt.Price)
                  FROM dbo.PAYMENT_TOTAL_TAB pt
                  ";

            var searchParams = new List<string>();

            if (!String.IsNullOrEmpty(request.Title)) searchParams.Add("pt.Title like '%' + @Title + '%'");
            if (request.Price > 0) searchParams.Add("pt.Price = @Price");
            if (request.BusinessId > 0) searchParams.Add("pt.BusinessId = @BusinessId");
            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("b.BusinessName like '%' + @BusinessName + '%'");
            if (request.DateFrom > DateTime.MinValue) searchParams.Add("pt.Date >= @DateFrom");
            if (request.DateTo > DateTime.MinValue) searchParams.Add("pt.Date <= @DateTo");
            if (request.Date > DateTime.MinValue) searchParams.Add("pt.Date = @DateTo");
            if (request.IsDeposit.HasValue) searchParams.Add("pt.IsDeposit = @IsDeposit");
            if (request.CreatedAt > DateTime.MinValue) searchParams.Add("pt.Date <= @DateTo");
            if (request.CreatedByUserId > 0) searchParams.Add("pt.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParams.Add("pt.CreatedAt <= @CreatedFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParams.Add("pt.CreatedAt <= @CreatedTo");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";
            return sql;
        }
        internal float GetSum(PaymentTotalRequest request)
        {
            return DbGetFirstOrDefault<float>(GetSumSql(request), request);
        }
        internal RecordDateModel GetFirstRecordDate()
        {
            string sql = $@"

                  SELECT
                        MIN(pt.Date) AS 'DATE'
                  FROM dbo.PAYMENT_TOTAL_TAB pt
                  ";
            return DbGetFirstOrDefault<RecordDateModel>(sql, null);
        }
        internal RecordDateModel GetLastRecordDate()
        {
            string sql = $@"

                  SELECT
                        MAX(pt.Date) AS 'DATE'
                  FROM dbo.PAYMENT_TOTAL_TAB pt
                  ";
            return DbGetFirstOrDefault<RecordDateModel>(sql, null);
        }

        private DynamicParameters GetParameters(PaymentTotalModel model)
        {
            var p = GetDynamicParameters(model);
            return p;
        }
    }
}

