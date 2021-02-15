using eWolfBootstrap.Interfaces;
using System.IO;
using System.Text;

namespace eWolfBootstrap.Helpers
{
    public static class HTMLHelper
    {
        public static void AddImageToGallery(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Append(HTMLHelper.BuildImageGalleryCard(newImagePath, newImagePathThumb, imageTitle));
        }

        public static void AddImageToPage(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Append(HTMLHelper.BuildImageCard(newImagePath, newImagePathThumb, imageTitle));
        }

        public static void AddQuickImage(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            htmlpath = htmlpath.Replace("\\\\", "\\");
            imagePath = imagePath.Replace("\\\\", "\\");

            string newImagePath = ImageHelper.CopyImageTo(imagePath, image, 1);
            newImagePath = newImagePath.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Append(HTMLHelper.BuildImageQuick(newImagePath, newImagePath, imageTitle));
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

        public static string BuildImageQuick(string image, string imageThumb, string title)
        {
            StringBuilder stringBuilder = new StringBuilder();

            //stringBuilder.AppendLine("<div class='col-md-6 col-lg-4 item'>");
            //stringBuilder.AppendLine($"<a class='lightbox' href='{image}'>");
            stringBuilder.AppendLine($"<img class='rounded float img-fluid image scale-on-hover' src='{imageThumb}'>");
            //stringBuilder.AppendLine("</a>");
            //stringBuilder.AppendLine("</div>");

            return stringBuilder.ToString();
        }

        public class Gallery
        {
            public static void AddGalleryFooter(Interfaces.IPageBuilder builder)
            {
                builder.Append("</div></div></section>");
            }

            public static void AddGalleryHeader(Interfaces.IPageBuilder builder, string name)
            {
                builder.Append("<section class='gallery-block grid-gallery'>");
                builder.Append("<div class='container'>");
                builder.Append("<div class='heading'>");
                if (!string.IsNullOrWhiteSpace(name))
                {
                    builder.Append($"<h2><a id='{name}'>{name}</a></h2>");
                }
                builder.Append("</div>");
                builder.Append("<div class='row'>");
            }

            public static void AddGalleryPageFooter(IPageBuilder pageBuilder)
            {
                pageBuilder.Append("<script src='https://cdnjs.cloudflare.com/ajax/libs/baguettebox.js/1.10.0/baguetteBox.min.js'></script>");
                pageBuilder.Append("<script>");
                pageBuilder.Append("baguetteBox.run('.grid-gallery', { animation: 'slideIn'});");
                pageBuilder.Append("</script>");
            }
        }
    }
}