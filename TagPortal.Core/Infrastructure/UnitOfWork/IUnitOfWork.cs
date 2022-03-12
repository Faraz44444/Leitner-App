using System;
using System.Data;

namespace TagPortal.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection IDbConnection { get; }
        IDbTransaction IDbTransaction { get; }
        void Commit();
    }
}
