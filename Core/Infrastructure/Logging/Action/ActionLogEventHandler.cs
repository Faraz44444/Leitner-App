using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Core.Request.Log;
using Core.Service;
using Domain.Model.ActionLog;

namespace Core.Infrastructure.Logging.Action
{
    public class ActionLogEventHandler
    {
        private Service<ActionLogRequest, ActionLogModel> ActionLogService { get; }
        private List<ActionLogModel> LogEntries { get; set; }

        public ActionLogEventHandler(ActionLogEventTrigger eventTrigger, Service<ActionLogRequest, ActionLogModel> actionLogService)
        {
            LogEntries = new List<ActionLogModel>();
            ActionLogService = actionLogService;

            eventTrigger.OnFinalizeLogging += OnFinalizeLog;
            eventTrigger.OnChange += OnChange;
        }

        public void OnFinalizeLog(object sender, EventArgs e)
        {
            if (LogEntries.Count == 0) return;

            var logEntries = LogEntries.ToList();
            LogEntries.Clear();

            foreach (var item in logEntries)
            {
                if (item == null)
                    continue;
                ActionLogService.Model = item;
                _ = ActionLogService.Insert();
            }
        }

        public void OnChange(object sender, ActionLogEventArgs e)
        {
            if (!e?.Validate() ?? false) return;
            LogEntries.Add(e.LogEntry);
        }
    }
}
