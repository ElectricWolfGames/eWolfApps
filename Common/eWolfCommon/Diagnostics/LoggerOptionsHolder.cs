using System.Text;

namespace eWolfCommon.Diagnostics
{
    public class LoggerOptionsHolder
    {
        private readonly StringBuilder _data = new StringBuilder();

        public LoggerOptionsHolder()
        {
            _data.Append("{");
        }

        public void AddOption(string name, string value)
        {
            _data.Append("[");
            _data.Append($"{name}={value}");
            _data.Append("]");
        }

        public override string ToString()
        {
            _data.Append("}");
            return _data.ToString();
        }
    }
}