using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.Constants;
using Core.Infrastructure.Exception;
using Core.Infrastructure.Security;
using Domain.Model.User;

public static class ClaimsIdentityExtension
{
    public static UserIdentity GetCurrentUser(this ClaimsIdentity currentUser)
    {
        return new UserIdentity(currentUser);
    }

    public static async Task<ClaimsIdentity> GenerateNewClaimsIdentityAsync(this UserModel user,
        string authenticationType, bool isPersistent)
    {
        var services = Core.AppContext.Current.Services;
        var userService = services.UserService;
        var roleService = services.RoleService;
        var rolePermissionService = services.RolePermissionService;
        var userClientService = services.UserClientService;

        userClientService.Request = new Core.Request.User.UserClientRequest() { UserId = user.UserId, ClientId = user.CurrentClientId };
        var userClient = await userClientService.GetFirstOrDefault();

        if (user == null || user.UserId < 1) throw new FeedbackException();
        if (user.Deleted) throw new FeedbackException("Sorry, your username has been deactivated!");
        if (userClient == null || userClient.ClientId < 1)
            throw new FeedbackException("Sorry, no access has been given to you!");

        rolePermissionService.Request = new Core.Request.Role.RolePermissionRequest() { RoleId = userClient.RoleId };
        var t_permission = await rolePermissionService.GetList();
        roleService.Request = new Core.Request.Role.RoleRequest() { RoleId = userClient.RoleId };
        var t_role = await roleService.GetById();

        if (t_role == null || t_role.RoleId < 1) throw new FeedbackException();

        var claims = new List<Claim>()
        {
            new Claim(AppClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(AppClaimTypes.Username, user.Username ?? ""),
            new Claim(AppClaimTypes.Email, user.Email),
            new Claim(AppClaimTypes.FirstName, user.FirstName??""),
            new Claim(AppClaimTypes.LastName, user.LastName ??""),
            new Claim(AppClaimTypes.FullName,
                $"{string.Join(" ", new List<string> {user.FirstName ?? "", user.LastName ?? ""})}"),
            new Claim(AppClaimTypes.CurrentClientId, user.CurrentClientId.ToString()),
            new Claim(AppClaimTypes.CurrentClientName, user.CurrentClientName),
            new Claim(AppClaimTypes.RoleName, t_role.Name ?? ""),
            new Claim(AppClaimTypes.IsLoginPersistent, isPersistent.ToString()),
            new Claim(AppClaimTypes.UserLastUpdateAt, user.LastUpdateAt.ToString())
        };


        if (t_permission != null)
            foreach (var permission in t_permission)
                claims.Add(new Claim(AppClaimTypes.Permission, permission.Permission.ToString()));
        return new ClaimsIdentity(claims, authenticationType);

    }

    public static T Get<T>(this ClaimsIdentity currentUser, string claimType)
    {
        if (!currentUser.TryGet<T>(claimType, out T value))
        {
            throw new UnauthorizedAccessException();
        }

        return value;
    }

    public static bool TryGet<T>(this ClaimsIdentity currentUser, string claimType, out T value)
    {
        value = default(T);
        try
        {
            var claim = currentUser.Claims.FirstOrDefault(x => x.Type == claimType);
            if (claim == null) return false;

            var converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter == null) return false;

            value = (T)converter.ConvertFromString(claim.Value);
            return true;
        }
        catch (NotSupportedException)
        {
            return false;
        }
    }
}

