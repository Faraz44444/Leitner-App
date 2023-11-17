using Core.Infrastructure.Mail;
using Core.Infrastructure.UnitOfWork;
using Core.Request.Category;
using Core.Request.Error;
using Core.Request.Event;
using Core.Request.Log;
using Core.Request.Payment;
using Core.Request.User;
using Core.Service.Mail;
using Core.Service.Security;
using Core.Table;
using Domain.Model.ActionLog;
using Domain.Model.Category;
using Domain.Model.Payment;
using Domain.Model.User;

namespace Core.Service
{
    public class ServiceContext
    {
        private TableContext Tables { get; set; } = new();

        public Service<UserRequest, UserModel> UserService { get; set; }
        public Service<ActionLogRequest, ActionLogModel> ActionLogService { get; set; }
        public Service<ErrorLogRequest, ErrorLogModel> ErrorLogService { get; set; }
        public Service<EventLogRequest, EventLogModel> EventLogService { get; set; }
        public Service<CategoryRequest, CategoryModel> CategoryService { get; set; }
        public Service<MaterialRequest, MaterialModel> MaterialService { get; set; }
        public SecurityService SecurityService { get; set; }
        public MailService MailService { get; set; }


        public ServiceContext(IUnitOfWorkProvider uowProvider, MailConfiguration mailConfiguration)
        {
            UserService = new Service<UserRequest, UserModel>(uowProvider,
                                                              Tables.UserTable,
                                                              new UserModel(),
                                                              new UserRequest());
            ActionLogService = new Service<ActionLogRequest, ActionLogModel>(uowProvider,
                                                                             Tables.ActionLogTable,
                                                                             new ActionLogModel(),
                                                                             new ActionLogRequest());
            ErrorLogService = new Service<ErrorLogRequest, ErrorLogModel>(uowProvider,
                                                                          Tables.ErrorLogTable,
                                                                          new ErrorLogModel(),
                                                                          new ErrorLogRequest());
            EventLogService = new Service<EventLogRequest, EventLogModel>(uowProvider,
                                                                          Tables.EventLogTable,
                                                                          new EventLogModel(),
                                                                          new EventLogRequest());
            CategoryService = new Service<CategoryRequest, CategoryModel>(uowProvider,
                                                                          Tables.CategoryTable,
                                                                          new CategoryModel(),
                                                                          new CategoryRequest());
            MaterialService = new Service<MaterialRequest, MaterialModel>(uowProvider,
                                                                          Tables.MaterialTable,
                                                                          new MaterialModel(),
                                                                          new MaterialRequest());

            MailService = new MailService(mailConfiguration);
            SecurityService = new SecurityService(UserService, MailService);
        }
    }
}
