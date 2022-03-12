using System;
using System.Linq;
using TagPortal.Core;
using TagPortal.Core.Service;
using TagPortal.DataMigrator.Repository;

namespace TagPortal.DataMigrator.Service
{
    public class ActivityService
    {
        private readonly string OldTagItConnectionString = "Data Source=192.168.17.10;Initial Catalog=TAGNO;Persist Security Info=True;User ID=wiseadmin;Password=!MRENI2Zb2y1";
        private readonly string NewTagPortalConnectionString = "Data Source=WISE-S07;Initial Catalog=TAG_PORTAL;Persist Security Info=True;User ID=TagPortal;Password=G.}02H6d!Qe5";
        private readonly ServiceContext Services = TagAppContext.Current.Services;

        public void MigrateActivities(long clientId)
        {
            //Configuration.Configure(NewTagPortalConnectionString);
            //ActivityRepo activityRepo = new ActivityRepo(OldTagItConnectionString);
            //bool keepGoing = true;
            //int currentPage = 0;
            //try
            //{
            //    do
            //    {
            //        var activityList = activityRepo.GetActivities(currentPage, 1000, clientId);
            //        if (activityList == null || activityList.Count < 1)
            //        {
            //            keepGoing = false;
            //            continue;
            //        }

            //        foreach (var a in activityList)
            //        {
            //            if (string.IsNullOrWhiteSpace(a.Project))
            //                continue;
            //            if (string.IsNullOrWhiteSpace(a.SFI))
            //                continue;
            //            if (string.IsNullOrWhiteSpace(a.Activity))
            //                continue;

            //            var projectNo = a.Project.Trim();
            //            var sfiCode = a.SFI.Trim();
            //            var activitySeq = a.Activity.Trim();

            //            var activityNo = projectNo + "." + sfiCode + "." + activitySeq;

            //            var project = Services.ProjectService.GetList(new Core.Request.Project.ProjectRequest() { ClientId = clientId, ProjectNo = projectNo }).FirstOrDefault();
            //            if (project == null || project.ProjectId < 1)
            //                continue;

            //            var sfi = Services.SfiService.GetList(new Core.Request.SFI.SfiRequest() { ClientId = clientId, SfiCode = sfiCode }).FirstOrDefault();
            //            if (sfi == null || sfi.SfiId < 1)
            //                continue;

            //            var activity = Services.ActivityService.GetList(new Core.Request.Activity.ActivityRequest() { ClientId = clientId, ActivityNo = activityNo }).FirstOrDefault();
            //            if (activity == null || activity.ActivityId < 1)
            //            {
            //                var id = Services.ActivityService.Insert(activityNo, activityNo, sfi.SfiId, project.ProjectId, clientId, true);
            //                activity = Services.ActivityService.GetById(id);
            //                activity.RegisteredHours = a.WorkHourSum;
            //                Services.ActivityService.Update(activity);
            //            }
            //            else
            //            {
            //                activity.ProjectId = project.ProjectId;
            //                activity.SfiId = sfi.SfiId;
            //                activity.RegisteredHours = a.WorkHourSum;
            //                Services.ActivityService.Update(activity);
            //            }
            //        }
            //        currentPage += 1;
            //    } while (keepGoing);
            //}
            //catch (Exception ex)
            //{
            //    Services.ErrorLogService.Insert(ex);
            //}
        }
    }
}
