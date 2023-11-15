using System;
using System.Globalization;

public static class DateTimeExtensions
{
    public static DateTime StartOfWeek(this DateTime dt)
    {
        DayOfWeek startOfWeek = DayOfWeek.Monday;
        int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        return dt.AddDays(-1 * diff).Date;
    }

    public static DateTime StartOfDay(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
    }

    public static DateTime EndOfDay(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, 23, 59, 59);
    }

    public static DateTime EndOfWeek(this DateTime dt)
    {
        return dt.StartOfWeek().AddDays(6);
    }

    public static DateTime StartOfWeek(this DateTime? dt)
    {
        DayOfWeek startOfWeek = DayOfWeek.Monday;
        int diff = (7 + (dt.Value.DayOfWeek - startOfWeek)) % 7;
        return dt.Value.AddDays(-1 * diff).Date;
    }

    public static DateTime StartOfDay(this DateTime? dt)
    {
        return new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, 0, 0, 0);
    }

    public static DateTime EndOfDay(this DateTime? dt)
    {
        return new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, 23, 59, 59);
    }

    public static int WeekOfYear(this DateTime dt)
    {
        DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(dt);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            dt = dt.AddDays(3);
        }

        // Return the week of our adjusted day
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
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

    public static bool IsBetween(this DateTime value, DateTime start, DateTime end, bool strict = false)
    {
        return (start < value && value < end) || !strict && (start == value || end == value);
    }
    public static bool IsBetween(this DateTime value, TimeSpan startTime, TimeSpan endTime, bool strict = false)
    {
        return (startTime < value.TimeOfDay && value.TimeOfDay < endTime) || !strict && (startTime == value.TimeOfDay || endTime == value.TimeOfDay);
    }
    public static bool IsBetween(this TimeSpan value, TimeSpan startTime, TimeSpan endTime, bool strict = false)
    {
        return (startTime < value && value < endTime) || !strict && (startTime == value || endTime == value);
    }

    public static bool Empty(this DateTime value)
    {
        return value == DateTime.MinValue;
    }
    public static bool Empty(this DateTime? value)
    {
        return !value.HasValue || value == DateTime.MinValue;
    }


    public enum EnumDatePart
    {
        Millisecond = 1,
        Second = 2,
        Minute = 3,
        Hour = 4,
    }
    public static DateTime RoundTo(this DateTime value, EnumDatePart datepart, int roundTo = 1)
    {
        double x = roundTo;

        if (datepart >= EnumDatePart.Millisecond) x *= 10000; // 10000ticks = 1ms 
        if (datepart >= EnumDatePart.Second) x *= 1000; // 1000ms = 1s 
        if (datepart >= EnumDatePart.Minute) x *= 60;   // 60s = 1min
        if (datepart >= EnumDatePart.Hour) x *= 60;     // 60min = 1h

        return new DateTime(ticks: (long)(Math.Round(value.Ticks / x) * x), kind: value.Kind);
    }
}
