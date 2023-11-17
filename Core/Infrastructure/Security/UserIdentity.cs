using Core.Infrastructure.Constants;
using Domain.Interfaces;
using System;
using System.Security.Claims;

namespace Core.Infrastructure.Security
{
    public class UserIdentity : ICurrentUser
    {
        private readonly ClaimsIdentity CurrentUser;
        public long UserId => CurrentUser.Get<long>(AppClaimTypes.NameIdentifier);
        public string Email => CurrentUser.Get<string>(AppClaimTypes.Email);
        public string Username => CurrentUser.Get<string>(AppClaimTypes.Username);
        public string FirstName => CurrentUser.Get<string>(AppClaimTypes.FirstName);
        public string LastName => CurrentUser.Get<string>(AppClaimTypes.LastName);
        public string FullName => CurrentUser.Get<string>(AppClaimTypes.FullName);
        public bool IsLoginPersistent => CurrentUser.Get<bool>(AppClaimTypes.IsLoginPersistent);
        public DateTime LastUpdateAt => CurrentUser.Get<DateTime>(AppClaimTypes.UserLastUpdateAt);

        public bool IsAuthenticated
        {
            get { return CurrentUser.TryGet(AppClaimTypes.Email, out string value); }
        }

        public UserIdentity(ClaimsIdentity currentUser)
        {
            this.CurrentUser = currentUser;
        }
    }
}
