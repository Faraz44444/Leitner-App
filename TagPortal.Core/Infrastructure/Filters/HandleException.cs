using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace TagPortal.Core.Infrastructure.Filters
{
    public class HandleException : ExceptionFilterAttribute, IExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception.GetType() == typeof(FeedbackException))
            {
                actionExecutedContext.Response = actionExecutedContext.Request
                    .CreateResponse(HttpStatusCode.BadRequest, new { ExceptionMessage = actionExecutedContext.Exception.Message });
            }
            else
            {
                // LOG EXCEPTION
                var services = TagAppContext.Current.Services;
                var currentUser = actionExecutedContext.ActionContext.RequestContext.Principal.Identity as Security.TagIdentity;

                object exceptionMessage = null;
#if DEBUG
                exceptionMessage = new { ExceptionMessage = "Internal Server Error::" + actionExecutedContext.Exception.Message };
#endif

                //services.ErrorLogService.Insert(
                //    actionExecutedContext.Exception,
                //    currentUser.UserId,
                //    currentUser.Username,
                //    currentUser.Email
                //);

                actionExecutedContext.Response = actionExecutedContext.Request
                    .CreateResponse(HttpStatusCode.InternalServerError, exceptionMessage);
            }

        }
    }
}
