using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DataManagement.Payment.PaymentTotal
{
    [Authorize]
    public class PaymentTotalListModel : PageModel
    {
        public string EntityName = "Total Payment List";
        public void OnGet()
        {
        }
    }
}
