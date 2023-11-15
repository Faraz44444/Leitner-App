using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Report.YearlyOverview
{
    [Authorize]
    public class YearlyOverviewModel : PageModel
    {
        public string EntityName = "Yearly Overview Report";
        public void OnGet()
        {
        }
    }
}
