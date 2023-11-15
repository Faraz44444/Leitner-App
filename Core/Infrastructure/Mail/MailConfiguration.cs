using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Mail
{
    public class MailConfiguration
    {
        internal SmtpDeliveryMethod DeliveryMethod { get; private set; }
        internal string Host { get; private set; }
        internal int Port { get; private set; }
        internal string NoReplyAddress { get; private set; }

        public MailConfiguration(SmtpDeliveryMethod deliveryMethod, string host, int port, string noReplyAddress)
        {
            DeliveryMethod = deliveryMethod;
            Host = host;
            Port = port;
            NoReplyAddress = noReplyAddress;
        }
    }
}
