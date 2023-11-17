using Core.Infrastructure;
using Core.Infrastructure.UnitOfWork;
using Domain.Enum.EntityType;
using Domain.Enum.OperationType;
using Domain.Interfaces;
using Domain.Model.ActionLog;
using Newtonsoft.Json;
using System;

namespace Core.Service
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWorkProvider UowProvider;

        public BaseService(IUnitOfWorkProvider uowProvider)
        {
            UowProvider = uowProvider;
        }

        // Action Log
        protected private static void ActionLog(
            EnumEntityType entityType,
            EnumOperationType operationType,
            long entityId,
            long clientId,
            long createdByUserId,
            string createdByFirstName,
            string createdByLastName,
            string name = "",
            object model = null,
            long alternateEntityId = 0)
        {
            string entityJson = JsonConvert.SerializeObject(model);

            ICurrentUser ActionBy(long userId, long clientId, string firstName, string lastName) =>
                new CurrentUser(userId, clientId, firstName, lastName);

            AppContext.Current.Log.Append(
                new ActionLogModel(
                    ActionBy(createdByUserId, clientId, createdByFirstName, createdByLastName),
                    name: name,
                    alternateEntityId: alternateEntityId,
                    entityId: entityId,
                    entityJson: entityJson,
                    entityType: entityType,
                    operationType: operationType
                    )
                );
        }

        protected private static void ActionLog(
            ICurrentUser actionBy,
            EnumEntityType entityType,
            EnumOperationType operationType,
            long entityId,
            string name = "",
            object model = null,
            long alternateEntityId = 0
                )
        {
            string entityJson = JsonConvert.SerializeObject(model);

            AppContext.Current.Log.Append(
                new ActionLogModel(
                    actionBy,
                    name: name,
                    alternateEntityId: alternateEntityId,
                    entityId: entityId,
                    entityJson: entityJson,
                    entityType: entityType,
                    operationType: operationType)
                );
        }


        // Error Log
        protected private static void ErrorLog(
            Exception exception,
            long clientId = 0,
            long createdByUserId = 0)
        {
            AppContext.Current.Services.ErrorLogService.Model = new ErrorLogModel()
            {
                Message = exception.Message,
                ClientId = clientId,
                CreatedByUserId = createdByUserId,
            };
            _ = AppContext.Current.Services.ErrorLogService.Insert();
        }
        protected private static void ErrorLog(
            string message,
            long clientId = 0,
            long createdByUserId = 0)
        {

            AppContext.Current.Services.ErrorLogService.Model = new ErrorLogModel()
            {
                Message = message,
                ClientId = clientId,
                CreatedByUserId = createdByUserId,
            };
            _ = AppContext.Current.Services.ErrorLogService.Insert();
        }

        // Event Log
        protected private static void EventLog(
            string message,
            long clientId = 0,
            long createdByUserId = 0)
        {

            AppContext.Current.Services.EventLogService.Model = new EventLogModel()
            {
                Message = message,
                ClientId = clientId,
                CreatedByUserId = createdByUserId,
            };
            _ = AppContext.Current.Services.EventLogService.Insert();
        }

    }
}
