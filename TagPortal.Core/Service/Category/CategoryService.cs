using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Category;
using TagPortal.Core.Service.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.Category;

namespace TagPortal.Core.Service.Category
{
    public class CategoryService : BaseService
    {
        private UserService UserService { get; }
        public CategoryService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory, UserService userService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
        }
        public PagedModel<CategoryModel> GetPagedList(CategoryRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.CategoryRepo(uow);
                return repo.GetPagedList(request);
            }
        }
        public List<CategoryModel> GetList(CategoryRequest request)
        {
            if (request == null) request = new CategoryRequest();
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.CategoryRepo(uow);
                return repo.GetList(request);
            }
        }
        public CategoryModel GetById(CategoryRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.CategoryRepo(uow);
                return repo.GetById(request);
            }
        }
        public bool Update(CategoryRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return Update(uow, request);
            }
        }


        internal bool Update(IUnitOfWork uow, CategoryRequest request)
        {
            var repo = RepoFactory.CategoryRepo(uow);
            var model = repo.GetById(request);
            model.CategoryName = request.CategoryName;
            model.CategoryPriority = request.CategoryPriority;
            model.WeeklyLimit = request.WeeklyLimit;
            model.MonthlyLimit = request.MonthlyLimit;
            ValidateModel(model);
            if (!repo.Update(model))
                throw new ArgumentException("Failed to update the Order");
            return true;
        }

        public long Insert(CategoryRequest dto)
        {
            var model = new CategoryModel()
            {
                CategoryName = dto.CategoryName,
                CategoryPriority = dto.CategoryPriority,
                WeeklyLimit = dto.WeeklyLimit,
                MonthlyLimit = dto.MonthlyLimit,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt
            };
            ValidateModel(model);

            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.CategoryRepo(uow);
                model.CategoryId = repo.Insert(model);

                if (model.CategoryId < 1)
                    throw new ArgumentException("Failed to insert the Payment");

                return model.CategoryId;
            }
        }

        public void Delete(CategoryModel model, long deletedByUserId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.CategoryRepo(uow);
                if (!repo.DeleteById(model.CategoryId))
                    throw new ArgumentException("Failed to delete the Order");

                uow.Commit();
            }
        }

        public void ValidateModel(CategoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CategoryName)) throw new ArgumentException("CategoryName is not set", "CategoryName");
            //if (model.CategoryPriority == null) throw new ArgumentException("CategoryPriority is not set", "CategoryPriority");
            if (model.CreatedAt < DateTime.MinValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
            if (model.CreatedByUserId < 1) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
        }
    }
}
