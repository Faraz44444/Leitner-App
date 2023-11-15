using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web0.Infrastructure.Helpers;

namespace Web.Pages.DataManagement.Category.Details
{
    [Authorize]
    public class CategoryDetailsModel : CustomPageModel
    {
        public string EntityName = "Category Details";
        public async Task<IActionResult> OnGet(long id)
        {
            if (id > 0)
            {
                Core.AppContext.Current.NewServices.CategoryService.Request = new Core.Request.Category.CategoryRequest() { CategoryId = id };
                var entity = await Core.AppContext.Current.NewServices.CategoryService.GetById();
                if (entity == null || entity.CategoryId < 1 || entity.ClientId != CurrentUser.CurrentClientId)
                    return RedirectToPage("/index");
                EntityId = entity.CategoryId;
            }
            return Page();
        }
    }
}
