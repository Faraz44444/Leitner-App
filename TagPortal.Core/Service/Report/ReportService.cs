using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Report;
using TagPortal.Core.Service.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.Aggregated;

namespace TagPortal.Core.Service.Report
{
    public class ReportService : BaseService
    {
        private UserService UserService { get; }

        public ReportService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory, UserService userService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
        }

        public PagedModel<MonthlyReportModel> GetCategoryBasedMonthlyOverviewPagedList(MonthlyReportRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.ReportRepo(uow);
                return repo.GetCategoryBasedMonthlyOverviewPagedList(request);
            }
        }
        public List<MonthlyReportModel> GetCategoryBasedMonthlyOverviewList(MonthlyReportRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.ReportRepo(uow);
                return repo.GetCategoryBasedMonthlyOverviewList(request);
            }
        }
    }
}
