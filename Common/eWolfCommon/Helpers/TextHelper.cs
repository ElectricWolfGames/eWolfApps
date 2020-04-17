using System.Text.RegularExpressions;

namespace eWolfCommon.Helpers
{
    public static class TextHelper
    {
        public static string ToSentenceCase(string word)
        {
            return Regex.Replace(word, "[a-z][A-Z]", m => $"{m.Value[0]} {char.ToLower(m.Value[1])}");
        }

        public static string ConvertTextToUnderscores(string sentence)
        {
            sentence = sentence.Replace('-', ' ');
            sentence = sentence.Replace("\'", "");
            string[] words = sentence.Split(' ');

            if (words.Length > 1)
            {
                for (int i = 1; i < words.Length; i++)
                {
                    words[i] = words[i].ToLower();
                }
            }

            return string.Join("_", words);
        }

        public static string RemovedSpaces(string sentence)
        {
            return sentence.Replace(" ", "");
        }

        public static string ConvertTextToStringVar(string sentence)
        {
            string words = ToSentenceCase(sentence);

            string wordSpaces = RemovedSpaces(words);
            wordSpaces = char.ToLower(wordSpaces[0]) + wordSpaces.Substring(1);

            string text = $"const string {wordSpaces} = \"{words}\";";

            return text;
            
        }

    }
}