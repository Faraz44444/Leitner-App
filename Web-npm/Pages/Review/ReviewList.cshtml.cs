using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Review
{
    [Authorize]
    public class ReviewListModel : PageModel
    {
        public string EntityName = "Material List";
        public void OnGet()
        {
        }
    }
}
