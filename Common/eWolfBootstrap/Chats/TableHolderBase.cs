using System.Collections.Generic;

namespace eWolfBootstrap.Chats
{
    public class TableHolderBase
    {
        protected string[] _header;
        protected readonly List<string[]> _rows = new List<string[]>();

        public void Header(string[] header)
        {
            _header = header;
        }

        public void AddRow(string[] row)
        {
            _rows.Add(row);
        }
    }
}
