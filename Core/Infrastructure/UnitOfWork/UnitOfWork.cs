using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        public IDbConnection IDbConnection { get; }
        public IDbTransaction IDbTransaction { get; }

        public UnitOfWork(IDbConnection connection, bool useTransaction)
        {
            IDbConnection = connection;
            IDbConnection.Open();
            if (useTransaction)
                IDbTransaction = IDbConnection.BeginTransaction(IsolationLevel.ReadUncommitted);
        }

        public void Commit()
        {
            if(IDbTransaction!=null)
                IDbTransaction.Commit();
        }

        public void Dispose()
        {
            if(IDbTransaction != null)
                IDbTransaction.Dispose();
            if(IDbConnection !=null)
                IDbConnection.Close();
            if(IDbConnection !=null)
                IDbConnection.Dispose();
        }
    }
}
