﻿namespace Core.Infrastructure.UnitOfWork
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork GetUnitOfWork(bool useTransaction = false);
    }
}
