using System.Collections.Generic;

namespace eWolfBootstrap.Chats
{
    public class TableHolderBase
    {
        protected readonly List<string[]> _rows = new List<string[]>();
        protected string[] _header;

        public void AddRow(string[] row)
        {
            _rows.Add(row);
        }

        public void Header(string[] header)
        {
            _header = header;
        }
    }
}