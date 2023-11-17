//using Core.Request.Payment;
//using Domain.Aggregated;
//using Domain.Model.BaseModels;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Threading.Tasks;

//namespace Core.Repository.Report
//{
//    class ReportRepository : BaseRepository
//    {
//        public ReportRepository(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
//        {

//        }
//        private string GetCategoryBasedMonthlyOverviewWhereSql(MonthlyOverviewRequest request = null, bool usePaging = false)
//        {
//            string whereStatements = "";
//            var searchParams = new List<string>();

//            if (request.CategoryId > 0) searchParams.Add("p.CategoryId = @CategoryId");
//            if (!String.IsNullOrEmpty(request.CategoryName)) searchParams.Add(" c.CategoryName like '%' + @CategoryName + '%'");
//            if (request.BusinessId > 0) searchParams.Add("p.BusinessId like '%' + @BusinessId + '%'");
//            if (!String.IsNullOrEmpty(request.BusinessName)) searchParams.Add("b.BusinessName like '%' + @BusinessName + '%'");
//            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit = @IsDeposit");
//            if (request.IsPaidToPerson.HasValue) searchParams.Add("p.IsPaidToPerson = @IsPaidToPerson");
//            if (request.Price > 0) searchParams.Add("p.Price = @Price");
//            if (request.PriceFrom > 0) searchParams.Add("p.Price >= @PriceFrom");
//            if (request.PriceTo > 0) searchParams.Add("p.Price < @PriceTo");
//            if (request.Year > DateTime.MinValue) searchParams.Add("YEAR(p.Date) = @Year");
//            if (request.Month > DateTime.MinValue) searchParams.Add("MONTH(p.Date) < @Month");

//            if (searchParams.Count > 0) whereStatements += $" WHERE {string.Join(" AND ", searchParams)}";
//            return whereStatements;
//        }
//        private string GetCategoryBasedMonthlyOverviewSql(MonthlyOverviewRequest request = null, long id = 0, bool usePaging = false)
//        {
//            string sql = $@"
//                        Select p.CategoryId, c.Name as 'CategoryName', p.IsDeposit, YEAR(p.Date) as 'Year', MONTH(p.Date) as 'Month', SUM(p.Price) as 'Price'
//	                        from PAYMENT_TAB p
//	                    left join CATEGORY_TAB c on c.CategoryId = p.CategoryId
//                            {GetCategoryBasedMonthlyOverviewWhereSql(request)}
//                        group by p.CategoryId, c.Name, p.IsDeposit, YEAR(p.Date), MONTH(p.Date) 
//            ";
//            //{UsePagingColumn(usePaging)}

//            sql += $@"
//                ORDER BY    
//                    CASE WHEN @OrderBy = {(int)EnumMonthlyOverviewRequest.CategoryName} THEN c.Name END {EnumOrderByDirectionToSql(request.OrderByDirection)},
//                    CASE WHEN @OrderBy = {(int)EnumMonthlyOverviewRequest.Price} THEN SUM(p.Price) END {EnumOrderByDirectionToSql(request.OrderByDirection)},
//                    CASE WHEN @OrderBy = {(int)EnumMonthlyOverviewRequest.Year} THEN YEAR(p.Date) END {EnumOrderByDirectionToSql(request.OrderByDirection)},
//                    CASE WHEN @OrderBy = {(int)EnumMonthlyOverviewRequest.Month} THEN MONTH(p.Date) END {EnumOrderByDirectionToSql(request.OrderByDirection)}

//                {UsePagingOffset(usePaging)}
//            ";

//            return sql;
//        }
//        public async Task<PagedModel<MonthlyOverviewModel>> GetCategoryBasedMonthlyOverviewPaged(MonthlyOverviewRequest req)
//        {
//            return await GetPagedList<MonthlyOverviewModel>(GetCategoryBasedMonthlyOverviewSql(req, usePaging: true), req);
//        }
//        public async Task<IEnumerable<YearlyOverviewModel>> GetYearlyOverviewList(DateTime dateFrom, DateTime dateTo, long clientId, long userId)
//        {
//            var sql = @$"
//                SELECT SUM(Price) AS 'Price', IsDeposit, YEAR([Date]) as 'Year', MONTH([Date]) as 'Month' 
//                FROM [PAYMENT_TOTAL_TAB] 
//                where[Date] >= @dateFrom AND [Date] < @dateTo AND ClientId = @clientId AND CreatedByUserId = @userId
//                Group by IsDeposit, YEAR([Date]), MONTH([Date]) ";
//            return await GetList<YearlyOverviewModel>(sql, new { dateFrom, dateTo, clientId, userId });
//        }
//        public async Task<IEnumerable<MonthlyOverviewModel>> GetCategoryBasedMonthlyOverviewList(MonthlyOverviewRequest req, bool usePaging = false)
//        {

