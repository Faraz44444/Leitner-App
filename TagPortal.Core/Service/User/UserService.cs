using System;
using System.Collections.Generic;
using TagPortal.Core.Repository;
using TagPortal.Core.Request.User;
//using TagPortal.Core.Service.Mail;
using TagPortal.Domain;
using TagPortal.Domain.Enum;
using TagPortal.Domain.Model.User;

namespace TagPortal.Core.Service.User
{
    public class UserService : BaseService
    {
        //private readonly MailService MailService;
        public UserService(IUnitOfWorkProvider uowProvider, RepositoryFactory repoFactory) : base(uowProvider, repoFactory)
        {
            //MailService = mailService;
        }

        public PagedModel<UserModel> GetPagedList(UserRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserRepo(uow);
                return repo.GetPagedList(request);
            }
        }

        public List<UserModel> GetList(UserRequest request)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserRepo(uow);
                return repo.GetList(request);
            }
        }

        public UserModel GetById(long id)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetById(uow, id);
            }
        }

        internal UserModel GetById(IUnitOfWork uow, long id)
        {
            var repo = RepoFactory.UserRepo(uow);
            return repo.GetById(id);
        }

        public UserModel GetByUsernameOrEmail(string usernameOrEmail)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserRepo(uow);
                return repo.GetByUsernameOrEmail(usernameOrEmail);
            }
        }

        public long Insert(string username, string userInitials, EnumUserType userType, string email, string firstName, string lastName, string temporaryPassword, string temporaryHash, Guid passwordResetGuid,
            bool forcePasswordRest = false, bool forceUserInformationUpdate = false, long clientId = 0, long siteId = 0, long supplierId = 0, string externalId = "", string phoneNumber = "", long phoneCountryId = 0)
        {
            var model = new UserModel()
            {
                Active = true,
                UserType = userType,
                CssFontSize = 12, 
                Email = email,
                FirstName = firstName,
                ForcePasswordReset = forcePasswordRest,
                ForceUserInformationUpdate = forceUserInformationUpdate,
                LastName = lastName,
                Username = username,
                UserInitials = userInitials,             
                LastLoginAt = new DateTime(1990, 1, 1),
                PhoneNumber = phoneNumber,
            };

            Validate(model);
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork(useTransaction: true))
            {
                var repo = RepoFactory.UserRepo(uow);

                var existingUsername = repo.GetByUsernameOrEmail(model.Username);
                var existingEmail = repo.GetByUsernameOrEmail(model.Email);

                var usernameExists = false;
                if (existingUsername != null && existingUsername.UserId > 0)
                {
                    usernameExists = true;
                }
                var emailExists = false;
                if (existingEmail != null && existingEmail.UserId > 0)
                {
                    emailExists = true;
                }
                if (usernameExists || emailExists)
                {
                    throw new FeedbackException(
                        string.Format(
                            "The following properties are not valid, because they are already in use by someone else: {0} {1}",
                            usernameExists ? $"Username ({model.Username})" : "",
                            emailExists ? (usernameExists ? $", Email ({model.Email})" : $"Email ({model.Email})") : ""
                        )
                    );
                }

                model.UserId = repo.Insert(model, temporaryHash, passwordResetGuid);

                //MailService.SendNewUserNotification(
                //    systemType: systemType,
                //    email: model.Email,
                //    username: model.Username,
                //    passwordResetGuid: passwordResetGuid);

                uow.Commit();
                return model.UserId;
            }
        }

        public bool Update(UserModel model)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return Update(uow, model);
            }
        }

        internal bool Update(IUnitOfWork uow, UserModel model)
        {
            var existingUser = GetById(uow, model.UserId);
            if (existingUser.UserType != model.UserType) throw new ArgumentException("This user is not admin");

            Validate(model);
            var repo = RepoFactory.UserRepo(uow);

            var existingUsername = repo.GetByUsernameOrEmail(model.Username);
            var existingEmail = repo.GetByUsernameOrEmail(model.Email);

            var usernameExists = false;
            if (existingUsername != null && existingUsername.UserId != model.UserId)
            {
                usernameExists = true;
            }
            var emailExists = false;
            if (existingEmail != null && existingEmail.UserId != model.UserId)
            {
                emailExists = true;
            }
            if (usernameExists || emailExists)
            {
                throw new FeedbackException(
                    string.Format(
                        "The following properties are not valid, because they are already in use by someone else: {0} {1}",
                        usernameExists ? $"Username ({model.Username})" : "",
                        emailExists ? (usernameExists ? $", Email ({model.Email})" : $"Email ({model.Email})") : ""
                    )
                );
            }

            return repo.Update(model);
        }

        public void UpdateLeftNavigationMinified(long userId, bool leftNavigationMinified)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserRepo(uow);
                repo.UpdateLeftNavigationMinified(userId, leftNavigationMinified);
            }
        }

        public string GetUserInitials(long userId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetUserInitials(uow, userId);
            }
        }

        internal string GetUserInitials(IUnitOfWork uow, long userId)
        {
            if (userId < 1) throw new ArgumentException("UserId is not set");
            var repo = RepoFactory.UserRepo(uow);
            return repo.GetUserInitials(userId);
        }

        public int GetNextTagNo(long userId)
        {
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                return GetNextTagNo(uow, userId);
            }
        }

        internal int GetNextTagNo(IUnitOfWork uow, long userId)
        {
            if (userId < 1) throw new ArgumentException("UserId is not set");
            var repo = RepoFactory.UserRepo(uow);
            return repo.GetNextTagNo(userId);
        }

        internal bool UpdateNextTagNo(IUnitOfWork uow, long userId, int nextTagNo)
        {
            if (userId < 1) throw new ArgumentException("UserId is not set");
            if (nextTagNo < 1) throw new ArgumentException("nextTagNo is not set");
            var repo = RepoFactory.UserRepo(uow);
            if (nextTagNo > 999999)
                nextTagNo = 1;
            return repo.UpdateNextTagNo(userId, nextTagNo);
        }

        public string GetTagSearchFilterJson(long userId)
        {
            if (userId < 1) throw new ArgumentException("UserId is not set");
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserRepo(uow);
                return repo.GetTagSearchFilterJson(userId);
            }
        }

        public string SaveTagSearchFilterJson(long userId, string tagSearchFilterJson)
        {
            if (userId < 1) throw new ArgumentException("UserId is not set");
            using (IUnitOfWork uow = UowProvider.GetUnitOfWork())
            {
                var repo = RepoFactory.UserRepo(uow);
                return repo.SaveTagSearchFilterJson(userId, tagSearchFilterJson);
            }
        }

        private void Validate(UserModel model)
        {

            if (string.IsNullOrWhiteSpace(model.Email)) throw new ArgumentException("Email is not set");
            if (string.IsNullOrWhiteSpace(model.FirstName)) throw new ArgumentException("FirstName is not set");
            if (string.IsNullOrWhiteSpace(model.LastName)) throw new ArgumentException("LastName is not set");
            if (string.IsNullOrWhiteSpace(model.Username)) throw new ArgumentException("Username is not set");
            if (!Enum.IsDefined(typeof(EnumUserType), model.UserType)) throw new ArgumentException("UserType is not set");

        }
    }
}
