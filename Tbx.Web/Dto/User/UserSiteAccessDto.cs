﻿using System.Collections.Generic;
using TagPortal.Domain.Enum;

namespace TbxPortal.Web.Dto.User
{
    public class UserSiteAccessDto
    {
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public List<EnumPermissionType> PermissionTypeList { get; set; }
        public long ClientId { get; set; }
        public UserSiteAccessDto()
        {
            PermissionTypeList = new List<EnumPermissionType>();
        }
    }
}