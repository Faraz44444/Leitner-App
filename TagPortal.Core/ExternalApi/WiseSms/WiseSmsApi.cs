using System.Collections.Generic;
using TagPortal.Core.ExternalApi.WiseSms.Dto;

namespace TagPortal.Core.ExternalApi.WiseSmsApi
{
    internal class WiseSmsApi : BaseApi
    {
        public WiseSmsApi(string baseUrl, string accessToken) : base(baseUrl, accessToken)
        {
        }

        public AuthenticateResponseDto Login(string username, string password)
        {
            var request = new AuthenticateRequestDto { Username = username, Password = password };
            return HttpPost<AuthenticateResponseDto>("authenticate/login", request);
        }

        //public long SendSms(SendSMSDto dto)
        //{
        //    return HttpPost<long>("message/send", dto);
        //}

        //public List<long> SendSmsBatch(List<SendSMSDto> dtos)
        //{
        //    return HttpPost<List<long>>("message/sendBatch", dtos);
        //}
    }
}
