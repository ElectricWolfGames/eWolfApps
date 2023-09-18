using eWolfBootstrap.Builders;
using eWolfBootstrap.SiteBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eWolfBootstrap.SiteBuilder

{
    public abstract class PageDetails : ISitePageDetails
    {
        private string[] _linkedPackages = new string[0];
        public string DisplayTitle { get; set; }
        public bool DontBuildPage { get; set; }
        public bool DontShowNavigation { get; set; }
        public string FullLocalFilename { get; set; }
        public List<string> Keywords { get; set; } = new List<string>();
        public string MenuTitle { get; set; }
        public string RootAddress { get; set; }
        public WebPage WebPage { get; protected set; }




        public static List<ISitePageDetails> GetAllPages(Assembly assembly)
        {
            IEnumerable<ISitePageDetails> updates = from t in assembly.GetTypes()
                                                    where t.GetInterfaces().Contains(typeof(ISitePageDetails))
                                                          && t.GetConstructor(Type.EmptyTypes) != null
                                                    select Activator.CreateInstance(t) as ISitePageDetails;

            return updates.Select(x => x).ToList();
        }

        public abstract void CreatePage();
        public virtual void NoBuildAction()
        {
        }

        public string GetRooloffSet()
        {
            string[] pathParts = WebPage.HtmlPath.Split("\\", StringSplitOptions.RemoveEmptyEntries);

            int folderCount = pathParts.Length;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < folderCount; i++)
            {
                sb.Append("../");
            }

            return sb.ToString();
        }

        public string[] LinkedPackages
        {
            get
            {
                return _linkedPackages;
            }
        }

        public void LinkedThePackages(params string[] packages)
        {
            _linkedPackages = packages;
        }

        protected void AddIndexItemsWithSideBar(List<HTMLIndexedItems> items)
        {
            // Start Side Bar
            WebPage.Append("<div class=\"d-flex flex-sm-row flex-column justify-content-between\">");
            WebPage.Append("<div class=\"border-end bg-white col-md-2\" id=\"sidebar-wrapper\">");
            WebPage.Append("<div class=\"sticky-top\">");
            WebPage.Append("<div class=\"list-group list-group-flush\">");

            // Create Index
            HTMLBuilder options = new HTMLBuilder();
            options.CreateSideBarItems(items);
            WebPage.Append(options.Output());

            // End Side Bar
            WebPage.Append("</div>");
            WebPage.Append("</div>");
            WebPage.Append("</div>");

            WebPage.Append("<div id=\"page-content-wrapper col-md-8\">");

            // Add all pages here
            options = new HTMLBuilder();
            options.CreateIndexItems(items, "12");
            WebPage.Append(options.Output());
            WebPage.Append("</div>");

            // End
            WebPage.Append("</div>");
            WebPage.Append("</div>");
        }
    }
}