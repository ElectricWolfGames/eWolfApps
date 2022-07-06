﻿using eWolfBootstrap.SiteBuilder;
using eWolfBootstrap.SiteBuilder.Builders;
using System.Collections.Generic;
using System.Text;

namespace eWolfBootstrap.Builders
{
    public class HTMLBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private HTMLSection _left;
        private HTMLSection _mid;
        private HTMLSection _right;

        public HTMLBuilder()
        {
        }

        public void Bold(string text)
        {
            _stringBuilder.Append($"<strong>{text}</strong>");
        }

        public void CreateIndex(List<HTMLIndexedItems> items)
        {
            HTMLSection a = new HTMLSection("col-md-2");
            HTMLSection b = new HTMLSection("col-md-8");
            HTMLSection c = new HTMLSection("col-md-2");
            SetThreeSections(a, b, c);

            HTML h = new();
            h.TextCenter().Br();
            foreach (var item in items)
            {
                h.IndexItem(item);
            }
            h.EndDiv();

            b.Text(h.Output());
            b.NewLine();
        }

        public void CreateIndexItems(List<HTMLIndexedItems> items)
        {
            foreach (var item in items)
            {
                _stringBuilder.Append(item.BuildItem());
            }
        }

        public void EndTextCenter()
        {
            _stringBuilder.Append("</div>");
        }

        public void EndTextCenterLeft()
        {
            _stringBuilder.Append("</div>");
        }

        public void EndTextCenterRight()
        {
            _stringBuilder.Append("</div>");
        }

        public void EndTextMiddel()
        {
            _stringBuilder.Append("</p>");
            _stringBuilder.Append("</div>");
        }

        public void Image(string imageName, float percentage = 100)
        {
            _stringBuilder.Append($@"<img src='images/{imageName}' width={percentage}%px >");
        }

        public void ImageCard(string imageName, float percentage = 100)
        {
            _stringBuilder.Append("<div class='col-md-4'>");
            _stringBuilder.Append("<div class='thumbnail'>");
            _stringBuilder.Append($@"<img class='rounded mx-auto d-block' src='images/{imageName}' width={percentage}%px >");
            _stringBuilder.Append("<div class='caption'>&nbsp;</div>");
            _stringBuilder.Append("</div>");
            _stringBuilder.Append("</div>");
        }

        public void ImageCenter(string imageName, float percentage = 100)
        {
            _stringBuilder.Append($@"<img class='img-fluid rounded mx-auto d-block' src='images/{imageName}' width={percentage}%px >");
        }

        public void ImageLeft(string imageName, float percentage = 100)
        {
            StartTextCenterLeft();
            Image(imageName, percentage);
            EndTextCenterLeft();
        }

        public void ImageRight(string imageName, float percentage = 100)
        {
            StartTextCenterRight();
            Image(imageName, percentage);
            EndTextCenterRight();
        }

        public void Images(int percentage, params string[] images)
        {
            foreach (var image in images)
            {
                _stringBuilder.Append($@"<img class='img-fluid ' src='images/{image}' width={percentage}%px >");
            }
        }

        public void IndexTitle(HTMLIndexedItems indexItem)
        {
            string linkName = indexItem.Title;
            _stringBuilder.Append($"<li><a href='#{indexItem.Index}'>{linkName}</a></li>");
        }

        public void InspectorDetails(string field, string description)
        {
            _stringBuilder.Append($"<strong>{field}: </strong>{description}<br />");
        }

        public void Jumbotron(string title, string body, int size = 12)
        {
            _stringBuilder.AppendLine("<div class='jumbotron'>");
            _stringBuilder.AppendLine("<div class='row'>");
            _stringBuilder.AppendLine($"<div class='col-md-{size}'>");

            _stringBuilder.AppendLine($"<h1>{title}</h1>");
            _stringBuilder.AppendLine($"<p class='lead'>{body}</p>");

            _stringBuilder.AppendLine("</div>");
            _stringBuilder.AppendLine("</div>");
            _stringBuilder.AppendLine("</div>");
        }

