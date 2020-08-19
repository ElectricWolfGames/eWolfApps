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
            stringBuilder.Append("    <meta charset='UTF-8'>");
            stringBuilder.Append($"    <title>{pageDetails.Title}</title>");
            stringBuilder.Append("<meta http-equiv='Content -Type' content='text/html; charset=UTF-8'>");
            stringBuilder.Append($"<meta name='description' content='{pageDetails.MetaDetails}'/>");
            stringBuilder.Append($"<meta name='keywords' content='{string.Join(" ", pageDetails.Keywords)}'/>");
            stringBuilder.Append($"<meta name='title' content='{pageDetails.Title}'/>");
            stringBuilder.Append($"<meta name='author' content='{pageDetails.Author}'>");
            stringBuilder.Append("    <meta name='viewport' content='width=device-width, initial-scale=1, shrink-to-fit=no'>");
            stringBuilder.Append($@"    <link rel='stylesheet' href='{offSet}Scripts/style.css'>");
            stringBuilder.Append(@"    <link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>");
            stringBuilder.Append(@"    <script src='https://code.jquery.com/jquery-3.3.1.slim.min.js' integrity='sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo' crossorigin='anonymous'></script>");
            stringBuilder.Append(@"    <script src='https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js' integrity='sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1' crossorigin='anonymous'></script>");
            stringBuilder.Append(@"    <script src='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js' integrity='sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM' crossorigin='anonymous'></script>");

            AddSiteTracker(stringBuilder);
            stringBuilder.Append("</head>");

            return stringBuilder.ToString();
        }

        public static void AddSiteTracker(StringBuilder sb)
        {
            sb.Append(@"
<!-- Global site tag (gtag.js) - Google Analytics -->
<script async src='https://www.googletagmanager.com/gtag/js?id=UA-12875541-9'></script>
<script>
  window.dataLayer = window.dataLayer || [];  function gtag(){dataLayer.push(arguments);} gtag('js', new Date());  gtag('config', 'UA-12875541-9');
</script>");
        }
    }
}
