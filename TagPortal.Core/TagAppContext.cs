using System;
using TagPortal.Core.Repository;
using TagPortal.Core.Service;
using TagPortal.Core.Service.Category;
using TagPortal.Core.Service.Business;
using TagPortal.Core.Service.Payment;
using TagPortal.Core.Service.Payment.PaymentPriority;
//using TagPortal.Core.Service.Log;
//using TagPortal.Core.Service.Mail;
using TagPortal.Core.Service.Role;
using TagPortal.Core.Service.Security;
using TagPortal.Core.Service.User;
using TagPortal.Core.Service.Payment.PaymentTotal;
using TagPortal.Core.Service.Report;

namespace TagPortal.Core
{
    public class TagAppContext
    {
        public static TagAppContext Current;
        public readonly ServiceContext Services;
        public TagAppContext(ServiceContext serviceContext)
        {
            if (Current != null)
                throw new InvalidOperationException("TagItAppContext is already initialized");
            Services = serviceContext;
        }

        public static ServiceContext GetServiceContext(string sqlConnectionString)
        {
            UnitOfWorkProvider.SqlConnectionString = sqlConnectionString;
            var uowProvider = new UnitOfWorkProvider();
            var repoFactory = new RepositoryFactory();

            //var errorLogService = new ErrorLogService(uowProvider, repoFactory);
            //var eventLogService = new EventLogService(uowProvider, repoFactory);
            //var mailService = new MailService(uowProvider, repoFactory, errorLogService);



            var userService = new UserService(uowProvider, repoFactory);
            var rolePermissionService = new RolePermissionService(uowProvider, repoFactory);
            var roleService = new RoleService(uowProvider, repoFactory, rolePermissionService);


       

            var userSiteAccessService = new UserSiteAccessService(uowProvider, repoFactory, userService, rolePermissionService);

            var userSiteService = new UserSiteService(uowProvider, repoFactory);

            var securityService = new SecurityService(uowProvider, repoFactory, userService, userSiteAccessService);

            var businessService = new BusinessService(uowProvider, repoFactory, userService);
            var paymentService = new PaymentService(uowProvider, repoFactory, userService, businessService);
            var paymentTotalService = new PaymentTotalService(uowProvider, repoFactory, userService, businessService);
            var paymentPriorityService = new PaymentPriorityService(uowProvider, repoFactory, userService);
            var categoryService = new CategoryService(uowProvider, repoFactory, userService);
            var reportService = new ReportService(uowProvider, repoFactory, userService);

            return new ServiceContext(
                //errorLogService: errorLogService,
                //eventLogService: eventLogService,
                //mailService: mailService,
                securityService: securityService,
                userService: userService,
                paymentService: paymentService,
                paymentTotalService: paymentTotalService,
                paymentPriorityService: paymentPriorityService,
                categoryService: categoryService,
                businessService: businessService,
                rolePermissionService: rolePermissionService,
                roleService: roleService,
                userSiteAccessService: userSiteAccessService,
                userSiteService: userSiteService,
                reportService: reportService
                );
        }
    }
}
