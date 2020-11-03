using System.Collections.Generic;

namespace eWolfBootstrap.Helpers
{
    public static class HTMLRemover
    {
        // "<span class=\"nowrap\"><a href=\"/wiki/2-6-4\" title=\"2-6-4\">2-6-4</a><a href=\"/wiki/Side_tank_locomotive\" class=\"mw-redirect\" title=\"Side tank locomotive\"><abbr title=\"Side tank\">T</abbr></a></span></td></tr>"

        public static string[] CreateTagGroups(string text)
        {
            text += "   ";
            List<string> tags = new List<string>();

            bool contine = true;
            int startingIndex = 0;

            while (contine)
            {
                int start = text.IndexOf('<', startingIndex);
                if (start == -1)
                    break;

                startingIndex = start;
                int end = text.IndexOf('>', startingIndex);
                if (end == -1)
                    break;

                int startWordEnd = text.IndexOf(' ', start);
                if (startWordEnd > end)
                {
                    startWordEnd = end;
                }
                if (startWordEnd == -1)
                {
                    break;
                }

                string tagWord = text.Substring(start + 1, (startWordEnd - 1) - start);

                string endTag = $"</{tagWord}>";

                startingIndex = end + 1;
                end = text.IndexOf(endTag, startingIndex);
                if (end == -1)
                    break;
                startingIndex = end + 1;

                string partA = text.Substring(start, (end + endTag.Length) - start);
                tags.Add(partA);
            }

            return tags.ToArray();
        }

        public static string GetTextFromTagPair(string text)
        {
            int startingIndex = 0;
            int start = text.IndexOf('>', startingIndex);
            if (start == -1)
                return string.Empty;

            startingIndex = start;
            int end = text.IndexOf('<', startingIndex);
            if (end == -1)
                return string.Empty;

            string word = text.Substring(start + 1, (end - 1) - start);
            return word;
        }

        public static string RemoveAll(string text)
        {
            // "45&#160;ft <span class=\"frac nowrap\">9<span class=\"visualhide\">&#160;</span><sup>3</sup>&#8260;<sub>4</sub></span>&#160;in (13.96&#160;m)"
            int end = text.IndexOf('<', 0);
            if (end < 1)
            {
                return text;
            }
            string word = text.Substring(0, (end - 1));

            return word;
        }

        public static string RemoveKeepInner(string text, string tagName)
        {
            string[] tagGroups = CreateTagGroups(text);

            foreach (string line in tagGroups)
            {
                if (!line.Contains(tagName))
                    continue;

                return GetTextFromTagPair(line);
            }

            return text;
        }
    }
}
