namespace Football.Domain.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            return string.Concat($"{value[0]}".ToUpper(), value.AsSpan(1));
        }

        public static string Left(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
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
            if (string.IsNullOrEmpty(value)) return new string[0];
            return value.Split(new string[] { separator }, StringSplitOptions.None);
        }
    }
}
