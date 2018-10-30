using System.Collections.Generic;

namespace AudioWolfUI.Tags
{
    public class GlobalTagStore
    {
        // need to keepp a list of all the tags in the system.
        // need to save out theis list
        private static GlobalTagStore _globalTagStore = null;

        private GlobalTagStore()
        {
        }

        public static GlobalTagStore Instance
        {
            get
            {
                if (_globalTagStore == null)
                {
                    _globalTagStore = new GlobalTagStore();
                }
                return _globalTagStore;
            }
        }

        public List<TagData> Tags { get; } = new List<TagData>();

        public void AddTag(TagData tag)
        {
            Tags.Add(tag);
        }
    }
}
