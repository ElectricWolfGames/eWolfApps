using eWolfCommon.Helpers;
using eWolfPodcasterCore.Library;
using System.Collections.Generic;
using System.Linq;

namespace eWolfPodcasterCore.Services
{
    public class ShowLibraryService
    {
        private readonly List<ShowLibraryData> _library = new List<ShowLibraryData>();

        public ShowLibraryService()
        {
        }

        public List<ShowLibraryData> GetList(string name)
        {
            return _library.Where(x => x.Catergery == name).ToList();
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

        public List<CatergeryData> Groups()
        {
            List<CatergeryData> groups = new List<CatergeryData>();
            foreach (ShowLibraryData showLibraryData in _library)
            {
                CatergeryData cd = new CatergeryData
                {
                    Name = showLibraryData.Catergery
                };
                groups.Add(cd);
            }

            return groups;
        }

        public void Load(string file)
        {
            try
            {
                eWolfPodcast eWolfPodcast = ReadWriteFileHelper.ReadFromXmlFile<eWolfPodcast>(file);
                ProcessFiles(eWolfPodcast);
            }
            catch
            {
                // Can't find or load library file
            }
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
