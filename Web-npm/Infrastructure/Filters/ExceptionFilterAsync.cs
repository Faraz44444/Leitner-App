using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Infrastructure.Constants;
using Core.Infrastructure.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.Dto;

namespace Web.Infrastructure.Filters
{
    public class ExceptionFilterAsync : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            var message = "An Error Has Occurred.";
            EnumFeedbackDataType? dataType = null;
            object data = null;
            bool isComplexWarning = false;

            if (context.Exception.GetType() == typeof(FeedbackException))
            {
                status = HttpStatusCode.BadRequest;
                message = context.Exception.Message;
            }
            else if (context.Exception.GetType().IsGenericType &&
                     context.Exception.GetType().GetGenericTypeDefinition() == typeof(FeedbackDataException<>))
            {
                isComplexWarning = true;
                status = HttpStatusCode.OK;
                message = context.Exception.Message;
                dataType = (EnumFeedbackDataType) context.Exception.GetType().GetProperty("DataType")
                    .GetValue(context.Exception);
                data = context.Exception.GetType().GetProperty("ExceptionData").GetValue(context.Exception);
            }
            else
            {
                ClaimsIdentity claimsIdentity = context.HttpContext.User.Identities.First();

                claimsIdentity.TryGet(AppClaimTypes.NameIdentifier, out long userId);
                claimsIdentity.TryGet(AppClaimTypes.CurrentClientId, out long clientId);
                claimsIdentity.TryGet(AppClaimTypes.FirstName, out string firstName);
                claimsIdentity.TryGet(AppClaimTypes.LastName, out string lastName);
            }

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) status;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int) status;
            response.ContentType = "application/json";
#if DEBUG
            if (status == HttpStatusCode.OK && isComplexWarning)
                context.Result = new ObjectResult(new DefaultResponseDto<object>(new
                {
                    Type = dataType,
                    Data = data
                }, ok: false, message));
            else if (status == HttpStatusCode.BadRequest)
                context.Result = new ObjectResult(new {ExceptionMesaage = message});
            else
            {
                context.Result = new ObjectResult(new
                {
                    ExceptionMessage = $"[{context.Exception.GetType().Name}]::{context.Exception.Message}",
                    ExceptionStackTrace = context.Exception.StackTrace
                });
            }
#else
            if(status == HttpStatusCode.OK && isComplexWarning)
                context.Result = new ObjectResult(new DefaultResponseDto<object>(new {Type = dataType, Data = data}, ok: false, message));
            else
                context.Result = new ObjectResult(new { ExceptionMessage = message});
#endif
            return Task.CompletedTask;
        }
    }
}
