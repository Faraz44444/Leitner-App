using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class EnumExtension
    {
        public static bool Empty<T>(this T enumVal)
            where T: Enum
        {
            return !Enum.IsDefined(typeof(T), enumVal);
        }

        public static List<T> GetEnumValueList<T>()
            where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}
