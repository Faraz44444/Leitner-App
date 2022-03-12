using System;
using System.Collections.Generic;
using System.Linq;

namespace TagPortal.Extensions
{
    public static class DecimalExtensions
    {
        public static decimal Truncate(this decimal number)
        {
            return number > 0 ? Math.Truncate(100 * number) / 100 : 0;
        }
    }
}
