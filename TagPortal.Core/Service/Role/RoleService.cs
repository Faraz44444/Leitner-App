using System;
using System.Collections.Generic;
using System.Linq;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Role;
using TagPortal.Domain;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.Role;

namespace TagPortal.Core.Service.Role
{
    public class RoleService : BaseService
    {
        private readonly RolePermissionService RolePermissionService;
        public RoleService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory,
            RolePermissionService rolePermissionService) : base(uowProvider, repoFactory)
        {
            RolePermissionService = rolePermissionService;
        }

        public PagedModel<RoleModel> GetPagedList(RoleRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.RoleRepo(uow);
                return repo.GetPagedList(request);
            }
        }
        public List<RoleModel> GetList(RoleRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.RoleRepo(uow);
                return repo.GetList(request);
            }
        }

        public RoleModel GetById(long id)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.RoleRepo(uow);
                return repo.GetById(id);
            }
        }

        public long Insert(string name, List<RolePermissionModel> permissionList, long clientId = 0, long supplierId = 0)
        {
            if (permissionList == null) throw new ArgumentException("PermissionList is not set");

            var model = new RoleModel()
            {
                Name = name,
                SupplierId = supplierId,
                ClientId = clientId,
                IsLocked = false
            };
            Validate(model);
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.RoleRepo(uow);
                model.RoleId = repo.Insert(model);

                foreach (var p in permissionList)
                {
                    RolePermissionService.Insert(uow, model.RoleId, p.PermissionType, p.IsLocked);
                }

                uow.Commit();
            }
            return model.RoleId;
        }

        public bool Update(RoleModel model, List<RolePermissionModel> permissionList = null, bool updatePermissionList = false)
        {
            Validate(model);
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.RoleRepo(uow);
                var existing = repo.GetById(model.RoleId);
                if (existing.IsLocked) throw new FeedbackException("This role is locked and cannot be modified");
                repo.Update(model);

                if (updatePermissionList)
                {
                    RolePermissionService.DeleteByRoleId(uow, model.RoleId);
                    var existingPermissions = RolePermissionService.GetList(uow, new RolePermissionsRequest() { RoleId = model.RoleId });
                    foreach (var p in permissionList.Where(x => !existingPermissions.Any(y => y.PermissionType == x.PermissionType)))
                    {
                        RolePermissionService.Insert(uow, model.RoleId, p.PermissionType, p.IsLocked);
                    }
                }

                uow.Commit();
            }
            return true;
        }

        private void Validate(RoleModel model)
        {
            if (model.ClientId < 1 && model.SupplierId < 1) throw new ArgumentException("Either ClientId Or SupplierId must be set");
            if (string.IsNullOrWhiteSpace(model.Name)) throw new ArgumentException("Name is not set");
        }
    }
}
