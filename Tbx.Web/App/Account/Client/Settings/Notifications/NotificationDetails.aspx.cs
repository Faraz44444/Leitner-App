using System;
using System.Web.UI;
using TbxPortal.Web.App;

namespace TbxPortal.Web.App.Account.Client.Settings.Notifications
{
    public partial class NotificationDetails : TbxPortalBasePage
    {

        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (CurrentUser.UserType == TagPortal.Domain.Enum.EnumUserType.SupplierUser)
            {
                RedirectToDefault();
            }
        }
    }
}