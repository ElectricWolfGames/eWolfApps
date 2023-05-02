using System;
using System.Text;
using System.Web;

namespace eWolfBootstrap.SiteBuilder
{
    public class HTMLIndexedItems
    {
        public Func<string, string> Body = null;
        public string BodyText;
        public string Index;

        public string Title;

        public HTMLIndexedItems(string title, Func<string, string> body)
        {
            Title = title;
            Body = body;
            Index = title.Replace(" ", "");
        }

        public HTMLIndexedItems(string title, string bodyText)
        {
            Title = title;
            BodyText = bodyText;
            Index = title.Replace(" ", "");
        }

        public string BuildItem(string size)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a id='{Index}'></a>");
            sb.AppendLine("<div class ='row bg-light'>");
            if (size != "na")
                sb.AppendLine($"<div class='col-md-{size}'>");

            sb.AppendLine("<div class='section-title'>");
            sb.Append("<div class='text-left'>");
            sb.AppendLine($"<h2>{Title}</h2>");
            sb.AppendLine("</div>");

            if (Body != null)
                sb.Append(Body.Invoke(string.Empty));
            else
                sb.Append(BodyText);

            sb.AppendLine("</div>");
            if (size != "na")
                sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<br />");
            return sb.ToString();
        }
    }
}