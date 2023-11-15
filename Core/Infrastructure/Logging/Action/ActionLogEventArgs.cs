using System;
using Domain.Model.ActionLog;

namespace Core.Infrastructure.Logging.Action
{
    public class ActionLogEventArgs : EventArgs
    {
        public ActionLogModel LogEntry { get; set; }

        public ActionLogEventArgs(ActionLogModel logEntry)
        {
            LogEntry = logEntry;
        }

        public bool Validate()
        {
            return LogEntry != null;
        }

    }
}
