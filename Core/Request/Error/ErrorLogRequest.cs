using Core.Request.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Error
{
    public class ErrorLogRequest : BaseRequestPaged
    {
        public long ErrorId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public long ClientId { get; set; }
    }
    public enum EnumErrorLogRequest
    {
        Message = 1,
        StackTrace = 2,
        InnerException = 3,
        ClientId = 4,
    }
}
