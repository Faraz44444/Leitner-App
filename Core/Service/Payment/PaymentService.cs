using Core.Infrastructure.Database;
using Core.Infrastructure.UnitOfWork;
using Core.Request.Business;
using Core.Request.Payment;
using Domain.Model.Business;
using Domain.Model.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service.Payment
{
    public class PaymentService : Service<PaymentRequest, PaymentModel>
    {
        public PaymentService(IUnitOfWorkProvider uowProvider, TableInfo paymentTable) :
            base(uowProvider, paymentTable, new PaymentModel(), new PaymentRequest())
        {

        }

        override public async Task<long> Insert()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                Model = await BusinessHandler(uow, Model);
                if (Model.BusinessId < 1) throw new ArgumentException("BusinessId is not set", "BusinessId");
                return await Insert(uow);
            }
        }
        private async Task<PaymentModel> BusinessHandler(IUnitOfWork uow, PaymentModel model)
        {
            var BusinessService = AppContext.Current.Services.BusinessService;
            BusinessService.Request = new BusinessRequest() { Name = Model.BusinessName };
            var existingBusiness = BusinessService.GetList().Result.FirstOrDefault();
            if (existingBusiness == null)
            {

                BusinessService.Model = new BusinessModel()
                {
                    Name = model.BusinessName,
                    ClientId = model.ClientId,
                    CreatedAt = model.CreatedAt,
                    CreatedByUserId = model.CreatedByUserId,
                    CreatedByFirstName = model.CreatedByFirstName,
                    CreatedByLastName = model.CreatedByLastName
                };
                model.BusinessId = await BusinessService.Insert();
            }
            else
            {
                model.BusinessName = existingBusiness.Name;
                model.BusinessId = existingBusiness.BusinessId;
            }
            return model;
        }
    }
}
