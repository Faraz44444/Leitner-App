//using System;
//using TagPortal.Core.Repository;
////using TagPortal.Domain.Model.Log;

//namespace TagPortal.Core.Service.Log
//{
//    public class ErrorLogService : BaseService
//    {
//        public ErrorLogService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory) : base(uowProvider, repoFactory)
//        {
//        }

//        public long Insert(Exception exception, long userId = 0, string username = "", string userEmail = "")
//        {
//            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
//            {
//                return Insert(uow, exception, userId, username, userEmail);
//            }
//        }

//        public long Insert(string message, long userId = 0, string username = "", string userEmail = "", long clientId = 0)
//        {
//            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
//            {
//                return Insert(uow, DateTime.Now, message, string.Empty, string.Empty, string.Empty, userId, username, userEmail, clientId);
//            }
//        }

//        internal long Insert(IUnitOfWork uow, Exception exception, long userId = 0, string username = "", string userEmail = "", long clientId = 0)
//        {
//            DateTime createdAt = DateTime.Now;
//            string message = exception.Message;
//            string stackTrace = exception.StackTrace;
//            string innerException = "";
//            string method = "";
//            if (exception.InnerException != null)
//                innerException = exception.InnerException.ToString();
//            if (exception.TargetSite != null)
//                method = exception.TargetSite.Name;

//            return Insert(uow, createdAt, message, stackTrace, innerException, method, userId, username, userEmail, clientId);
//        }

//        internal long Insert(IUnitOfWork uow, DateTime createdAt, string message, string stacktrace, string innerException, string method,
//                            long userId = 0, string username = "", string userEmail = "", long clientId = 0)
//        {
//            var model = new ErrorLogModel()
//            {
//                CreatedAt = createdAt,
//                Message = message,
//                StackTrace = stacktrace,
//                InnerException = innerException,
//                Method = method,
//                UserId = userId,
//                Username = username,
//                UserEmail = userEmail,
//                ClientId = clientId
//            };
//            var repo = RepoFactory.ErrorLogRepo(uow);
//            return repo.Insert(model);
//        }
//    }
//}
