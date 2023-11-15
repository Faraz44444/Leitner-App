using System.Collections.Generic;

namespace Core.Infrastructure.Database
{
    public class TableColumn
    {
        public TableColumn(string name, string tableAlias, string alias)
        {
            Name = name;
            TableAlias = tableAlias;
            Alias = alias;
        }

        public string Name { get; set; }
        public string TableAlias { get; set; }
        public string Alias { get; set; }
        public string SelectQuery { get { return $"{TableAlias}.{Name} AS '{Alias}'"; } }
        public string OrderByQuery { get { return $"{TableAlias}.{Name}"; } }
    }
}
