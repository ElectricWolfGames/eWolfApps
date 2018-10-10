namespace eWolfCommon.Builders
{
    public static class HTMLStringExtensions
    {
        public static string HTMLBold(this string value)
        {
            return $"<strong>{value}</strong>";
        }

        public static string HTMLItalic(this string value)
        {
            return $"<i>{value}</i>";
        }
    }
}
