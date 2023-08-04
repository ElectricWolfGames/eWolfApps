using eWolfBootstrap.Interfaces;
using System.Text;

namespace eWolfBootstrap.Helpers
{
    public class PageHeaderHelper
    {
        public static string PageHeader(IPageHeader pageDetails, string offSet)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("<!DOCTYPE html>");
            stringBuilder.Append("<html lang='en' >");
            stringBuilder.Append("<head>");
            AddSiteTracker(stringBuilder);
            stringBuilder.Append("    <meta charset='UTF-8'>");
            stringBuilder.Append($"    <title>{pageDetails.Title}</title>");
            stringBuilder.Append("<meta http-equiv='Content -Type' content='text/html; charset=UTF-8'>");
            stringBuilder.Append($"<meta name='description' content='{pageDetails.Description}'/>");
            stringBuilder.Append($"<meta name='keywords' content='{string.Join(" ", pageDetails.Keywords)}'/>");
            stringBuilder.Append($"<meta name='title' content='{pageDetails.Title}'/>");
            stringBuilder.Append($"<meta name='author' content='{pageDetails.Author}'>");
            stringBuilder.Append("    <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>");

            stringBuilder.Append($@"    <link rel='stylesheet' href='{offSet}Scripts/style.css'>");

            stringBuilder.Append("<link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css'>");
            stringBuilder.Append("<script src='https://cdn.jsdelivr.net/npm/jquery@3.6.4/dist/jquery.slim.min.js'></script>");
            stringBuilder.Append("<script src='https://cdn.jsdelivr.net/npm/popper.js@1.16.1/dist/umd/popper.min.js'></script>");
            stringBuilder.Append("<script src='https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/js/bootstrap.bundle.min.js'></script>");

            if (pageDetails.ExtraIncludes.Contains(Enums.BootstrapOptions.GALLERY))
            {
                stringBuilder.Append($@"<script src='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js' integrity='sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM' crossorigin='anonymous'></script>");
                stringBuilder.Append($@"<link rel='stylesheet' href='https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css'>");
                stringBuilder.Append($@"<link rel='stylesheet' href='https://cdnjs.cloudflare.com/ajax/libs/baguettebox.js/1.10.0/baguetteBox.min.css' />");
                stringBuilder.Append($@"<link rel='stylesheet' href='{offSet}Scripts/grid-gallery.css'>");
            }
            else
            {
                if (pageDetails.ExtraIncludes.Contains(Enums.BootstrapOptions.CHART))
                {
                    stringBuilder.Append($@"    <script type='text/javascript' src='{offSet}Scripts/Chart.js'></script>");
                }

                if (pageDetails.ExtraIncludes.Contains(Enums.BootstrapOptions.BT))
                {
                    stringBuilder.Append(@"<link rel='stylesheet' href='https://unpkg.com/bootstrap-table@1.18.0/dist/bootstrap-table.min.css'>");
                }

                stringBuilder.Append($@"<script src='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js' integrity='sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM' crossorigin='anonymous'></script>");
            }

            
            stringBuilder.Append("</head>");

            return stringBuilder.ToString();
        }

        private static void AddSiteTracker(StringBuilder sb)
        {
            sb.Append(@"<!-- Google tag (gtag.js) -->
<script async src=""https://www.googletagmanager.com/gtag/js?id=G-N0VN5409QJ""></script>
<script>
  window.dataLayer = window.dataLayer || [];
  function gtag(){dataLayer.push(arguments);}
  gtag('js', new Date());

  gtag('config', 'G-N0VN5409QJ');
</script>");
        }
    }
}