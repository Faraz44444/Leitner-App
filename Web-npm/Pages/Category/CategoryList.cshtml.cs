using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DataManagement.Category
{
    [Authorize]
    public class CategoryListModel : PageModel
    {
        public string EntityName = "Category List";
        public void OnGet()
        {
        }
    }
}
