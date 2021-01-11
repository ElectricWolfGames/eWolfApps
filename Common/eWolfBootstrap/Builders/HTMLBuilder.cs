using System.Text;

namespace eWolfBootstrap.Builders
{
    public class HTMLBuilder
    {
        private StringBuilder _stringBuilder = new StringBuilder();
        private HTMLSection _left;
        private HTMLSection _right;

        public HTMLBuilder()
        {
        }

        public void Bold(string text)
        {
            _stringBuilder.Append($"<strong>{text}</strong>");
        }

        public void SetTwoSections(HTMLSection left, HTMLSection right)
        {
            Text("<div class='row'>");
            _left = left;
            _right = right;
        }

        public void TextBoldText(string textA, string boldA, string textB)
        {
            Text(textA);
            Bold(boldA);
            Text(textB);
        }

        public void Text(string text)
        {
            _stringBuilder.Append(text);
        }

        public void Title(string text)
        {
            _stringBuilder.Append($"<h2>{text}</h2>");
        }

        public void Unity3DLink(string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($"Available at the <a href='{link}' target='Blank' >Unity Asset Store</a>");
            _stringBuilder.Append("</div>");
        }

        public void NamedUnity3DLink(string name, string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($"Download {name} from <a href='{link}' target='Blank' >Unity Asset Store</a>");
            _stringBuilder.Append("</div>");
        }

        public void YouTubeLink(string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($@"<iframe width='560' height='315' src='https://www.youtube.com/embed/" + link + "?rel=0' frameborder='0' allowfullscreen></iframe>");
            _stringBuilder.Append("</div>");
        }

        public virtual string Output()
        {
            if (_left != null)
            {
                _left.End();
                _stringBuilder.Append(_left.Output());
            }

            if (_right != null)
            {
                _right.End();
                _stringBuilder.Append(_right.Output());
                Text("</div>");
            }

            return _stringBuilder.ToString();
        }

        public void NewLine()
        {
            _stringBuilder.Append("<br />");
        }

        public void InspectorDetails(string field, string description)
        {
            _stringBuilder.Append($"<strong>{field}: </strong>{description}<br />");
        }

        public void Image(string imageName, float percnetage = 100)
        {
            _stringBuilder.Append($@"<img src='images//{imageName}' width={percnetage}%px >");
        }

        public void ImageCard(string imageName, float percnetage = 100)
        {
            _stringBuilder.Append("<div class='col-md-4'>");
            _stringBuilder.Append("<div class='thumbnail'>");
            _stringBuilder.Append($@"<img class='rounded mx-auto d-block' src='images//{imageName}' width={percnetage}%px >");
            _stringBuilder.Append("<div class='caption'>&nbsp;</div>");
            _stringBuilder.Append("</div>");
            _stringBuilder.Append("</div>");
        }

        public void ImageCenter(string imageName, float percnetage = 100)
        {
            _stringBuilder.Append($@"<img class='img-fluid rounded mx-auto d-block' src='images//{imageName}' width={percnetage}%px >");
        }
    }
}