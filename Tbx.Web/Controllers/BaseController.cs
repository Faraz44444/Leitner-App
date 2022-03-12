using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Web;
using System.Web.Http;
using TagPortal.Core;
using TagPortal.Core.Infrastructure.Filters;
using TagPortal.Core.Security;
using TagPortal.Core.Service;

namespace TbxPortal.Web.Controllers
{
    [HandleException]
    public class BaseController : ApiController
    {
        protected TagIdentity CurrentUser => (TagIdentity)TagPrincipal.Identity;
        protected ServiceContext Services => TagAppContext.Current.Services;
        private TagPrincipal TagPrincipal
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    throw new SecurityException("Not authenticated");
                if (!(HttpContext.Current.User is TagPrincipal))
                    throw new SecurityException("Invalid principal");

                return (TagPrincipal)HttpContext.Current.User;
            }
        }

        protected void LogException(Exception exception)
        {
            //Services.ErrorLogService.Insert(exception, CurrentUser.UserId, CurrentUser.Username, CurrentUser.Email);
        }
    }
}