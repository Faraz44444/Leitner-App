using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagPortal.Core.Request.Payment.PaymentPriority;
using TagPortal.Domain;
using TagPortal.Domain.Model.PaymentPriority;

namespace TagPortal.Core.Repository.Payment.PaymentPriority
{
    class PaymentPriorityRepo : BaseRepository
    {
        public PaymentPriorityRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }

        private string GetSql(PaymentPriorityRequest request = null, long id = 0,  bool usePaging = false)
        {
            string sql = $@"
                SELECT 
                   PP.PaymentPriorityId, PP.PaymentPriorityName, PP.CreatedByUserId, u.FirstName AS 'CreatedByFirstName', u.LastName AS 'CreatedByLastName', PP.CreatedAt
                    {UsePagingColumn(usePaging)}
                FROM dbo.PAYMENT_PRIORITY_TAB PP
                    LEFT JOIN [USER_TAB] u on u.UserId = Pp.CreatedByUserId
            ";

            if (id > 0)
            {
                sql += "WHERE PP.PaymentPriorityId = @PaymentPriorityId";
                return sql;
            }

            var searchParams = new List<string>();

            if (!String.IsNullOrEmpty(request.PaymentPriorityName)) searchParams.Add("PP.PaymentPriorityName like '%' + @PaymentPriorityName + '%'");
            if (request.CreatedAt > DateTime.MinValue) searchParams.Add("P.Date <= @DateTo");
            if (request.CreatedByUserId > 0) searchParams.Add("P.CreatedByUserId = @CreatedByUserId");
            if (request.CreatedFrom > DateTime.MinValue) searchParams.Add("P.CreatedAt <= @CreatedAtFrom");
            if (request.CreatedTo > DateTime.MinValue) searchParams.Add("P.CreatedAt <= @CreatedAtTo");

            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";

            sql += $@"
                ORDER BY    
                    CASE WHEN @OrderBy = {(int)PaymentPriorityOrderByEnum.PaymentPriorityName} THEN PP.PaymentPriorityName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentPriorityOrderByEnum.CreatedAt} THEN PP.CreatedAt END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentPriorityOrderByEnum.CreatedByFullName} THEN u.FirstName  END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)PaymentPriorityOrderByEnum.CreatedByFullName} THEN u.LastName  END {EnumOrderByDirectionToSql(request.OrderByDirection)}


                {UsePagingOffset(usePaging)}
            ";

            return sql;
        }

        internal PagedModel<PaymentPriorityModel> GetPagedList(PaymentPriorityRequest request)
        {
            return DbGetPagedList<PaymentPriorityModel>(GetSql(request, usePaging: true), request);
        }

        internal List<PaymentPriorityModel> GetList(PaymentPriorityRequest request)
        {
            return DbGetList<PaymentPriorityModel>(GetSql(request, usePaging: false), request);

        }

        internal PaymentPriorityModel GetById(long id)
        {
            return DbGetFirstOrDefault<PaymentPriorityModel>(GetSql(id:id), new { id});
        }

        internal PaymentPriorityModel SingleOrDefault(PaymentPriorityRequest request)
        {
            return DBGetSingleOrDefault<PaymentPriorityModel>(GetSql(request, usePaging: false), request);
        }

        internal bool Update(PaymentPriorityModel model)
        {
            string sql = @"
                UPDATE PAYMENT_PRIORITY_TAB SET
                   PaymentPriorityName = @PaymenPriorityName,
                   CreatedByUserId = @CreatedByUserId,
                   CreatedAt = @CreatedAt,

                WHERE PaymentPriorityId = @PaymentPriorityId
            ";

            return DbUpdate(sql, GetDynamicParameters(model));
        }

        internal long Insert(PaymentPriorityModel model)
        {
            string sql = @"
                INSERT INTO dbo.PAYMENT_PRIORITY_TAB (
                     P.PaymentPriorityName, P.CreatedByUserId, P.CreatedAt
                )
                OUTPUT INSERTED.PaymentPriorityId
                VALUES (
                   @PaymentPriorityName, @CreatedByUserId, @CreatedAt
                )
            ";

            return DbInsert<long>(sql, GetDynamicParameters(model));
        }

        internal bool DeleteById(long OrderId)
        {
            string sql = @"
                DELETE FROM dbo.PAYMENT_PRIORITY_TAB 
                WHERE PaymentPriorityId = @PaymentPriorityId
            ";

            return DbDelete(sql, new { OrderId });
        }

    }
}


