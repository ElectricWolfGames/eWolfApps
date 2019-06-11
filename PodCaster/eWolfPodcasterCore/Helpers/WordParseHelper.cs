using System.Collections.Generic;
using System.Text;

namespace eWolfPodcasterCore.Helpers
{
    public static class WordParseHelper
     {
        public static string[] GetWordsPerLine(string text, int lineLength)
        {
            List<string> outputLines = new List<string>();
            string[] words = text.Split(' ');

            StringBuilder line = new StringBuilder();

            foreach (string word in words)
            {
                if (line.Length > lineLength)
                {
                    outputLines.Add(line.ToString().Trim());
                    line = new StringBuilder();
                }
                line.Append(word);
                line.Append(" ");
            }

            outputLines.Add(line.ToString().Trim());

            return outputLines.ToArray();
        }
    }
}
