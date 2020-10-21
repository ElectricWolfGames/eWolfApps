using System.IO;
using System.Text;

namespace eWolfBootstrap.Helpers
{
    public static class HTMLHelper
    {
        public static void AddImageToPage(string htmlpath, string imagePath, eWolfBootstrap.Interfaces.IPageBuilder stringBuilder, string image)
        {
            string newImagePath = ImageHelper.CopyImageTo(imagePath, image);
            string newImagePathThumb = ImageHelper.CopyImageToThumb(imagePath, image);

            newImagePath = newImagePath.Replace(htmlpath, string.Empty);
            newImagePathThumb = newImagePathThumb.Replace(htmlpath, string.Empty);

            string imageTitle = Path.GetFileNameWithoutExtension(newImagePath);
            stringBuilder.Append(HTMLHelper.BuildImageCard(newImagePath, newImagePathThumb, imageTitle));
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
    }
}
