using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Exception
{
    public class FeedbackException : System.Exception
    {
        public string Info { get; }
        public FeedbackException(): base(){}
        public FeedbackException(string message): base(message){}

        public FeedbackException(string message, string info) : base(message)
        {
            Info = info;
        }
    }
}
