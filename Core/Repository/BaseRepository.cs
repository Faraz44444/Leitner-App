using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Core.Request.BaseRequests;
using Dapper;
using Domain.Enum;
using Domain.Model;
using Domain.Model.BaseModels;

namespace Core.Repository
{
    public static class RepoExtensions
    {
        public static void AddBaseWhereParams<T>(this List<string> searchParams, string tableAlias, T request)
            where T : IBaseRequest
        {
            if (request.CreatedByUserId > 0) searchParams.Add($"{tableAlias}.CreatedByUserId = @CreatedByUserId");
            if (!request.CreatedByFullName.Empty())
            {
                searchParams.Add(
                    $"REPLACE(CONCAT({tableAlias}.CreatedByFirstName, {tableAlias}.CreatedByLastName), ' ', '')" +
                    $" like '%' + REPLACE(@CreatedByFullName, ' ', '') + '%'");
            }
            if (request.CreatedFrom.HasValue) searchParams.Add($"CAST({tableAlias}.CreatedAt as Date) >= CAST(@CreatedFrom As Date)");
            if (request.CreatedTo.HasValue) searchParams.Add($"CAST({tableAlias}.CreatedAt as Date) <= CAST(@CreatedTo As Date)");

            if (request.Deleted.HasValue) searchParams.Add($"{tableAlias}.Deleted = @Deleted");
            if (request.DeletedFrom.HasValue) searchParams.Add($"CAST({tableAlias}.DeletedAt as Date) >= CAST(@DeletedFrom As Date)");
            if (request.DeletedTo.HasValue) searchParams.Add($"CAST({tableAlias}.DeletedAt as Date) <= CAST(@DeletedTo As Date)");

        }
    }
    internal abstract class BaseRepository
    {
        private readonly IDbConnection _db;
        private readonly IDbTransaction _ta;

        public BaseRepository(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _db = dbConnection;
            _ta = dbTransaction;
        }

        protected static string EnumOrderByDirectionToSql(EnumOrderByDirection orderByDirection, bool revert = false)
        {
            if (revert)
                orderByDirection = orderByDirection == 0 || orderByDirection == EnumOrderByDirection.Asc ? EnumOrderByDirection.Desc : EnumOrderByDirection.Asc;
            if (orderByDirection == 0 || orderByDirection == EnumOrderByDirection.Asc)
                return " ASC ";
            return " DESC ";
        }
        protected static string UsePagingOffset(bool usePaging)
        {
            if (usePaging)
                return "OFFSET (@CurrentPage * @ItemsPerPage) ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
            else
                return "";
        }
        protected Task<IEnumerable<T>> GetList<T>(string query, object parameters)
        {
            return _db.QueryAsync<T>(query, parameters, transaction: _ta);
        }

        protected Task<T> GetFirstOrDefault<T>(string query, object parameters)
        {
            return _db.QueryFirstOrDefaultAsync<T>(query, parameters, transaction: _ta);
        }

        protected Task<T> GetSingleOrDefault<T>(string query, object parameters)
        {
            return _db.QuerySingleOrDefaultAsync<T>(query, parameters, transaction: _ta);
        }

        protected Task<T> GetSingle<T>(string query, object parameters)
        {
            return _db.QuerySingleAsync<T>(query, parameters, transaction: _ta);
        }
        protected Task Insert(string query, object parameters)
        {
            return _db.ExecuteScalarAsync(query, parameters, transaction: _ta);
        }

        protected Task<T> Insert<T>(string query, object parameters)
        {
            return _db.ExecuteScalarAsync<T>(query, parameters, transaction: _ta);
        }

        protected Task<int> Execute(string query, object parameters)
        {
            return _db.ExecuteAsync(query, parameters, transaction: _ta);
        }

        protected async Task<PagedModel<T>> GetPagedList<T>(string sql, IBaseRequestPaged request)
            where T : PagedModel
        {
            var p = new PagedModel<T>
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };

            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");

            request.CurrentPage--;
            var result = await GetList<T>(sql, request);
            p.Items = result;
            p.TotalNumberOfItems = p.Items.Any() ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }

        protected async Task<PagedModel<T>> GetPagedList<T>(string sql, IBaseRequestPaged request, int totalNumberOfItems)
            where T : PagedModel
        {
            var p = new PagedModel<T>
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage,
                TotalNumberOfItems = totalNumberOfItems

            };

            if (request.CurrentPage == 0) throw new ArgumentException("Page is invalid");
            request.CurrentPage--;
            var result = await GetList<T>(sql, request);
            p.Items = result;
            return p;

        }
        protected private DynamicParameters GetDynamicParameters(object request)
        {
            var param = new DynamicParameters();

            foreach (var field in request.GetType().GetFields().Where(x => CheckIfValidType(x.FieldType)))
            {
                var value = field.GetValue(request);

                if (CheckNullOrEmpty(value)) param.Add(field.Name);
                else param.Add(field.Name, value: value);
            }
            foreach (var prop in request.GetType().GetProperties().Where(x => CheckIfValidType(x.PropertyType)))
            {
                var value = prop.GetValue(request);
                var isEmpty = CheckNullOrEmpty(value);
                if (isEmpty) param.Add(prop.Name);
                else param.Add(prop.Name, value: value);
            }
            return param;
        }
        private bool CheckIfValidType(Type type)
        {
            if (type.IsEnum) return true;
            else if (type.IsValueType) return true;
            else if (type == typeof(string)) return true;
            else if (type.IsAssignableFrom(typeof(DateTime))) return true;
            else if (type.IsAssignableFrom(typeof(List<>)) && type.GenericTypeArguments.Count() == 1) return CheckIfValidType(type.GenericTypeArguments[0]);
            else if (type == typeof(byte[])) return true;
            else return false;
        }
        private bool CheckNullOrEmpty(object value)
        {
            if (value == null) return true;
            else if (value.GetType() == typeof(bool)) return false;
            else if (value.GetType() == typeof(string)) return string.IsNullOrEmpty(value as string);
            else if (value.GetType() == typeof(byte[])) return true;
            //check what it does actually
            else return value.Equals(Activator.CreateInstance(value.GetType()));
        }

    }
}
