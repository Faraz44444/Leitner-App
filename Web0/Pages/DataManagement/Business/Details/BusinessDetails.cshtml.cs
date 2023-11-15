using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web0.Infrastructure.Helpers;

namespace Web.Pages.DataManagement.Business.Details
{
    [Authorize]
    public class BusinessDetailsModel : CustomPageModel
    {
        public string EntityName = "Business Details";
        public async Task<IActionResult> OnGet(long id)
        {
            if (id > 0)
            {
                Core.AppContext.Current.NewServices.BusinessService.Request = new Core.Request.Business.BusinessRequest() { BusinessId = id };
                var entity = await Core.AppContext.Current.NewServices.BusinessService.GetById();
                if (entity == null || entity.BusinessId < 1 || entity.ClientId != CurrentUser.CurrentClientId)
                    return RedirectToPage("/index");
                EntityId = entity.BusinessId;
            }
            return Page();
        }
    }
}
