
namespace TagPortal.Core.Request
{
    public abstract class BaseRequest<T>
    {
        public EnumOrderByDirection OrderByDirection { get; set; }
        public T OrderBy { get; set; }
    }

    public enum EnumOrderByDirection
    {
        Asc = 1,
        Desc = 2
    }
}
