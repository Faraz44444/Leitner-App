using Core.Infrastructure.Security;
using Core.Request.Client;
using Core.Service;
using Domain.Model.Client;
//using Domain.Model.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web0.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ServiceContext Services => Core.AppContext.Current.Services;
        public virtual UserIdentity CurrentUser => HttpContext.User.Identities.First().GetCurrentUser();


        protected async Task<ClientModel> GetCurrentClient()
        {
            if (CurrentUser.CurrentClientId < 1)
                throw new InvalidOperationException("CurrentUser.CurrentClientId is not set");
            Services.ClientService.Request = new ClientRequest() { ClientId = CurrentUser.CurrentClientId };
            return await Services.ClientService.GetById();
        }
    }
}