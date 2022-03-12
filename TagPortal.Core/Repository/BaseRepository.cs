using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TagPortal.Core.Request;
using TagPortal.Domain;

namespace TagPortal.Core.Repository
{
    internal abstract class BaseRepository
    {
        private readonly IDbConnection _db;
        private readonly IDbTransaction _ta;
        public BaseRepository(IDbConnection dbConnection, IDbTransaction dbTransaction)
        {
            _db = dbConnection;
            _ta = dbTransaction;
        }

        protected static string EnumOrderByDirectionToSql(EnumOrderByDirection orderByDirection)
        {
            if (orderByDirection == 0 || orderByDirection == EnumOrderByDirection.Asc)
                return " ASC ";
            return " DESC ";
        }

        protected static string UsePagingColumn(bool usePaging)
        {
            if (usePaging)
                return " , COUNT(*) OVER() AS 'TotalNumberOfItems' ";
            return "";
        }

        protected static string UsePagingOffset(bool usePaging)
        {
            if (usePaging)
                return " OFFSET ( @CurrentPage * @ItemsPerPage ) ROWS FETCH NEXT @ItemsPerPage ROWS ONLY ";
            return "";
        }

        protected List<T> DbGetList<T>(string query, object parameters)
        {
            return _db.Query<T>(query, parameters, transaction: _ta).ToList();
        }

        protected T DbGetFirstOrDefault<T>(string query, object parameters)
        {
            return _db.QueryFirstOrDefault<T>(query, parameters, transaction: _ta);
        }
        protected T DBGetSingleOrDefault<T>(string query, object parameters)
        {
            return _db.QuerySingleOrDefault<T>(query, parameters, transaction: _ta);
        }
        protected T DbInsert<T>(string query, object parameters)
        {
            return _db.ExecuteScalar<T>(query, parameters, transaction: _ta);
        }
        protected bool DbInsert(string query, object parameters)
        {
            return Execute(query, parameters);
        }
        protected bool DbDelete(string query, object parameters)
        {
            return Execute(query, parameters);
        }
        protected bool DbUpdate(string query, object parameters)
        {
            return Execute(query, parameters);
        }
        private bool Execute(string query, object parameters)
        {
            return _db.Execute(query, parameters, transaction: _ta) > 0;
        }
        protected List<T> ExecuteStoredProccedure<T>(string sproc, object request)
        {
            return _db.Query<T>(sproc, request, transaction: _ta, commandType: CommandType.StoredProcedure).ToList();
        }


        protected PagedModel<T> DbGetPagedList<T>(string sql, IBaseRequestPaged request)
            where T : PagedModel
        {
            var p = new PagedModel<T>
            {
                CurrentPage = request.CurrentPage,
                ItemsPerPage = request.ItemsPerPage
            };

            if (request.CurrentPage == 0) throw new ArgumentException("Page 0 is invalid");
            request.CurrentPage -= 1;
            p.Items = DbGetList<T>(sql, request);
            p.TotalNumberOfItems = p.Items.Count > 0 ? p.Items.First().TotalNumberOfItems : 0;
            return p;
        }


        protected private DynamicParameters GetDynamicParameters(object request)
       {
            var param = new DynamicParameters();

            foreach (var field in request.GetType().GetFields().Where(x => CheckIfValidType(x.FieldType)))
            {
                var value = field.GetValue(request);

                if (CheckNullOrEmpty(value))
                    param.Add(field.Name);
                else
                    param.Add(field.Name, value: value);
            }

            foreach (var prop in request.GetType().GetProperties().Where(x => CheckIfValidType(x.PropertyType)))
            {
                var value = prop.GetValue(request);

                if (CheckNullOrEmpty(value))
                    param.Add(prop.Name);
                else
                    param.Add(prop.Name, value: value);
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
            return false;
        }

        private bool CheckNullOrEmpty(object value)
        {
            if (value == null) return true;
            else if (value.GetType() == typeof(bool)) return false;
            else if (value.GetType() == typeof(string)) return string.IsNullOrEmpty(value as string);
            else if (value.GetType() == typeof(byte[])) return true;
            else return value.Equals(Activator.CreateInstance(value.GetType()));
        }
    }
}
