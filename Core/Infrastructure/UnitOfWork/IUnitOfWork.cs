using System;
using System.Data;

namespace Core.Infrastructure.UnitOfWork
{
    /// <summary>
    /// IDisposable: is used to release unmanaged resources that the garbage collector cannot release.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbConnection IDbConnection { get; }
        IDbTransaction IDbTransaction { get; }
        void Commit();
    }
}
