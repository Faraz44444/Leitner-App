using Core.Infrastructure.Logging;
using Core.Infrastructure.Mail;
using Core.Infrastructure.UnitOfWork;
using Core.Service;
using System;
using System.Net.Mail;

namespace Core
{
    public class AppContext
    {
        public static AppContext Current;
        public readonly ServiceContext Services;
        public readonly LogContext Log;

        public AppContext(
            string sqlConnectionString, string siteLogoByte, string siteName, string siteUrl, string siteUrlFriendly,
            string resetPasswordUrl,
            string clientImageDirectory, string noReplyAddress, SmtpDeliveryMethod smtpDeliveryMethod, string smtpHost,
            int smtpPort)
        {
            if (Current != null)
                throw new InvalidOperationException("App context is already initialized");

            _ = new Infrastructure.Helpers.SiteHelper(
                siteName: siteName,
                siteLogoByte: siteLogoByte,
                siteUrl: siteUrl,
                siteUrlFriendly: siteUrlFriendly,
                resetPasswordUrl: resetPasswordUrl,
                clientImageDirectory: clientImageDirectory
            );

            UnitOfWorkProvider.SqlConnectionString = sqlConnectionString;
            var uowProvider = new UnitOfWorkProvider();

            Services = new ServiceContext(
                uowProvider: uowProvider,
                new MailConfiguration(smtpDeliveryMethod, smtpHost, smtpPort, noReplyAddress));

            Log = new LogContext(Services.ActionLogService);

        }
    }
}