using System;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using TagPortal.Core;
using TagPortal.Web;
using TbxPortal.Web.App_Start;

namespace TbxPortal.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            MapperConfig.InitAutomapper();
            AppContextConfig.InitServiceContext();
            HangfireConfig.RegisterJobs();

        }

        private void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (cookie != null)
            {
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(cookie.Value);

                if (ticket != null && !ticket.Expired)
                {
                    var principal = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(ticket.Name);
                    if (principal != null && principal.Identity != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
            }
        }
    }
}