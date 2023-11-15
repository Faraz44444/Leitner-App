using Core.Request.User;
using Core.Service;
using Core.Service.Security;
using Domain.Enum.Permission;
using Domain.Model.BaseModels;
using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web0.Dto;
using Web0.Dto.User;
using Web0.Infrastructure.AutoMapper;
using Web0.Infrastructure.Filters;

namespace Web0.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private SecurityService SecurityService => Services.SecurityService;
        private Service<UserRequest, UserModel> NewUserService => NewServices.UserService;
        private Service<UserClientRequest, UserClientModel> UserClientService => NewServices.UserClientService;

        [Route("lookup")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Lookup([FromQuery] UserRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();
            NewUserService.Request = req;
            var data = await NewUserService.GetList();

            return Ok(Mapper.MapList<UserDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromQuery] UserRequest req)
        {
            req.ClientId = CurrentUser.CurrentClientId;
            if (!req.Validate()) return BadRequest();
            try
            {
                var data = await NewUserService.GetList();
                return Ok(Mapper.MapList<UserDto>(data));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return BadRequest();
        }

        [Route("")]
        [HttpPost]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Post([FromBody] UserDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new UserModel(
                username: dto.Username,
                firstName: dto.FirstName,
                lastName: dto.LastName,
                createdByUserId: CurrentUser.UserId,
                createdByFirstName: CurrentUser.FirstName,
                createdByLastName: CurrentUser.LastName,
                email: dto.Email,
                currentClientId: CurrentUser.CurrentClientId,
                employeeId: dto.EmployeeId,
                isSystemUser: dto.IsSystemUser);
            NewUserService.Model = model;
            var id = await NewUserService.Insert();
            var request = new UserRequest()
            {
                ClientId = CurrentUser.CurrentClientId, 
                UserId = id
            };
            NewUserService.Request = request;
            model = await NewUserService.GetById();
            if (model != null && model.UserId > 0)
                _ = SecurityService.SetPasswordResetToken(usernameOrEmail: model.Username, isNewUser: true);

            return Ok(new DefaultResponseDto<UserDto>(Mapper.Map<UserDto>(model)));
        }

        [Route("{id}")]
        [HttpGet]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            if (id < 1) return BadRequest();

            var request = new UserRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                UserId = id
            };
            NewUserService.Request = request;
            var model = await NewUserService.GetById();
            UserClientService.Request = new UserClientRequest() { UserId = id, ClientId = CurrentUser.CurrentClientId };
            var userClient = await UserClientService.GetList();
            if (model.CurrentClientId != CurrentUser.CurrentClientId && (userClient == null || userClient.FirstOrDefault().UserId < 1)) return BadRequest();

            return Ok(Mapper.Map<UserDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] UserDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.UserId != id) return BadRequest();

            var request = new UserRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                UserId = id
            };
            NewUserService.Request = request;
            var model = await NewUserService.GetById();
            UserClientService.Request = new UserClientRequest() { UserId = id, ClientId = CurrentUser.CurrentClientId };
            var userClient = await UserClientService.GetList();
            if (model.CurrentClientId != CurrentUser.CurrentClientId && (userClient == null || userClient.FirstOrDefault().UserId < 1)) return BadRequest();

            model.FirstName = dto.FirstName;
            model.LastName = dto.LastName;
            model.Email = dto.Email;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;

            NewUserService.Model = model;
            await NewUserService.Update();

            request = new UserRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                UserId = id
            };
            NewUserService.Request = request;
            return Ok(new DefaultResponseDto<UserDto>(Mapper.Map<UserDto>(await NewUserService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        [AnyPermission(EnumPermission.AdminAccount)]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {

            var request = new UserRequest()
            {
                ClientId = CurrentUser.CurrentClientId,
                UserId = id
            };
            NewUserService.Request = request;
            var model = await NewUserService.GetById();
            UserClientService.Request = new UserClientRequest() { UserId = id, ClientId = CurrentUser.CurrentClientId };
            var userClient = await UserClientService.GetList();
            if (userClient == null || userClient.FirstOrDefault().UserId < 1) return BadRequest();

            model.UserId = id;
            await NewUserService.Delete();

            return Ok(new DefaultResponseDto(true));
        }

        //[Route("{id}/userclient")]
        //[HttpGet]
        //[AnyPermission(EnumPermission.AdminAccount)]
        //public async Task<IActionResult> GetUserClient([FromRoute] long id)
        //{
        //    if (id < 1) return BadRequest();
        //    var model = await UserService.GetUserClientByUserId(id, CurrentUser.CurrentClientId);
        //    if (model != null && model.ClientId != CurrentUser.CurrentClientId) return BadRequest();
        //    if (model == null)
        //        model = new UserClientModel();

        //    return Ok(Mapper.Map<UserClientDto>(model));
        //}

        //[Route("{id}/userclient")]
        //[HttpPost]
        //[AnyPermission(EnumPermission.AdminAccount)]
        //public async Task<IActionResult> PostUserClient([FromRoute] long id, [FromBody] UserClientDto dto)
        //{
        //    if (id < 1) return BadRequest();
        //    if (dto == null || dto.RoleId < 1) return BadRequest();

        //    var model = await UserService.GetUserClientByUserId(id, CurrentUser.CurrentClientId);
        //    if (model == null || model.ClientId < 1)
        //    {
        //        model = new UserClientModel(id, CurrentUser.CurrentClientId, dto.RoleId);
        //        await UserService.InsertUserClient(model, CurrentUser.UserId, CurrentUser.FirstName, CurrentUser.LastName);
        //    }
        //    else
        //    {
        //        if (model.RoleId != dto.RoleId)
        //        {
        //            model.RoleId = dto.RoleId;
        //            await UserService.UpdateUserClient(model, CurrentUser.UserId, CurrentUser.FirstName, CurrentUser.LastName);
        //        }
        //    }

        //    return Ok(new DefaultResponseDto<UserClientDto>(Mapper.Map<UserClientDto>(model)));
        //}
    }
}
