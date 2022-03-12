using System;
using System.Web;
using System.Web.Security;
using TagPortal.Core;
using TagPortal.Core.Security;

namespace TbxPortal.Web
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        private Guid passwordResetGuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated && HttpContext.Current.User is TagPrincipal)
            {
                Redirect();
                return;
            }

            string passwordResetGuidString = Request.QueryString["g"];
            if (string.IsNullOrWhiteSpace(passwordResetGuidString))
            {
                Redirect();
                return;
            }

            if (!Guid.TryParse(HttpUtility.UrlDecode(passwordResetGuidString), out passwordResetGuid))
            {
                Redirect();
                return;
            }

            if (!TagAppContext.Current.Services.SecurityService.IsPasswordResetGuidValid(passwordResetGuid))
            {
                Redirect();
                return;
            }
        }

        protected void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(newPassword.Value))
            {
                ShowNewPasswordIsEmptyMessage();
                return;
            }

            if (string.IsNullOrWhiteSpace(confirmedPassword.Value))
            {
                ShowConfirmedPasswordIsEmptyMessage();
                return;
            }

            if (newPassword.Value != confirmedPassword.Value)
            {
                ShowPasswordMismatchMessage();
                return;
            }

            if (!TagAppContext.Current.Services.SecurityService.UpdatePassword(passwordResetGuid, newPassword.Value))
            {
                returnMessage.InnerText = "Something went wrong. Please contact system provider.";
                return;
            }

            Redirect();
            return;
        }

        private void ShowNewPasswordIsEmptyMessage()
        {
            returnMessage.InnerText = "Please fill in new password";
        }

        private void ShowConfirmedPasswordIsEmptyMessage()
        {
            returnMessage.InnerText = "Please confirm new password";
        }

        private void ShowPasswordMismatchMessage()
        {
            returnMessage.InnerText = "New password and confirmed password does not match";
        }


        private void Redirect()
        {
            Response.Redirect(FormsAuthentication.DefaultUrl, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}