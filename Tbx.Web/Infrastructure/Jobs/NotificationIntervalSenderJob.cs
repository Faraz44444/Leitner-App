using System;
using TagPortal.Core;
//using TagPortal.Core.Service.Log;

namespace TagPortal.Web.Infrastructure.Jobs
{
    public class NotificationIntervalSenderJob
    {
        //private ErrorLogService ErrorLogService => TagAppContext.Current.Services.ErrorLogService;
        //private EventLogService EventLogService => TagAppContext.Current.Services.EventLogService;
        public void SendMaintenanceNotifications()
        {
            try
            {
                //EventLogService.Insert("Starting Notificantion Sender", 0, "HangFire", "", 0);
            }
            catch (Exception ex)
            {
                //ErrorLogService.Insert(ex);
            }

        }
    }
}