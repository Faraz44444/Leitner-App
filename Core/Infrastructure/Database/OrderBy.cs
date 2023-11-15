using System.Reflection.Metadata;

namespace Core.Infrastructure.Database
{
    public class OrderBy
    {
        public OrderBy(int value, string query)
        {
            Value = value;
            Query = query;
        }

        public int Value { get; set; }
        public string Query { get; set; }
    }
}
