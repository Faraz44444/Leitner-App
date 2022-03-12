using System;

namespace TbxPortal.Web.App.Client.Dashboard
{
    public partial class Dashboard : TbxPortalBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (CurrentUser.UserType == TagPortal.Domain.Enum.EnumUserType.SupplierUser)
            {
                Redirect("~/company/Dashboard");
            }
        }
    }
}