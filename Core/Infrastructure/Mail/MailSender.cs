using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mail
{
    internal static class MailSender
    {
        internal static void SendMail(MailConfiguration mailConfiguration, MailMessage mailMessage)
        {
            mailMessage.From = new MailAddress("budgeting.app.faraz.safarpour@gmail.com");
            mailMessage.Subject = "test";
            mailMessage.IsBodyHtml= true;
            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
            {
                smtp.Credentials = new NetworkCredential("budgeting.app.faraz.safarpour@gmail.com", "BlauBlau%");
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }
    }
}
