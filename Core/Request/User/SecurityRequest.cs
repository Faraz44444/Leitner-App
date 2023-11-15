using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.User
{
    public class SecurityRequest
    {
        public string UsernameOrEmail { get; set; }
        public long UserId { get; set; }
        public bool IsNewUser { get; set; } = false;
        public string AccessToken { get; set; }
        public string PasswordResetToken { get; set; }
        public string EnteredPassword { get; set; }
        public string NewPassword { get; set; }
        public string CurrentPasswordSalt { get; set; }
        public string CurrentPasswordHash { get; set; }
        public byte[] Salt { get; set; }
    }
}
