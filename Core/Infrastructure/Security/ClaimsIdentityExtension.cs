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


        if (user == null || user.UserId < 1) throw new FeedbackException();
        if (user.Deleted) throw new FeedbackException("Sorry, your username has been deactivated!");
        var claims = new List<Claim>()
        {
            new Claim(AppClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(AppClaimTypes.Username, user.Username ?? ""),
            new Claim(AppClaimTypes.Email, user.Email),
            new Claim(AppClaimTypes.FirstName, user.FirstName??""),
            new Claim(AppClaimTypes.LastName, user.LastName ??""),
            new Claim(AppClaimTypes.FullName,
                $"{string.Join(" ", new List<string> {user.FirstName ?? "", user.LastName ?? ""})}"),
            new Claim(AppClaimTypes.IsLoginPersistent, isPersistent.ToString()),
            new Claim(AppClaimTypes.UserLastUpdateAt, user.LastUpdateAt.ToString())
        };

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

