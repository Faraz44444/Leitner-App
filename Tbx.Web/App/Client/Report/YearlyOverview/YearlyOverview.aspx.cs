using System;

namespace TbxPortal.Web.App.Client.Report.YearlyOverview
{
    public partial class YearlyOverview: TbxPortalBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

             if (!CurrentUser.HasPermission(TagPortal.Domain.Enum.EnumPermissionType.Tbx_Article_List))
            {
                RedirectToDefault();
            }
        }
    }
}