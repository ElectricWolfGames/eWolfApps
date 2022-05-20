using System.Text;

namespace eWolfBootstrap.SiteBuilder.Builders
{
    public class HTML
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private int _divCount = 0;

        public HTML Br()
        {
            _stringBuilder.Append("<br />");
            return this;
        }

        public HTML EndDiv()
        {
            _stringBuilder.Append(@"</div>");
            _divCount--;
            return this;
        }

        public void IndexItem(HTMLIndexedItems indexItem)
        {
            string linkName = indexItem.Title;
            _stringBuilder.Append($"<a href='#{indexItem.Index}'>{linkName}</a></br>");
        }

        public string Output()
        {
            return _stringBuilder.ToString();
        }

        public HTML TextCenter()
        {
            _stringBuilder.Append("<div class='text-center'>");
            _divCount++;
            return this;
        }
    }
}