using System.Collections.Generic;

namespace Web0.Dto
{
    public class ListDto<T>
    {
        public List<T> Items { get; set; }
        public ListDto() { }

        public ListDto(List<T> items)
        {
            this.Items = items;
        }
    }
}
