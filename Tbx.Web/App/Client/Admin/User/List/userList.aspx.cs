using System;

namespace TbxPortal.Web.App.Client.Admin.User.List
{
    public partial class UserList : TbxPortalBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            //if (!CurrentUser.HasPermission(TagPortal.Domain.Enum.EnumPermissionType.Tbx_Admin_Users))
            //{
            //    RedirectToDefault();
            //}
        }
    }
}