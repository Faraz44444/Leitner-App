using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TagPortal.DataMigrator.Dto;

namespace TagPortal.DataMigrator.Repository
{
    public class ActivityRepo : IDisposable
    {
        private IDbConnection SqlConnection;

        public ActivityRepo(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
            SqlConnection.Open();
        }

        public void Dispose()
        {
            SqlConnection.Dispose();
            SqlConnection = null;
        }

        internal List<ActivityDto> GetActivities(int currentPage, int itemsPerPage, long clientId)
        {
            var sql = @"select cmh.ClientId, cmh.Project, cmh.SFI, cmh.ActivityNo as 'Activity', cmh.WorkHourSum, cmh.Description
                        From CLIENT.CRM_MAN_HOUR_TAB cmh
                        where cmh.ClientId = @clientId
                        order by cmh.Project, cmh.SFI, cmh.ActivityNo
                        OFFSET ( @CurrentPage * @ItemsPerPage ) ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";

            var p = new DynamicParameters();
            p.Add("CurrentPage", currentPage);
            p.Add("ItemsPerPage", itemsPerPage);
            p.Add("clientId", clientId);

            return SqlConnection.Query<ActivityDto>(sql, p).ToList();
        }

    }
}
