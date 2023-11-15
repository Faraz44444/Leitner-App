using Core.Infrastructure.Logging.Action;
using Core.Request.Log;
using Core.Service;
using Domain.Model.ActionLog;

namespace Core.Infrastructure.Logging
{
    public class LogContext
    {
        private readonly ActionLogEventTrigger EventTrigger;

        public LogContext(Service<ActionLogRequest, ActionLogModel> actionLogService)
        {
            EventTrigger = new ActionLogEventTrigger();
            _ = new ActionLogEventHandler(EventTrigger, actionLogService);
        }

        public void Commit()
        {
            EventTrigger.FinalizeLog();
        }

        public void Append(ActionLogModel model)
        {
            EventTrigger.AppendToLog(new ActionLogEventArgs(model));
        }
    }
}
