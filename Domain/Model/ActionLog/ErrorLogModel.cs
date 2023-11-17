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
        public long ErrorId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public long ClientId { get; set; }
    }
}
