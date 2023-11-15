using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web0.Infrastructure.Helpers;

namespace Web.Pages.DataManagement.Payment.PaymentPriority.Details
{
    [Authorize]
    public class PaymentPriorityDetailsModel : CustomPageModel
    {
        public string EntityName = "Payment Priority Details";
        public async Task<IActionResult> OnGet(long id)
        {
            if (id > 0)
            {
                Core.AppContext.Current.Services.PaymentPriorityService.Request =
                    new Core.Request.Payment.PaymentPriorityRequest() { PaymentPriorityId = id };
                var entity = await Core.AppContext.Current.Services.PaymentPriorityService.GetById();
                if (entity == null || entity.PaymentPriorityId < 1 || entity.ClientId != CurrentUser.CurrentClientId)
                    return RedirectToPage("/index");
                EntityId = entity.PaymentPriorityId;
            }
            return Page();
        }
    }
}
