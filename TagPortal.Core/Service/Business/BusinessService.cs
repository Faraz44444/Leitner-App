
using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Business;
using TagPortal.Core.Service.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.Business;

namespace TagPortal.Core.Service.Business
{
    public class BusinessService : BaseService
    {
        private UserService UserService { get; }

        public BusinessService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory, UserService userService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
        }

        public PagedModel<BusinessModel> GetPagedList(BusinessRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.BusinessRepo(uow);
                return repo.GetPagedList(request);
            }
        }
        public List<BusinessModel> GetList(BusinessRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.BusinessRepo(uow);
                return repo.GetList(request);
            }
        }
        public BusinessModel GetById(long id)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetById(uow, id);
            }
        }
        public int GetByName(string name)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetByName(uow, name);
            }
        }

        internal int GetByName(IUnitOfWork uow, string name)
        {
            var repo = RepoFactory.BusinessRepo(uow);

            return repo.GetByName(name);
        }
        internal BusinessModel GetById(IUnitOfWork uow, long id)
        {
            var repo = RepoFactory.BusinessRepo(uow);

            var item = repo.GetById(id);

            return item;
        }

        internal void Update(IUnitOfWork uow, BusinessModel model)
        {
            ValidateModel(model);
            var repo = RepoFactory.BusinessRepo(uow);
            if (!repo.Update(model))
                throw new ArgumentException("Failed to update the Order");
        }

        public long Insert(BusinessModel model)
        {
            ValidateModel(model);

            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                uow.Commit();
                return Insert(uow, model);
            }

        }
        internal long Insert(IUnitOfWork uow, BusinessModel model)
        {
            ValidateModel(model);
            var repo = RepoFactory.BusinessRepo(uow);
            model.BusinessId = repo.Insert(model);
            uow.Commit();
            return model.BusinessId;

        }

        public void Delete(BusinessModel model, long deletedByUserId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.BusinessRepo(uow);
                if (!repo.DeleteById(model.BusinessId))
                    throw new ArgumentException("Failed to delete the Order");

                uow.Commit();
            }
        }

        public void ValidateModel(BusinessModel model)
        {
            if (string.IsNullOrWhiteSpace(model.BusinessName)) throw new ArgumentException("BusinessName is not set", "BusinessName");
            if (model.CreatedByUserId < 1) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (model.CreatedAt < DateTime.MinValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
        }
    }
}
