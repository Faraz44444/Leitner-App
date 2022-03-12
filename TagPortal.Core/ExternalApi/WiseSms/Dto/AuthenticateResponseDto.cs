using System;
using System.Collections.Generic;

namespace TagPortal.Core.ExternalApi.WiseSms.Dto
{
    public class AuthenticateResponseDto
    {
        public string Token { get; set; }
        public DateTime TokenExpiresUtc { get; set; }
    }

    

}
