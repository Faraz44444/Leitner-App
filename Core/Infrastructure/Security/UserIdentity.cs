using Core.Infrastructure.Constants;
using Domain.Enum.Permission;
using Domain.Interfaces;
using System;
using System.Linq;
using System.Security.Claims;

namespace Core.Infrastructure.Security
{
    public class UserIdentity : ICurrentUser
    {
        private readonly ClaimsIdentity CurrentUser;
        public long UserId => CurrentUser.Get<long>(AppClaimTypes.NameIdentifier);
        public long CurrentClientId => CurrentUser.Get<long>(AppClaimTypes.CurrentClientId);
        public long CurrentClientName => CurrentUser.Get<long>(AppClaimTypes.CurrentClientName);
        public string Email => CurrentUser.Get<string>(AppClaimTypes.Email);
        public string Username => CurrentUser.Get<string>(AppClaimTypes.Username);
        public string FirstName => CurrentUser.Get<string>(AppClaimTypes.FirstName);
        public string LastName => CurrentUser.Get<string>(AppClaimTypes.LastName);
        public string FullName => CurrentUser.Get<string>(AppClaimTypes.FullName);
        public string RoleName => CurrentUser.Get<string>(AppClaimTypes.RoleName);
        public bool IsLoginPersistent => CurrentUser.Get<bool>(AppClaimTypes.IsLoginPersistent);
        public DateTime LastUpdateAt => CurrentUser.Get<DateTime>(AppClaimTypes.UserLastUpdateAt);

        public bool IsAuthenticated
        {
            get { return CurrentUser.TryGet(AppClaimTypes.Email, out string value); }
        }

        public UserIdentity(ClaimsIdentity currentUser)
        {
            this.CurrentUser = currentUser;
        }

        private bool HasPermission(EnumPermission[] permissions, bool requireAll = true)
        {
            //Check if any permissions are required
            //Check if the page requires all permissions, if so; check if they have
            //Else check if the user has any of the permissions
            if ((permissions?.Length ?? 0) == 0) return true;
            else if (requireAll)
                return permissions.All(permission =>
                    CurrentUser.HasClaim(AppClaimTypes.Permission, permission.ToString()));
            else
                return permissions.Any(permission =>
                    CurrentUser.HasClaim(AppClaimTypes.Permission, permission.ToString()));
        }

        public bool HasAllPermissions(params EnumPermission[] permissions)
        {
            return HasPermission(permissions, requireAll: true);
        }

        public bool HasAnyPermissions(params EnumPermission[] permissions)
        {
            return HasPermission(permissions, requireAll: false);
        }
    }
}
