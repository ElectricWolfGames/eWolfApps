namespace eWolfCommon.Builders
{
    public static class StringExtensions
    {
        public static string Bold(this string value)
        {
            return $"<strong>{value}</strong>";
        }

        public static string Italic(this string value)
        {
            return $"<i>{value}</i>";
        }
    }
}
