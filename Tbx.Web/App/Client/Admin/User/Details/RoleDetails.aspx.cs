using System;
using System.Web.UI;

namespace TbxPortal.Web.App.Client.Admin.User.Details
{
    public partial class RoleDetails : TbxPortalBasePage
    {
        public string EntityName { get; set; }
        public string EntityIdString { get; set; }
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            EntityIdString = Page.RouteData.Values["id"]?.ToString();
            if (EntityIdString.ToLower() == "new")
            {
                EntityName = "new";
                return;
            }

            long.TryParse(EntityIdString, out long entityId);

            string redirectUrl = Page.ResolveUrl("~/admin/users");
            if (entityId < 1)
            {
                Redirect(redirectUrl);
                return;
            }


            //if (!CurrentUser.HasPermission(TagPortal.Domain.Enum.EnumPermissionType.Tbx_Admin_Users))
            //{
            //    RedirectToDefault();
            //}
        }
    }
}