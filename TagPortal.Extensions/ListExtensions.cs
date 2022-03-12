using System.Collections.Generic;
using System.Linq;

namespace TagPortal.Extensions
{
    public static class ListExtensions
    {
        public static string Join(this IEnumerable<string> arr, string separator, bool ignoreWhitespace = true)
        {
            arr = arr.Where(x => !ignoreWhitespace || !string.IsNullOrWhiteSpace(x));
            return string.Join(separator, arr);
        }
        public static string Join(this List<string> arr, string separator, bool ignoreWhitespace = true)
        {
            var items = arr.Where(x => !ignoreWhitespace || !string.IsNullOrWhiteSpace(x));
            return string.Join(separator, items);
        }
        public static string Join(this string[] arr, string separator, bool ignoreWhitespace = true)
        {
            var items = arr.Where(x => !ignoreWhitespace || !string.IsNullOrWhiteSpace(x));
            return string.Join(separator, items);
        }

        public static bool Empty<T>(this IEnumerable<T> arr)
        {
            return arr == null || arr.Count() == 0;
        }
        public static bool Empty<T>(this List<T> arr)
        {
            return arr == null || arr.Count() == 0;
        }
        public static bool Empty<T>(this T[] arr)
        {
            return arr == null || arr.Count() == 0;
        }
    }
}