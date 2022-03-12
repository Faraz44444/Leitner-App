using TbxPortal.Web.Dto;
using TbxPortal.Web.Dto.User;
using System.Linq;
using System.Web.Http;
using TagPortal.Core.Request.User;
using TagPortal.Core.Service.Security;
using TagPortal.Core.Service.User;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.User;
using TagPortal.Web.Infrastructure.Attributes;
using System;

namespace TbxPortal.Web.Controllers.ClientApi.User
{
    [RoutePrefix("api/user")]
    public class UserController : BaseController
    {
        private UserService UserService => Services.UserService;
        private UserSiteService UserSiteService => Services.UserSiteService;
        private UserSiteAccessService UserSiteAccessService => Services.UserSiteAccessService;
        private SecurityService SecurityService => Services.SecurityService;

        [Route("lookup")]
        [HttpGet]
        public IHttpActionResult Lookup([FromUri] UserRequest request)
        {
            request.UserType = EnumUserType.ClientUser;
            request.OrderBy = UserOrderByEnum.UserFullName;
            var data = UserService.GetList(request);
            return Ok(DataManagementMapper.MapList<LookupItemDto>(data));
        }

        [Route("")]
        [HttpGet]
        //[RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetList([FromUri] UserRequest request)
        {

            var data = UserService.GetPagedList(request);
            return Ok(DataManagementMapper.MapPagedList<UserDto>(data));
        }

        [Route("")]
        [HttpPost]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult Create([FromBody] UserDto dto)
        {
            if (dto == null) return BadRequest();
          
            dto.UserType = EnumUserType.ClientUser;

            string temporaryPassword = SecurityService.GenerateTemporaryPassword();
            string temporaryHash = SecurityService.CreatePasswordHash(temporaryPassword);
            var passwordResetGuid = Guid.NewGuid();
            dto.UserId = UserService.Insert(
                username: dto.Username,
                userInitials: dto.UserInitials,
                userType: EnumUserType.ClientUser,
                email: dto.Email,
                firstName: dto.FirstName,
                lastName: dto.LastName,
                temporaryPassword: temporaryPassword,
                temporaryHash: temporaryHash,
                forcePasswordRest: true,
                forceUserInformationUpdate: true,
                clientId: dto.ClientId,
                siteId: dto.CurrentSiteId,
                externalId: dto.ExternalId,
                passwordResetGuid: passwordResetGuid);

            return Ok(new DefaultResponseDto<UserDto>(dto.UserId, true, dto));
        }


        [Route("{id}")]
        [HttpGet]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetById([FromUri] long id)
        {
            if (id < 1) return BadRequest();
            var data = UserService.GetById(id);
         
            return Ok(DataManagementMapper.Map<UserDto>(data));
        }

        [Route("{id}")]
        [HttpPost]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult Update([FromUri] long id, [FromBody] UserDto dto)
        {
            if (id < 1 || dto == null || id != dto.UserId) return BadRequest();
            var data = UserService.GetById(id);
            if (data.UserType != EnumUserType.ClientUser) return BadRequest();
            var user = UserService.GetById(id);
            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Active = dto.Active;

            UserService.Update(user);
            data = UserService.GetById(id);
            return Ok(new DefaultResponseDto<UserDto>(data.UserId, true, DataManagementMapper.Map<UserDto>(data)));
        }

        [Route("{id}/role")]
        [HttpGet]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetUserRoles([FromUri] long id)
        {
            if (id < 1) return BadRequest();
            var user = UserService.GetById(id);
            if (user.UserType != EnumUserType.ClientUser) return BadRequest();
            var data = UserSiteService.GetList(new UserSiteRequest()
            {
                UserId = user.UserId,
                OrderBy = UserSiteOrderByEnum.SiteName
            });

            return Ok(DataManagementMapper.MapList<UserSiteDto>(data));
        }

        [Route("{id}/role")]
        [HttpPost]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult UpdateUserRoles([FromUri] long id, [FromBody] UserSiteRequestDto dto)
        {
            if (id < 1) return BadRequest();
            if (dto == null)
                dto = new UserSiteRequestDto();

            if (dto.UserSiteList.Any(x => x.UserId != id)) return BadRequest();

            var user = UserService.GetById(id);
            if (user.UserType != EnumUserType.ClientUser) return BadRequest();


            //UserSiteService.Update( user.UserId, TbxMapper.MapList<UserSiteModel>(dto.UserSiteList));
            return Ok(new DefaultResponseDto(user.UserId, true));
        }

        [Route("{id}/access")]
        [HttpGet]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetUserAccessList([FromUri] long id)
        {
            if (id < 1) return BadRequest();
            var user = UserService.GetById(id);
            if (user.UserType != EnumUserType.ClientUser) return BadRequest();
            var data = UserSiteAccessService.GetList(new UserSiteAccessRequest()
            {
                UserId = id
            });

            return Ok(DataManagementMapper.MapList<UserSiteAccessDto>(data));
        }

        [Route("supplierUsers/{id}")]
        [HttpGet]
        public IHttpActionResult GetSupplierUserList([FromUri] long id)
        {
            if (id < 1) return BadRequest();
            var request = new UserRequest()
            {
                UserType = EnumUserType.SupplierUser,
                Active = true,
                OrderBy = UserOrderByEnum.UserFullName
            };

            var data = UserService.GetList(request);
            return Ok(DataManagementMapper.MapList<UserDto>(data));
        }

    }
}