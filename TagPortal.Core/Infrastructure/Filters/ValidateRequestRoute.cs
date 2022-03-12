namespace TagPortal.Core.Infrastructure.Filters
{
    //public class ValidateRequestRoute : ActionFilterAttribute, IActionFilter
    //{
    //    public override void OnActionExecuting(HttpActionContext actionContext)
    //    {
    //        base.OnActionExecuting(actionContext);

    //        var routeData = actionContext.Request.GetRouteData().Values;
    //        var queryString = actionContext.Request.GetQueryNameValuePairs().ToDictionary(x => x.Key, x => x.Value);

    //        var duplicatedKeys = queryString.Keys.Where(s => routeData.Keys.Contains(s));

    //        if (duplicatedKeys.Any())
    //            throw new HttpException((int)HttpStatusCode.Conflict, "Duplicated parameter(s): " + string.Join(", ", duplicatedKeys));
    //    }
    //}
}