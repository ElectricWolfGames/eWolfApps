using System.Collections.Generic;

namespace AudioWolfUI.Tags
{
    public class GlobalTagStore : TagHolderBase
    {
        public GlobalTagStore()
        {
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
