using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using eWolfBootstrap.Helpers;
using eWolfBootstrap.Interfaces;

namespace eWolfBootstrap.Builders
{
    public class PageBuilder : IPageBuilder
    {
        protected string _fileName;
        protected string _offSet;
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
            _offSet = offSet;
            _stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader, offSet));
            _stringBuilder.Append("<Body>");
        }

        public PageBuilder()
        { }

        public void AddImage(string htmlpath, string imagePath, string path)
        {
            HTMLHelper.AddQuickImage(htmlpath, imagePath, this, path);
        }

        public void AddImageCenter(string htmlpath, string imagePath, string path)
        {
            HTMLHelper.AddQuickImageCenter(htmlpath, imagePath, this, path);
        }


        public void AddImagesGroupedByDate(string htmlpath, string imagePath, string path)
        {
            List<string> images = ImageHelper.GetAllImages(path);
            
            Dictionary<string, List<string>> byDate = new Dictionary<string, List<string>>();

            foreach (string image in images)
            {
                var time = File.GetLastWriteTime(image);
                string date = $"{time.Year}-{time.Month.ToString("00")}-{time.Day.ToString("00")}";
                List<string> files = new List<string>();

                if (byDate.TryGetValue(date, out files))
                {
                    files.Add(image);
                }
                else
                {
                    files = new List<string>();
                    files.Add(image);
                    byDate.Add(date, files);
                }
            }

            List<string> keyList = new List<string>(byDate.Keys);
            keyList = keyList.OrderByDescending(x => x).ToList();

            foreach (var date in keyList)
            {
                var list = byDate[date];
                string name =$"{date}";
                HTMLHelper.Gallery.AddGalleryHeaderWithDate(this, name);
                foreach (var fileName in list)
                {
                    HTMLHelper.AddImageToGallery(htmlpath, imagePath, this, fileName);
                }
                HTMLHelper.Gallery.AddGalleryFooter(this);
            }

        }

        public void AddImages(string htmlpath, string imagePath, string path)
        {
            List<string> images = ImageHelper.GetAllImages(path);
            HTMLHelper.Gallery.AddGalleryHeader(this, null);

            foreach (string image in images)
            {
                HTMLHelper.AddImageToGallery(htmlpath, imagePath, this, image);
            }

            HTMLHelper.Gallery.AddGalleryFooter(this);
        }

        public void AddImages(List<string> imageToUse, string htmlpath, string imagePath, string path, string offSetFolder)
        {
            HTMLHelper.Gallery.AddGalleryHeader(this, null);

            foreach (string image in imageToUse)
            {
                HTMLHelper.AddImageToGallery(htmlpath, imagePath, this, image, offSetFolder);
            }

            HTMLHelper.Gallery.AddGalleryFooter(this);
        }

        public void AddImagesWithSeeMore(List<string> imageToUse, List<string> imageToUseSmall, string htmlpath, string imagePath, string path, string offSetFolder, string seeMore)
        {
            HTMLHelper.Gallery.AddGalleryHeaderLocos(this, null);

            foreach (string image in imageToUse)
            {
                HTMLHelper.AddImageToGallery(htmlpath, imagePath, this, image, offSetFolder);
            }

            foreach (string image in imageToUseSmall)
            {
                HTMLHelper.AddImageToGallerySmall(htmlpath, imagePath, this, image, offSetFolder);
            }


            Append(HTMLHelper.CreateCard(seeMore));

            HTMLHelper.Gallery.AddGalleryFooter(this);
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

        public void Append(string text)
        {
            _stringBuilder.AppendLine(text);
        }

        public void ConvertImages(string htmlpath, string imagePath, string path)
        {
            List<string> images = ImageHelper.GetAllImages(path);

            foreach (string image in images)
            {
                HTMLHelper.ConvertImageToGallery(htmlpath, imagePath, this, image);
            }
        }

        public string GetString()
        {
            return _stringBuilder.ToString();
        }

        public void Jumbotron(string title, string body, int size = 4)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<div class='jumbotron'>");
            stringBuilder.AppendLine("<div class='row'>");
            stringBuilder.AppendLine($"<div class='col-md-{size}'>");
            stringBuilder.AppendLine(title);
            stringBuilder.AppendLine(body);

            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</div>");

            Append(stringBuilder.ToString());
        }

        public void JumbotronWithImage(string title, string body, string imageHtmlPath, string imagePath, string imageName, int size = 4)
        {
            Append("<div class='jumbotron'>");
            Append("<div class='row'>");

            Append($"<div class='col-md-6'>");
            Append(title);
            Append(body);
            Append("</div>");

            Append("<div class='col-md-4'>");

            AddImage(
                imageHtmlPath,
                imagePath,
                imageName);

            Append("</div>");

            Append("</div>");
            Append("</div>");
        }

        public virtual void Output()
        {
            if (_pageHeader.ExtraIncludes.Contains(Enums.BootstrapOptions.BT))
            {
                _stringBuilder.Append(@"<script src='https://unpkg.com/bootstrap-table@1.18.0/dist/bootstrap-table.min.js'></script>");
                _stringBuilder.Append(@"<script src='https://unpkg.com/bootstrap-table@1.18.0/dist/locale/bootstrap-table-zh-CN.min.js'></script>");
            }

            if (_pageHeader.ExtraIncludes.Contains(Enums.BootstrapOptions.GALLERY))
            {
                HTMLHelper.Gallery.AddGalleryPageFooter(this);
            }

            _stringBuilder.Append("</Body>");
            Directory.CreateDirectory(_path);
            File.WriteAllText(_path + _fileName, _stringBuilder.ToString());
        }
    }
}