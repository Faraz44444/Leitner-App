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
    public class PaymentTotalController : BaseController
    {
        private Service<PaymentTotalRequest, PaymentTotalModel> PaymentTotalService => Services.PaymentTotalService;
        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] PaymentTotalRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            PaymentTotalService.Request = req;
            var data = await PaymentTotalService.GetList();

            return Ok(Mapper.MapList<PaymentTotalDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] PaymentTotalRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            PaymentTotalService.Request = req;
            var data = await PaymentTotalService.GetPaged();
            return Ok(Mapper.MapPagedList<PaymentTotalDto>(data));
        }

        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] PaymentTotalDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new PaymentTotalModel(
                title: dto.Title,
                businessId: dto.BusinessId,
                businessName: dto.BusinessName,
                isDeposit: dto.IsDeposit,
                price: dto.Price,
                date: dto.Date,
                clientId: CurrentUser.CurrentClientId,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName);
            try
            {
                PaymentTotalService.Model = model;
                var id = await PaymentTotalService.Insert();

                var request = new PaymentTotalRequest()
                {
                    ClientId = CurrentUser.CurrentClientId,
                };
                PaymentTotalService.Request = request;
                model = await PaymentTotalService.GetById();
                return Ok(new DefaultResponseDto<PaymentTotalDto>(Mapper.Map<PaymentTotalDto>(model)));
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
            var request = new PaymentTotalRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId= CurrentUser.UserId
            };
            PaymentTotalService.Request = request;
            var model = await PaymentTotalService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            return Ok(Mapper.Map<PaymentTotalDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] PaymentTotalDto dto)
        {
            if (dto == null) return BadRequest();
            var request = new PaymentTotalRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId= CurrentUser.UserId
            };
            PaymentTotalService.Request = request;
            var model = await PaymentTotalService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Title = dto.Title ;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                PaymentTotalService.Model = model;
                await PaymentTotalService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<PaymentTotalDto>(Mapper.Map<PaymentTotalDto>(await PaymentTotalService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await PaymentTotalService.GetById();
            if(model == null || model.PaymentTotalId != id || model.CreatedByUserId != CurrentUser.UserId) return BadRequest();
            PaymentTotalService.Request = new PaymentTotalRequest() { PaymentTotalId = id };
            await PaymentTotalService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
