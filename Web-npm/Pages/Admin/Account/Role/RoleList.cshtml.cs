using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web0.Pages.Admin.Account.Role
{
    [Authorize]
    public class RoleListModel : PageModel
    {
        public string EntityName = "Role List";
        public void OnGet()
        {
        }
    }
}
