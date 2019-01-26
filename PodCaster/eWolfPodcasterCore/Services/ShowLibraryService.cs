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
                if (!groups.Any(x => x.Name == showLibraryData.Catergery))
                {
                    CatergeryData cd = new CatergeryData(showLibraryData.Catergery);
                    groups.Add(cd);
                }
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

            foreach (eWolfPodcastShowsShow podcast in eWolfPodcast.Shows.Show)
            {
                if (podcast != null)
                {
                    ShowLibraryData sld = new ShowLibraryData
                    {
                        Name = podcast.Name,
                        Description = podcast.Description,
                        Catergery = podcast.Category,
                        URL = podcast.Url
                    };
                    _library.Add(sld);
                }
            }
        }
    }
}
