﻿using eWolfBootstrap.SiteBuilder.Interfaces;
using System.Collections.Generic;
using System.Diagnostics;

namespace eWolfBootstrap.SiteBuilder
{
    public class BuildSite : IBuildSite
    {
        public List<ISitePageDetails> AllPages { get; set; }

        public string WebSiteRootAddress { get; set; }

        public void Create()
        {
            foreach (var page in AllPages)
            {
                page.RootAddress = WebSiteRootAddress;
                page.CreatePage();
                if (page.DisplayTitle.Contains("Modular"))
                    OpenSite(page);
            }
        }

        public void OpenHomePage()
        {
            OpenSite(AllPages[0]);
        }

        public void PreProcess(System.Reflection.Assembly assembly)
        {
            AllPages = PageDetails.GetAllPages(assembly);
        }

        private static void OpenSite(ISitePageDetails pageDetails)
        {
            var psi = new ProcessStartInfo
            {
                FileName = pageDetails.FullLocalFilename,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }

    public class NavigationBuilder : INavigationBuilder
    {
    }
}