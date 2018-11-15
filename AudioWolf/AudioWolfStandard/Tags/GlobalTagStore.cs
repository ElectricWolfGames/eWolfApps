using System.Collections.Generic;

namespace AudioWolfStandard.Tags
{
    public class GlobalTagStore : TagHolderBase
    {
        public GlobalTagStore()
        {
            // TODO : Need to load in the globel tag list.
        }

        public override void Add(string name)
        {
            TagData td = new TagData
            {
                Name = name
            };
            Add(td);
        }

        public void Add(TagData tag)
        {
            TagData tdold = GetTagWithName(tag.Name);
            if (tdold != null)
                return;

            Tags.Add(tag);
        }

        public void AddTagRange(List<TagData> tags)
        {
            Tags.AddRange(tags);
        }
    }
}
