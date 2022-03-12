using System.Net;

namespace TagPortal.Core.ExternalApi.Astrup.Dto
{

    public class AstrupResponseDto
    {
        public HttpStatusCode httpStatus { get; set; }
        public string responseString { get; set; }
        public string requestString { get; set; }
    }

}
