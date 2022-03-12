using TagPortal.Core.Repository;

namespace TagPortal.Core.Service
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWorkProvider UowProvider;
        protected readonly RepositoryFactory RepoFactory;
        public BaseService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory)
        {
            UowProvider = uowProvider;
            RepoFactory = repoFactory;
        }
    }
}
