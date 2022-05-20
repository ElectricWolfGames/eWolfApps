using System;
using System.Text;

namespace eWolfBootstrap.SiteBuilder
{
    public class HTMLIndexedItems
    {
        public Func<string, string> Body;

        public string Index;

        public string Title;

        public HTMLIndexedItems(string title, Func<string, string> body)
        {
            Title = title;
            Body = body;
            Index = title.Replace(" ", "");
        }

        public string BuildItem()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a id='{Index}'></a>");
            sb.AppendLine("<div class ='row bg-light'>");
            sb.AppendLine($"<div class='col-md-12'>");

            sb.AppendLine("<div class='section-title'>");
            sb.Append("<div class='text-left'>");
            sb.AppendLine($"<h2>{Title}</h2>");
            sb.AppendLine("</div>");

            sb.Append(Body.Invoke(string.Empty));

            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<br />");
            return sb.ToString();
        }
    }
}