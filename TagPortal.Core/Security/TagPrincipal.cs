using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Principal;
using TagPortal.Domain.Aggregated.User;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Security
{
    public class TagPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public TagPrincipal()
        {
        }

        public TagPrincipal(TagIdentity identity)
        {
            Identity = identity;
        }

        public TagPrincipal(UserModel user, List<UserSiteAccess> userSiteAccessList)
        {
            if (user.UserType != EnumUserType.SystemUser)
                throw new SecurityException("Expected user of type 'SystemUser'");
            Identity = new TagIdentity(user, userSiteAccessList);
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
