using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Role;
using TagPortal.Domain;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.Role;

namespace TagPortal.Core.Service.Role
{
    public class RolePermissionService : BaseService
    {
        public RolePermissionService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory) : base(uowProvider, repoFactory)
        {
        }

        public PagedModel<RolePermissionModel> GetPagedList(RolePermissionsRequest request)
        {

            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.RolePermissionRepo(uow);
                var items = repo.GetPagedList(request);
                SetRolePermissions(items.Items);
                return items;
            }
        }
        public List<RolePermissionModel> GetList(RolePermissionsRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetList(uow, request);
            }
        }

        internal List<RolePermissionModel> GetList(IUnitOfWork uow, RolePermissionsRequest request)
        {

            var repo = RepoFactory.RolePermissionRepo(uow);
            var items = repo.GetList(request);
            SetRolePermissions(items);
            return items;
        }

        internal bool Insert(IUnitOfWork uow, long roleId, EnumPermissionType permissionType, bool isLocked = false)
        {
            var model = new RolePermissionModel()
            {
                IsLocked = isLocked,
                PermissionType = permissionType,
                RoleId = roleId,
            };

            Validate(model);

            var repo = RepoFactory.RolePermissionRepo(uow);
            return repo.Insert(model);
        }

        internal bool DeleteByRoleId(IUnitOfWork uow, long roleId)
        {
            if (roleId < 1) throw new ArgumentException("RoleId is not set");
            var repo = RepoFactory.RolePermissionRepo(uow);
            return repo.DeleteByRoleId(roleId);
        }

        private void Validate(RolePermissionModel model)
        {

            if (model.RoleId < 1) throw new ArgumentException("RoleId is not set");
            if (!Enum.IsDefined(typeof(EnumPermissionType), model.PermissionType)) throw new ArgumentException("PermissionsType is not set");
        }

        private void SetRolePermissions(List<RolePermissionModel> items)
        {
            string[] permissionText;
            foreach (var item in items)
            {
                permissionText = item.PermissionType.ToString().Split(char.Parse("_"));
                if (permissionText.Length == 2)
                {
                    item.PermissionGroup = permissionText[0];
                    item.PermissionName = permissionText[1];
                }
                else
                {
                    item.PermissionGroup = "NOT APPOINTED";
                    item.PermissionName = "NOT APPOINTED";
                }
            }
        }
    }
}
