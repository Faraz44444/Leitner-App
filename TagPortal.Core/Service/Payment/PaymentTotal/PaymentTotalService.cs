using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Payment.PaymentTotal;
using TagPortal.Core.Service.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.Payment.PaymentTotal;

namespace TagPortal.Core.Service.Payment.PaymentTotal
{
    public class PaymentTotalService : BaseService
    {
        private UserService UserService { get; }

        public PaymentTotalService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory, UserService userService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
        }

        public PagedModel<PaymentTotalModel> GetPagedList(PaymentTotalRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentTotalRepo(uow);
                return repo.GetPagedList(request);
            }
        }
        public List<PaymentTotalModel> GetList(PaymentTotalRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentTotalRepo(uow);
                return repo.GetList(request);
            }
        }

        public float GetSum(PaymentTotalRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentTotalRepo(uow);
                return repo.GetSum(request);
            }
        }

        public PaymentTotalModel GetById(PaymentTotalRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentTotalRepo(uow);
                return repo.GetById(request.PaymentTotalId);
            }
        }

        public bool Update(PaymentTotalModel model)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                model.Date = new DateTime(model.Date.Year, model.Date.Year, 1);
                uow.Commit();
                return Update(uow, model);
            }
        }
        internal bool Update(IUnitOfWork uow, PaymentTotalModel model)
        {
            ValidateModel(model);
            var repo = RepoFactory.PaymentTotalRepo(uow);
            return repo.Update(model);

        }

        public long Insert(PaymentTotalModel model)
        {


            ValidateModel(model);

            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                model.Date = new DateTime(model.Date.Year, model.Date.Month, 1);

                var repo = RepoFactory.PaymentTotalRepo(uow);

                // Check for the same month, year and IsDeposit duplication!
                var request = new PaymentTotalRequest()
                {
                    Date = model.Date,
                    IsDeposit = model.IsDeposit
                };
                var existingSimilarModel = repo.GetList(request);
                if (existingSimilarModel.Count > 0)
                    throw new ArgumentException("There is already a record for the same month, year and payment type (deposit or not)");


                model.PaymentTotalId = repo.Insert(model);

                if (model.PaymentTotalId < 1)
                    throw new ArgumentException("Failed to insert the Payment");

                uow.Commit();
                return model.PaymentTotalId;
            }
        }
        public void Delete(PaymentTotalModel model, long deletedByUserId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.PaymentTotalRepo(uow);
                if (!repo.DeleteById(model.PaymentTotalId))
                    throw new ArgumentException("Failed to delete the Order");

                uow.Commit();
            }
        }

        public void ValidateModel(PaymentTotalModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Title)) throw new ArgumentException("Title is not set", "Title");
            if (model.Price < 1) throw new ArgumentException("Price is not set", "Price");
            if (model.Date < DateTime.MinValue) throw new ArgumentException("Date is not set", "Date");
            if (model.CreatedByUserId < 1) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (model.CreatedAt < DateTime.MinValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
        }
    }
}
