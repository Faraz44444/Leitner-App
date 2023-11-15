using Core.Infrastructure.Mail;
using Core.Infrastructure.UnitOfWork;
using Core.Request.Business;
using Core.Request.Category;
using Core.Request.Client;
using Core.Request.Error;
using Core.Request.Event;
using Core.Request.Log;
using Core.Request.Payment;
using Core.Request.Role;
using Core.Request.User;
using Core.Service.Mail;
using Core.Service.Payment;
using Core.Service.Report;
using Core.Service.Security;
using Core.Table;
using Domain.Model.ActionLog;
using Domain.Model.Business;
using Domain.Model.Category;
using Domain.Model.Client;
using Domain.Model.Payment;
using Domain.Model.Role;
using Domain.Model.User;

namespace Core.Service
{
    public class ServiceContext
    {
        private TableContext Tables { get; set; } = new();

        public Service<UserRequest, UserModel> UserService { get; set; }
        public Service<ClientRequest, ClientModel> ClientService { get; set; }
        public Service<UserClientRequest, UserClientModel> UserClientService { get; set; }
        public Service<RoleRequest, RoleModel> RoleService { get; set; }
        public Service<RolePermissionRequest, RolePermissionModel> RolePermissionService { get; set; }
        public Service<ActionLogRequest, ActionLogModel> ActionLogService { get; set; }
        public Service<ErrorLogRequest, ErrorLogModel> ErrorLogService { get; set; }
        public Service<EventLogRequest, EventLogModel> EventLogService { get; set; }
        public Service<BusinessRequest, BusinessModel> BusinessService { get; set; }
        public Service<CategoryRequest, CategoryModel> CategoryService { get; set; }
        public Service<PaymentPriorityRequest, PaymentPriorityModel> PaymentPriorityService { get; set; }
        public Service<PaymentRequest, PaymentModel> PaymentService { get; set; }
        public Service<PaymentTotalRequest, PaymentTotalModel> PaymentTotalService { get; set; }
        public SecurityService SecurityService { get; set; }
        public MailService MailService { get; set; }

        public ReportService ReportService { get; set; }

        public ServiceContext(IUnitOfWorkProvider uowProvider, MailConfiguration mailConfiguration)
        {
            UserServiceInitializer(uowProvider);
            UserClientServiceInitializer(uowProvider);
            RoleServiceInitializer(uowProvider);
            ClientServiceInitializer(uowProvider);
            RolePermissionServiceInitializer(uowProvider);
            ActionLogServiceInitializer(uowProvider);
            ErrorLogServiceInitializer(uowProvider);
            EventLogServiceInitializer(uowProvider);
            BusinessServiceInitializer(uowProvider);
            CategoryServiceInitializer(uowProvider);
            PaymentPriorityInitializer(uowProvider);
            PaymentInitializer(uowProvider);
            PaymentTotalInitializer(uowProvider);

            MailService = new MailService(mailConfiguration);
            SecurityService = new SecurityService(UserService, MailService);
            ReportService = new ReportService(uowProvider);
        }
        public void UserServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            var model = new UserModel();
            var request = new UserRequest();
            UserService = new Service<UserRequest, UserModel>(uowProvider, Tables.UserTable, model, request);
        }
        public void UserClientServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            var model = new UserClientModel();
            var request = new UserClientRequest();
            UserClientService = new Service<UserClientRequest, UserClientModel>(uowProvider, Tables.UserClientTable, model, request);
        }
        public void ClientServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            var model = new ClientModel();
            var request = new ClientRequest();
            ClientService = new Service<ClientRequest, ClientModel>(uowProvider, Tables.ClientTable, model, request);
        }
        public void RoleServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            var model = new RoleModel();
            var request = new RoleRequest();
            RoleService = new Service<RoleRequest, RoleModel>(uowProvider, Tables.RoleTable, model, request);
        }
        public void RolePermissionServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            var model = new RolePermissionModel();
            var request = new RolePermissionRequest();
            RolePermissionService = new Service<RolePermissionRequest, RolePermissionModel>(uowProvider, Tables.RolePermissionTable, model, request);
        }
        public void ActionLogServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            var model = new ActionLogModel();
            var request = new ActionLogRequest();
            ActionLogService = new Service<ActionLogRequest, ActionLogModel>(uowProvider, Tables.ActionLogTable, model, request);
        }
        public void ErrorLogServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            ErrorLogService = new Service<ErrorLogRequest, ErrorLogModel>(uowProvider, Tables.ErrorLogTable,
                new ErrorLogModel(), new ErrorLogRequest());
        }
        public void EventLogServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            EventLogService = new Service<EventLogRequest, EventLogModel>(uowProvider, Tables.EventLogTable,
                new EventLogModel(), new EventLogRequest());
        }
        public void BusinessServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            BusinessService = new Service<BusinessRequest, BusinessModel>(uowProvider, Tables.BusinessTable,
                new BusinessModel(), new BusinessRequest());
        }
        public void CategoryServiceInitializer(IUnitOfWorkProvider uowProvider)
        {
            CategoryService = new Service<CategoryRequest, CategoryModel>(uowProvider, Tables.CategoryTable,
                new CategoryModel(), new CategoryRequest());
        }
        public void PaymentPriorityInitializer(IUnitOfWorkProvider uowProvider)
        {
            PaymentPriorityService = new Service<PaymentPriorityRequest, PaymentPriorityModel>(uowProvider, Tables.PaymentPriorityTable,
                new PaymentPriorityModel(), new PaymentPriorityRequest());
        }
        public void PaymentInitializer(IUnitOfWorkProvider uowProvider)
        {
            PaymentService = new PaymentService(uowProvider, Tables.PaymentTable);
        }
        public void PaymentTotalInitializer(IUnitOfWorkProvider uowProvider)
        {
            PaymentTotalService = new PaymentTotalService(uowProvider, Tables.PaymentTotalTable);
        }
    }
}
