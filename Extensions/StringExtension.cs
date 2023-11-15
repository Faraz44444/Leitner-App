public static class StringExtension
{
    private const int shortStringLength = 20;
    private const int mediumStringLength = 40;
    public const int longStringLength = 60;

    public static bool Empty(this string str, bool allowWhiteSpace = false)
    {
        if (allowWhiteSpace) return string.IsNullOrEmpty(str);
        else return string.IsNullOrWhiteSpace(str);
    }
    public static string TrimmedShort(this string str)
    {
        if (str.Empty()) return "";
        if (str.Length < shortStringLength) return str;
        return str[..(shortStringLength - 3)] + "...";
    }
    public static string TrimmedMedium(this string str)
    {
        if (str.Empty()) return "";
        if(str.Length < mediumStringLength) return str;
        return str[..(mediumStringLength - 3)] + "...";
    }
    public static string Trimmedlong(this string str)
    {
        if (str.Empty()) return "";
        if(str.Length < longStringLength) return str;
        return str[..(longStringLength - 3)] + "...";  
    }
}