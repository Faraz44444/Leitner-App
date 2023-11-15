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
    public class PaymentTotalService : Service<PaymentTotalRequest, PaymentTotalModel>
    {
        public PaymentTotalService(IUnitOfWorkProvider uowProvider, TableInfo paymentTotalTable) :
            base(uowProvider, paymentTotalTable, new PaymentTotalModel(), new PaymentTotalRequest())
        {

        }

        override public async Task<long> Insert()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                if (!Model.BusinessName.Empty())
                    Model = await BusinessHandler(uow, Model);
                return await Insert(uow);
            }
        }
        private async Task<PaymentTotalModel> BusinessHandler(IUnitOfWork uow, PaymentTotalModel model)
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