//            return await GetList<MonthlyOverviewModel>(GetCategoryBasedMonthlyOverviewSql(req), req);
//        }
//        private string GetPaymentsSumSql(DashboardSumsRequest request = null, bool usePaging = false)
//        {
//            string sql = $@"
//                        Select ISNULL(SUM(p.Price), 0)
//                            from PAYMENT_TAB p
//                        WHERE p.Date >= @DateFrom AND p.Date <= @DateTo";
//            var searchParams = new List<string>();

//            if (request.ClientId > 0) searchParams.Add("p.ClientId = @ClientId");
//            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit= @IsDeposit");

//            if (searchParams.Count > 0) sql += $" AND {string.Join(" AND ", searchParams)}";
//            return sql;
//        }
//        public async Task<float> GetPaymentsSum(DashboardSumsRequest req)
//        {
//            return await GetFirstOrDefault<float>(GetPaymentsSumSql(req), req);
//        }
//        private string GetPaymentsOverviewSql(MaterialRequest request = null, bool usePaging = false)
//        {
//            string sql = $@"
//                        SELECT Month(p.Date) as 'Month', SUM(p.Price) as 'Price'
//                            FROM PAYMENT_TAB p";
//            var searchParams = new List<string>();

//            if (request.ClientId > 0) searchParams.Add("p.ClientId = @ClientId");
//            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit= @IsDeposit");
//            if (request.DateFrom.HasValue) searchParams.Add("p.Date >= @DateFrom");
//            if (request.DateTo.HasValue) searchParams.Add("p.Date <= @DateTo");

//            if (searchParams.Count > 0) sql += $" WHERE {string.Join(" AND ", searchParams)}";
//            sql += " Group by Month(p.Date)";
//            return sql;
//        }
//        public async Task<IEnumerable<PaymentsOverviewData>> GetPaymentsOverview(MaterialRequest req)
//        {
//            return await GetList<PaymentsOverviewData>(GetPaymentsOverviewSql(req), req);
//        }
//        public async Task<PagedModel<PaymentRecommendationModel>> GetPaymentRecommendation(PaymentRecommendationRequest req)
//        {
//            string sql = $@"
//                            SELECT 
//                            	a.[BusinessId],[IsDeposit],a.[CategoryId], count(*) AS 'Repetition', AVG(price) AS 'AveragePrice',
//                                b.[Name] AS 'BusinessName', 
//                                c.[Name] AS 'CategoryName', 
//                                COUNT(*) OVER() AS 'TotalNumberOfItems'
//                            FROM 
//                            (
//                                SELECT
//                                    [BusinessId], [IsDeposit], [CategoryId], [Price], Year([DATE]) as 'Date_Year', MONTH([Date]) as 'Date_Month'
//                                FROM [PAYMENT_TAB] p
//                                 {GetPaymentRecommendationWhereSql(req)}
//                                GROUP BY 
//                                    BusinessId, IsDeposit, CategoryId, Price, Year([DATE]), MONTH([Date])
//                             ) a
//                             LEFT JOIN [BUSINESS_TAB] b on b.BusinessId = a.BusinessId
//                             LEFT JOIN [CATEGORY_TAB] c on c.CategoryId = a.CategoryId
//                             GROUP BY 
//                                a.[BusinessId],[IsDeposit],a.[CategoryId], b.[Name], c.[Name]
//                             HAVING COUNT(*) > 3
//                             ORDER BY COUNT(*) DESC 
//                            OFFSET (@CurrentPage * @ItemsPerPage) ROWS FETCH NEXT @ItemsPerPage ROWS ONLY
//                             ";
//            return await GetPagedList<PaymentRecommendationModel>(sql, req);
//        }

//        private string GetPaymentRecommendationWhereSql(PaymentRecommendationRequest request)
//        {
//            var searchParams = new List<string>();
//            if (request.ClientId > 0) searchParams.Add("p.ClientId = @ClientId");
//            if (request.IsDeposit.HasValue) searchParams.Add("p.IsDeposit= @IsDeposit");
//            if (request.DateFrom.HasValue) searchParams.Add("p.Date >= @DateFrom");
//            if (request.DateTo.HasValue) searchParams.Add("p.Date <= @DateTo");
//            return searchParams.Count > 0 ? $" WHERE {string.Join(" AND ", searchParams)}" : "";
//        }
//    }
//}
