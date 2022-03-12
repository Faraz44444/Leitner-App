namespace TagPortal.Core
{
    public interface IUnitOfWorkProvider
    {
        IUnitOfWork GetUnitOfWork(bool useTransaction = false);
    }
}
