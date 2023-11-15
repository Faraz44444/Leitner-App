using Core.Request.Payment;
using Core.Service;
using Domain.Enum.Permission;
using Domain.Model.Payment;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto.Category;
using Web0.Dto;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.Payment
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentPriorityController : BaseController
    {
        private Service<PaymentPriorityRequest, PaymentPriorityModel> PaymentPriorityService => Services.PaymentPriorityService;
        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] PaymentPriorityRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            PaymentPriorityService.Request = req;
            var data = await PaymentPriorityService.GetList();

            return Ok(Mapper.MapList<PaymentPriorityDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] PaymentPriorityRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            req.CreatedByUserId = CurrentUser.UserId;
            if (!req.Validate()) return BadRequest();
            PaymentPriorityService.Request = req;
            var data = await PaymentPriorityService.GetPaged();
            return Ok(Mapper.MapPagedList<PaymentPriorityDto>(data));
        }

        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] PaymentPriorityDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new PaymentPriorityModel(
                name: dto.Name,
                clientId: CurrentUser.CurrentClientId,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName,
                deleted: false);
            try
            {
                PaymentPriorityService.Request = new PaymentPriorityRequest { Name = dto.Name.ToLower() };
                var existingBusinesses = await PaymentPriorityService.GetList();
                if (existingBusinesses != null && existingBusinesses.Count() > 0)
                {
                    return BadRequest("There is already a Business with the given name");
                }

                PaymentPriorityService.Model = model;
                var id = await PaymentPriorityService.Insert();

                var request = new PaymentPriorityRequest()
                {
                    ClientId = CurrentUser.CurrentClientId,
                    PaymentPriorityId = id
                };
                PaymentPriorityService.Request = request;
                model = await PaymentPriorityService.GetById();
                return Ok(new DefaultResponseDto<PaymentPriorityDto>(Mapper.Map<PaymentPriorityDto>(model)));
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
            var request = new PaymentPriorityRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId = CurrentUser.UserId,
                PaymentPriorityId = id
            };
            PaymentPriorityService.Request = request;
            var model = await PaymentPriorityService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            return Ok(Mapper.Map<PaymentPriorityDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] PaymentPriorityDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.PaymentPriorityId != id) return BadRequest();
            var request = new PaymentPriorityRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                CreatedByUserId = CurrentUser.UserId,
                PaymentPriorityId = id
            };
            PaymentPriorityService.Request = request;
            var model = await PaymentPriorityService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Name = dto.Name;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                PaymentPriorityService.Model = model;
                await PaymentPriorityService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<PaymentPriorityDto>(Mapper.Map<PaymentPriorityDto>(await PaymentPriorityService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await PaymentPriorityService.GetById();
            if (model == null || model.PaymentPriorityId < 1 || model.CreatedByUserId != CurrentUser.UserId) return BadRequest();

            PaymentPriorityService.Request = new PaymentPriorityRequest() { PaymentPriorityId = id };
            await PaymentPriorityService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
