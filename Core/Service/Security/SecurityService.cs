using Core.Infrastructure.Security;
using Core.Request.User;
using Core.Service.Mail;
using Domain.Model.User;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Core.Service.Security
{
    public class SecurityService
    {
        private static readonly int saltSize = 128 / 8;

        private Service<UserRequest, UserModel> UserService;
        private readonly MailService MailService;

        public SecurityService(Service<UserRequest, UserModel> userService, MailService mailService)
        {
            MailService = mailService;
            UserService = userService;
        }

        public async Task<bool> ValidateCredentials(string usernameOrEmail, string enteredPassword)
        {
            var user = await GetByUsernameOrEmail(usernameOrEmail);
            if (user == null || user.UserId < 1)
                return false;

            if (!await ValidateCredentials(user.UserId, enteredPassword))
                return false;

            user.LastUpdateAt = DateTime.Now;
            UserService.Model = user;
            try
            {
                await UserService.Update();

            }
            catch (Exception ex)
            {

            }
            return true;
        }
        public async Task<UserModel> GetByUsernameOrEmail(string usernameOrEmail)
        {
            UserService.Request = new UserRequest() { Username = usernameOrEmail };
            var userByUsername = await UserService.GetFirstOrDefault();
            UserService.Request = new UserRequest() { Email = usernameOrEmail };
            var userByEmail = await UserService.GetFirstOrDefault();
            if (userByUsername != null && userByUsername.UserId > 0)
                return userByUsername;

            if (userByEmail != null && userByEmail.UserId > 0)
                return userByEmail;
            return null;
        }

        private async Task<bool> ValidateCredentials(long userId, string enteredPassword)
        {
            var passwordHashAndSaltTuple = await GetPasswordHashAndSalt(userId);
            var passwordHash = passwordHashAndSaltTuple.hash;
            var passwordSalt = passwordHashAndSaltTuple.salt;

            if (passwordHash.Empty())
                return false;
            if (passwordSalt.Empty())
                return false;
            if (!ValidatePassword(enteredPassword, passwordSalt, passwordHash))
                return false;

            return true;
        }

        public async Task<bool> SetPasswordResetToken(string usernameOrEmail, bool isNewUser = false)
        {
            var user = await GetByUsernameOrEmail(usernameOrEmail);
            if (user == null || user.UserId < 1)
                return false;

            var now = DateTime.Now;

            user.PasswordResetToken = JwtToken.GenerateToken(user.UserId, now.AddDays(1)); ;
            user.PasswordResetTokenGeneratedAt = DateTime.Now;
            UserService.Model = user;
            await UserService.Update();

            if (isNewUser)
            {
                try
                {

                    MailService.SendNewUserMail(user.Email, user.PasswordResetToken);
                }

                catch (Exception ex)
                {
                    var test = 2;
                }
            }
            else
            {
                MailService.SendResetPasswordTokenMail(user.Email, user.PasswordResetToken);
            }

            return true;
        }

        public async Task<bool> IsResetPasswordTokenValid(string passwordResetToken)
        {
            if (!JwtToken.ValidateCurrentToken(passwordResetToken))
                return false;

            UserService.Request = new UserRequest() { PasswordResetToken = passwordResetToken };
            var user = await UserService.GetFirstOrDefault();

            return user != null && user.UserId > 0;
        }

        public async Task<bool> SetPassword(long userId, string newPassword, string enteredPassword)
        {
            if (!await ValidateCredentials(userId, enteredPassword))
                return false;

            UserService.Request = new UserRequest() { UserId = userId };
            var user = await UserService.GetById();

            await UpdatePassword(user.UserId, user.Email, newPassword);

            return true;
        }

        public async Task SetPassword(string accessToken, string newPassword)
        {
            UserService.Request = new UserRequest() { PasswordResetToken = accessToken };
            var user = await UserService.GetFirstOrDefault();

            if (user.UserId < 1)
                throw new ArgumentException(string.Format("Invalid password reset token: {0}", accessToken));

            await UpdatePassword(user.UserId, user.Email, newPassword);

            UserService.Request = new UserRequest() { UserId = user.UserId };
            var updatedUser = await UserService.GetById();
            updatedUser.PasswordResetToken = null;
            updatedUser.PasswordResetTokenGeneratedAt = null;
            UserService.Model = updatedUser;
            await UserService.Update();
        }

        private async Task UpdatePassword(long userId, string email, string newPassword)
        {
            if (newPassword.Empty())
                throw new ArgumentException("New password is not set");

            var salt = GeneratePasswordSalt();
            var passwordHash = GeneratePasswordHash(newPassword, salt);

            UserService.Request = new UserRequest() { UserId = userId };
            var user = await UserService.GetFirstOrDefault();
            if (user == null)
                throw new ArgumentException("The requested user does not exist");

            user.PasswordHash = passwordHash;
            user.PasswordLastSetAt = DateTime.Now;
            user.PasswordSalt = Convert.ToBase64String(salt);
            UserService.Model = user;
            await UserService.Update();

            MailService.SendPasswordUpdatedMail(email);
        }

        private async Task<(string hash, string salt)> GetPasswordHashAndSalt(long userId)
        {
            UserService.Request = new UserRequest() { UserId = userId };
            var user = await UserService.GetById();

            return (user.PasswordHash, user.PasswordSalt);
        }

        public async Task<bool> IsUserUpdated(long userId, DateTime lastUpdateAt)
        {
            if (lastUpdateAt == DateTime.MinValue) lastUpdateAt = new DateTime(599266080000000000);
            UserService.Request = new UserRequest() { UserId = userId };
            var user = await UserService.GetFirstOrDefault();
            return (user.LastUpdateAt - lastUpdateAt).TotalSeconds > 0;

        }

        private static bool ValidatePassword(string password, string currentPasswordSalt, string currentPasswordHash)
        {
            var passwordHash = GeneratePasswordHash(password, Convert.FromBase64String(currentPasswordSalt));
            return passwordHash == currentPasswordHash;
        }

        private static byte[] GeneratePasswordSalt()
        {
            byte[] salt = new byte[saltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static string GeneratePasswordHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: saltSize));
        }
    }
}
