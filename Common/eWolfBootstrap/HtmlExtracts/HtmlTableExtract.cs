using System.Collections.Generic;

namespace eWolfBootstrap.HtmlExtracts
{
    public class HtmlTableExtract
    {
        private string _header;
        public string Name { get; set; }
        private Dictionary<string, string> _tableParts = new Dictionary<string, string>();

        public void SetHeader(string value)
        {
            _header = value;
        }

        public void AddTableLine(string[] parts)
        {
            string left = parts[0];
            string right = parts[1];

            _tableParts.Add(left, right);
        }

        public string GetValue(string key)
        {
            foreach (var pair in _tableParts)
            {
                if (pair.Key.Contains(key))
                {
                    return pair.Value;
                }
            }
            return null;
        }

        public int GetIntDisplayText(string name)
        {
            string value = GetDisplayText(name);
            if (value == "A1: 52<br />A3: 51 rebuilt + 27 new (78)")
                return 78;

            if (value == "71 (6959 <i>Peatling Hall</i>-7929 <i>Wyke Hall</i>)")
                return 71;

            if (value == "<div class=\"plainlist\">\n<ul><li>6400: 40</li>\n<li>7400: 50</li></ul>\n</div>")
                return 90;

            int val = int.Parse(value);
            return val;
        }

        public string GetDisplayText(string tag)
        {
            string name = GetValue(tag);
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            if (tag == "Whyte" && name.Contains("2-6-4"))
            {
                // need to call HTMLRemover
                int i = 0;
                i++;
                return "2-6-4";
            }

            if (name.Contains("18.8 short tons"))
            {
                int endA = name.LastIndexOf("<a");
                if (endA > 2)
                {
                    name = name.Substring(0, endA - 1);
                }
            }

            if (name.Contains("buffers"))
            {
                int endA = name.LastIndexOf("<a");
                if (endA > 2)
                {
                    name = name.Substring(0, endA - 5);
                }
            }

            string displayName = name;
            if (name.Contains("<a"))
            {
                int endA = name.LastIndexOf("</a");
                name = name.Substring(0, endA);
                int endB = name.LastIndexOf("\">") + 2;
                displayName = name.Substring(endB, name.Length - endB);
                displayName = displayName.Replace("R. A. Riddles", "Robert Riddles");
                displayName = displayName.Replace("R. A. Riddles", "Robert Riddles");
                displayName = displayName.Replace("R.A. Riddles", "Robert Riddles");
            }
            else
            {
                displayName = name.Replace("</td></tr>", string.Empty);
                displayName = displayName.Replace("</body></table>", string.Empty);
                displayName = displayName.Replace("</tbody></table>", string.Empty);
            }

            if (tag == "Whyte")
            {
                if (displayName.Contains("Pacific"))
                    return "4-6-2";
            }

            return displayName;
        }
    }
}
