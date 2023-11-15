using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Report.MonthlyOverview
{
    [Authorize]
    public class MonthlyOverviewModel : PageModel
    {
        public string EntityName = "Monthly Overview Report";
        public void OnGet()
        {
        }
    }
}
