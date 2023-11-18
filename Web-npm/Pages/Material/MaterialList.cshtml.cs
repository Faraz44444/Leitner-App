using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Material
{
    [Authorize]
    public class MaterialListModel : PageModel
    {
        public string EntityName = "Material List";
        public void OnGet()
        {
        }
    }
}
