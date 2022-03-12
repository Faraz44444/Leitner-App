using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using TagPortal.Core;
using TagPortal.Core.Security;
using TagPortal.Domain.Enum;

namespace TbxPortal.Web
{
    public partial class TbxPortal : System.Web.UI.MasterPage
    {
        public TagIdentity CurrentUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            CurrentUser = (TagIdentity)HttpContext.Current.User.Identity;

            SetTopNavigationData();
            SetActiveMenu();
            if (CurrentUser.UserType == EnumUserType.SystemUser)
            {
                SetAvailableMenusForClient();
            }

            username.InnerText = CurrentUser.Username;
        }

        private void SetTopNavigationData()
        {
            if (CurrentUser.CssFontSize != 12)
            {
                applicationBody.Attributes.CssStyle.Add("font-size", CurrentUser.CssFontSize.ToString(System.Globalization.CultureInfo.InvariantCulture) + "px");
                applicationHtml.Attributes.CssStyle.Add("font-size", CurrentUser.CssFontSize.ToString(System.Globalization.CultureInfo.InvariantCulture) + "px");
            }

        }

        protected void BtnLogout_Click(object sender, EventArgs e)
        {
            SignOut();
        }

        private void SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie1);

            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "")
            {
                Expires = DateTime.Now.AddYears(-1)
            };
            Response.Cookies.Add(cookie2);

            FormsAuthentication.RedirectToLoginPage();
        }

        private void SetActiveMenu()
        {
            var url = Request.RawUrl.ToLower();
            if (url.Contains("signout"))
            {
                SignOut();
                return;
            }

            bool pageIsForSuppliers = url.Split('/').First(x => !string.IsNullOrWhiteSpace(x)) == "company";

            if (!pageIsForSuppliers)
            {
                if (url.Contains("/dashboard"))
                {
                    NavDashboard.Attributes.Add("class", "active");
                    return;
                }

                if (url.Contains("/payment/"))
                {
                    NavDataManagement.Attributes.Add("class", "opened subMenuActive");
                    if (url.Contains("list"))
                    {
                        NavPayment.Attributes.Add("class", "active");
                        return;
                    }
                   
                    return;
                }
                if (url.Contains("/ptotal/"))
                {
                    NavDataManagement.Attributes.Add("class", "opened subMenuActive");
                    if (url.Contains("list"))
                    {
                        NavPaymentTotal.Attributes.Add("class", "active");
                        return;
                    }
                   
                    return;
                }
                if (url.Contains("/priority/"))
                {
                    NavDataManagement.Attributes.Add("class", "opened subMenuActive");
                    if (url.Contains("list"))
                    {
                        NavPaymentPriority.Attributes.Add("class", "active");
                        return;
                    }
                   
                    return;
                }
                if (url.Contains("/category/"))
                {
                    NavDataManagement.Attributes.Add("class", "opened subMenuActive");
                    if (url.Contains("list"))
                    {
                        NavCategory.Attributes.Add("class", "active");
                        return;
                    }
                   
                    return;
                }
                if (url.Contains("/business/"))
                {
                    NavDataManagement.Attributes.Add("class", "opened subMenuActive");
                    if (url.Contains("list"))
                    {
                        NavBusiness.Attributes.Add("class", "active");
                        return;
                    }
                   
                    return;
                }


                if (url.Contains("/admin/"))
                {
                    if (url.Contains("users"))
                    {
                        NavAdmin_Users.Attributes.Add("class", "active");
                        return;
                    }
                    return;
                }
            }
            else
            {
                if (url.Contains("/dashboard"))
                {
                    NavSupplierDashboard.Attributes.Add("class", "active");
                    return;
                }
            }
        }

        private void SetAvailableMenusForClient()
        {

            NavDashboard.Visible = true;



            //if (CurrentUser.HasPermission(EnumPermissionType.Tbx_Article_List))
            //{
                NavDataManagement.Visible = true;
                NavPayment.Visible = true;
            //}

            //if (CurrentUser.HasPermission(EnumPermissionType.Tbx_Article_List))
            //{
                NavDataManagement.Visible = true;
                NavPaymentTotal.Visible = true;
            //}

            //if (CurrentUser.HasPermission(EnumPermissionType.Tbx_Article_List))
            //{
                NavDataManagement.Visible = true;
                NavPaymentPriority.Visible = true;
            //}

            //if (CurrentUser.HasPermission(EnumPermissionType.Tbx_Category_List))
            //{
                NavDataManagement.Visible = true;
                NavCategory.Visible = true;
            //}

            //if (CurrentUser.HasPermission(EnumPermissionType.Tbx_Category_List))
            //{
                NavDataManagement.Visible = true;
                NavBusiness.Visible = true;
            //}

            //if (CurrentUser.HasPermission(EnumPermissionType.Tbx_Admin_Users))
            //{
                NavAdmin.Visible = true;
                NavAdmin_Users.Visible = true;
            //}

        }

        public void SetClientNotifications()
        {

        }

        public void SetSupplierNotifications()
        {
            
            
        }

        private string GetBadgeHtml(int notificationCount, string elementId = "")
        {
            return string.Format("<span {0} class='badge badge-danger ml-1 p-1'>{1}</span>",
                string.IsNullOrWhiteSpace(elementId) ? "" : "id='" + elementId + "'",
                notificationCount >= 99 ? "99+" : notificationCount.ToString());
        }

        private void SetWiseUser()
        {

            SwitchUserType.Visible = true;
            NavWiseAdmin.Visible = true;

            if (CurrentUser.UserType == EnumUserType.ClientUser)
            {
                switchToSupplier.Visible = true;
               
               

              
            }
     
             

                SetSupplierNotifications();

                {
                }

               
            }

         
        }
    }
