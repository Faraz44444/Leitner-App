using Domain.Enum.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web0.Infrastructure.Filters
{
    public class PermissionsAuthorizationRequirement : IAuthorizationRequirement
    {
        public IEnumerable<EnumPermission> RequiredPermissions { get; }

        public PermissionsAuthorizationRequirement(IEnumerable<EnumPermission> requiredPermissions)
        {
            RequiredPermissions = requiredPermissions;
        }
    }

    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(params EnumPermission[] permissions) : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAuthorizationFilter
        {
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresPermissionAttributeImpl(PermissionsAuthorizationRequirement requiredPermissions)
            {
                _requiredPermissions = requiredPermissions;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                bool hasAllPermissions = false;

                var currentUser = context.HttpContext.User.Identities.First().GetCurrentUser();

                //if (currentUser != null)
                //    hasAllPermissions = currentUser.HasAllPermissions(_requiredPermissions.RequiredPermissions.ToArray());

                //if (!hasAllPermissions)
                //    context.Result = new UnauthorizedResult();
            }
        }
    }
    public class AnyPermissionAttribute : TypeFilterAttribute
    {
        public AnyPermissionAttribute(params EnumPermission[] permissions) : base(typeof(RequiresPermissionAttributeImpl))
        {
            Arguments = new[] { new PermissionsAuthorizationRequirement(permissions) };
        }

        private class RequiresPermissionAttributeImpl : Attribute, IAuthorizationFilter
        {
            private readonly PermissionsAuthorizationRequirement _requiredPermissions;

            public RequiresPermissionAttributeImpl(PermissionsAuthorizationRequirement requiredPermissions)
            {
                _requiredPermissions = requiredPermissions;
            }

            public void OnAuthorization(AuthorizationFilterContext context)
            {
                bool hasAllPermissions = false;

                var currentUser = context.HttpContext.User.Identities.First().GetCurrentUser();

                //if (currentUser != null)
                //    hasAllPermissions = currentUser.HasAnyPermissions(_requiredPermissions.RequiredPermissions.ToArray());

                //if (!hasAllPermissions)
                //    context.Result = new UnauthorizedResult();
            }
        }
    }
}
