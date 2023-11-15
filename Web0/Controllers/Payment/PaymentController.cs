using Core.Request.Payment;
using Core.Service;
using Domain.Enum.Permission;
using Domain.Model.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
        private Service<PaymentRequest, PaymentModel> PaymentService => NewServices.PaymentService;
        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] PaymentRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
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
            if (!req.Validate()) return BadRequest();
            PaymentService.Request = req;
            var data = await PaymentService.GetPaged();
            return Ok(Mapper.MapPagedList<PaymentDto>(data));
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
            if (dto.PaymentPriorityId != id) return BadRequest();
            var request = new PaymentRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                PaymentPriorityId = id
            };
            PaymentService.Request = request;
            var model = await PaymentService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Title = dto.Title ;
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
            if (model == null || model.PaymentPriorityId < 1) return BadRequest();

            PaymentService.Request = new PaymentRequest() { PaymentId = id };
            await PaymentService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
