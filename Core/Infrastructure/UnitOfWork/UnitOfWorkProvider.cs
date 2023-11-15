using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.UnitOfWork
{
    internal class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        public static string SqlConnectionString { get; set; }
        private static SqlConnection SqlConnection => new SqlConnection(SqlConnectionString);

        public IUnitOfWork GetUnitOfWork(bool useTransaction = false)
        {
            return new UnitOfWork(SqlConnection, useTransaction);
        }
    }
}
