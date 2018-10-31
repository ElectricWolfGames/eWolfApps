using System.Collections.Generic;

namespace AudioWolfStandard.Options
{
    public class OptionsHolder
    {
        private readonly List<string> _pathsToSearch = new List<string>();

        public OptionsHolder()
        {
            _pathsToSearch.Add(@"D:\OffLine\Music\1991 Final Fantasy IV\");
        }

        public List<string> PathsToSearch => _pathsToSearch;
    }
}