        public void JumbotronImage(string title, string body, string imageName, float percentage, int size = 12)
        {
            _stringBuilder.AppendLine("<div class='jumbotron'>");
            _stringBuilder.AppendLine("<div class='row'>");
            _stringBuilder.AppendLine($"<div class='col-md-{size}'>");

            _stringBuilder.Append($@"<img class='rounded mx-auto d-block' src='images/{imageName}' width={percentage}%px >");
            _stringBuilder.AppendLine($"<p class='lead'>{body}</p>");

            _stringBuilder.AppendLine("</div>");
            _stringBuilder.AppendLine("</div>");
            _stringBuilder.AppendLine("</div>");
        }

        public void NamedUnity3DLink(string name, string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($"Download {name} from <a href='{link}' target='Blank' >Unity Asset Store</a>");
            _stringBuilder.Append("</div>");
        }

        public void NewLine()
        {
            _stringBuilder.Append("</br>");
        }

        public void OtherDetails(string field, string description)
        {
            _stringBuilder.Append($"<strong>{field} </strong>{description}<br />");
        }

        public virtual string Output()
        {
            if (_left != null)
            {
                _left.End();
                _stringBuilder.Append(_left.Output());
            }

            if (_mid != null)
            {
                _mid.End();
                _stringBuilder.Append(_mid.Output());
            }

            if (_right != null)
            {
                _right.End();
                _stringBuilder.Append(_right.Output());
                Text("</div>");
            }

            return _stringBuilder.ToString();
        }

        public void PageLink(string per, string linkName, string link)
        {
            // TODO: Need to linke 'link' to item just in case we change it one day!
            _stringBuilder.Append($"{per} <a href='{link}'>{linkName}</a>");
        }

        public void SetThreeSections(HTMLSection left, HTMLSection mid, HTMLSection right)
        {
            Text("<div class='row'>");
            _left = left;
            _right = right;
            _mid = mid;
        }

        public void SetTwoSections(HTMLSection left, HTMLSection right)
        {
            Text("<div class='row'>");
            _left = left;
            _right = right;
        }

        public void StartTextCenter()
        {
            _stringBuilder.Append("<div class='text-center'>");
        }

        public void StartTextCenterLeft()
        {
            _stringBuilder.Append("<div class='text-left'>");
        }

        public void StartTextCenterRight()
        {
            _stringBuilder.Append("<div class='text-right'>");
        }

        public void StartTextMiddel(float size)
        {
            _stringBuilder.Append($"<div class='d-flex align-items-middle' style='height: {size}px'><p>");
        }

        public void Text(string text)
        {
            _stringBuilder.Append(text);
        }

        public void TextBold(string textA, string boldA)
        {
            Text(textA);
            Bold(boldA);
        }

        public void TextBoldText(string textA, string boldA, string textB)
        {
            Text(textA);
            Bold(boldA);
            Text(textB);
        }

        public void Title(string text)
        {
            _stringBuilder.Append($"<h2>{text}</h2>");
        }

        public void Unity3DLink(string link)
        {
            _stringBuilder.Append($"Available at the <a href='{link}' target='Blank' >Unity Asset Store</a>");
        }

        public void Unity3DLinkTextCenter(string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($"Available at the <a href='{link}' target='Blank' >Unity Asset Store</a>");
            _stringBuilder.Append("</div>");
        }

        public void YouTubeLink(string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($@"<iframe width='560' height='315' src='https://www.youtube.com/embed/" + link + "?rel=0' frameborder='0' allowfullscreen></iframe>");
            _stringBuilder.Append("</div>");
        }

        public void YouTubeLinkBig(string link)
        {
            _stringBuilder.Append("<div class='text-center'>");
            _stringBuilder.Append($@"<iframe width='854' height='480' src='https://www.youtube.com/embed/" + link + "?rel=0' frameborder='0' allowfullscreen></iframe>");
            _stringBuilder.Append("</div>");
        }
    }
}