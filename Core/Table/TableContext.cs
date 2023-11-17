using Core.Infrastructure.Database;
using Domain.Model.ActionLog;
using Domain.Model.BaseModels;
using Domain.Model.Category;
using Domain.Model.Payment;
using Domain.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Table
{
    internal class TableContext
    {
        internal TableInfo ClientTable;
        internal TableInfo UserTable;
        internal TableInfo UserClientTable;
        internal TableInfo RoleTable;
        internal TableInfo RolePermissionTable;
        internal TableInfo ActionLogTable;
        internal TableInfo ErrorLogTable;
        internal TableInfo EventLogTable;
        internal TableInfo BusinessTable;
        internal TableInfo CategoryTable;
        internal TableInfo PaymentPriorityTable;
        internal TableInfo MaterialTable;
        internal TableInfo PaymentTotalTable;

        internal TableContext()
        {
            UserTableSetter();
            ActionLogSetter();
            ErrorLogSetter();
            EventLogSetter();
            CategoryTableSetter();
            MaterialTableSetter();
        }

        internal void UserTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(UserModel).GetProperties().
                Where(x => !Attribute.IsDefined(x, typeof(IsNotTableColumnAttribute)) ||
                !x.GetCustomAttribute<IsNotTableColumnAttribute>().IsNotTableColumn).ToList().ForEach(x =>
                {
                    columns.Add(new TableColumn(name: x.Name, tableAlias: "u", alias: x.Name));
                });
            UserTable = new TableInfo(name: "dbo.USER_TAB",
                                      alias: "u",
                                      primaryKey: "UserId",
                                      columns: columns);
        }
        internal void ActionLogSetter()
        {
            var columns = new List<TableColumn>();
            typeof(ActionLogModel).GetProperties().Where(x => !Attribute.IsDefined(x, typeof(IsNotTableColumnAttribute)) ||
               x.GetCustomAttribute<IsNotTableColumnAttribute>().IsNotTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "al", alias: x.Name));
               });
            ActionLogTable = new TableInfo(name: "dbo.ACTION_LOG_TAB",
                                          alias: "al",
                                          primaryKey: "ActionLogId",
                                          columns: columns);
        }
        internal void ErrorLogSetter()
        {
            var columns = new List<TableColumn>();
            typeof(ErrorLogModel).GetProperties().Where(x => !Attribute.IsDefined(x, typeof(IsNotTableColumnAttribute)) ||
               !x.GetCustomAttribute<IsNotTableColumnAttribute>().IsNotTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "el", alias: x.Name));
               });
            ErrorLogTable = new TableInfo(name: "dbo.ERROR_LOG_TAB",
                                          alias: "el",
                                          primaryKey: "ErrorLogId",
                                          columns: columns,
                                          joins: new List<TableJoin>() { });
        }
        internal void EventLogSetter()
        {
            var columns = new List<TableColumn>();
            typeof(EventLogModel).GetProperties().Where(x => !Attribute.IsDefined(x, typeof(IsNotTableColumnAttribute)) ||
               !x.GetCustomAttribute<IsNotTableColumnAttribute>().IsNotTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "evl", alias: x.Name));
               });
            EventLogTable = new TableInfo(name: "dbo.EVENT_LOG_TAB",
                                          alias: "evl",
                                          primaryKey: "EventLogId",
                                          columns: columns);
        }
        internal void CategoryTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(CategoryModel).GetProperties().Where(x => !Attribute.IsDefined(x, typeof(IsNotTableColumnAttribute)) ||
               !x.GetCustomAttribute<IsNotTableColumnAttribute>().IsNotTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "ca", alias: x.Name));
               });
            CategoryTable = new TableInfo(name: "dbo.CATEGORY_TAB",
                                          alias: "ca",
                                          primaryKey: "CategoryId",
                                          columns: columns);
        }
        internal void MaterialTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(MaterialModel).GetProperties().Where(x => !Attribute.IsDefined(x, typeof(IsNotTableColumnAttribute)) ||
               !x.GetCustomAttribute<IsNotTableColumnAttribute>().IsNotTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "p", alias: x.Name));
               });

            var categoryJoin = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: CategoryTable,
                                      source: null,
                                      destinationConnectionColumn: CategoryTable.PrimaryKey,
                                      sourceConnectionColumn: "p.CategoryId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "CategoryName",
                                                                                                 tableAlias: CategoryTable.Alias) });

            MaterialTable = new TableInfo(name: "dbo.PAYMENT_TAB",
                                         alias: "p",
                                         primaryKey: "PaymentId",
                                         columns: columns,
                                         joins: new List<TableJoin>() {  categoryJoin });
        }
    }
}
