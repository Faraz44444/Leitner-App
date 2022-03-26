using System;
using System.Web.Http;
using TagPortal.Core.Request.Report;
using TagPortal.Core.Service.Report;
using TbxPortal.Web.Dto.Report;

namespace TbxPortal.Web.Controllers.ClientApi.Report
{
    [RoutePrefix("api/report")]
    public class ReportController : BaseController
    {
        private ReportService ReportService => Services.ReportService;

        [Route("monthlyoverview")]
        [HttpGet]
        public IHttpActionResult GetPagedList([FromUri] MonthlyReportRequest request)
        {
            if (request == null) request = new MonthlyReportRequest();

            var items = ReportService.GetCategoryBasedMonthlyOverviewList(request);
            var dto = DataManagementMapper.MapList<MonthlyOverviewDto>(items);

            return Ok(dto);
        }
    }
}

