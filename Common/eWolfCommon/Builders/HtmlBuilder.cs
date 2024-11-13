using System;
using System.Text;

namespace eWolfCommon.Builders
{
    public class HtmlBuilder
    {
        private readonly StringBuilder _data = new StringBuilder();
        private int _inTableCount = 0;
        private int _inUnordedList = 0;

        public void AddLink(string displayName, string link)
        {
            _data.Append($"<a hreft='{link}'>{displayName}</a>");
        }

        public void AddListItem(string listItem)
        {
            if (_inUnordedList == 0)
                throw new InvalidProgramException();

            _data.Append($"<li>{listItem}</li>");
        }

        public void AddNewLine()
        {
            _data.Append("<br/>");
        }

        public void AddTableHeader(string[] headerList)
        {
            foreach (string header in headerList)
            {
                _data.Append($"<th>{header}</th>");
            }
        }

        public void AddTableRow(string[] dataRow, string[] attributes = null)
        {
            if (dataRow == null)
                return;

            _data.Append("<tr>");

            for (int i = 0; i < dataRow.Length; i++)
            {
                string row = dataRow[i];
                string attribute = string.Empty;
                if (attributes != null && i < attributes.Length)
                    attribute = attributes[i];

                _data.Append($"<td {attribute}>{row}</td>");
            }

            _data.Append("</tr>");
        }

        public void Append(string name)
        {
            _data.Append(name);
        }

        public void EndTable()
        {
            if (_inTableCount < 1)
                throw new InvalidProgramException();

            _data.Append("</table>");
            _inTableCount--;
        }

        public void EndUnordedList()
        {
            if (_inUnordedList < 1)
                throw new InvalidProgramException();

            _data.Append("</ul>");
            _inUnordedList--;
        }

        public string Output()
        {
            if (_inTableCount != 0)
                throw new InvalidProgramException();

            if (_inUnordedList != 0)
                throw new InvalidProgramException();

            return _data.ToString();
        }

        public void StartTable()
        {
            _data.Append("<table>");
            _inTableCount++;
        }

        public void StartTableWithBorder()
        {
            _data.Append("<table border='1'>");
            _inTableCount++;
        }

        public void StartUnordedList()
        {
            _data.Append("<ul>");
            _inUnordedList++;
        }
    }
}