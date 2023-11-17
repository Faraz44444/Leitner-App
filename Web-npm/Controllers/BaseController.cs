using Core.Infrastructure.Security;
using Core.Service;
//using Domain.Model.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected ServiceContext Services => Core.AppContext.Current.Services;
        public virtual UserIdentity CurrentUser => HttpContext.User.Identities.First().GetCurrentUser();
    }
}