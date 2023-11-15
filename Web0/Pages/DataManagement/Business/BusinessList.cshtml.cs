using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DataManagement.Business
{
    [Authorize]
    public class BusinessListModel : PageModel
    {
        public string EntityName = "Business List";
        public void OnGet()
        {
        }
    }
}
