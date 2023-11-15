using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DataManagement.Payment.PaymentPriority
{
    [Authorize]
    public class PaymentPriorityListModel : PageModel
    {
        public string EntityName = "Payment Priority List";
        public void OnGet()
        {
        }
    }
}
