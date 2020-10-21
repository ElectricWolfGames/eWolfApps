using System.Text;

namespace eWolfBootstrap.Chats
{
    public class SortableTableHolder : TableHolderBase
    {
        // https://examples.bootstrap-table.com/#column-options/sortable.html

        public string Output()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='table' id='table' data-toggle='table' data-height='800' >");
            sb.Append("<thead>");
            sb.Append("<tr>");

            foreach (string header in _header)
            {
                sb.Append($"<th data-field='{header}' data-sortable='true'>{header}</th>");
            }

            sb.Append("</tr>");
            sb.Append("</thead>");

            sb.Append("<body>");

            foreach (var row in _rows)
            {
                sb.Append("<tr>");
                int index = 0;
                foreach (string field in row)
                {
                    string headerId = _header[index++];
                    sb.Append($"<th data-field='{headerId}' >{field}</th>");
                }
                sb.Append("</tr>");
            }

            sb.Append("</body>");

            return sb.ToString();
        }
    }
}
