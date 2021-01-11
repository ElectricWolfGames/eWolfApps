namespace eWolfBootstrap.Helpers
{
    public static class HTMLExtraction
    {
        public static string GetTagLine(string raw, string tagName, int startingindex)
        {
            int headerindex = raw.IndexOf($"<{tagName}", startingindex);
            int headerindexEnd = raw.IndexOf($"</{tagName}>", headerindex) + tagName.Length + 3;
            string headerLine = raw.Substring(headerindex, headerindexEnd - headerindex);
            return headerLine;
        }
    }
}