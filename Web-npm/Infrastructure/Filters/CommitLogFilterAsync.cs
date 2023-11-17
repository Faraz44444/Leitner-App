using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Web.Infrastructure.Filters
{
    public class CommitLogFilterAsync : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await next();
            Core.AppContext.Current.Log.Commit();
        }
    }
}