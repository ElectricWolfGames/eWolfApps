﻿using eWolfBootstrap.SiteBuilder.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace eWolfBootstrap.SiteBuilder

{
    public abstract class PageDetails : ISitePageDetails
    {
        public string DisplayTitle { get; set; }
        public bool DontBuildPage { get; set; }
        public string FullLocalFilename { get; set; }
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
    }
}