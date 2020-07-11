using System;
using System.IO;
using System.Text;

namespace eWolfBootstrap
{
    public class PageBuilder
    {
        private StringBuilder _stringBuilder = new StringBuilder();
        private string _fileName;
        private string _path;

        public PageBuilder(string fileName, string path)
        {
            _fileName = fileName;
            _path = path;
        }

        public void Append(string text)
        {
            _stringBuilder.Append(text);
        }

        public void Output()
        {
            File.WriteAllText(_path + _fileName, _stringBuilder.ToString());
        }
    }
}
