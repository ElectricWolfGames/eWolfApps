namespace eWolfBootstrap.SiteBuilder.Interfaces
{
    public interface ISitePageDetails
    {
        string DisplayTitle { get; set; }
        bool DontBuildPage { get; set; }
        bool DontShowNavigation { get; set; }
        string FullLocalFilename { get; set; }
        string MenuTitle { get; set; }
        string RootAddress { get; set; }// do we need this??

        WebPage WebPage { get; }

        void CreatePage();
        void NoBuildAction();

        string GetRooloffSet();
    }
}