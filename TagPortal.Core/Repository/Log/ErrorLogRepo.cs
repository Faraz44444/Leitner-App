//using System.Data;
//using TagPortal.Domain.Model.Log;

//namespace TagPortal.Core.Repository.Log
//{
//    internal class ErrorLogRepo : BaseRepository
//    {
//        public ErrorLogRepo(IDbConnection dbConnection, IDbTransaction dbTransaction) : base(dbConnection, dbTransaction)
//        {
//        }

//        public long Insert(ErrorLogModel model)
//        {
//            var sql = @"INSERT INTO ERROR_LOG_TAB (CreatedAt, Message, StackTrace, InnerException, Method, UserId, Username, UserEmail, ClientId)
//                        OUTPUT inserted.ErrorLogId
//                        VALUES (@CreatedAt, @Message, @StackTrace, @InnerException, @Method, @UserId, @Username, @UserEmail, @ClientId)";
//            return DbInsert<long>(sql, model);
//        }
//    }
//}
