using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.BaseModels;

namespace Domain.Model.ActionLog
{
    public class ErrorLogModel : PagedBaseModel
    {
        [IsTableColumn(true)]
        public long ErrorId { get; set; }
        [IsTableColumn(true)]
        public string Message { get; set; }
        [IsTableColumn(true)]
        public string StackTrace { get; set; }
        [IsTableColumn(true)]
        public string InnerException { get; set; }
        [IsTableColumn(true)]
        public long ClientId { get; set; }
    }
}
