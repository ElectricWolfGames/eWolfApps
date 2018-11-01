﻿using System.Collections.Generic;

namespace AudioWolfUI.Tags
{
    public abstract class TagHolderBase
    {
        public List<TagData> Tags { get; } = new List<TagData>();

        public abstract void Add(string name);

        public void ClearTags()
        {
            Tags.Clear();
        }

        protected TagData GetTagWithName(string name)
        {
            foreach (TagData td in Tags)
            {
                if (td.Name == name)
                    return td;
            }
            return null;
        }
    }
}
