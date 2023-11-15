using Core.Request.Payment;
using Core.Request.Report;
using Core.Service;
using Core.Service.Report;
using Domain.Enum.Permission;
using Domain.Model.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto.Payment;
using Web.Dto.Report;
using Web.Dto.Report.Dashboard;
using Web0.Dto;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.Report
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        private Service<PaymentRequest, PaymentModel> PaymentService => NewServices.PaymentService;
        private Service<PaymentTotalRequest, PaymentTotalModel> PaymentTotalService => NewServices.PaymentTotalService;
        private ReportService ReportService => NewServices.ReportService;

        [Route("yearlytotaloverview")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> YearlyOverview([FromQuery] PaymentTotalRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.OrderBy = EnumPaymentTotalRequest.Date;
            req.OrderByDirection = Domain.Enum.EnumOrderByDirection.Asc;
            if (!req.Validate()) return BadRequest();

            PaymentTotalService.Request = req;
            var firstRecord = await PaymentTotalService.GetFirstOrDefault();

            PaymentTotalService.Request.OrderByDirection = Domain.Enum.EnumOrderByDirection.Desc;
            var lastRecord = await PaymentTotalService.GetFirstOrDefault();

            PaymentTotalService.Request = new PaymentTotalRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                DateFrom = firstRecord.Date,
                DateTo = lastRecord.Date
            };
            var data = await PaymentTotalService.GetList();

            return Ok(Mapper.MapList<PaymentTotalDto>(data));
        }

        [Route("yearlytotaloverview/firstdate")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> YearlyOverviewFirstDate([FromQuery] PaymentTotalRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();

            req.OrderByDirection = Domain.Enum.EnumOrderByDirection.Asc;
            req.OrderBy = EnumPaymentTotalRequest.Date;

            PaymentTotalService.Request = req;
            var firstRecord = await PaymentTotalService.GetFirstOrDefault();

            return Ok(firstRecord.Date);
        }
        [Route("yearlytotaloverview/lastdate")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> YearlyOverviewLastDate([FromQuery] PaymentTotalRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();

            req.OrderByDirection = Domain.Enum.EnumOrderByDirection.Desc;
            req.OrderBy = EnumPaymentTotalRequest.Date;

            PaymentTotalService.Request.OrderByDirection = Domain.Enum.EnumOrderByDirection.Desc;
            var lastRecord = await PaymentTotalService.GetFirstOrDefault();

            return Ok(lastRecord.Date);
        }

        [Route("monthlyoverview/firstdate")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> MonthlyOverviewFirstDate([FromQuery] PaymentRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();

            req.OrderByDirection = Domain.Enum.EnumOrderByDirection.Asc;
            req.OrderBy = EnumPaymentRequest.Date;

            PaymentService.Request = req;
            var firstRecord = await PaymentService.GetFirstOrDefault();

            return Ok(firstRecord.Date);
        }
        [Route("monthlyoverview/lastdate")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> MonthlyOverviewLastDate([FromQuery] PaymentRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();

            req.OrderByDirection = Domain.Enum.EnumOrderByDirection.Desc;
            req.OrderBy = EnumPaymentRequest.Date;

            PaymentService.Request.OrderByDirection = Domain.Enum.EnumOrderByDirection.Desc;
            var lastRecord = await PaymentService.GetFirstOrDefault();

            return Ok(lastRecord.Date);
        }
        [Route("monthlyoverview")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> GetMonthlyOverviewByCategories([FromQuery] MonthlyOverviewRequest req)
        {
            if (req == null) req = new MonthlyOverviewRequest();


            var items = await ReportService.GetCategoryBasedMonthlyOverviewList(req);
            return Ok(Mapper.MapList<MonthlyOverviewDto>(items));
        }
        [Route("paymentssum")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> GetIncomeSums([FromQuery] DashboardSumsRequest req)
        {
            if (req == null) req = new DashboardSumsRequest();
            req.DateTo = req.DateTo.EndOfDay();

            req.IsDeposit = true;
            var thisMonthIncome = ReportService.GetPaymentsSum(req);

            req.IsDeposit = false;
            var thisMonthExpenditures = ReportService.GetPaymentsSum(req);

            req.DateFrom = req.DateFrom.AddMonths(-1);
            req.DateTo = req.DateTo.AddMonths(-1).EndOfMonth();
            var lastMonthExpenditures = ReportService.GetPaymentsSum(req);

            req.IsDeposit = true;
            var lastMonthIncome = ReportService.GetPaymentsSum(req);

            await Task.WhenAll(thisMonthIncome, lastMonthIncome);
            var incomesDto = new MonthlySumsDto(thisMonthIncome.Result, lastMonthIncome.Result);

            await Task.WhenAll(thisMonthExpenditures, lastMonthExpenditures);
            var expendituresDto = new MonthlySumsDto(thisMonthExpenditures.Result, lastMonthExpenditures.Result);

            return Ok(new DashboardsSumDto(incomesDto, expendituresDto));
        }

        [Route("paymentsoverview")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> GetPaymentSums([FromQuery] PaymentsOverviewRequest req)
        {
            var paymentRequest = new PaymentRequest()
            {
                DateFrom = new DateTime(req.Date.Year, 1, 1).StartOfDay(),
                DateTo = DateTime.Now,
                IsDeposit = req.IsDeposit
            };
            try
            {
                var data = await ReportService.GetPaymentsOverview(paymentRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return BadRequest();
        }



    }
}
