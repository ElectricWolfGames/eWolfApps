using System.Collections.Generic;

namespace AudioWolfStandard.Options
{
    public class OptionsHolder
    {
        private readonly List<string> _pathsToSearch = new List<string>();

        public OptionsHolder()
        {
            _pathsToSearch.Add(@"C:\GitHub\eWolfApps\AudioWolf\ExmapleData\");
        }

        public List<string> PathsToSearch => _pathsToSearch;
    }
}
