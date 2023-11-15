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
        [IsTableColumn(true)]
        public long EventLogId { get; set; }
        [IsTableColumn(true)]
        public string Message { get; set; }
        [IsTableColumn(true)]
        public long ClientId { get; set; }

    }
}
