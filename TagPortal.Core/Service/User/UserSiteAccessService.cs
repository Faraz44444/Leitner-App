using System;
using System.Collections.Generic;
using System.Linq;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.Role;
using TagPortal.Core.Request.User;
using TagPortal.Core.Service.Role;
using TagPortal.Domain.Aggregated.User;
using TagPortal.Domain.Enum;

namespace TagPortal.Core.Service.User
{
    public class UserSiteAccessService : BaseService
    {
        private readonly UserService UserService;
        private readonly RolePermissionService RolePermissionService;
        public UserSiteAccessService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory,
            UserService userService, RolePermissionService rolePermissionService) : base(uowProvider, repoFactory)
        {
            
            UserService = userService;
            RolePermissionService = rolePermissionService;
        }

        public List<UserSiteAccess> GetList(UserSiteAccessRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                {
                    var repo = RepoFactory.UserSiteAccessRepo(uow);
                    var items = repo.GetList(request);
                    if (items != null)
                    {
                        foreach (var item in items)
                        {
                            var rolePermissions = RolePermissionService.GetList(new RolePermissionsRequest() {  RoleId = item.RoleId });
                            if (rolePermissions != null)
                                item.PermissionTypeList = rolePermissions.Select(x => x.PermissionType).ToList();
                        }
                    }
                    return items;
                }
            }
        }
    }
}
