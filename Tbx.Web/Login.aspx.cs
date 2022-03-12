using System;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using TagPortal.Core;
using TagPortal.Core.Security;
using TagPortal.Domain.Enum;

namespace TbxPortal.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User is TagPrincipal)
                Redirect();
        }

        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Value) || string.IsNullOrWhiteSpace(txtPassword.Value))
            {
                ShowIncorrectCredentialsMessage();
                return;
            }

            TagPrincipal user = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(txtUsername.Value, txtPassword.Value);
            if (user == null)
            {
                ShowIncorrectCredentialsMessage();
                return;
            }

            TagIdentity CurrentUser = (TagIdentity)user.Identity;

            var json = new JavaScriptSerializer().Serialize(user.Identity.Name); //Could store more data to prevent fetching for every request.
            var ticket = new FormsAuthenticationTicket(1, user.Identity.Name, DateTime.Now, DateTime.Now.AddHours(8), true, json);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            Response.Cookies.Add(cookie);
            Redirect();
        }

        protected void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(resetPasswordUsername.Value))
                TagAppContext.Current.Services.SecurityService.ResetPassword( resetPasswordUsername.Value);
        }

        private void ShowIncorrectCredentialsMessage()
        {
            returnMessage.InnerText = "Incorrect username or password";
        }

        private void ShowNoAccessMessage()
        {
            returnMessage.InnerText = "You don't have access to Tbx.";
        }

        private void Redirect()
        {
            var url = Request.QueryString["ReturnUrl"] ?? "";
            if (string.IsNullOrWhiteSpace(url) || url.ToLower().Contains("signout") || url.ToLower().Contains("login"))
                url = FormsAuthentication.DefaultUrl;
            Response.Redirect(url, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}