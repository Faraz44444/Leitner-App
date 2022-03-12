using System.Collections.Generic;

namespace TbxPortal.Web.Dto
{
    public class ListDto<T>
    {
        public List<T> Data { get; set; }
    }
}