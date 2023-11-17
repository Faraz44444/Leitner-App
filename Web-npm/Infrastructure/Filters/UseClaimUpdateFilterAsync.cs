using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Infrastructure.Filters
{
    public class UserClaimUpdateFilterAsync : IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var currentUser = context.HttpContext.User.Identities.First().GetCurrentUser();
            if (currentUser.IsAuthenticated)
            {
                var securityService = Core.AppContext.Current.Services.SecurityService;

                if (await securityService.IsUserUpdated(currentUser.UserId, currentUser.LastUpdateAt))
                {
                    Core.AppContext.Current.Services.UserService.Request = new Core.Request.User.UserRequest() { UserId = currentUser.UserId };
                    var user = await Core.AppContext.Current.Services.UserService.GetFirstOrDefault();
                    var claimsIdentity = await user.GenerateNewClaimsIdentityAsync(CookieAuthenticationDefaults.AuthenticationScheme, currentUser.IsLoginPersistent);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.HttpContext.SignInAsync(claimsPrincipal);
                }

            }

            await next();
        }
    }
}