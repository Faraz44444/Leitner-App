namespace Core.Infrastructure.Database
{
    public class JoinedColumn
    {
        public JoinedColumn(string name, string alias, string tableAlias)
        {
            Name = name;
            Alias = alias;
            TableAlias = tableAlias;
        }

        public string Name { get; set; }
        public string Alias { get; set; }
        public string TableAlias { get; set; }
        public string SelectQuery => $"{TableAlias}.{Name} AS '{Alias}'";
        public string OrderByQuery => $"{TableAlias}.{Name}";
    }
}
