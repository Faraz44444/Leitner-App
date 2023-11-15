using Core.Infrastructure.Database;
using Core.Infrastructure.UnitOfWork;
using Core.Repository;
using Core.Request.BaseRequests;
using Domain.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class Service<T1, T2> : BaseService
        where T1 : IBaseRequestPaged
        where T2 : PagedBaseModel
    {
        protected readonly IUnitOfWorkProvider UowProvider;
        protected TableInfo Table;
        public T1 Request;
        public T2 Model;
        internal Repository<T1, T2> NewBaseRepository(IUnitOfWork uow) =>
            new Repository<T1, T2>(Table, Request, Model, uow.IDbConnection, uow.IDbTransaction);

        internal Service(IUnitOfWorkProvider uowProvider, TableInfo table, T2 model, T1 request) : base(uowProvider)
        {
            UowProvider = uowProvider;
            Table = table;
            Model = model;
            Request = request;
        }

        public async Task<PagedModel<T2>> GetPaged()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                Request.TableAlias = Table.Alias;
                var repo = NewBaseRepository(uow);
                return await repo.GetPaged();
            }
        }

        public async Task<IEnumerable<T2>> GetList()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                Request.TableAlias = Table.Alias;
                var repo = NewBaseRepository(uow);
                return await repo.GetList();
            }
        }
        public async Task<T2> GetFirstOrDefault()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                Request.TableAlias = Table.Alias;
                var repo = NewBaseRepository(uow);
                return await repo.GetFirstOrDefault();
            }
        }

        public async Task<T2> GetById()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                Request.TableAlias = Table.Alias;
                var repo = NewBaseRepository(uow);
                return await repo.GetById();
            }

        }

        virtual public async Task<long> Insert()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return await Insert(uow);
            }
        }

        virtual internal async Task<long> Insert(IUnitOfWork uow)
        {
            var repo = NewBaseRepository(uow);
            return await repo.Insert();
        }

        public async Task Update()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = NewBaseRepository(uow);
                await repo.Update();
            }
        }
        public async Task Delete()
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = NewBaseRepository(uow);
                await repo.Delete();
            }
        }
        public async Task<DateTime> GetFirstRecordDate()
        {
            using(IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = NewBaseRepository(uow);
                return await repo.GetFirstRecordDate();
            }
        }
        public async Task<DateTime> GetLastRecordDate()
        {
            using(IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = NewBaseRepository(uow);
                return await repo.GetLastRecordDate();
            }
        }
    }
}
