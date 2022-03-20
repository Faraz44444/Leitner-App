using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Payment;
using TagPortal.Core.Service.Business;
using TagPortal.Core.Service.User;
using TagPortal.Domain;
using TagPortal.Domain.Model.Business;
using TagPortal.Domain.Model.Payment;

namespace TagPortal.Core.Service.Payment
{
    public class PaymentService : BaseService
    {
        private UserService UserService { get; }
        private BusinessService BusinessService { get; }

        public PaymentService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory, UserService userService, BusinessService businessService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
            BusinessService = businessService;
        }

        public PagedModel<PaymentModel> GetPagedList(PaymentRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentRepo(uow);
                return repo.GetPagedList(request);
            }
        }
        public List<PaymentModel> GetList(PaymentRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentRepo(uow);
                return repo.GetList(request);
            }
        }

        public PaymentSumModel GetSum(PaymentRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentRepo(uow);
                var sum = repo.GetSum(request);
                uow.Commit();
                return sum;
            }
        }
        public PaymentSumListModel GetSums(PaymentRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var result = new PaymentSumListModel();
                result.Sums = new List<PaymentSumModel>();
                var repo = RepoFactory.PaymentRepo(uow);
                var requestedPeriodStart = new DateTime(request.DateFrom.Year, request.DateFrom.Month, 1, 0, 0, 0);
                var requestedPeriodEnd = new DateTime(request.DateTo.Year, request.DateTo.Month, 1, 0, 0, 0);
                while (requestedPeriodStart <= requestedPeriodEnd)
                {
                    request.DateFrom = requestedPeriodStart;
                    requestedPeriodStart = requestedPeriodStart.AddMonths(1);
                    request.DateTo = requestedPeriodStart;
                    var sum = GetSum(request);
                    result.Sums.Add(sum);
                }
                return result;
            }
        }
        public PaymentSumModel GetSum(IUnitOfWork uow, PaymentRequest request)
        {
            var repo = RepoFactory.PaymentRepo(uow);
            return repo.GetSum(request);
        }
        public PaymentSumListModel GetSavings(PaymentRequest request)
        {
            var result = new PaymentSumListModel();
            result.Sums = new List<PaymentSumModel>();
            var requestedPeriodStart = new DateTime(request.DateFrom.Year, request.DateFrom.Month, 1, 0, 0, 0);
            var requestedPeriodEnd = new DateTime(request.DateTo.Year, request.DateTo.Month, 1, 0, 0, 0);
            while (requestedPeriodStart <= requestedPeriodEnd)
            {
                request.DateFrom = requestedPeriodStart;
                requestedPeriodStart = requestedPeriodStart.AddMonths(1);
                request.DateTo = requestedPeriodStart;
                result.Sums.Add(GetSaving(request));
            }
            return result;
        }
        public PaymentSumModel GetSaving(PaymentRequest request)
        {
            var result = new PaymentSumModel();
            request.IsDeposit = true;
            var income = GetSum(request);
            request.IsDeposit = false;
            var expenditures = GetSum(request);
            result.Sum = income.Sum - expenditures.Sum;
            return result;
        }

        public PaymentModel GetById(PaymentRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.PaymentRepo(uow);
                return repo.GetById(request.PaymentId);
            }
        }

        public bool Update(PaymentModel model)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                uow.Commit();
                return Update(uow, model);
            }
        }
        internal bool Update(IUnitOfWork uow, PaymentModel model)
        {
            ValidateModel(model);
            var repo = RepoFactory.PaymentRepo(uow);
            return repo.Update(model);

        }

        public long Insert(PaymentModel model)
        {


            ValidateModel(model);

            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                // Check if the businesses exist, if not create one
                BusinessHandler(uow, ref model);
                if (model.BusinessId < 1) throw new ArgumentException("BusinessId is not set", "BusinessId");

                var repo = RepoFactory.PaymentRepo(uow);
                model.PaymentId = repo.Insert(model);

                if (model.PaymentId < 1)
                    throw new ArgumentException("Failed to insert the Payment");

                uow.Commit();
                return model.PaymentId;
            }
        }
        private void BusinessHandler(IUnitOfWork uow, ref PaymentModel model)
        {
            var existingBusiness = BusinessService.GetByName(model.BusinessName);
            if (existingBusiness < 1)
            {

                var businessModel = new BusinessModel()
                {
                    BusinessName = model.BusinessName,
                    CreatedAt = model.CreatedAt,
                    CreatedByUserId = model.CreatedByUserId
                };
                model.BusinessId = BusinessService.Insert(uow, businessModel);
            }
            else
            {
                model.BusinessId = existingBusiness;
            }
        }

        public void Delete(PaymentModel model, long deletedByUserId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.PaymentRepo(uow);
                if (!repo.DeleteById(model.PaymentId))
                    throw new ArgumentException("Failed to delete the Order");

                uow.Commit();
            }
        }

        public void ValidateModel(PaymentModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Title)) throw new ArgumentException("Title is not set", "Title");
            if (string.IsNullOrWhiteSpace(model.BusinessName)) throw new ArgumentException("BusinessName is not set", "BusinessName");
            if (model.CategoryId < 1) throw new ArgumentException("CategoryId is not set", "CategoryId");
            if (model.PaymentPriorityId < 1) throw new ArgumentException("PaymentPriorityId is not set", "PaymentPriorityId");
            if (model.Price < 1) throw new ArgumentException("Price is not set", "Price");
            if (model.CreatedByUserId < 1) throw new ArgumentException("CreatedByUserId is not set", "CreatedByUserId");
            if (model.Date < DateTime.MinValue) throw new ArgumentException("Date is not set", "Date");
            if (model.CreatedAt < DateTime.MinValue) throw new ArgumentException("CreatedAt is not set", "CreatedAt");
        }
    }
}
