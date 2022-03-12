using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagPortal.Core.Repository;
using TagPortal.Core.Repository.Payment.PaymentPriority;
using TagPortal.Core.Request.Payment.PaymentPriority;
using TagPortal.Core.Service.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.PaymentPriority;

namespace TagPortal.Core.Service.Payment.PaymentPriority
{
    public class PaymentPriorityService : BaseService
    {
        private UserService UserService { get; }

        public PaymentPriorityService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory, UserService userService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
        }

        public PagedModel<PaymentPriorityModel> GetPagedList(PaymentPriorityRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentPriorityRepo(uow);
                return repo.GetPagedList(request);
            }
        }
        public List<PaymentPriorityModel> GetList(PaymentPriorityRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentPriorityRepo(uow);
                return repo.GetList(request);
            }
        }
        public PaymentPriorityModel GetById(long id)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetById(uow, id);
            }
        }

        internal PaymentPriorityModel GetById(IUnitOfWork uow, long id)
        {
            var repo = RepoFactory.PaymentPriorityRepo(uow);

            var item = repo.GetById(id);

            return item;
        }

        internal void Update(IUnitOfWork uow, PaymentPriorityModel model)
        {
            ValidateModel(model);
            var repo = RepoFactory.PaymentPriorityRepo(uow);
            if (!repo.Update(model))
                throw new ArgumentException("Failed to update the Order");
        }

        public long Insert(PaymentPriorityRequest dto)
        {
            var model = new PaymentPriorityModel()
            {
                PaymentPriorityName = dto.PaymentPriorityName,
                CreatedByUserId = dto.CreatedByUserId,
                CreatedAt = dto.CreatedAt
            };

            ValidateModel(model);

           using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
           {
               var repo = RepoFactory.PaymentPriorityRepo(uow);
               model.PaymentPriorityId = repo.Insert(model);

               if (model.PaymentPriorityId < 1)
                   throw new ArgumentException("Failed to insert the Payment");
                uow.Commit();
               return model.PaymentPriorityId;
           }

        }

        public void Delete(PaymentPriorityModel model, long deletedByUserId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.PaymentPriorityRepo(uow);
                if (!repo.DeleteById(model.PaymentPriorityId))
                    throw new ArgumentException("Failed to delete the Order");

                uow.Commit();
            }
        }

        public void ValidateModel(PaymentPriorityModel model)
        {
            if (string.IsNullOrWhiteSpace(model.PaymentPriorityName)) throw new ArgumentException("PaymentPriorityName is not set", "PaymentPriorityName");
            if (model.CreatedByUserId < 1) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (model.CreatedAt < DateTime.MinValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
        }
    }
}
