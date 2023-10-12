using eWolfBootstrap.Builders;
using eWolfBootstrap.Interfaces;
using System.IO;
using System.Text;

namespace eWolfBootstrap.Helpers
{
    public static class HTMLHelper
    {

        public static void AddImageToGallery(string htmlpath, string imagePath, HTMLBuilder stringBuilder, string image, string offSetFolder = "")
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Text(HTMLHelper.BuildImageGalleryCard(offSetFolder + newImagePath, offSetFolder + newImagePathThumb, imageTitle));
        }


        public static void AddImageToGallery(string htmlpath, string imagePath, IPageBuilder stringBuilder, string image, string offSetFolder = "")
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Text(HTMLHelper.BuildImageGalleryCard(offSetFolder + newImagePath, offSetFolder + newImagePathThumb, imageTitle));
        }

        public static void AddImageToGallerySmall(string htmlpath, string imagePath, IPageBuilder stringBuilder, string image, string offSetFolder = "")
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Text(HTMLHelper.BuildImageGalleryCardSmall(offSetFolder + newImagePath, offSetFolder + newImagePathThumb, imageTitle));
        }

        public static void AddImageToPage(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Text(HTMLHelper.BuildImageCard(newImagePath, newImagePathThumb, imageTitle));
        }

        public static void AddQuickImage(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image, 1);
            newImagePath = newImagePath.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Text(HTMLHelper.BuildImageQuick(newImagePath, newImagePath, imageTitle));
        }
        public static void AddQuickImage(string htmlpath, string imagePath, StringBuilder stringBuilder, string image)
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image, 1);
            newImagePath = newImagePath.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Append(HTMLHelper.BuildImageQuick(newImagePath, newImagePath, imageTitle));
        }

        public static void AddQuickImageCenter(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image, 1);
            newImagePath = newImagePath.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Text(HTMLHelper.BuildImageQuickCenter(newImagePath, newImagePath, imageTitle));
        }

        public static (string,string) CopyImageUploads(string imagePath, string image)
        {
            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);
            return (newImagePath, newImagePathThumb);
        }


        public static string BuildImageCard(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<div class='col-md-4'>");
            stringBuilder.AppendLine("<div class='card' style='width: 20rem;'>");
            stringBuilder.AppendLine($"<img class='card-img-top' data-toggle='modal' data-target='#ShowImage' data-imagename='{image}' data-imagetitle='{title}'");
            stringBuilder.AppendLine("alt='Card image cap'");
            stringBuilder.AppendLine("class='rounded mx-auto d-block'");
            stringBuilder.AppendLine($"src='{imageThumb}'");
            stringBuilder.AppendLine("style='width:100%'>");
            stringBuilder.AppendLine("<div class='card-body'>");
            stringBuilder.AppendLine("<p class='card-text'></p>");
            stringBuilder.AppendLine($"<a data-toggle='modal' data-target='#ShowImage' data-imagename='{image}' data-imagetitle='{title}' class='btn btn-outline-primary'>View</a>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</div>");

            return stringBuilder.ToString();
        }

        public static string BuildImageGalleryCard(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<div class='col-md-6 col-lg-4 item'>");
            stringBuilder.AppendLine($"<a class='lightbox' href='{image}'>");
            stringBuilder.AppendLine($"<img class='img-fluid image scale-on-hover' src='{imageThumb}'>");
            stringBuilder.AppendLine("</a>");
            stringBuilder.AppendLine("</div>");

            return stringBuilder.ToString();
        }

        public static string BuildImageGalleryCardFloatRight(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<div class='col-md-6 col-lg-4 item'>");
            stringBuilder.AppendLine($"<a class='lightbox' href='{image}'>");
            stringBuilder.AppendLine($"<img class='rounded float-right img-fluid image scale-on-hover' src='{imageThumb}'>");
            stringBuilder.AppendLine("</a>");
            stringBuilder.AppendLine("</div>");

            return stringBuilder.ToString();
        }

        public static string BuildImageGalleryCardSmall(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<div class='col-md-2 col-lg-2 item'>");
            stringBuilder.AppendLine($"<a class='lightbox' href='{image}'>");
            stringBuilder.AppendLine($"<img class='img-fluid image scale-on-hover' src='{imageThumb}'>");
            stringBuilder.AppendLine("</a>");
            stringBuilder.AppendLine("</div>");

            return stringBuilder.ToString();
        }

        public static string BuildImageQuick(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"<img class='rounded float img-fluid image scale-on-hover' src='{imageThumb}'>");
            return stringBuilder.ToString();
        }

        public static string BuildImageQuickCenter(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div class='text-center'>");
            stringBuilder.AppendLine($"<img class='rounded float img-fluid image scale-on-hover' align='center' width='80%' src='{imageThumb}'>");
            stringBuilder.Append("</div>");
            return stringBuilder.ToString();
        }

        public static string ConvertImageToGallery(string htmlpath, string imagePath, IPageBuilder stringBuilder, string image)
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            //newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            //string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            return newImagePath;
            //stringBuilder.Append(HTMLHelper.BuildImageGalleryCard(newImagePath, newImagePathThumb, imageTitle));
        }

        public static string CreateCard(string title)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("<div class='col-md-6 col-lg-2 item'>");
            stringBuilder.AppendLine(title);
            stringBuilder.AppendLine("</div>");

            return stringBuilder.ToString();
        }

        public class Gallery
        {
            public static void AddGalleryFooter(IPageBuilder builder)
            {
                builder.Text("</div></div></section>");
            }
            public static void AddGalleryFooter(HTMLBuilder builder)
            {
                builder.Text("</div></div></section>");
            }

            public static void AddGalleryHeader(IPageBuilder builder, string name)
            {
                builder.Text("<section class='gallery-block grid-gallery'>");
                builder.Text("<div class='container'>");
                builder.Text("<div class='heading'>");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    builder.Text($"<h2><a id='{name}'>{name}</a></h2>");
                }
                builder.Text("</div>");
                builder.Text("<div class='row'>");
            }

            public static void AddGalleryHeader(HTMLBuilder builder, string name)
            {
                builder.Text("<section class='gallery-block grid-gallery'>");
                builder.Text("<div class='container'>");
                builder.Text("<div class='heading'>");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    builder.Text($"</br><h2><a id='{name}'>{name}</a></h2>");
                }
                builder.Text("</div>");
                builder.Text("<div class='row'>");
            }

            public static void AddGalleryHeaderLocos(IPageBuilder builder, string name)
            {
                builder.Text("<section class='gallery-block grid-gallery'>");
                builder.Text("<div class='container'>");
                builder.Text("<div class='row'>");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    builder.Text($"<h2><a id='{name}'>{name}</a></h2>");
                }
            }

            public static void AddGalleryHeaderWithDate(IPageBuilder builder, string name)
            {
                builder.Text("<section class='gallery-block grid-gallery'>");
                builder.Text("<div class='container'>");
                builder.Text("<div class='col-md-12'>");
                builder.Text($"<hp>{name}</hp>");
                builder.Text("</div>");
                builder.Text("<div class='row'>");
            }

            public static void AddGalleryHeaderWithDate(HTMLBuilder builder, string name)
            {
                builder.Text("<section class='gallery-block grid-gallery'>");
                builder.Text("<div class='container'>");
                builder.Text("<div class='col-md-12'>");
                builder.Text($"<hp>{name}</hp>");
                builder.Text("</div>");
                builder.Text("<div class='row'>");
            }

            public static void AddGalleryPageFooter(IPageBuilder pageBuilder)
            {
                pageBuilder.Text("<script src='https://cdnjs.cloudflare.com/ajax/libs/baguettebox.js/1.10.0/baguetteBox.min.js'></script>");
                pageBuilder.Text("<script>");
                pageBuilder.Text("baguetteBox.run('.grid-gallery', { animation: 'slideIn'});");
                pageBuilder.Text("</script>");
            }
        }
    }
}