using System.Text;

namespace eWolfBootstrap.Chats
{
    public class SortableTableHolder : TableHolderBase
    {
        private readonly string _title = "name";

        // https://examples.bootstrap-table.com/#column-options/sortable.html
        public SortableTableHolder(string name)
        {
            _title = name;
        }

        public SortableTableHolder()
        {
        }

        public string Output()
        {
            StringBuilder sb = new StringBuilder();

            if (_rows.Count > 15)
            {
                sb.Append($"<table class='table' id='table{_title}' data-toggle='table' data-height='800' >");
            }
            else
            {
                sb.Append($"<table class='table' id='table{_title}' data-toggle='table'>");
            }

            sb.Append("<thead>");
            sb.Append("<tr>");

            foreach (string header in _header)
            {
                sb.Append($"<th data-field='{header}' data-sortable='true'>{header}</th>");
            }

            sb.Append("</tr>");
            sb.Append("</thead>");

            sb.Append("<tbody>");

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

            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
    }
}