using eWolfBootstrap.Helpers;
using eWolfBootstrap.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace eWolfBootstrap.Builders
{
    [Obsolete("Not used any more", false)]
    public class PageBuilder : IPageBuilder
    {
        protected string _fileName;
        protected string _offSet;
        protected string _path;
        protected StringBuilder _stringBuilder = new StringBuilder();
        private readonly IPageHeader _pageHeader;

        public PageBuilder(string fileName, string path, IPageHeader pageHeader)
        {
            _pageHeader = pageHeader;
            _fileName = fileName;
            _path = path;
            //_stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader, ""));
            _stringBuilder.Append($"<Body><!--{DateTime.Now.ToShortDateString()}-->");
        }

        public PageBuilder(string fileName, string path, IPageHeader pageHeader, string offSet)
        {
            _pageHeader = pageHeader;
            _fileName = fileName;
            _path = path;
            _offSet = offSet;
            //_stringBuilder.Append(PageHeaderHelper.PageHeader(pageHeader, offSet));
            _stringBuilder.Append($"<Body><!--{DateTime.Now.ToShortDateString()}-->");
        }

        public PageBuilder()
        { }

        public void AddImage(string htmlpath, string imagePath, string path)
        {
            HTMLHelper.AddQuickImage(htmlpath, imagePath, this, path);
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
                string name = $"{date}";
                HTMLHelper.Gallery.AddGalleryHeaderWithDate(this, name);
                foreach (var fileName in list)
                {
                    HTMLHelper.AddImageToGallery(htmlpath, imagePath, this, fileName);
                }
                HTMLHelper.Gallery.AddGalleryFooter(this);
            }
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

            Text(HTMLHelper.CreateCard(seeMore));

            HTMLHelper.Gallery.AddGalleryFooter(this);
        }

        public void Text(string text)
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

        public virtual string GetOutput()
        {
            _stringBuilder.Append("</Body>");
            return _stringBuilder.ToString();
        }

        public string GetString()
        {
            return _stringBuilder.ToString();
        }

        public void JumbotronWithImage(string title, string body, string imageHtmlPath, string imagePath, string imageName, int size = 4)
        {
            Text("<div class='jumbotron'>");
            Text("<div class='row'>");

            Text($"<div class='col-md-6'>");
            Text(title);
            Text(body);
            Text("</div>");

            Text("<div class='col-md-4'>");

            AddImage(
                imageHtmlPath,
                imagePath,
                imageName);

            Text("</div>");

            Text("</div>");
            Text("</div>");
        }

        public virtual void Output()
        {
           
            _stringBuilder.Append("</Body>");
            Directory.CreateDirectory(_path);
            File.WriteAllText(_path + _fileName, _stringBuilder.ToString());
        }
    }
}