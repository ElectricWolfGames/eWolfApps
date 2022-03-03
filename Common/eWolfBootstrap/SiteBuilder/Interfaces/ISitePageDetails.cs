namespace eWolfBootstrap.SiteBuilder.Interfaces
{
    public interface ISitePageDetails
    {
        string DisplayTitle { get; set; }
        string FullLocalFilename { get; set; }
        string MenuTitle { get; set; }

        string RootAddress { get; set; }// do we need this??

        WebPage WebPage { get; }

        void CreatePage();

        string GetRooloffSet();
    }
}