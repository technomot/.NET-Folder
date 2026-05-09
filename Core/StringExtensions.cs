namespace Core
{
    public static class StringExtensions
    {
        public static int WordCount(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return 0;
            return str.Trim().Split(' ').Length;
        }

        public static string ToCurrencyString(this double value)
        {
            return $"${value:F2}";
        }
    }
}