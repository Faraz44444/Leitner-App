using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.DataManagement.Payment
{
    [Authorize]
    public class PaymentListModel : PageModel
    {
        public string EntityName = "Payment List";
        public void OnGet()
        {
        }
    }
}
