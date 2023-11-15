using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Helpers
{
    public class SiteHelper
    {
        public static string SiteName { get; private set; }
        public static string SiteUrlFriendly { get; private set; }
        public static string SiteUrl { get; private set; }
        public static string ResetPasswordUrl { get; private set; }
        public static string SiteLogoByte { get; private set; }
        public static string ClientImageDirectory { get; private set; }

        public SiteHelper(string siteName, string siteLogoByte, string siteUrl, string siteUrlFriendly,
            string resetPasswordUrl,
            string clientImageDirectory)
        {
            SiteName = siteName;
            SiteLogoByte = siteLogoByte;
            SiteUrl = siteUrl;
            SiteUrlFriendly = siteUrlFriendly;
            ResetPasswordUrl = resetPasswordUrl;
            ClientImageDirectory = clientImageDirectory;
        }
    }
}
