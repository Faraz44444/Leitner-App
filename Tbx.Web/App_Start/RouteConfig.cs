using Microsoft.AspNet.FriendlyUrls;
using System.Web.Routing;

namespace TbxPortal.Web.App_Start
{
    public static class RouteConfig
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            };
            routes.EnableFriendlyUrls(settings);
            routes.RouteExistingFiles = true;

            //SIGN OUT
            routes.MapPageRoute("sign_out", "signout", "~/App/Client/Dashboard/Dashboard.aspx", true);
            routes.MapPageRoute("reset_password", "resetpassword", "~/ResetPassword.aspx", true);

            //ACCOUNT
            routes.MapPageRoute("account", "account", "~/App/Account/Account.aspx", true);
            routes.MapPageRoute("account_change_password", "account/changepassword", "~/App/Account/ChangePassword.aspx", true);

            routes.MapPageRoute("account_settings_notifications", "account/settings/notifications", "~/App/Account/Client/Settings/Notifications/NotificationDetails.aspx", true);
            //***********************CLIENT***********************
            //DASHBOARD
            routes.MapPageRoute("dashboard", "dashboard", "~/App/Client/Dashboard/Dashboard.aspx", true);

            //PAYMENT
            routes.MapPageRoute("payment_list", "payment/list", "~/App/Client/Payment/PaymentList/List/PaymentList.aspx", true);
            routes.MapPageRoute("payment_details", "payment/list/{id}", "~/App/Client/Payment/PaymentList/Details/PaymentDetails.aspx", true);
            
            //PAYMENT TOTAL
            routes.MapPageRoute("paymenttotal_list", "ptotal/list", "~/App/Client/PaymentTotal/PaymentTotalList.aspx", true);

            //PAYMENT PRIORITY
            routes.MapPageRoute("payment_priority_list", "priority/list", "~/App/Client/PaymentPriority/PaymentPriorityList/List/PaymentPriorityList.aspx", true);

            //CATEGORY
            routes.MapPageRoute("category_list", "category/list", "~/App/Client/Category/CategoryList/List/CategoryList.aspx", true);
            routes.MapPageRoute("category_details", "category/list/{id}", "~/App/Client/Category/CategoryList/Details/CategoryDetails.aspx", true);
            
            //BUSINESS
            routes.MapPageRoute("business_list", "business/list", "~/App/Client/Business/BusinessList/List/BusinessList.aspx", true);
            routes.MapPageRoute("business_details", "business/list/{id}", "~/App/Client/Business/BusinessList/Details/BusinessDetails.aspx", true);
            
            //REPORT
            routes.MapPageRoute("report_yearlyoverview", "report/yearlyoverview", "~/App/Client/Report/YearlyOverview/YearlyOverview.aspx", true);

            //ADMIN
            routes.MapPageRoute("admin_user", "admin/users", "~/App/Client/Admin/User/List/UserList.aspx", true);
            routes.MapPageRoute("admin_user_details", "admin/users/{id}", "~/App/Client/Admin/User/Details/UserDetails.aspx", true);
            routes.MapPageRoute("admin_role_details", "admin/users/roles/{id}", "~/App/Client/Admin/User/Details/RoleDetails.aspx", true);


            //***********************OTHER************************

            //USER MANUAL
            routes.MapPageRoute("user_manual", "usermanual", "~/App/UserManual/UserManualContent.aspx", true);

            //NEWS
            routes.MapPageRoute("news", "news", "~/App/News/NewsContent.aspx", true);

            //**********************WISE ADMIN********************
            //USER MANUAL
            routes.MapPageRoute("user_manual_edit", "wiseadmin/usermanual/edit", "~/App/UserManual/Edit/UserManualContentEdit.aspx", true);
            //NEWS
            routes.MapPageRoute("news_edit", "wiseadmin/news/edit", "~/App/News/Edit/NewsContentEdit.aspx", true);

        }
    }
}