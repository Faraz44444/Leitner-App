using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TagPortal.Domain.Aggregated.User;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Security
{
    public class TagIdentity : IIdentity
    {
        public long UserId { get; private set; }
        public string Username { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public long PhoneCountryId { get; set; }
        public string PhoneCountryCode { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public bool ForcePasswordReset { get; private set; }
        public bool ForceUserInformationUpdate { get; private set; }
        public decimal CssFontSize { get; private set; }
        public bool LeftNavigationMinified { get; private set; }
        public List<UserSiteAccess> UserSiteAccessList { get; private set; }
        public EnumUserType UserType { get; private set; }

        public TagIdentity(UserModel user)
        {
            UserId = user.UserId;
            Username = user.Username;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            ForcePasswordReset = user.ForcePasswordReset;
            ForceUserInformationUpdate = user.ForceUserInformationUpdate;
            CssFontSize = user.CssFontSize;
            UserType = user.UserType;
            PhoneNumber = user.PhoneNumber;
        }

        public TagIdentity(UserModel user, List<UserSiteAccess> userSiteAccessList)
        {
            UserId = user.UserId;
            Username = user.Username;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
            
            ForcePasswordReset = user.ForcePasswordReset;
            ForceUserInformationUpdate = user.ForceUserInformationUpdate;
            CssFontSize = user.CssFontSize;
            UserSiteAccessList = userSiteAccessList;
            UserType = user.UserType;
            PhoneNumber = user.PhoneNumber;
        }

        public string Name => Username;

        public string AuthenticationType => "Custom authentication";

        public bool IsAuthenticated => UserId > 0;

        #region Permission Validation

        public bool HasPermission(EnumPermissionType permission) => UserType == EnumUserType.SystemUser && UserSiteAccessList.Any(x => x.PermissionTypeList.Any(y => y == permission));
        public bool HasPermission(EnumPermissionType permission, long siteId) => UserType == EnumUserType.SystemUser && UserSiteAccessList.Where(x => x.SiteId == siteId).Any(x => x.PermissionTypeList.Any(y => y == permission));
        public bool HasAnyPermission(int[] permissions) => UserType == EnumUserType.SystemUser && UserSiteAccessList.Any(x => x.PermissionTypeList.Any(y => permissions.Contains((int)y)));
        public bool HasAnyPermission(int[] permissions, long siteId) => UserType == EnumUserType.SystemUser && UserSiteAccessList.Where(x => x.SiteId == siteId).Any(x => x.PermissionTypeList.Any(y => permissions.Contains((int)y)));
        public bool HasAllPermission(int[] permissions) => UserType == EnumUserType.SystemUser && UserSiteAccessList.Any(x => x.PermissionTypeList.All(y => permissions.Contains((int)y)));
        public bool HasAllPermission(int[] permissions, long siteId) => UserType == EnumUserType.SystemUser && UserSiteAccessList.Where(x => x.SiteId == siteId).Any(x => x.PermissionTypeList.All(y => permissions.Contains((int)y)));
        #endregion

    }
}
