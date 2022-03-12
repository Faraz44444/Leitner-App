using System;
using System.Collections.Generic;
using System.Linq;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.User;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Service.User
{
    public class UserSiteService : BaseService
    {
        public UserSiteService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory) : base(uowProvider, repoFactory)
        {
        }
        public List<UserSiteModel> GetList(UserSiteRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserSiteRepo(uow);
                return repo.GetList(request);
            }
        }

        internal bool Insert(IUnitOfWork uow, long clientId, long roleId, long siteId, long userId)
        {
            var model = new UserSiteModel()
            {
                ClientId = clientId,
                RoleId = roleId,
                SiteId = siteId,
                UserId = userId
            };

            Validate(model);

            var repo = RepoFactory.UserSiteRepo(uow);
            return repo.Insert(model);
        }

        public bool Update(long clientId, long userId, List<UserSiteModel> model)
        {
            if (userId < 1) throw new ArgumentException("UserId is not set");
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                DeleteByUserId(uow, userId);
                foreach (var m in model.Where(x => x.RoleId > 0).ToList())
                {
                    Insert(uow, clientId, m.RoleId, m.SiteId, userId);
                }
                uow.Commit();
            }
            return true;
        }

        internal bool DeleteByUserId(IUnitOfWork uow, long userId)
        {
            var repo = RepoFactory.UserSiteRepo(uow);
            return repo.DeleteByUserId(userId);
        }

        public void Validate(UserSiteModel model)
        {
            if (model.ClientId < 1) throw new ArgumentException("ClientId is not set");
            if (model.RoleId < 1) throw new ArgumentException("RoleId is not set");
            if (model.SiteId < 1) throw new ArgumentException("SiteId is not set");
            if (model.UserId < 1) throw new ArgumentException("UserId is not set");
        }
    }
}
