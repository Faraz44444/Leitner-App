using Core.Request.User;
using Core.Service;
using Core.Service.Security;
using Domain.Model.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Dto;
using Web.Dto.User;
using Web.Infrastructure.AutoMapper;

namespace Web.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private SecurityService SecurityService => Services.SecurityService;
        private Service<UserRequest, UserModel> UserService => Services.UserService;

        [Route("lookup")]
        [HttpGet]
        public async Task<IActionResult> Lookup([FromQuery] UserRequest req)
        {
            if (!req.Validate()) return BadRequest();
            UserService.Request = req;
            var data = await UserService.GetList();

            return Ok(Mapper.MapList<UserDto>(data));
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserRequest req)
        {
            if (!req.Validate()) return BadRequest();
            try
            {
                UserService.Request = req;
                var data = await UserService.GetList();
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
        public async Task<IActionResult> Post([FromBody] UserDto dto)
        {
            if (dto == null) return BadRequest();
            var model = new UserModel(username: (dto.Username == null ? dto.FirstName : dto.Username),
                                      firstName: dto.FirstName,
                                      lastName: dto.LastName,
                                      createdByUserId: CurrentUser.UserId,
                                      createdByFirstName: CurrentUser.FirstName,
                                      createdByLastName: CurrentUser.LastName,
                                      email: dto.Email);
            UserService.Model = model;
            long id = await UserService.Insert();
            var request = new UserRequest()
            {
                UserId = id
            };
            UserService.Request = request;
            model = await UserService.GetById();
            if (model != null && model.UserId > 0)
                _ = SecurityService.SetPasswordResetToken(usernameOrEmail: model.Username, isNewUser: true);
            return Ok(new DefaultResponseDto<UserDto>(Mapper.Map<UserDto>(model)));
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] long id)
        {
            if (id < 1) return BadRequest();

            var request = new UserRequest()
            {
                UserId = id
            };
            UserService.Request = request;
            var model = await UserService.GetById();
            return Ok(Mapper.Map<UserDto>(model));
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Put([FromRoute] long id, [FromBody] UserDto dto)
        {
            if (dto == null) return BadRequest();
            if (dto.UserId != id) return BadRequest();

            var request = new UserRequest()
            {
                UserId = id
            };
            UserService.Request = request;
            var model = await UserService.GetById();

            model.FirstName = dto.FirstName;
            model.LastName = dto.LastName;
            model.Email = dto.Email;
            model.Deleted = dto.Deleted;
            model.DeletedAt = dto.Deleted ? DateTime.Now : null;

            UserService.Model = model;
            await UserService.Update();

            request = new UserRequest()
            {
                UserId = id
            };
            UserService.Request = request;
            return Ok(new DefaultResponseDto<UserDto>(Mapper.Map<UserDto>(await UserService.GetById())));
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long id)
        {

            var request = new UserRequest()
            {
                UserId = id
            };
            UserService.Request = request;
            var model = await UserService.GetById();
            model.UserId = id;
            await UserService.Delete();

            return Ok(new DefaultResponseDto(true));
        }

    }
}
