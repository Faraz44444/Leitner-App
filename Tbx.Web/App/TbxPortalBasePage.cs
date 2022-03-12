using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using TagPortal.Core;
using TagPortal.Core.Security;
using TagPortal.Core.Service;

namespace TbxPortal.Web.App
{
    public class TbxPortalBasePage : Page
    {
        protected TagIdentity CurrentUser { get; private set; }
        protected ServiceContext Services => TagAppContext.Current.Services;
        protected virtual void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                FormsAuthentication.RedirectToLoginPage();
            if (!(HttpContext.Current.User is TagPrincipal))
                FormsAuthentication.RedirectToLoginPage();

            CurrentUser = (TagIdentity)HttpContext.Current.User.Identity;
            if (CurrentUser.ForcePasswordReset && !Request.Url.AbsolutePath.ToLower().Contains("account/changepassword"))
                Redirect("~/account/changepassword");
            if (CurrentUser.ForceUserInformationUpdate && (!Request.Url.AbsolutePath.ToLower().Contains("account") && !CurrentUser.ForcePasswordReset))
                Redirect("~/account");
        }

        protected void Redirect(string url)
        {
            Response.Redirect(Page.ResolveUrl(url.ToLower()), false);
            Context.ApplicationInstance.CompleteRequest();
        }

        protected void RedirectToDefault()
        {
            if (CurrentUser.UserType == TagPortal.Domain.Enum.EnumUserType.ClientUser)
            {
                Response.Redirect(Page.ResolveUrl("~/dashboard"), false);
            }
            else if (CurrentUser.UserType == TagPortal.Domain.Enum.EnumUserType.SupplierUser)
            {
                Response.Redirect(Page.ResolveUrl("~/company/dashboard"), false);
            }

        }
    }
}