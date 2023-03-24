using System;

public static class StringExtensions
{
    public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };

    public static string Left(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        maxLength = Math.Abs(maxLength);
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    public static string Right(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return string.Empty;
        maxLength = Math.Abs(maxLength);
        return value.Length <= maxLength ? value : value.Substring(value.Length - maxLength);
    }

    public static string[] SplitWithString(this string value, string separator)
    {
        return value.Split(new string[] { separator }, StringSplitOptions.None);
    }

}