namespace eWolfPodcasterCore.Interfaces
{
    public interface IProjectDetails
    {
        string GetBaseFolder();

        string GetDownloadFolder();

        string GetLibraryPath();

        string GetOutputFolder();
    }
}
