using Core.Request.Business;
using Core.Service;
using Domain.Enum.Permission;
using Domain.Model.Business;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto.Business;
using Web0.Dto;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController : BaseController
    {
        private Service<BusinessRequest, BusinessModel> BusinessService => NewServices.BusinessService;
        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] BusinessRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();
            BusinessService.Request = req;
            var data = await BusinessService.GetList();

            return Ok(Mapper.MapList<BusinessDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] BusinessRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();
            BusinessService.Request = req;
            var data = await BusinessService.GetPaged();
            return Ok(Mapper.MapPagedList<BusinessDto>(data));
        }

        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] BusinessDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new BusinessModel(
                name: dto.Name,
                clientId: CurrentUser.CurrentClientId,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName,
                deleted: false);
            try
            {
                BusinessService.Request = new BusinessRequest { Name = dto.Name.ToLower() };
                var existingBusinesses = await BusinessService.GetList();
                if (existingBusinesses != null && existingBusinesses.Count() > 0)
                {
                    return BadRequest("There is already a Business with the given name");
                }

                BusinessService.Model = model;
                var id = await BusinessService.Insert();

                var request = new BusinessRequest()
                {
                    ClientId = CurrentUser.CurrentClientId,
                    BusinessId = id
                };
                BusinessService.Request = request;
                model = await BusinessService.GetById();
                return Ok(new DefaultResponseDto<BusinessDto>(Mapper.Map<BusinessDto>(model)));
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
            var request = new BusinessRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                BusinessId = id
            };
            BusinessService.Request = request;
            var model = await BusinessService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            return Ok(Mapper.Map<BusinessDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] BusinessDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.BusinessId != id) return BadRequest();
            var request = new BusinessRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                BusinessId = id
            };
            BusinessService.Request = request;
            var model = await BusinessService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Name = dto.Name;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                BusinessService.Model = model;
                await BusinessService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<BusinessDto>(Mapper.Map<BusinessDto>(await BusinessService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await BusinessService.GetById();
            if (model == null || model.BusinessId < 1) return BadRequest();

            await BusinessService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
