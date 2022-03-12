using System;

namespace TbxPortal.Web.App.Client.PaymentPriority.PaymentPriorityList.List
{
    public partial class PaymentPriorityList : TbxPortalBasePage
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);

            //if (!currentuser.haspermission(tagportal.domain.enum.enumpermissiontype.tbx_article_list))
            //{
            //    redirecttodefault();
            //}
        }
    }
}