using System.Text.RegularExpressions;

namespace eWolfCommon.Helpers
{
    public static class TextHelper
    {
        public static string ToSentenceCase(string word)
        {
            return Regex.Replace(word, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }
    }
}