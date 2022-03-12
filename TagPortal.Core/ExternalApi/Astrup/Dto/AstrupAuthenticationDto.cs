using System;

namespace TagPortal.Core.ExternalApi.Astrup.Dto
{
    public class AstrupAuthenticationDto
    {
        public string ZpiderAccessToken { get; set; }
        public bool IsSuccessful { get; set; }
        public DateTime? ZpiderAccessTokenExpiresUtc { get; set; }

    }



}
