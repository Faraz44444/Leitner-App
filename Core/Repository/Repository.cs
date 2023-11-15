using Core.Infrastructure.Database;
using Core.Request.BaseRequests;
using Domain.Model.BaseModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Core.Repository
{
    internal class Repository<T1, T2> : BaseRepository
        where T1 : IBaseRequestPaged
        where T2 : PagedBaseModel
    {
        private readonly IDbConnection _db;
        private readonly IDbTransaction _ta;
        private T1 Request;
        private T2 Model;
        private readonly TableInfo Table;


        public Repository(
            TableInfo table,
            T1 request,
            T2 model,
            IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
        {
            Table = table;
            Request = request;
            Model = model;
            _db = dbConnection;
            _ta = dbTransaction;
        }

        public string GetSql(bool usePaging = false)
        {
            Table.SetGetQuery(usePaging);
            var sql = string.Join(" \n",
                new List<string>() {
                    Table.GetQuery,
                    Request.WhereSql(),
                    Table.GetOrderByQuery(Request.OrderBy, Request.OrderByDirection),
                    UsePagingOffset(usePaging)
                });
            return sql;
        }
        public string GetByIdSql()
        {
            var sql = Table.GetQuery + Request.WhereSql();
            return sql;
        }
        public async Task<PagedModel<T2>> GetPaged()
        {
            return await GetPagedList<T2>(GetSql(usePaging: true), Request);
        }
        public async Task<IEnumerable<T2>> GetList()
        {
            return await GetList<T2>(GetSql(), Request);
        }
        public async Task<T2> GetFirstOrDefault()
        {
            return await GetFirstOrDefault<T2>(GetSql(), Request);
        }
        public async Task<T2> GetById()
        {
            return await GetFirstOrDefault<T2>(GetByIdSql(), Request);
        }
        public async Task<long> Insert()
        {
            return await Insert<long>(Table.InsertQuery, GetDynamicParameters(Model));
        }
        public async Task Update()
        {
            await Execute(Table.UpdateQuery, GetDynamicParameters(Model));
        }
        public async Task Delete()
        {
            await Execute(Table.DeleteQuery, GetDynamicParameters(Model));
        }
        public async Task<DateTime> GetFirstRecordDate()
        {
            return await GetFirstOrDefault<DateTime>(Table.FirstRecordDateQuery, Request);
        }
        public async Task<DateTime> GetLastRecordDate()
        {
            return await GetFirstOrDefault<DateTime>(Table.LastRecordDateQuery, Request);
        }
    }
}
