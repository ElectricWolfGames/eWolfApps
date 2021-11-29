using System.Collections.Generic;
using eWolfBootstrap.Helpers;

namespace eWolfBootstrap.HtmlExtracts
{
    public class HtmlTableExtract
    {
        private string _header;
        private Dictionary<string, string> _tableParts = new Dictionary<string, string>();
        public string DisplayName { get; set; }
        public string Name { get; set; }

        public void AddTableLine(string[] parts)
        {
            string left = parts[0];
            string right = parts[1];

            _tableParts.Add(left, right);
        }

        public string GetDisplayText(string tag)
        {
            string name = GetValue(tag);
            if (string.IsNullOrWhiteSpace(name))
                return string.Empty;

            if (tag == "Operators" && name == "<a href=\"/wiki/British_Railways\" class=\"mw-redirect\" title=\"British Railways\">British Railways</a></td></tr>")
                return "British Railways";

            if (tag == "Operators" && name == "<div class=\"plainlist\"><ul><li><a href=\"/wiki/Great_Central_R")
                return "Great Central Railways";

            if (tag == "Operators" && name == "<div class=\"plainlist\"><ul><li><a href=\"/wiki/Great_Western_Railway\" title=\"Great Western")
                return "Great Central Railways";

            if (tag == "Length" && name == "<div class=\"plainlist\"><ul><li><i>Streamlined:</i> 73&#160;ft <span class=\"frac nowrap\">9<span class=\"visualhide\">&#160;</span><sup>3</sup>&#8260;<sub>4</sub></span>&#160;in (22.498&#160;m)</li><li><i>Conventional:</i> 73&#160;ft <span class=\"frac nowrap\">10<span class=\"visualhide\">&#160;</span><sup>1</sup>&#8260;<sub>4</sub></span>&#160;in (22.511&#160;m)</li></ul></div></td></tr>")
            {
                return "73 ft 10 1⁄4 in (22.511 m) Conventional";
            }

            if (tag == "Length" && name.Contains("visualhide"))
            {
                return HTMLRemover.RemoveAll(name);
            }

            if (tag == "Whyte" && name.Contains("2-6-4"))
            {
                if (name == "<a href=\"/wiki/2-6-4T\" class=\"mw-redirect\" title=\"2-6-4T\">2-6-4T</a></td></tr>")
                {
                    return "2-6-4T";
                }

                return "2-6-4" + HTMLRemover.RemoveKeepInner(name, "abbr");
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

        public int GetIntDisplayText(string name)
        {
            string value = GetDisplayText(name);
            if (value == "A1: 52<br />A3: 51 rebuilt + 27 new (78)")
                return 78;

            if (value == "71 (6959 <i>Peatling Hall</i>-7929 <i>Wyke Hall</i>)")
                return 71;

            if (value == "<div class=\"plainlist\">\n<ul><li>6400: 40</li>\n<li>7400: 50</li></ul>\n</div>")
                return 90;

            if (value == "40 (9K / C13)<br />12 (9L / C14)")
                return 40;

            if (value == "<div class=\"plainlist\"><ul><li>8K: 126</li><li>8M: 19</li><li>ROD 2-8-0: 521</li></ul></div>")
                return 126;

            if (value == "&#91;1&#93;")
                return 91;

            if (string.IsNullOrWhiteSpace(value))
                return 0;

            int val = 0;
            int.TryParse(value, out val);

            return val;
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

        public void SetHeader(string value)
        {
            _header = value;
        }
    }
}