using Core.Infrastructure.Database;
using Core.Infrastructure.UnitOfWork;
using Core.Request.Payment;
using Domain.Model.Payment;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Service.Payment
{
    public class MaterialService : Service<MaterialRequest, MaterialModel>
    {
        public MaterialService(IUnitOfWorkProvider uowProvider, TableInfo paymentTable) :
            base(uowProvider, paymentTable, new MaterialModel(), new MaterialRequest())
        {

        }

        override public async Task<long> Insert()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return await Insert(uow);
            }
        }
    }
}
