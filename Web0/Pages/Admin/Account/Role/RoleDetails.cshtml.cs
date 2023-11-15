using Core.Request.User;
using Domain.Enum.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Web0.Infrastructure.Helpers;

namespace Web0.Pages.Admin.Account.Role
{
    [Authorize]
    public class RoleDetailsModel : CustomPageModel
    {
        public async Task<IActionResult> OnGet(long id)
        {
            if (id > 0)
            {
                Core.AppContext.Current.NewServices.RoleService.Request = new Core.Request.Role.RoleRequest() { RoleId = id };
                var entity = await Core.AppContext.Current.NewServices.RoleService.GetById();
                if (entity == null || entity.RoleId < 1 || entity.ClientId != CurrentUser.CurrentClientId)
                    return RedirectToPage("/index");
                EntityId = entity.RoleId;
            }
            return Page();
        }
    }
}
