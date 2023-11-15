using Core.Request.Role;
using Core.Service;
using Domain.Enum.Permission;
using Domain.Model.Role;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web0.Dto;
using Web0.Dto.Role;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private Service<RoleRequest, RoleModel> NewRoleService => NewServices.RoleService;
        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] RoleRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();
            NewRoleService.Request = req;
            var data = await NewRoleService.GetList();

            return Ok(Mapper.MapList<RoleDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] RoleRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();
            NewRoleService.Request = req;
            var data = await NewRoleService.GetPaged();
            return Ok(Mapper.MapPagedList<RoleDto>(data));
        }

        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] RoleDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new RoleModel(
                name: dto.Name,
                clientId: CurrentUser.CurrentClientId,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName,
                deleted: false);
            try
            {
                NewRoleService.Model = model;
                var id = await NewRoleService.Insert();

                var request = new RoleRequest()
                {
                    ClientId = CurrentUser.CurrentClientId,
                    RoleId = id
                };
                NewRoleService.Request = request;
                model = await NewRoleService.GetById();
                return Ok(new DefaultResponseDto<RoleDto>(Mapper.Map<RoleDto>(model)));
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
            var request = new RoleRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                RoleId = id
            };
            NewRoleService.Request = request;
            var model = await NewRoleService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            return Ok(Mapper.Map<RoleDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] RoleDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.RoleId != id) return BadRequest();
            var request = new RoleRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                RoleId = id
            };
            NewRoleService.Request = request;
            var model = await NewRoleService.GetById();
            if (model.ClientId != CurrentUser.CurrentClientId) return BadRequest();

            model.Name = dto.Name;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;
            try
            {
                NewRoleService.Model = model;
                await NewRoleService.Update();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new DefaultResponseDto<RoleDto>(Mapper.Map<RoleDto>(await NewRoleService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {
            var model = await NewRoleService.GetById();
            if (model == null || model.RoleId < 1) return BadRequest();

            await NewRoleService.Delete();

            return Ok(new DefaultResponseDto(true));
        }
    }
}
