using System;
using System.Linq;

namespace TagPortal.Core.Infrastructure.Helpers
{
    public static class TagIndexHelper
    {
        private static readonly string[] ValidIndexes = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

        public static string GetIndex(string currentIndex, int nextIndexNumber)
        {
            var idx = Array.IndexOf(ValidIndexes, currentIndex);
            if ((idx + nextIndexNumber) > (ValidIndexes.Count() - 1))
                throw new ArgumentException("Number of allowed indexes exceeded");

            return ValidIndexes[idx + nextIndexNumber];
        }
    }
}
