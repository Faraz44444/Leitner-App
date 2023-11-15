using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Constants
{
    public class AppClaimTypes
    {
        public const string NameIdentifier = "userId";
        public const string Username = "username";
        public const string Email = "email";
        public const string FirstName = "firstName";
        public const string LastName = "lastName";
        public const string FullName = "name";
        public const string CurrentClientId = "clientId";
        public const string CurrentClientName = "clientName";
        public const string RoleName= "role";
        public const string Permission= "permission";
        public const string IsLoginPersistent = "isLoginPersistent";
        public const string UserLastUpdateAt = "userLastUpdateAt";
    }
}
