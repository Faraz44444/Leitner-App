using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Infrastructure.Helpers;

namespace Web.Pages.Admin.Account.User
{
    [Authorize]
    public class UserDetailsModel : CustomPageModel
    {
        public async Task<IActionResult> OnGet(long id)
        {
            if (id > 0)
            {
                Core.AppContext.Current.Services.UserService.Request = new Core.Request.User.UserRequest() { UserId = id };
                var entity = await Core.AppContext.Current.Services.UserService.GetById();
                if (entity == null || entity.UserId < 1)
                    return RedirectToPage("/index");
                EntityId = entity.UserId;
            }
            return Page();
        }
    }
}
