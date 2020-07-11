using System;
using System.IO;
using System.Text;

namespace eWolfBootstrap
{
    public class PageBuilder
    {
        private string _fileName;
        private string _path;
        private StringBuilder _stringBuilder = new StringBuilder();

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
            Directory.CreateDirectory(_path);
            File.WriteAllText(_path + _fileName, _stringBuilder.ToString());
        }
    }
}
