using System;
using System.Net.Mail;

namespace TagPortal.Core
{
    static class MailSender
    {
        public static bool SendMail(MailMessage message)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = "192.168.1.19";
                smtp.Port = 25;
#if DEBUG
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Host = "127.0.0.1";

                try
                {
                    smtp.Send(message);
                    return true;
                }
                catch (Exception)
                {
                    return true;
                }
#endif
                smtp.Send(message);
                return true;
            }
        }
    }
}
