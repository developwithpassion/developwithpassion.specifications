namespace developwithpassion.specifications.extensions
{
    public static class StringExtensions
    {
        public static string format_using(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}