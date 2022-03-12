using TbxPortal.Web.Dto;
using TbxPortal.Web.Dto.Role;
using System.Collections.Generic;
using System.Web.Http;
using TagPortal.Core.Request.Role;
using TagPortal.Core.Service.Role;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.Role;
using TagPortal.Web.Infrastructure.Attributes;

namespace TbxPortal.Web.Controllers.ClientApi.User
{
    [RoutePrefix("api/role")]
    public class RoleController : BaseController
    {
        private RoleService RoleService => Services.RoleService;
        private RolePermissionService RolePermissionService => Services.RolePermissionService;

        [Route("lookup")]
        [HttpGet]
        public IHttpActionResult Lookup([FromUri] RoleRequest request)
        {
            var data = RoleService.GetList(request);
            return Ok(DataManagementMapper.MapList<LookupItemDto>(data));
        }

        [Route("all")]
        [HttpGet]
        //[RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetListAll([FromUri] RoleRequest request)
        {
            if (request == null) request = new RoleRequest();
            var data = RoleService.GetList(request);
            var dto = DataManagementMapper.MapList<RoleDto>(data);

            foreach (var d in dto)
            {
                var permissionList = RolePermissionService.GetList(new RolePermissionsRequest() { RoleId = d.RoleId});
                d.PermissionList = DataManagementMapper.MapList<RolePermissionDto>(permissionList);
            }

            return Ok(dto);
        }

        [Route("")]
        [HttpGet]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetList([FromUri] RoleRequest request)
        {
            if (request == null) request = new RoleRequest();
            var data = RoleService.GetPagedList(request);
            var dto = DataManagementMapper.MapPagedList<RoleDto>(data);

            foreach (var d in dto.Items)
            {
                var permissionList = RolePermissionService.GetList(new RolePermissionsRequest() { RoleId = d.RoleId});
                d.PermissionList = DataManagementMapper.MapList<RolePermissionDto>(permissionList);
            }

            return Ok(dto);
        }

        [Route("")]
        [HttpPost]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult Create([FromBody] RoleDto dto)
        {
            if (dto == null) return BadRequest();

            dto.RoleId = RoleService.Insert(dto.Name, DataManagementMapper.MapList<RolePermissionModel>(dto.PermissionList), clientId: dto.ClientId);
            var permissionList = RolePermissionService.GetList(new RolePermissionsRequest() { RoleId = dto.RoleId});
            dto.PermissionList = DataManagementMapper.MapList<RolePermissionDto>(permissionList);
            return Ok(new DefaultResponseDto<RoleDto>(dto.RoleId, true, dto));
        }

        [Route("{id}")]
        [HttpGet]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult GetById([FromUri] long id)
        {
            if (id < 1) return BadRequest();
            var data = RoleService.GetById(id);
            var dto = DataManagementMapper.Map<RoleDto>(data);
            var permissionList = RolePermissionService.GetList(new RolePermissionsRequest() { RoleId = data.RoleId});
            dto.PermissionList = DataManagementMapper.MapList<RolePermissionDto>(permissionList);
            return Ok(dto);
        }

        [Route("{id}")]
        [HttpPost]
        [RequiresPermissions(new int[] { (int)EnumPermissionType.Tbx_Admin_Users })]
        public IHttpActionResult Update([FromUri] long id, [FromBody] RoleDto dto)
        {
            if (id < 1 || dto == null || id != dto.RoleId) return BadRequest();
            var data = RoleService.GetById(id);
            if (dto.PermissionList == null)
                dto.PermissionList = new List<RolePermissionDto>();

            data.Name = dto.Name;

            RoleService.Update(data, DataManagementMapper.MapList<RolePermissionModel>(dto.PermissionList), true);
            data = RoleService.GetById(id);
            dto = DataManagementMapper.Map<RoleDto>(data);
            var permissionList = RolePermissionService.GetList(new RolePermissionsRequest() { RoleId = data.RoleId});
            dto.PermissionList = DataManagementMapper.MapList<RolePermissionDto>(permissionList);
            return Ok(new DefaultResponseDto<RoleDto>(data.RoleId, true, dto));
        }
    }
}