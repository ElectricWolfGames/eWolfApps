using System;

namespace eWolfBootstrap.SiteBuilder
{
    public class HTMLIndexedItems
    {
        public HTMLIndexedItems(string title, Func<string, string> body)
        {
            Title = title;
            Body = body;

        }
        public string Title;
        public Func<string, string> Body;
    }
}