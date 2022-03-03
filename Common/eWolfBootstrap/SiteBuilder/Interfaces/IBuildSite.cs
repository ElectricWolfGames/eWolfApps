using System.Collections.Generic;

namespace eWolfBootstrap.SiteBuilder.Interfaces

{
    public interface IBuildSite
    {
        List<ISitePageDetails> AllPages { get; set; }
        string WebSiteRootAddress { get; set; }
    }

    public interface INavigationBuilder
    {
    }
}