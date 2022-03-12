using System;
using System.Web.UI;

namespace TbxPortal.Web.App.Client.Article.ArticleList.Details
{
    public partial class ArticleDetails : TbxPortalBasePage
    {
        public string EntityName { get; set; }
        public string EntityIdString { get; set; }
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            if (!CurrentUser.HasPermission(TagPortal.Domain.Enum.EnumPermissionType.Tbx_Article_List))
            {
                RedirectToDefault();
            }

            EntityIdString = Page.RouteData.Values["id"]?.ToString();
            if (EntityIdString.ToLower() == "new")
            {
                EntityName = "new";
                return;
            }

            long.TryParse(EntityIdString, out long entityId);

            string redirectUrl = Page.ResolveUrl("~/article/list");
            if (entityId < 1)
            {
                Redirect(redirectUrl);
                return;
            }

            //var entity = Services.ArticleService.GetById(entityId);
            //if (entity == null)
            //{
            //    Redirect(redirectUrl);
            //    return;
            //}

            //EntityName = entity.ArticleNo;


        }
    }
}