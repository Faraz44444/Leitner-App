//using System.Data;
//using TagPortal.Domain.Model.Log;

//namespace TagPortal.Core.Repository.Log
//{
//    internal class EventLogRepo : BaseRepository
//    {
//        public EventLogRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
//        {
//        }

//        public long Insert(EventLogModel model)
//        {
//            var sql = @"INSERT INTO EVENT_LOG_TAB (CreatedAt, Message, UserId, Username, UserEmail, ClientId)
//                        OUTPUT inserted.EventLogId
//                        VALUES (@CreatedAt, @Message, @UserId, @Username, @UserEmail, @ClientId)";
//            return DbInsert<long>(sql, model);
//        }
//    }
//}
