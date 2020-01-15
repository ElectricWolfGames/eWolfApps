using System;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Helpers
{
    public static class DataCleansing
    {
        public static string FileSafeFileName(string filename)
        {
            if (filename == null)
                filename = string.Empty;

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                filename = filename.Replace(c, '-');
            }
            return filename;
        }

        public static string RemoveAllStrings(string inbound, List<string> stringToRemove)
        {
            foreach (string str in stringToRemove)
            {
                inbound = inbound.Replace(str, "");
            }

            return inbound;
        }

        public static string RemoveDoubleSpaces(string str)
        {
            string[] words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(" ", words);
        }
    }
}