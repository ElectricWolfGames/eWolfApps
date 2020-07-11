using eWolfBootstrap.Helpers;
using eWolfBootstrap.Interfaces;
using System;
using System.IO;
using System.Text;

namespace eWolfBootstrap.Builder
{
    public class PageBuilder : IPageBuilder
    {
        private string _fileName;
        private string _path;
        private StringBuilder _stringBuilder = new StringBuilder();

        public PageBuilder(string fileName, string path, IPageHeader pageHeader)
        {
            _fileName = fileName;
            _path = path;
            _stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader));
            _stringBuilder.Append("<Body>");
        }

        public PageBuilder()
        { }

        public void Append(string text)
        {
            _stringBuilder.Append(text);
        }

        public string GetString()
        {
            return _stringBuilder.ToString();
        }

        public void Output()
        {
            _stringBuilder.Append("</Body>");
            Directory.CreateDirectory(_path);
            File.WriteAllText(_path + _fileName, _stringBuilder.ToString());
        }
    }
}
