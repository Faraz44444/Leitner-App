namespace Core.Infrastructure.Mail

{
    internal static class MailTemplates

    {
        private static string SiteUrl = Helpers.SiteHelper.SiteUrl;
        private static string SiteUrlFriendly = Helpers.SiteHelper.SiteUrlFriendly;
        private static string ResetPasswordUrl = Helpers.SiteHelper.ResetPasswordUrl;
        private static string SiteLogoBytes = Helpers.SiteHelper.SiteLogoByte;
        //private static string SiteLink => $"<a href='{SiteUrl}'>{SiteUrlFriendly}</a>"; 

        internal static string NewUserTemplate(string token) {
            return DefaultEmailTemplate(
                title: "Ny Bruker",
                message: $@"Det er registrert en ny bruker til deg på {SiteUrlFriendly}. 
                            For å fullføre registering må du klikke på linken under og opprette et nytt passord, 
                            deretter kan du logge inn med denne e-post adressen. 
                            <br /> 
                            <br /> 
                            Ved første innlogging kan det hende du må fylle inn informasjon om deg selv.",
                linkUrl: string.Format("{0}/{1}", ResetPasswordUrl, token),
                linkName: "Fullfør registrering");
        }
        internal static string ResetPasswordTemplate(string token)
        {
            return DefaultEmailTemplate(
                title: "Tilbakestill Passord",
                message: $@"Vi har mottatt en forespørsel om å tilbakestille passordet ditt på {SiteUrlFriendly}. 
                            For å tilbakestille passordet må du klikke på linken under, fylle inn nytt passord, 
                            deretter kan du logge inn igjen med ditt nye passord. 
                            <br /> 
                            <br /> 
                            Dersom du ikke har sendt denne forespørselen kan du se vekk fra denne e-posten.",
                linkUrl: string.Format("{0}/{1}", ResetPasswordUrl, token),
                linkName: "Tilbakestill passord");
        }

        internal static string PasswordUpdatedTemplate()
        {
            return DefaultEmailTemplate(
                title: "Oppdatert passord",
                message: $@"Passordet til din bruker på {SiteUrlFriendly} har blitt oppdatert. 
                            <br /> 
                            <br /> 
                            Dersom det ikke var du som gjorde dette bør du tilbakestille passordet ditt.",
                linkUrl: SiteUrl,
                linkName: "Logg Inn");
        }

        private static string DefaultEmailTemplate(string title, string message, string linkUrl, string linkName, string footer = "")
        {
            var footerContent = "";
            if (!footer.Empty())
                footerContent = $@"  
                                <tr> 
                                    <td style=' height:80px;'> 
                                        <div style='color:gray; text-align:center;'> 
                                            <i>{footer}</i> 
                                        </div> 
                                    </td> 
                                </tr>";
            return $@" 
                <html lang=' en-US'> 
                   <head> 
                      <meta content=' text/html; charset=utf-8' http-equiv=' Content-Type'> 
                      <title>New User</title> 
                      <meta name=' description' content=' New User.'> 
                      <style type=' text/css'> 
                         a:hover {{text-decoration: underline !important;}} 
                    </style> 
                   </head> 
  
                   <body marginheight='0' topmargin='0' marginwidth='0' style='margin: 0px; background-color: #f2f3f8;' 
                      leftmargin='0'> 
                      <table cellspacing='0' border='0' cellpadding='0' width='100%' bgcolor=' #f2f3f8' style=' @import 
                         url(https://fonts.googleapis.com/css?family=Rubik:300,400,500,700|Open+Sans:300,400,600,700); font-family: ' 
                         open='' sans',='' sans-serif;'=''> 
                         <tbody> 
                            <tr> 
                               <td> 
                                  <table style=' background-color: #f2f3f8; max-width:670px; margin:0 auto;' width=' 100%' border=' 
                                     0' align=' center' cellpadding=' 0' cellspacing=' 0'> 
                                     <tbody> 
                                        <tr> 
                                           <td style=' height:80px;'>&nbsp;</td> 
                                        </tr> 
                                        <tr> 
                                           <td style='text-align:center;'> 
                                              <a href=' {SiteUrl}' title=' logo' target=' _blank' style='display:inline-block;'> 
                                                 <img src='{SiteLogoBytes}' title=' logo' alt=' logo'> 
                                              </a> 
                                           </td> 
                                        </tr> 
                                        <tr> 
                                           <td style=' height:20px;'>&nbsp;</td> 
                                        </tr> 
                                        <tr> 
                                           <td> 
                                              <table border=' 0' cellpadding=' 0' cellspacing=' 0' 
                                                 style=' 
                                                    margin: auto; 
                                                    background: #fff; 
                                                    border-radius: 3px; 
                                                    max-width: 670px; 
                                                    width: 100%; 
                                                    -webkit-box-shadow: 0 6px 18px 0 rgb(0 0 0 / 6%); 
                                                    -moz-box-shadow: 0 6px 18px 0 rgba(0,0,0,.06); 
                                                    box-shadow: 0 6px 18px 0 rgb(0 0 0 / 6%); 
                                                    '> 
                                                 <tbody> 
                                                    <tr> 
                                                       <td style=' height:40px;'>&nbsp;</td> 
                                                    </tr> 
                                                    <tr> 
                                                       <td style='padding:0 35px;'> 
                                                          <h1 style='  
                                                            color:#1e1e2d;  
                                                            font-weight:500; 
                                                            margin:0; 
                                                            font-size:32px; 
                                                            text-align:center; 
                                                            font-family:'rubik,sans-serif;''>{title}</h1> 
                                                       </td> 
                                                    </tr> 
                                                    <tr> 
                                                       <td style=' padding:10px 35px; vertical-align:middle; border-bottom:1px solid 
                                                          #cecece;'> 
                                                       </td> 
                                                    </tr> 
                                                    <tr> 
                                                       <td style='padding:20px 35px; color:#455056; font-size:15px;line-height:24px; margin:0; text-align:center;'> 
                                                            {message} 
                                                       </td> 
                                                    </tr> 
                                                    <tr> 
                                                        <td style=' padding:35px 35px;'> 
                                                            <table align='center'> 
                                                                <tbody> 
                                                                    <tr> 
                                                                        <td style=' background:#666A86; font-weight:500; color:#fff; 
                                                                            font-size:14px; padding:10px 24px; border-radius:50px;'> 
                                                                            <a href='{linkUrl}' style=' text-decoration:none !important; 
                                                                                text-transform:uppercase; display:inline-block; color:#fff;'> 
                                                                                {linkName} 
                                                                            </a> 
                                                                        </td> 
                                                                    </tr> 
                                                                </tbody> 
                                                            </table> 
                                                        </td> 
                                                    </tr> 
                                                    <tr> 
                                                       <td style=' height:40px;'>&nbsp;</td> 
                                                    </tr> 
                                                 </tbody> 
                                              </table> 
                                           </td> 
                                        </tr> 
                                        <tr> 
                                           <td style=' height:20px;'>&nbsp;</td> 
                                        </tr> 
                                        {footerContent} 
                                     </tbody> 
                                  </table> 
                               </td> 
                            </tr> 
                         </tbody> 
                      </table> 
                   </body> 
                </html> 
            ";
        }
    }
}

