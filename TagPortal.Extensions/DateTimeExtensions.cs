using System;
using System.Globalization;

namespace TagPortal.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dt)
        {
            DayOfWeek startOfWeek = DayOfWeek.Monday;
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        public static int WeekNumber(this DateTime value)
        {
            var currentCulture = CultureInfo.InvariantCulture;

            return currentCulture.Calendar.GetWeekOfYear(
                value,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek
            );
        }

        public static DateTime StartOfYear(this DateTime value)
        {
            return new DateTime(value.Year, 1, 1);
        }

        public static DateTime EndOfYear(this DateTime value)
        {
            return new DateTime(value.Year + 1, 1, 1).AddSeconds(-1);
        }


        public static DateTime StartOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime EndOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, DateTime.DaysInMonth(value.Year, value.Month), 23, 59, 59, 999);
        }

        //public static DateTime StartOfWeek(this DateTime value)
        //{
        //    var dayOfWeek = System.Convert.ToInt32(value.DayOfWeek);
        //    return value.AddDays(-1 * dayOfWeek);
        //}

        public static DateTime EndOfWeek(this DateTime value)
        {
            var dayOfWeek = Convert.ToInt32(value.DayOfWeek);
            return value.AddDays(7 - dayOfWeek).AddSeconds(-1);
        }
        public static DateTime StartOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
        }

        public static DateTime EndOfDay(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, value.Day, 23, 59, 59, 999);
        }
    }
}
