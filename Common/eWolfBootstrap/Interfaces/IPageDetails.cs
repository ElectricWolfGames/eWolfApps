namespace eWolfBootstrap.Interfaces
{
    public interface IPageDetails
    {
        string HtmlFileName { get; }
        string HtmlPath { get; }
        string LocalPath { get; }
        string PageTitle { get; }
    }
}