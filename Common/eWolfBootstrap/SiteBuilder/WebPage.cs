using System;
using System.IO;
using System.Text;
using eWolfBootstrap.SiteBuilder.Attributes;
using eWolfBootstrap.SiteBuilder.Helpers;
using eWolfBootstrap.SiteBuilder.Interfaces;

namespace eWolfBootstrap.SiteBuilder
{
    public class WebPage
    {
        private ISitePageDetails _pageDetails;
        private StringBuilder _stringBuilder = new StringBuilder();

        public WebPage(ISitePageDetails pageDetails)
        {
            Type types = pageDetails.GetType();

            _pageDetails = pageDetails;
            PageTitleAttribute MyAttribute =
              (PageTitleAttribute)Attribute.GetCustomAttribute(
                      types,
                      typeof(PageTitleAttribute));

            _path += PagePath.GetPath(pageDetails);
            _title = MyAttribute.Title;
        }

        private string _path { get; set; }

        private string _title { get; set; }

        public void AddBody()
        {
            _stringBuilder.Append("<Body>");
            _stringBuilder.Append("</Body>");
        }

        public void AddHeader()
        {
            var pageHeaderDetails = SiteBuilderServiceLocator.Instance.GetService<IPageHeaderDetails>();
            _stringBuilder.Append(pageHeaderDetails.Output());
        }

        // TODO: AddStickyHeader -Need to check if this is the best place
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

        public void Append(string text)
        {
            _stringBuilder.AppendLine(text);
        }

        public void Output()
        {
            _pageDetails.FullLocalFilename = @$"{_pageDetails.RootAddress}\\{_path}\\{_title}";

            Directory.CreateDirectory(_pageDetails.RootAddress);
            Directory.CreateDirectory(_pageDetails.RootAddress + "\\" + _path);
            File.WriteAllText(_pageDetails.FullLocalFilename, _stringBuilder.ToString());
        }
    }
}