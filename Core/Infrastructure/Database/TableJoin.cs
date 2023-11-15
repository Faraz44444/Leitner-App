using System.Collections.Generic;

namespace Core.Infrastructure.Database
{
    public class TableJoin
    {
        public JoinType Type { get; set; }
        public TableInfo Source { get; set; }
        public TableInfo Destination { get; set; }
        public string SourceConnectionColumn { get; set; }
        public string DestinationConnectionColumn { get; set; }
        public List<JoinedColumn> Columns { get; set; } = new List<JoinedColumn>();
        public string JoinQuery
        {
            get { return GetJoinQuery(); }
        }

        public TableJoin(JoinType type,
                         TableInfo destination,
                         TableInfo source,
                         string destinationConnectionColumn,
                         string sourceConnectionColumn,
                         List<JoinedColumn> selectColumns = null)
        {
            Type = type;
            Destination = destination;
            Source = source;
            DestinationConnectionColumn = Destination.Alias + "." + destinationConnectionColumn;
            SourceConnectionColumn = (Source == null ? "" : Source.Alias) + sourceConnectionColumn;
            Columns = selectColumns;
        }

        public string GetJoinQuery()
        {
            return string.Join(" ",
                new List<string>() {
                    Type.Name,
                    Destination.Name,
                    Destination.Alias,
                    "ON",
                    DestinationConnectionColumn,
                    "=",
                    SourceConnectionColumn
                });
        }
    }
}
