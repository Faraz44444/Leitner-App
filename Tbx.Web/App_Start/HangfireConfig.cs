using TagPortal.Web.Infrastructure.Jobs;
using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Collections.Generic;
using TagPortal.Core;
using TagPortal.Core.Service;

namespace TagPortal.Web
{
    public static class HangfireConfig
    {
        private static ServiceContext Services => TagAppContext.Current.Services;

        private static IEnumerable<IDisposable> GetHangfireConfig()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage();


            yield return new BackgroundJobServer();
        }

        public static void RegisterJobs()
        {
            #region "Quick help"

            // Run the Job.Print method after 10 seconds (could be set to a spesific datetime by using DateTimeOffset)
            // Dim J As New Job
            // BackgroundJob.Schedule(Sub() J.Print("Hello World! I did it!"), TimeSpan.FromSeconds(10))
            //
            // Run the Job.Print method every minuite.
            // RecurringJob.AddOrUpdate("PrintEveryMinute", Sub() J.Print("Print to debug console every minuite"), "*/1 * * * *")
            //
            // If you cant wait a minuite may u use the Trigger metod, (Will keep the original schedule)
            // RecurringJob.Trigger("PrintEveryMinuite")
            // https://api.hangfire.io/html/N_Hangfire.htm
            // https://docs.hangfire.io/en/latest/background-methods/index.html



            //   ┌────────── minute (0 - 59)
            //   │ ┌──────── hour (0 - 23)
            //   │ │ ┌────── day Of the month (1 - 31)
            //   │ │ │ ┌──── month (1 - 12)
            //   │ │ │ │ ┌── day Of the week (0 - 6) (Sunday To Saturday; 7 Is also Sunday On some systems)                                 
            //   │ │ │ │ │
            // " * * * * * " --> The Format of the cron expression
            // https://en.wikipedia.org/wiki/Cron#CRON_expression

            #endregion

            #region "Actual Jobs"
            try
            {


                HangfireAspNet.Use(GetHangfireConfig);

                NotificationIntervalSenderJob NotificationIntervalSenderJob = new NotificationIntervalSenderJob();

                #region FOR TESTING ONLY
                //BackgroundJob.Schedule(() => DnbCurrencySyncJob.SyncCurrencyRates(), TimeSpan.FromSeconds(10));
                //BackgroundJob.Schedule(() => KeepBitsAlive(), TimeSpan.FromSeconds(10));
                //BackgroundJob.Schedule(() => ToolsIntegrationSyncJob.SyncProducts(), TimeSpan.FromSeconds(10));
                //BackgroundJob.Schedule(() => AstrupIntegrationSyncJob.SyncProducts(), TimeSpan.FromSeconds(10));
                //BackgroundJob.Schedule(() => NotificationIntervalSenderJob.SendNotifications(), TimeSpan.FromSeconds(10));
                //BackgroundJob.Schedule(() => NotificationIntervalSenderJob.SendMaintenanceNotifications(), TimeSpan.FromSeconds(10));
                #endregion
                //RecurringJob.AddOrUpdate("SendMaintenanceNotifications", () => NotificationIntervalSenderJob.SendMaintenanceNotifications(), "0 8 * * *", TimeZoneInfo.Local);

            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            #endregion
        }

        public static void KeepBitsAlive()
        {

            //Services.EventLogService.Insert("Waking up Bits and Pieces :)", 0, "HangFire", "", 0);
        }



        private static void LogException(Exception exception)
        {
        }
    }
}