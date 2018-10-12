using eWolfCommon.Helpers;
using eWolfPodcasterCore.Library;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Services
{
    public class ShowLibraryService
    {
        private readonly List<ShowLibraryData> _library = new List<ShowLibraryData>();

        public ShowLibraryService()
        {
        }

        public List<ShowLibraryData> GetList()
        {
            return _library;
        }

        public static ShowLibraryService GetLibrary
        {
            get
            {
                return ServiceLocator.Instance.GetService<ShowLibraryService>();
            }
        }

        public void Load(string file)
        {
            eWolfPodcast eWolfPodcast = ReadWriteFileHelper.ReadFromXmlFile<eWolfPodcast>(file);
            ProcessFiles(eWolfPodcast);
        }

        internal void ProcessFiles(eWolfPodcast eWolfPodcast)
        {
            _library.Clear();

            foreach (eWolfPodcastShows podcast in eWolfPodcast.Shows)
            {
                if (podcast != null && podcast.Show != null)
                {
                    ShowLibraryData sld = new ShowLibraryData
                    {
                        Name = podcast.Show.Name,
                        Description = podcast.Show.Description,
                        Catergery = podcast.Show.Category,
                        URL = podcast.Show.Url
                    };
                    _library.Add(sld);
                }
            }
        }
    }
}
