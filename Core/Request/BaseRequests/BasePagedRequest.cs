using Domain.Enum;
using System;

namespace Core.Request.BaseRequests
{
    public interface IBaseRequestPaged
    {
        int ItemsPerPage { get; set; }
        int CurrentPage { get; set; }
        string TableAlias { get; set; }
        string OrderBy { get; set; }
        EnumOrderByDirection OrderByDirection { get; set; }
        string WhereSql();
    }


    public abstract class BaseRequestPaged : BaseRequest, IBaseRequestPaged
    {
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string OrderBy { get; set; }
        public EnumOrderByDirection OrderByDirection { get; set; }
        public virtual string TableAlias { get; set; }
        public virtual string WhereSql()
        {
            throw new NotImplementedException();
        }
    }

}
