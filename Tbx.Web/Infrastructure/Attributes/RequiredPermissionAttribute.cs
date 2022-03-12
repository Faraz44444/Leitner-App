using System;
using System.Security;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using TagPortal.Core.Security;

namespace TagPortal.Web.Infrastructure.Attributes
{
    public class RequiredPermissionAttribute : AuthorizeAttribute
    {
        protected TagIdentity CurrentUser
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    throw new SecurityException("Not authenticated");
                if (!(HttpContext.Current.User is TagPrincipal))
                    throw new SecurityException("Invalid principal");

                return (TagIdentity)HttpContext.Current.User.Identity;
            }
        }
    }

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
    public class RequiresPermissions : RequiredPermissionAttribute
    {
        private readonly int[] Permissions;
        private readonly bool SiteRestricted;
        private readonly EnumPermissionRequirement PermissionRequirement;
        public RequiresPermissions(int[] permissions, bool siteRestricted = false, EnumPermissionRequirement permissionRequirement = EnumPermissionRequirement.Any)
        {
             Permissions = permissions;
            SiteRestricted = siteRestricted;
            PermissionRequirement = permissionRequirement;
        }
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (CurrentUser.UserType != Domain.Enum.EnumUserType.ClientUser)
            {
                base.HandleUnauthorizedRequest(actionContext);
                return false;
            }
            if (SiteRestricted)
            {
                if (PermissionRequirement == EnumPermissionRequirement.All)
                {
                    if (!CurrentUser.HasAllPermission(Permissions))
                    {
                        base.HandleUnauthorizedRequest(actionContext);
                        return false;
                    }
                }
                else
                {
                    if (!CurrentUser.HasAnyPermission(Permissions))
                    {
                        base.HandleUnauthorizedRequest(actionContext);
                        return false;
                    }
                }
            }
            else
            {
                if (PermissionRequirement == EnumPermissionRequirement.All)
                {
                    if (!CurrentUser.HasAllPermission(Permissions))
                    {
                        base.HandleUnauthorizedRequest(actionContext);
                        return false;
                    }
                }
                else
                {
                    if (!CurrentUser.HasAnyPermission(Permissions))
                    {
                        base.HandleUnauthorizedRequest(actionContext);
                        return false;
                    }
                }
            }
            return base.IsAuthorized(actionContext);
        }

        public enum EnumPermissionRequirement
        {
            Any = 1,
            All = 2
        }
    }
}