namespace eWolfBootstrap.SiteBuilder.Interfaces

{
    public interface ISitePageDetails
    {
        string FullLocalFilename { get; set; }

        string RootAddress { get; set; }

        void CreatePage();
    }
}