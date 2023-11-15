using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ListExtension
{
    public static bool Empty<T>(this List<T> val)
    {
        return val == null || !val.Any();
    }

    public static bool Empty<T>(this T[] val)
    {
        return val == null || !val.Any();
    }

    public static bool Empty<T>(this IEnumerable<T> val)
    {
        return val == null || !val.Any();
    }

    public static string Join(this List<string> list, string separator, bool ignoreEmpty = true)
    {
        if (list.Empty()) return "";

        var items = list.Where(x => !ignoreEmpty || !string.IsNullOrWhiteSpace(x));
        if (!items.Any()) return "";

        return string.Join(separator, items);
    }
    
    public static string Join(this string[] list, string separator, bool ignoreEmpty = true)
    {
        if (list.Empty()) return "";

        var items = list.Where(x => !ignoreEmpty || !string.IsNullOrWhiteSpace(x));
        if (!items.Any()) return "";

        return string.Join(separator, items);
    }
    
    public static string Join(this IEnumerable<string> list, string separator, bool ignoreEmpty = true)
    {
        if (list.Empty()) return "";

        var items = list.Where(x => !ignoreEmpty || !string.IsNullOrWhiteSpace(x));
        if (!items.Any()) return "";

        return string.Join(separator, items);
    }
    
    public static string Join(this IEnumerable<int> list, string separator, bool ignoreEmpty = true)
    {
        if (list.Empty()) return "";

        var strList = list.Select(x => x.ToString());
        strList = strList.Where(x => !ignoreEmpty || !string.IsNullOrWhiteSpace(x));
        if (!strList.Any()) return "";

        return string.Join(separator, strList);
    }

    public static T LastIfOutOfRange<T>(this IEnumerable<T> list, int index)
    {
        if (list.Count() <= index) return list.Last();
        else return list.ToArray()[index];
    }

    public static bool TryGet<T>(this IEnumerable<T> list, int index, out T value)
    {
        value = default(T);
        if (list.Count() > index) value = list.ToArray()[index];
        return list.Count() > index;
    }
}


