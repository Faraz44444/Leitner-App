using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Core.Infrastructure.Helpers;
using Core.Infrastructure.Mail;
using Core.Infrastructure.UnitOfWork;
using Core.Repository;

namespace Core.Service.Mail
{
    public class MailService
    {
        private readonly MailConfiguration MailConfiguration;
        private static string SiteName => SiteHelper.SiteName;

        public MailService(MailConfiguration mailConfiguration)
        {
            MailConfiguration = mailConfiguration;
        }

        private void SendMail(string subject, string message, string recipientEMail, string senderAddress)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using (MailMessage mailMessage = new MailMessage())
            {
                mailMessage.Subject = subject;
                mailMessage.Body = message;
                mailMessage.To.Add(recipientEMail);
                mailMessage.From = new MailAddress(senderAddress, senderAddress);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = Encoding.GetEncoding(1252);
                mailMessage.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(mailMessage.Body , new ContentType("text/html")));

                MailSender.SendMail(mailConfiguration: MailConfiguration, mailMessage: mailMessage);
            }
        }

        internal void SendNewUserMail(string email, string token)
        {
            string message = MailTemplates.NewUserTemplate(token: token);
            SendMail(
                subject: $"{SiteName} - New User",
                message: message,
                recipientEMail: email,
                senderAddress: MailConfiguration.NoReplyAddress);
        }

        internal void SendResetPasswordTokenMail(string email, string token)
        {
            string message = MailTemplates.ResetPasswordTemplate(token: token);
            SendMail(
                subject:$"{SiteName} - Reset Password",
                message: message,
                recipientEMail: email,
                senderAddress:MailConfiguration.NoReplyAddress);
        }

        internal void SendPasswordUpdatedMail(string email)
        {
            string message = MailTemplates.PasswordUpdatedTemplate();
            SendMail(
                subject:$"{SiteName} - Password Updated",
                message: message,
                recipientEMail: email, 
                senderAddress: MailConfiguration.NoReplyAddress);
        }
    }
}
