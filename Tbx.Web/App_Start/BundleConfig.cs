using System.Web.Optimization;

namespace TbxPortal.Web.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //**LOGIN**
            bundles.Add(new ScriptBundle("~/jsLogin").Include(
                "~/Scripts/jquery-3.0.0.js",
                "~/Scripts/umd/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/app/login.js"));

            //**RESET PASSWORD**
            bundles.Add(new ScriptBundle("~/jsResetPassword").Include(
                "~/Scripts/jquery-3.0.0.js",
                "~/Scripts/umd/popper.js",
                "~/Scripts/bootstrap.js"));

            //**MAIN**
            bundles.Add(new ScriptBundle("~/jsSite").Include(
                    "~/Scripts/jquery-3.0.0.min.js",
                    "~/Scripts/umd/popper.js",
                    "~/Scripts/bootstrap.js",
                    "~/Scripts/moment.min.js",
                    "~/Scripts/bootstrap-datetimepicker.js",
                    "~/App/_common/apiservice.js",
                    "~/App/_common/currentuser.js",
                    "~/App/_common/datamanipulation.js",
                    "~/App/_common/feedback.js",
                    "~/App/_common/formvalidation.js",
                    "~/App/_common/pagination.js",
                    "~/App/_common/loadhandler.js",
                    "~/App/_common/lookup.js",
                    "~/App/_common/localStorage.js",
                    "~/App/_common/orderByHandler.js",
                    "~/App/_common/eventHandler.js",
                    "~/App/_common/colors.js",
                    "~/App/_common/clipboard.js",
                    "~/Scripts/app/site.js"));

            bundles.Add(new ScriptBundle("~/jsChartJs293").Include(
                "~/Scripts/Chart.min.js",
                "~/Scripts/Gauge.js"));
            bundles.Add(new ScriptBundle("~/jsChartJs310").Include(
                "~/Scripts/Chart_3.1.0.min.js"));
            bundles.Add(new ScriptBundle("~/jsGaugeRecognizer").Include(
                "~/Scripts/gauge_recognizer.min.js"));

            bundles.Add(new ScriptBundle("~/jsQuill").Include(
               "~/Scripts/quill.min.js",
               "~/Scripts/quill.image-resize.min.js"));
            bundles.Add(new StyleBundle("~/cssQuill").Include(
              "~/Content/quill/quill.bubble.css",
              "~/Content/quill/quill.core.css",
              "~/Content/quill/quill.snow.css"));

            bundles.Add(new ScriptBundle("~/jsjqueryUi").Include(
                   "~/Scripts/jquery-ui.min.js"));
            bundles.Add(new StyleBundle("~/cssjqueryUi").Include(
                   "~/Content/jqueryUi/jquery-ui.min.css"));

            bundles.Add(new ScriptBundle("~/jsNoUiSlider").Include(
                   "~/Scripts/nouislider.js"));
            bundles.Add(new StyleBundle("~/cssNoUiSlider").Include(
                   "~/Content/noUiSlider/nouislider.css"));

#if DEBUG
            bundles.Add(new ScriptBundle("~/jsVue").Include(
                "~/Scripts/vue.js"));
#else
            bundles.Add(new ScriptBundle("~/jsVue").Include(
                "~/Scripts/vue.min.js"));

#endif

            bundles.Add(new ScriptBundle("~/jsVueComponents").Include(
                 "~/App/_common/vueComponents/table/sortableColumn.js",
                 "~/App/_common/vueComponents/datepicker/datepicker.js",
                 "~/App/_common/vueComponents/base/hyperLink.js",
                 "~/App/_common/vueComponents/dragAndDrop/DragAndDrop.js",
                 "~/App/_common/vueComponents/base/contentSection.js",
                 "~/App/_common/vueComponents/base/customInputGroup.js",
                 "~/App/_common/vueComponents/datepicker/datepicker.js"));

            bundles.Add(new ScriptBundle("~/jsVueFilters").Include(
                 "~/App/_common/vueFilters/momentFilter.js",
                  "~/App/_common/vueFilters/fileSize.js",
                 "~/App/_common/vueFilters/textLengthFilter.js",
                 "~/App/_common/vueFilters/decimalFilter.js"));

            //ACCOUNT
            bundles.Add(new ScriptBundle("~/jsAccount").Include(
                "~/App/Account/account.js"));
            bundles.Add(new ScriptBundle("~/jsChangePassword").Include(
                "~/App/Account/changePassword.js"));

            bundles.Add(new ScriptBundle("~/jsDashboard").Include(
             "~/App/Client/Dashboard/dashboard.js"));

            bundles.Add(new ScriptBundle("~/jsNotificationDetails").Include(
               "~/App/Account/Client/Settings/Notifications/notificationDetails.js"));

            //CATEGORY
            bundles.Add(new ScriptBundle("~/jsCategoryList").Include(
                "~/App/Client/Category/CategoryList/List/categoryList.js"));
            bundles.Add(new ScriptBundle("~/jsCategoryDetails").Include(
                "~/App/Client/Category/CategoryDetails/Details/categoryDetails.js"));

            //PAYMENT
            bundles.Add(new ScriptBundle("~/jsPaymentList").Include(
                "~/App/Client/Payment/PaymentList/List/paymentList.js"));
            bundles.Add(new ScriptBundle("~/jsPaymentDetails").Include(
                "~/App/Client/Payment/PaymentList/Details/paymentDetails.js"));

            //PAYMENT TOTAL
            bundles.Add(new ScriptBundle("~/jsPaymentTotalList").Include(
                "~/App/Client/PaymentTotal/PaymentTotalList.js"));

            //PAYMENT PRIORITY
            bundles.Add(new ScriptBundle("~/jsPaymentPriorityList").Include(
                "~/App/Client/PaymentPriority/PaymentPriorityList/List/paymentPriorityList.js"));
            
            //BUSINESS
            bundles.Add(new ScriptBundle("~/jsBusinessList").Include(
                "~/App/Client/Business/BusinessList/List/BusinessList.js"));
            
            //REPORTS
            bundles.Add(new ScriptBundle("~/jsYearlyOverview").Include(
                "~/App/Client/Report/YearlyOverview/YearlyOverview.js"));
            bundles.Add(new ScriptBundle("~/jsMonthlyOverview").Include(
                "~/App/Client/Report/MonthlyOverview/MonthlyOverview.js"));




            //USERS
            bundles.Add(new ScriptBundle("~/jsUserList").Include(
                "~/App/Client/Admin/User/List/userList.js"));
            bundles.Add(new ScriptBundle("~/jsUserDetails").Include(
                "~/App/Client/Admin/User/Details/userDetails.js"));
            bundles.Add(new ScriptBundle("~/jsRoleDetails").Include(
                "~/App/Client/Admin/User/Details/roleDetails.js"));
        }
    }
}