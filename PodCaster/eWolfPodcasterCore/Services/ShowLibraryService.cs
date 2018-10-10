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
            _library.Clear();
            eWolfPodcast wp = ReadWriteFileHelper.ReadFromXmlFile<eWolfPodcast>(file);

            foreach (var p in wp.Shows)
            {
                if (p != null && p.Show != null)
                {
                    ShowLibraryData sld = new ShowLibraryData();
                    sld.Name = p.Show.Name;
                    sld.Catergery = p.Show.Category;
                    sld.URL = p.Show.Url;
                    _library.Add(sld);
                }
            }
            int i = 0;
            i++;
        }
    }
}
