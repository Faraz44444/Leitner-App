using System;

namespace TagPortal.Core.ExternalApi.Astrup.Dto
{
    public class AstrupAuthenticationResponseDto
    {
        public string Token { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime ExpiresUtc { get; set; }

    }



}
