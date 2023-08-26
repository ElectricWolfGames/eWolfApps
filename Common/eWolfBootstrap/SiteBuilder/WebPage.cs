using eWolfBootstrap.SiteBuilder.Attributes;
using eWolfBootstrap.SiteBuilder.Enums;
using eWolfBootstrap.SiteBuilder.Helpers;
using eWolfBootstrap.SiteBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace eWolfBootstrap.SiteBuilder
{
    public class WebPage
    {
        private readonly ISitePageDetails _pageDetails;
        private readonly StringBuilder _stringBuilder = new();

        private readonly List<string> _tags = new List<string>();

        public WebPage(ISitePageDetails pageDetails)
        {
            Type types = pageDetails.GetType();

            _pageDetails = pageDetails;
            PageTitleAttribute MyAttribute =
              (PageTitleAttribute)Attribute.GetCustomAttribute(
                      types,
                      typeof(PageTitleAttribute));

            HtmlPath += PagePath.GetPath(pageDetails);
            HtmlTitle = MyAttribute.Title;

            NavigationAttribute navigation =
              (NavigationAttribute)Attribute.GetCustomAttribute(
                      types,
                      typeof(NavigationAttribute));

            if (navigation != null)
            {
                NavigationTypes = navigation.NavigationType;
                NavigationIndex = navigation.Index;
            }
        }

        public string HtmlPath { get; set; }

        public string HtmlTitle { get; set; }

        public int NavigationIndex { get; set; }

        public NavigationTypes NavigationTypes { get; set; }

        public void AddHeader(PageDetails pageDetails)
        {
            var pageHeaderDetails = SiteBuilderServiceLocator.Instance.GetService<IPageHeaderDetails>();
            _stringBuilder.Append(pageHeaderDetails.Output(pageDetails));
        }

        public void AddNavigation(NavigationTypes navigationType, string offSet = "")
        {
            List<NavigationPageAddressDetails> pageAddress = new();
            PopulatePageAddresses(navigationType, pageAddress);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<nav class='navbar navbar-expand-md navbar-dark bg-dark'>");

            var homePage = pageAddress.First(x => x.Title == "Home");
            stringBuilder.AppendLine($"<a class='navbar-brand' href='{offSet}{homePage.Address}'>{homePage.Title}</a>");

            stringBuilder.AppendLine("<button class='navbar-toggler' type='button' data-toggle='collapse' data-target='#navbarsExample04' aria-controls='navbarsExample04' aria-expanded='false' aria-label='Toggle navigation'>");
            stringBuilder.AppendLine("<span class='navbar-toggler-icon'></span>");
            stringBuilder.AppendLine("</button>");
            stringBuilder.AppendLine("<div class='collapse navbar-collapse' id='navbarsExample04'>");
            stringBuilder.AppendLine("<ul class='navbar-nav mr-auto'>");

            foreach (var kvp in pageAddress)
            {
                if (kvp.Title == "Home")
                    continue;

                stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link'" +
                        $"href='{offSet}{kvp.Address}'>{kvp.Title}</a></li>");
            }

            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</ul>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</nav>");

            _stringBuilder.Append(stringBuilder.ToString());
        }

        public void AddNavigation(string offSet)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<nav class='navbar navbar-expand-md navbar-dark bg-dark'>");
            stringBuilder.AppendLine($"<a class='navbar-brand' href='{offSet}index.html'>Home</a>");
            stringBuilder.AppendLine("<button class='navbar-toggler' type='button' data-toggle='collapse' data-target='#navbarsExample04' aria-controls='navbarsExample04' aria-expanded='false' aria-label='Toggle navigation'>");
            stringBuilder.AppendLine("<span class='navbar-toggler-icon'></span>");
            stringBuilder.AppendLine("</button>");
            stringBuilder.AppendLine("<div class='collapse navbar-collapse' id='navbarsExample04'>");
            stringBuilder.AppendLine("<ul class='navbar-nav mr-auto'>");

            string modelEvents = $"{offSet}ModelEvents/index.html";
            string modelLocations = $"{offSet}Constants.ModelEvents/locations.html";
            string stations = $"{offSet}Constants.Stations/index.html";
            string locomotives = $"{offSet}Constants.LocomotiveLocoRef.html";
            string myLayouts = $"{offSet}MyLayouts/index.html";
            string shop = $"{offSet}Shop/index.html";
            string auctions = $"{offSet}Shop/GCRAuctions.html";
            string stockVideos = $"{offSet}Constants.StockVideos/index.html";

            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{myLayouts}'>Cattington</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{shop}'>Shop</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{auctions}'>Auctions</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{locomotives}'>Loco Ref</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{stockVideos}'>Stock Videos</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{stations}'>Stations</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{modelEvents}'>Model Events</a></li>");
            stringBuilder.AppendLine($"<li class='nav-item active'><a class='nav-link' href='{modelLocations}'>Locations</a></li>");
            stringBuilder.AppendLine("<li class='nav-item dropdown'>");
            stringBuilder.AppendLine("<a class='nav-link dropdown-toggle' href='http://example.com' id='dropdown04' data-toggle='dropdown' aria-haspopup='true' aria-expanded='false'>Model Events</a>");
            stringBuilder.AppendLine("<div class='dropdown-menu' aria-labelledby='dropdown04'>");

            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</li>");
            stringBuilder.AppendLine("</ul>");

            stringBuilder.AppendLine("</form>");
            stringBuilder.AppendLine("</div>");
            stringBuilder.AppendLine("</nav>");

            _stringBuilder.Append(stringBuilder.ToString());
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
            _stringBuilder.Append(text);
        }

        public void CloseAllsDiv()
        {
            while (_tags.Any())
            {
                CloseDiv();
            }
        }

        public void CloseDiv()
        {
            if (!_tags.Any())
                return;

            _stringBuilder.Append("</div>");
            _tags.RemoveAt(0);
        }

        public void EndBody()
        {
            _stringBuilder.Append("</Body>");
        }

        public void IndexTitle(HTMLIndexedItems indexItem)
        {
            string linkName = indexItem.Title;
            Append($"<li><a href='#{linkName}'>{linkName}</a></li>");
        }

        public void Output()
        {
            _pageDetails.FullLocalFilename = @$"{_pageDetails.RootAddress}\\{HtmlPath}\\{HtmlTitle}";

            Directory.CreateDirectory(_pageDetails.RootAddress);
            Directory.CreateDirectory(_pageDetails.RootAddress + "\\" + HtmlPath);
            if (!_pageDetails.DontBuildPage)
                File.WriteAllText(_pageDetails.FullLocalFilename, _stringBuilder.ToString());
        }

        public void StartBody()
        {
            _stringBuilder.Append($"<Body><!--{DateTime.Now.ToShortDateString()}-->");
        }

        public void StartDiv(string tag)
        {
            _stringBuilder.Append(tag);
            _tags.Add("/div");
        }

        private void PopulatePageAddresses(NavigationTypes navigationType, List<NavigationPageAddressDetails> pageAddress)
        {
            var siteBuilder = SiteBuilderServiceLocator.Instance.GetService<IBuildSite>();
            var navs = siteBuilder.AllPages.Where(x => x.WebPage.NavigationTypes == navigationType);
            navs = navs.OrderBy(x => x.WebPage.NavigationIndex);

            string[] parts = HtmlPath.Split("\\", StringSplitOptions.RemoveEmptyEntries);
            int count = parts.Length;

            foreach (var nav in navs)
            {
                string htmlPath = "";
                if (!string.IsNullOrWhiteSpace(nav.WebPage.HtmlPath))
                    htmlPath = nav.WebPage.HtmlPath + "/";

                pageAddress.Add(
                        new NavigationPageAddressDetails()
                        {
                            Address = $"{htmlPath}{nav.WebPage.HtmlTitle}",
                            Title = nav.MenuTitle
                        }
                    );
            }
        }
    }
}