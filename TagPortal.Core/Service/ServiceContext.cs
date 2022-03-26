using TagPortal.Core.Service.Business;
using TagPortal.Core.Service.Category;
//using TagPortal.Core.Service.Log;
//using TagPortal.Core.Service.Mail;
using TagPortal.Core.Service.Payment;
using TagPortal.Core.Service.Payment.PaymentPriority;
using TagPortal.Core.Service.Payment.PaymentTotal;
using TagPortal.Core.Service.Report;
using TagPortal.Core.Service.Role;
using TagPortal.Core.Service.Security;
using TagPortal.Core.Service.User;

namespace TagPortal.Core.Service
{
    public class ServiceContext
    {
        //public ErrorLogService ErrorLogService { get; private set; }
        //public EventLogService EventLogService { get; private set; }
        //public MailService MailService { get; private set; }
        public SecurityService SecurityService { get; private set; }
        public RoleService RoleService { get; private set; }
        public RolePermissionService RolePermissionService { get; private set; }
        public UserService UserService { get; private set; }
        public PaymentService PaymentService { get; private set; }
        public PaymentTotalService PaymentTotalService { get; private set; }
        public PaymentPriorityService PaymentPriorityService { get; private set; }
        public CategoryService CategoryService { get; private set; }
        public BusinessService BusinessService { get; private set; }
        public UserSiteAccessService UserSiteAccessService { get; private set; }
        public UserSiteService UserSiteService { get; private set; }
        public ReportService ReportService { get; private set; }
        public ServiceContext(
                //ErrorLogService errorLogService,
                //EventLogService eventLogService,
                //MailService mailService,
                SecurityService securityService,
                UserService userService,
                PaymentService paymentService,
                PaymentTotalService paymentTotalService,
                PaymentPriorityService paymentPriorityService,
                CategoryService categoryService,
                BusinessService businessService,
                RolePermissionService rolePermissionService,
                RoleService roleService,
                UserSiteAccessService userSiteAccessService,
                UserSiteService userSiteService,
                ReportService reportService)
        {
            //ErrorLogService = errorLogService;
            //EventLogService = eventLogService;
            //MailService = mailService;
            SecurityService = securityService;
            UserService = userService;
            PaymentService = paymentService;
            PaymentTotalService = paymentTotalService;
            PaymentPriorityService = paymentPriorityService;
            CategoryService = categoryService;
            BusinessService = businessService;
            RolePermissionService = rolePermissionService;
            RoleService = roleService;
            UserSiteAccessService = userSiteAccessService;
            UserSiteService = userSiteService;
            ReportService = reportService;
        }
    }
}
