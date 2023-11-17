using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Infrastructure.Database
{
    public class TableInfo
    {
        public string Name { get; set; }
        public string PrimaryKey { get; set; }
        public string Alias { get; set; }
        public string Description { get; set; }
        public List<TableColumn> Columns { get; set; }
        public List<TableJoin> Joins { get; set; }
        public string GetQuery { get; set; }
        public string InsertQuery { get; set; }
        public string UpdateQuery { get; set; }
        public string DeleteQuery { get; set; }
        public string FirstRecordDateQuery { get; set; }
        public string LastRecordDateQuery { get; set; }
        public TableInfo(string name,
                         string alias,
                         string primaryKey,
                         List<TableColumn> columns,
                         List<TableJoin> joins = null)
        {
            Name = name;
            Alias = alias;
            PrimaryKey = primaryKey;
            Columns = columns;
            Joins = joins;
            SetGetQuery(false);
            SetInsertQuery();
            SetUpdateQuery();
            SetDeleteQuery();
            SetGetFirstRecordQuery();
            SetGetLastRecordQuery();
        }
        public virtual void SetGetQuery(bool usePaging)
        {
            var columns = new List<string>(Columns.Select(x => x.SelectQuery).ToList());
            Joins?.ForEach(x =>
            {
                if (!x.Columns.Empty())
                    columns.AddRange(x.Columns?.Select(x => x.SelectQuery));
            });
            var joinQueries = Joins != null ? string.Join("\n ", Joins?.Select(x => x.JoinQuery)) : "";

            GetQuery = string.Join("\n ", new List<string>
            {
                "SELECT",
                string.Join(", ", columns),
                {UsePagingColumn(usePaging)},
                "FROM",
                Name + " " + Alias,
                joinQueries
});
        }
        public virtual void SetInsertQuery()
        {
            var columnVariables = new List<string>(Columns.Select(x => x.Name).ToList());
            for (var i = 0; i < columnVariables.Count; i++)
            {
                if (columnVariables[i] == PrimaryKey)
                {
                    columnVariables.RemoveAt(i);
                    i--;
                    continue;
                }
                columnVariables[i] = "@" + columnVariables[i];
            }
            var columns = new List<string>(Columns.Where(x => x.Name != PrimaryKey).Select(x => x.Name).ToList());
            InsertQuery = string.Join("\n ", new List<string>()
            {
                "INSERT INTO",
                Name,
                "(", string.Join(", ", columns), ")",
                "OUTPUT INSERTED."+PrimaryKey,
                "VALUES",
                "(", string.Join(", ", columnVariables), ")"
            });
        }
        public virtual void SetUpdateQuery()
        {
            var columns = new List<string>(Columns.Select(x => x.Name));
            for (var i = 0; i < columns.Count; i++)
            {
                if (columns[i] == PrimaryKey)
                {
                    columns.RemoveAt(i);
                    i--;
                    continue;
                }
                columns[i] = columns[i] + " = @" + columns[i];
            }
            UpdateQuery = string.Join("\n ", new List<string>()
            {
                "UPDATE",
                Name,
                "SET",
                string.Join(", ", columns),
                "WHERE",
                PrimaryKey + " = " + "@" + PrimaryKey
            });
        }
        public virtual void SetDeleteQuery()
        {
            DeleteQuery = string.Join("\n ", new List<string>()
            {
                "DELETE FROM",
                Name,
                "WHERE",
                PrimaryKey + " = " + "@" + PrimaryKey
            });
        }
        public virtual void SetGetFirstRecordQuery()
        {
            var clientFilter = !Columns.Any(x => x.Name == "ClientId") ? "" : "ClientId = @ClientId";

            FirstRecordDateQuery = !Columns.Any(x => x.Name == "CreatedAt") ? "" :
            String.Join("\n", new List<string>()
            {
                "SELECT MIN(CreatedAt)",
                "FROM " +  Name,
                "WHERE",
                clientFilter
            });
        }
        public virtual void SetGetLastRecordQuery()
        {
            var clientFilter = !Columns.Any(x => x.Name == "ClientId") ? "" : "ClientId = @ClientId";

            LastRecordDateQuery = !Columns.Any(x => x.Name == "CreatedAt") ? "" :
            String.Join("\n", new List<string>()
            {
                "SELECT MAX(CreatedAt)",
                "FROM " +  Name,
                "WHERE",
                clientFilter
            });
        }
        protected static string UsePagingColumn(bool usePaging)
        {
            if (usePaging)
                return " , COUNT(*) OVER() AS 'TotalNumberOfItems' ";
            else
                return "";
        }
        protected static string UsePagingOffset(bool usePaging)
        {
            if (usePaging)
                return "OFFSET (@CurrentPage * @ItemsPerPage) ROWS FETCH NEXT @ItemsPerPage ROWS ONLY";
            else
                return "";
        }

        public string GetOrderByQuery(string orderBy, EnumOrderByDirection orderByDirection)
        {
            if (orderBy == null && !PrimaryKey.Empty())
                return $"ORDER BY {Alias}.{PrimaryKey} DESC";
            var tableOrderByColumn = Columns.FirstOrDefault(x => x.Name == orderBy);
            if (tableOrderByColumn != null)
                return $"ORDER BY {tableOrderByColumn.OrderByQuery} {(orderByDirection == EnumOrderByDirection.Desc ? "DESC" : "ASC")}";
            if (Joins != null)
                foreach (var join in Joins)
                {
                    var match = join.Columns?.FirstOrDefault(x => x.Alias == orderBy);
                    if (match != null)
                        return $"ORDER BY {match.OrderByQuery}  {(orderByDirection == EnumOrderByDirection.Desc ? "DESC" : "ASC")}";
                }
            return "";
        }
    }
}
