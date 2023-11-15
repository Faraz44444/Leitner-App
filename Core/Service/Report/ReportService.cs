using Core.Infrastructure.UnitOfWork;
using Core.Repository.Report;
using Core.Request.Payment;
using Core.Request.Report;
using Domain.Aggregated;
using Domain.Model.BaseModels;
using Domain.Model.Payment;
using Domain.Model.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Report
{
    public class ReportService : BaseService
    {
        public ReportService(IUnitOfWorkProvider uowProvider): base(uowProvider)
        {

        }
        public async Task<IEnumerable<YearlyOverviewModel>> GetYearlyOverviewList(DateTime from, DateTime to, long clientId, long userId)
        {
            using(IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                ReportRepository ReportRepository(IUnitOfWork uow) => new(uow.IDbConnection, uow.IDbTransaction);
                var repo = ReportRepository(uow);
                return await repo.GetYearlyOverviewList(from, to, clientId, userId);
            }
        }
        public async Task<IEnumerable<MonthlyOverviewModel>> GetCategoryBasedMonthlyOverviewList(MonthlyOverviewRequest req, int num = 0)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                ReportRepository ReportRepository(IUnitOfWork uow) => new ReportRepository(uow.IDbConnection, uow.IDbTransaction);

                var repo = ReportRepository(uow);
                return await repo.GetCategoryBasedMonthlyOverviewList(req);
            }
        }
        public async Task<PagedModel<MonthlyOverviewModel>> GetCategoryBasedMonthlyOverviewPaged(MonthlyOverviewRequest req)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                ReportRepository ReportRepository(IUnitOfWork uow) => new ReportRepository(uow.IDbConnection, uow.IDbTransaction);

                var repo = ReportRepository(uow);
                return await repo.GetCategoryBasedMonthlyOverviewPaged(req);
            }
        }
        public async Task<float> GetPaymentsSum(DashboardSumsRequest req)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                ReportRepository ReportRepository(IUnitOfWork uow) => new ReportRepository(uow.IDbConnection, uow.IDbTransaction);

                var repo = ReportRepository(uow);
                return await repo.GetPaymentsSum(req);
            }
        }
        public async Task<IEnumerable<PaymentsOverviewData>> GetPaymentsOverview(PaymentRequest req)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                ReportRepository ReportRepository(IUnitOfWork uow) => new ReportRepository(uow.IDbConnection, uow.IDbTransaction);

                var repo = ReportRepository(uow);
                return await repo.GetPaymentsOverview(req);
            }
        }
        public async Task<PagedModel<PaymentRecommendationModel>> GetPaymentRecommendation(PaymentRecommendationRequest req)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                ReportRepository ReportRepository(IUnitOfWork uow) => new ReportRepository(uow.IDbConnection, uow.IDbTransaction);

                var repo = ReportRepository(uow);
                return await repo.GetPaymentRecommendation(req);
            }
        }

    }
}
