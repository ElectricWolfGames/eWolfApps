using System.Collections.Generic;

namespace eWolfTagHolders.Tags
{
    public class GroupTags
    {
        public GroupTags(string masterTag)
        {
            MasterTag = masterTag;
        }

        public List<string> ClearableTags { get; set; } = new List<string>();
        public List<string> IndedifiableTags { get; set; } = new List<string>();
        public string MasterTag { get; set; }

        public void Add(string tag)
        {
            IndedifiableTags.Add(tag);
        }

        public void AddClearTags(string tag)
        {
            ClearableTags.Add(tag);
        }
    }
}