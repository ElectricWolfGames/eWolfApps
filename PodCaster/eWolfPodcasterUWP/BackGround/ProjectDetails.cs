using eWolfPodcasterCore.Interfaces;
using System;
using System.IO;

namespace eWolfPodcasterUWP.BackGround
{
    public class ProjectDetails : IProjectDetails
    {
        public string GetBaseFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf", "PodCaster");
        }

        public string GetDownloadFolder()
        {
            return Path.Combine(GetBaseFolder(), "Downloads");
        }

        public string GetLibraryPath()
        {
            return Path.Combine(GetBaseFolder(), "PodcastList.xml");
        }

        public string GetOutputFolder()
        {
            return GetBaseFolder();
        }
    }
}
