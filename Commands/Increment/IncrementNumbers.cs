namespace Increment
{
    public class IncrementNumbers
    {
        internal static string Process(string key, string text)
        {
            int index = 0;
            int pos = text.IndexOf(key, index);
            while (pos > 0)
            {
                index = pos;
                char a = text[pos + 2];
                char b = text[pos + 3];
                string val = a.ToString() + b.ToString();
                if (int.TryParse(val, out int nunmber))
                {
                    System.Console.Write($"Found {key}{nunmber},");
                    nunmber++;
                    string backToStr = nunmber.ToString("00");
                    text = text.Substring(0, pos + 2) + backToStr + text.Substring(pos + 4);
                }
                pos = text.IndexOf(key, index + key.Length);
            }

            return text;
        }
    }
}
