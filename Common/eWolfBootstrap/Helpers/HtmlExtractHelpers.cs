using eWolfBootstrap.HtmlExtracts;

namespace eWolfBootstrap.Helpers
{
    public static class HtmlExtractHelpers
    {
        public static HtmlTableExtract GetTable(string file, string section)
        {
            HtmlTableExtract hte = new HtmlTableExtract();

            int index = file.IndexOf(section);

            string header = file.Substring(0, index);
            int lastIndex = header.LastIndexOf("<table");

            int headerindex = file.IndexOf("/table");

            string sectionText = file.Substring(lastIndex, (index + headerindex) - lastIndex);

            string[] lines = sectionText.Split("<tr>");

            foreach (string line in lines)
            {
                string[] parts = line.Split("<td>");
                if (parts.Length == 2)
                {
                    hte.AddTableLine(parts);
                }
                else
                {
                    hte.SetHeader(line);
                }
            }
            return hte;
        }
    }
}
