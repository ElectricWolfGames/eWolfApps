using System.Text;

namespace eWolfBootstrap.Chats
{
    public class TableHolder : TableHolderBase
    {
        public string Output()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table'>");
            sb.Append("<thead>");
            sb.Append("<tr>");

            foreach (string header in _header)
            {
                sb.Append($"<th>{header}</th>");
            }

            sb.Append("</tr>");
            sb.Append("</thead>");

            sb.Append("<body>");

            foreach (var row in _rows)
            {
                sb.Append("<tr>");
                foreach (string field in row)
                {
                    sb.Append($"<th>{field}</th>");
                }
                sb.Append("</tr>");
            }

            sb.Append("</body>");

            return sb.ToString();
        }
    }
}
