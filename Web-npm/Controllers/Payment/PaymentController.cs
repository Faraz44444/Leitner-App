using Core.Request.Payment;
using Core.Service;
using Core.Service.Report;
using Domain.Enum.Permission;
using Domain.Model.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Dto.Payment;
using Web0.Dto;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : BaseController
    {
        private Service<PaymentRequest, PaymentModel> PaymentService => Services.PaymentService;
        private ReportService ReportService => Services.ReportService;

        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] PaymentRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            PaymentService.Request = req;
            var data = await PaymentService.GetList();

            return Ok(Mapper.MapList<PaymentDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] PaymentRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            PaymentService.Request = req;
            var data = await PaymentService.GetPaged();
            return Ok(Mapper.MapPagedList<PaymentDto>(data));
        }

        [Route("recommendations")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> GetPaymentRecommendation([FromQuery] PaymentRecommendationRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            var data = await ReportService.GetPaymentRecommendation(req);
            return Ok(Mapper.MapPagedList<PaymentRecommendationDto>(data));
        }
        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] PaymentDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new PaymentModel(
                title: dto.Title,
                paymentPriorityId: dto.PaymentPriorityId,
                businessId: dto.BusinessId,
                businessName: dto.BusinessName,
                isDeposit: dto.IsDeposit,
                isPaidToPerson: dto.IsPaidToPerson,
                categoryId: dto.CategoryId,
                price: dto.Price,
                date: dto.Date,
                clientId: CurrentUser.CurrentClientId,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName);
            try
            {
                PaymentService.Model = model;
                var id = await PaymentService.Insert();

                var request = new PaymentRequest()
                {
                    ClientId = CurrentUser.CurrentClientId,
                    PaymentPriorityId = id
                };
                PaymentService.Request = request;
                model = await PaymentService.GetById();
                return Ok(new DefaultResponseDto<PaymentDto>(Mapper.Map<PaymentDto>(model)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("{id}")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            if (id < 1) return BadRequest();
            var request = new PaymentRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId = CurrentUser.UserId,
                PaymentPriorityId = id
            };
            PaymentService.Request = request;
            var model = await PaymentService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            return Ok(Mapper.Map<PaymentDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] PaymentDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.PaymentId != id) return BadRequest();
            var request = new PaymentRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId = CurrentUser.UserId,
                PaymentId = id
            };
            PaymentService.Request = request;
            var model = await PaymentService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Title = dto.Title;
            model.Price = dto.Price;
            model.CategoryId = dto.CategoryId;
            model.Date = dto.Date;
            model.BusinessId = dto.BusinessId;
            model.PaymentPriorityId = dto.PaymentPriorityId;
            model.IsDeposit = dto.IsDeposit;
            model.IsPaidToPerson = dto.IsPaidToPerson;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                PaymentService.Model = model;
                await PaymentService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<PaymentDto>(Mapper.Map<PaymentDto>(await PaymentService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await PaymentService.GetById();
            if (model == null || model.PaymentPriorityId < 1 || model.CreatedByUserId != CurrentUser.UserId) return BadRequest();

            PaymentService.Request = new PaymentRequest() { PaymentId = id };
            await PaymentService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
