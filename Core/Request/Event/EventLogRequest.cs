using Core.Request.BaseRequests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Request.Event
{
    public class EventLogRequest : BaseRequestPaged
    {
        public long EventLogId { get; set; }
        public string Message { get; set; }
        public long ClientId { get; set; }
    }
    public enum EnumEventLogRequest
    {
        Message = 1,
    }
}
