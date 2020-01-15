using eWolfCommon.Helpers;
using eWolfCommon.Reflection;
using eWolfPodcasterCore.Library;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace eWolfPodcasterCore.Services
{
    public class ShowLibraryService
    {
        private readonly List<ShowLibraryData> _library = new List<ShowLibraryData>();

        public ShowLibraryService()
        {
        }

        public static ShowLibraryService GetLibrary
        {
            get
            {
                return ServiceLocator.Instance.GetService<ShowLibraryService>();
            }
        }

        public List<ShowLibraryData> GetList(string name)
        {
            return _library.Where(x => x.Catergery == name).ToList();
        }

        public List<ShowLibraryData> GetList()
        {
            return _library;
        }

        public List<CatergeryData> Groups()
        {
            List<CatergeryData> groups = new List<CatergeryData>();

            groups.AddRange(DefaultGroups());

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

        private IEnumerable<CatergeryData> DefaultGroups()
        {
            List<CatergeryData> catergerys = new List<CatergeryData>();

            catergerys.AddRange(CategoryHolderService.GetAllCategories);
            return catergerys.OrderBy(x => x.Name);
        }

        public void Load(string fileName)
        {
            CreateLibraryFileFromProject(fileName);

            try
            {
                eWolfPodcast eWolfPodcast = ReadWriteFileHelper.ReadFromXmlFile<eWolfPodcast>(fileName);
                ProcessFiles(eWolfPodcast);
            }
            catch
            {
                // fail safe
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

        private void CreateLibraryFileFromProject(string fileName)
        {
            string file = ProjectItems.LoadFile("eWolfPodcasterCore.RawData.PodcastList.xml");
            File.WriteAllText(fileName, file);
        }
    }
}