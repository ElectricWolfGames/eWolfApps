using eWolfBootstrap.Helpers;
using eWolfBootstrap.Interfaces;
using System.IO;
using System.Text;

namespace eWolfBootstrap.Builders
{
    public class PageBuilder : IPageBuilder
    {
        protected string _fileName;
        protected string _path;
        protected StringBuilder _stringBuilder = new StringBuilder();
        private IPageHeader _pageHeader;

        public PageBuilder(string fileName, string path, IPageHeader pageHeader)
        {
            _pageHeader = pageHeader;
            _fileName = fileName;
            _path = path;
            _stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader, ""));
            _stringBuilder.Append("<Body>");
        }

        public PageBuilder(string fileName, string path, IPageHeader pageHeader, string offSet)
        {
            _pageHeader = pageHeader;
            _fileName = fileName;
            _path = path;
            _stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader, offSet));
            _stringBuilder.Append("<Body>");
        }

        public PageBuilder()
        { }

        public void Append(string text)
        {
            _stringBuilder.Append(text);
        }

        public void AddStickyHeader(string name)
        {
            string text = @"<script>
window.onscroll = function() {myFunction()};

var header = document.getElementById(" + name + @");
var sticky = header.offsetTop;

function myFunction() {
  if (window.pageYOffset > sticky) {
    header.classList.add('sticky');
  } else {
    header.classList.remove('sticky');
  }
}
</script>";

            _stringBuilder.Append(text);
        }

        public string GetString()
        {
            return _stringBuilder.ToString();
        }

        public virtual void Output()
        {
            if (_pageHeader.ExtraIncludes.Contains(Enums.BootstrapOptions.BT))
            {
                _stringBuilder.Append(@"<script src='https://unpkg.com/bootstrap-table@1.18.0/dist/bootstrap-table.min.js'></script>");
                _stringBuilder.Append(@"<script src='https://unpkg.com/bootstrap-table@1.18.0/dist/locale/bootstrap-table-zh-CN.min.js'></script>");
            }

            _stringBuilder.Append("</Body>");
            Directory.CreateDirectory(_path);
            File.WriteAllText(_path + _fileName, _stringBuilder.ToString());
        }
    }
}
