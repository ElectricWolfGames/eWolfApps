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
                string stringAfterKey = a.ToString() + b.ToString();
                if (int.TryParse(stringAfterKey, out int nunmber))
                {
                    nunmber++;
                    string backToStr = nunmber.ToString("00");
                    text = text.Substring(0, pos + 2) + backToStr + text.Substring(pos + 4);

                    System.Console.Write($"Found {key}{stringAfterKey} > {backToStr} , ");
                }
                pos = text.IndexOf(key, index + key.Length);
            }

            return text;
        }
    }
}
