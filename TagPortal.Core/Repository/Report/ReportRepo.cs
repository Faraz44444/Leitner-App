using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using TagPortal.Core.Request.Payment;
using TagPortal.Core.Request.Report;
using TagPortal.Domain;
using TagPortal.Domain.Model.Aggregated;
using TagPortal.Domain.Model.Payment;

namespace TagPortal.Core.Repository.Report
{
    internal class ReportRepo : BaseRepository
    {
        public ReportRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
        }
        private string GetCategoryBasedMonthlyOverviewWhereSql(MonthlyReportRequest request = null, bool usePaging = false)
        {
            string whereStatements = "";
            var searchParams = new List<string>();

            if (request.CategoryId > 0) searchParams.Add("p.CategoryId = @CategoryId");
            if (!String.IsNullOrEmpty(request.CategoryName)) searchParams.Add(" c.CategoryName like '%' + @CategoryName + '%'");
            if (request.BusinessId > 0) searchParams.Add("p.BusinessId like '%' + @BusinessId + '%'");
            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("b.BusinessName like '%' + @BusinessName + '%'");
            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit = @IsDeposit");
            if (request.IsPaidToPerson.HasValue) searchParams.Add("p.IsPaidToPerson = @IsPaidToPerson");
            if (request.Price > 0) searchParams.Add("p.Price = @Price");
            if (request.PriceFrom > 0) searchParams.Add("p.Price >= @PriceFrom");
            if (request.PriceTo > 0) searchParams.Add("p.Price < @PriceTo");
            if (request.Year > DateTime.MinValue) searchParams.Add("YEAR(p.Date) = @Year");
            if (request.Month > DateTime.MinValue) searchParams.Add("MONTH(p.Date) < @Month");

            if (searchParams.Count > 0) whereStatements += $" WHERE {string.Join(" AND ", searchParams)}";
            return whereStatements;
        }
        private string GetCategoryBasedMonthlyOverviewSql(MonthlyReportRequest request = null, long id = 0, bool usePaging = false)
        {
            string sql = $@"
                        Select p.CategoryId, c.CategoryName, p.IsDeposit, YEAR(p.Date) as 'Year', MONTH(p.Date) as 'Month', SUM(p.Price) as 'Price'
	                        from PAYMENT_TAB p
	                    left join CATEGORY_TAB c on c.CategoryId = p.CategoryId
                            {GetCategoryBasedMonthlyOverviewWhereSql(request)}
                        group by p.CategoryId, c.CategoryName, p.IsDeposit, YEAR(p.Date), MONTH(p.Date) 
                    {UsePagingColumn(usePaging)}
            ";

            sql += $@"
                ORDER BY    
                    CASE WHEN @OrderBy = {(int)MonthlyOverviewOrderByEnum.CategoryName} THEN c.CategoryName END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)MonthlyOverviewOrderByEnum.Price} THEN SUM(p.Price) END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)MonthlyOverviewOrderByEnum.Year} THEN YEAR(p.Date) END {EnumOrderByDirectionToSql(request.OrderByDirection)},
                    CASE WHEN @OrderBy = {(int)MonthlyOverviewOrderByEnum.Month} THEN MONTH(p.Date) END {EnumOrderByDirectionToSql(request.OrderByDirection)}

                {UsePagingOffset(usePaging)}
            ";

            return sql;
        }

        internal PagedModel<MonthlyReportModel> GetCategoryBasedMonthlyOverviewPagedList(MonthlyReportRequest request)
        {
            return DbGetPagedList<MonthlyReportModel>(GetCategoryBasedMonthlyOverviewSql(request, usePaging: true), request);
        }

        internal List<MonthlyReportModel> GetCategoryBasedMonthlyOverviewList(MonthlyReportRequest request)
        {
            return DbGetList<MonthlyReportModel>(GetCategoryBasedMonthlyOverviewSql(request, usePaging: false), request);

        }


        private DynamicParameters GetParameters(MonthlyReportModel model)
        {
            var p = GetDynamicParameters(model);
            return p;
        }
    }
}

