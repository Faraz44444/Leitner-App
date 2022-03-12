using TagPortal.Core.Repository.Business;
using TagPortal.Core.Repository.Category;
//using TagPortal.Core.Repository.Log;
using TagPortal.Core.Repository.Payment;
using TagPortal.Core.Repository.Payment.PaymentPriority;
using TagPortal.Core.Repository.Payment.PaymentTotal;
using TagPortal.Core.Repository.Role;
using TagPortal.Core.Repository.Security;
using TagPortal.Core.Repository.User;

namespace TagPortal.Core.Repository
{
    public class RepositoryFactory
    {
      
        //internal ErrorLogRepo ErrorLogRepo(IUnitOfWork uow) => new ErrorLogRepo(uow.IDbConnection, uow.IDbTransaction);
        //internal EventLogRepo EventLogRepo(IUnitOfWork uow) => new EventLogRepo(uow.IDbConnection, uow.IDbTransaction);
        internal RolePermissionRepo RolePermissionRepo(IUnitOfWork uow) => new RolePermissionRepo(uow.IDbConnection, uow.IDbTransaction);
        internal RoleRepo RoleRepo(IUnitOfWork uow) => new RoleRepo(uow.IDbConnection, uow.IDbTransaction);
        internal SecurityRepo SecurityRepo(IUnitOfWork uow) => new SecurityRepo(uow.IDbConnection, uow.IDbTransaction);
        internal UserRepo UserRepo(IUnitOfWork uow) => new UserRepo(uow.IDbConnection, uow.IDbTransaction);
        internal UserSiteAccessRepo UserSiteAccessRepo(IUnitOfWork uow) => new UserSiteAccessRepo(uow.IDbConnection, uow.IDbTransaction);
        internal UserSiteRepo UserSiteRepo(IUnitOfWork uow) => new UserSiteRepo(uow.IDbConnection, uow.IDbTransaction);
        internal PaymentRepo PaymentRepo(IUnitOfWork uow) => new PaymentRepo(uow.IDbConnection, uow.IDbTransaction);
        internal PaymentTotalRepo PaymentTotalRepo(IUnitOfWork uow) => new PaymentTotalRepo(uow.IDbConnection, uow.IDbTransaction);
        internal PaymentPriorityRepo PaymentPriorityRepo(IUnitOfWork uow) => new PaymentPriorityRepo(uow.IDbConnection, uow.IDbTransaction);
        internal CategoryRepo CategoryRepo(IUnitOfWork uow) => new CategoryRepo(uow.IDbConnection, uow.IDbTransaction);
        internal BusinessRepo BusinessRepo(IUnitOfWork uow) => new BusinessRepo(uow.IDbConnection, uow.IDbTransaction);

    }
}
