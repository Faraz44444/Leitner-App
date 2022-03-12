//using System;
//using TagPortal.Core.Repository;
////using TagPortal.Domain.Model.Log;

//namespace TagPortal.Core.Service.Log
//{
//    public class EventLogService : BaseService
//    {
//        public EventLogService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory) : base(uowProvider, repoFactory)
//        {
//        }

//        public long Insert(string message, long userId = 0, string username = "", string userEmail = "", long clientId = 0)
//        {
//            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
//            {
//                return Insert(uow, DateTime.Now, message, userId, username, userEmail, clientId);
//            }
//        }

//        internal long Insert(IUnitOfWork uow, DateTime createdAt, string message,long userId = 0, string username = "", string userEmail = "", long clientId = 0)
//        {
//            var model = new EventLogModel()
//            {
//                CreatedAt = createdAt,
//                Message = message,
//                UserId = userId,
//                Username = username,
//                UserEmail = userEmail,
//                ClientId = clientId
//            };
//            var repo = RepoFactory.EventLogRepo(uow);
//            return repo.Insert(model);
//        }
//    }
//}
