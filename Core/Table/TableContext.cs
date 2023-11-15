using Core.Infrastructure.Database;
using Domain.Model.ActionLog;
using Domain.Model.BaseModels;
using Domain.Model.Business;
using Domain.Model.Category;
using Domain.Model.Client;
using Domain.Model.Payment;
using Domain.Model.Role;
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
        internal TableInfo PaymentTable;
        internal TableInfo PaymentTotalTable;

        internal TableContext()
        {
            TableSetter();
        }

        internal void TableSetter()
        {
            ClientTableSetter();
            UserTableSetter();
            RoleTableSetter();
            UserClientTableSetter();
            RolePermissionTableSetter();
            ActionLogSetter();
            ErrorLogSetter();
            EventLogSetter();
            BusinessTableSetter();
            CategoryTableSetter();
            PayemntPriorityTableSetter();
            PayemntTableSetter();
            PaymentTotalTableSetter();
        }

        internal void ClientTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(ClientModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "c", alias: x.Name));
               });
            ClientTable = new TableInfo(name: "dbo.CLIENT_TAB",
                                        alias: "c",
                                        primaryKey: "ClientId",
                                        columns: columns);

        }
        internal void RoleTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(RoleModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "r", alias: x.Name));
               });
            RoleTable = new TableInfo(name: "dbo.ROLE_TAB",
                                      alias: "r",
                                      primaryKey: "RoleId",
                                      columns: columns);
        }
        internal void UserTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(UserModel).GetProperties().
                Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
                x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
                {
                    columns.Add(new TableColumn(name: x.Name, tableAlias: "u", alias: x.Name));
                });
            columns = columns.Where(x => x.Name != "ClientId").ToList();
            var join = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                     destination: ClientTable,
                                     source: null,
                                     destinationConnectionColumn: ClientTable.PrimaryKey,
                                     sourceConnectionColumn: "u.CurrentClientId",
                                     selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                alias: "CurrentClientName",
                                                                                                tableAlias: ClientTable.Alias) }); ;
            UserTable = new TableInfo(name: "dbo.USER_TAB",
                                      alias: "u",
                                      primaryKey: "UserId",
                                      columns: columns,
                                      joins: new List<TableJoin>() { join });
        }
        internal void UserClientTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(UserClientModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "uc", alias: x.Name));
               });
            columns = columns.Where(x => !typeof(PagedBaseModel).GetProperties().Any(y => y.Name == x.Name && x.Name != "ClientId")).ToList();
            var join = new TableJoin(new JoinType(EnumJoinType.InnerJoin),
                                     ClientTable,
                                     null,
                                     ClientTable.PrimaryKey,
                                     "uc.ClientId");
            var join2 = new TableJoin(new JoinType(EnumJoinType.InnerJoin),
                                      UserTable,
                                      null,
                                      UserTable.PrimaryKey,
                                      "uc.UserId");
            var join3 = new TableJoin(new JoinType(EnumJoinType.InnerJoin),
                                      RoleTable,
                                      null,
                                      RoleTable.PrimaryKey,
                                      "uc.RoleId");
            UserClientTable = new TableInfo(name: "dbo.USER_CLIENT_TAB",
                                            alias: "uc",
                                            primaryKey: "",
                                            columns: columns,
                                            joins: new List<TableJoin>() { join, join2, join3 });
        }
        internal void RolePermissionTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(RolePermissionModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "rp", alias: x.Name));
               });
            RolePermissionTable = new TableInfo(name: "dbo.ROLE_PERMISSION_TAB",
                                                alias: "rp",
                                                primaryKey: "RoleId",
                                                columns: columns);
        }
        internal void ActionLogSetter()
        {
            var columns = new List<TableColumn>();
            typeof(ActionLogModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
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
            typeof(ErrorLogModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
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
            typeof(EventLogModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "evl", alias: x.Name));
               });
            EventLogTable = new TableInfo(name: "dbo.EVENT_LOG_TAB",
                                          alias: "evl",
                                          primaryKey: "EventLogId",
                                          columns: columns);
        }
        internal void BusinessTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(BusinessModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "b", alias: x.Name));
               });
            BusinessTable = new TableInfo(name: "dbo.BUSINESS_TAB",
                                          alias: "b",
                                          primaryKey: "BusinessId",
                                          columns: columns);
        }
        internal void CategoryTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(CategoryModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "ca", alias: x.Name));
               });
            CategoryTable = new TableInfo(name: "dbo.CATEGORY_TAB",
                                          alias: "ca",
                                          primaryKey: "CategoryId",
                                          columns: columns);
        }
        internal void PayemntPriorityTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(PaymentPriorityModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "pp", alias: x.Name));
               });
            PaymentPriorityTable = new TableInfo(name: "dbo.PAYMENT_PRIORITY_TAB",
                                                 alias: "pp",
                                                 primaryKey: "PaymentPriorityId",
                                                 columns: columns);
        }
        internal void PayemntTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(PaymentModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "p", alias: x.Name));
               });

            var join1 = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: ClientTable,
                                      source: null,
                                      destinationConnectionColumn: ClientTable.PrimaryKey,
                                      sourceConnectionColumn: "p.ClientId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "ClientName",
                                                                                                 tableAlias: ClientTable.Alias) });
            var join2 = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: BusinessTable,
                                      source: null,
                                      destinationConnectionColumn: BusinessTable.PrimaryKey,
                                      sourceConnectionColumn: "p.BusinessId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "BusinessName",
                                                                                                 tableAlias: BusinessTable.Alias) });
            var join3 = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: CategoryTable,
                                      source: null,
                                      destinationConnectionColumn: CategoryTable.PrimaryKey,
                                      sourceConnectionColumn: "p.CategoryId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "CategoryName",
                                                                                                 tableAlias: CategoryTable.Alias) });
            var join4 = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: PaymentPriorityTable,
                                      source: null,
                                      destinationConnectionColumn: PaymentPriorityTable.PrimaryKey,
                                      sourceConnectionColumn: "p.PaymentPriorityId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "PaymentPriorityName",
                                                                                                 tableAlias: PaymentPriorityTable.Alias) });

            PaymentTable = new TableInfo(name: "dbo.PAYMENT_TAB",
                                         alias: "p",
                                         primaryKey: "PaymentId",
                                         columns: columns,
                                         joins: new List<TableJoin>() { join1, join2, join3, join4 });
        }
        internal void PaymentTotalTableSetter()
        {
            var columns = new List<TableColumn>();
            typeof(PaymentTotalModel).GetProperties().Where(x => Attribute.IsDefined(x, typeof(IsTableColumnAttribute)) &&
               x.GetCustomAttribute<IsTableColumnAttribute>().IsTableColumn).ToList().ForEach(x =>
               {
                   columns.Add(new TableColumn(name: x.Name, tableAlias: "tp", alias: x.Name));
               });


            var join1 = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: ClientTable,
                                      source: null,
                                      destinationConnectionColumn: ClientTable.PrimaryKey,
                                      sourceConnectionColumn: "tp.ClientId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "ClientName",
                                                                                                 tableAlias: ClientTable.Alias) });
            var join2 = new TableJoin(type: new JoinType(EnumJoinType.LeftJoin),
                                      destination: BusinessTable,
                                      source: null,
                                      destinationConnectionColumn: BusinessTable.PrimaryKey,
                                      sourceConnectionColumn: "tp.BusinessId",
                                      selectColumns: new List<JoinedColumn>() { new JoinedColumn(name: "Name",
                                                                                                 alias: "BusinessName",
                                                                                                 tableAlias: BusinessTable.Alias) });
            PaymentTotalTable = new TableInfo(name: "dbo.PAYMENT_TOTAL_TAB",
                                              alias: "tp",
                                              primaryKey: "PaymentTotalId",
                                              columns: columns,
                                              joins: new List<TableJoin>() { join1, join2 });
        }
    }
}
