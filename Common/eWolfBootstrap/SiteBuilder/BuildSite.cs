using System.Collections.Generic;
using System.Diagnostics;
using eWolfBootstrap.SiteBuilder.Interfaces;

namespace eWolfBootstrap.SiteBuilder
{
    public class BuildSite
    {
        private List<ISitePageDetails> _allPages;
        public string WebSiteRootAddress { get; set; } = @"D:\Projects\ElectricWolfWebBuilder\eWolfSiteBuilder\DemoSite\";

        public void Create()
        {
            foreach (var page in _allPages)
            {
                page.RootAddress = WebSiteRootAddress;
                page.CreatePage();
            }
        }

        public void OpenHomePage()
        {
            OpenSite(_allPages[0]);
        }

        public void PreProcess(System.Reflection.Assembly assembly)
        {
            _allPages = PageDetails.GetAllPages(assembly);
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
}