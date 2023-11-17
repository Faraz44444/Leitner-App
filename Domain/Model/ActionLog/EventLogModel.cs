using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.BaseModels;

namespace Domain.Model.ActionLog
{
    public class EventLogModel : PagedBaseModel
    {
        public long EventLogId { get; set; }
        public string Message { get; set; }
        public long ClientId { get; set; }

    }
}
