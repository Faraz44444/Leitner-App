using System;

namespace Core.Infrastructure.Logging.Action
{
    public class ActionLogEventTrigger
    {
        internal event EventHandler OnFinalizeLogging;
        internal event EventHandler<ActionLogEventArgs> OnChange;

        public void FinalizeLog()
        {
            OnFinalizeLogging?.Invoke(this, EventArgs.Empty);
        }

        public void AppendToLog(ActionLogEventArgs e)
        {
            if (!e?.Validate() ?? false) return;
            OnChange?.Invoke(this, e);
        }

    }
}
