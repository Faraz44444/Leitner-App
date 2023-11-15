using Core.Infrastructure.Database;
using Core.Repository;
using Core.Request.BaseRequests;
using Core.Service;
using Domain.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data
{
    public class BaseData<T1, T2>
        where T1 : IBaseRequestPaged
        where T2 : PagedBaseModel
    {
        public T1 Model;
        public T2 Request;
        private readonly TableInfo Table;
        internal Repository<T1, T2> NewBaseRepository;
        public Service<T1, T2> NewBaseService;

        internal BaseData(T1 model, T2 request, TableInfo table, Repository<T1, T2> newBaseRepository, Service<T1, T2> newBaseService)
        {
            Model = model;
            Request = request;
            Table = table;
            NewBaseRepository = newBaseRepository;
            NewBaseService = newBaseService;
        }
        //var model = new UserModel();
        //TableContext Tables = new();
        //var test = new NewBaseRepository<UserRequest, UserModel>(
        //    Tables.UserTable,
        //    req,
        //    model,
        //    uow.IDbConnection,
        //    uow.IDbTransaction
        //    );
        //var test2 = await test.GetList();
    }
}
// define new class DatabaseInfo pass it in     
