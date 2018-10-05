using eWolfPodcasterCore.Library;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using eWolfCommon.Helpers;

namespace eWolfPodcasterCore.Services
{
    public class ShowLibraryService
    {
        // list of shows we can add
        // each show needs
        // need to be able to load this in from a file or from the EW site.
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
            eWolfPodcast wp = ReadWriteFileHelper.ReadFromXmlFile<eWolfPodcast>(file);
            // need to add the files to the library
        }
    }
}
