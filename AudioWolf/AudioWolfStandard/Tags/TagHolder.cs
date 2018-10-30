using System.Collections.Generic;
using System.Linq;

namespace AudioWolfUI.Tags
{
    public class TagHolder
    {
        private readonly TagOptions _options;
        private string _orginalName = string.Empty;
        private string _orginalStartOfName = string.Empty;

        public TagHolder(TagOptions options)
        {
            _options = options;
        }

        public List<TagData> Tags { get; } = new List<TagData>();

        public void Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return;

            name = ClenseName(name);
            TagData tdold = GetTagWithName(name);
            if (tdold != null)
                return;

            TagData td = new TagData
            {
                Name = name
            };

            Tags.Add(td);
        }

        public void ClearTags()
        {
            Tags.Clear();
        }

        public string CreateNameFromTags()
        {
            string fullName = string.Empty;
            if (_options.KeepFirstPartOfName)
            {
                fullName = _orginalStartOfName + _options.Seperator.ToString();
            }
            // do we need to sort the list ?
            fullName += string.Join(_options.Seperator.ToString(), Tags.Select(x => x.Name).ToList());
            return fullName;
        }

        public void SplitName(string name)
        {
            _orginalName = name;
            ClearTags();

            bool skipFirst = _options.KeepFirstPartOfName;

            string[] parts = name.Split(_options.Seperator);
            foreach (string part in parts)
            {
                if (skipFirst)
                {
                    skipFirst = false;
                    _orginalStartOfName = part;
                    continue;
                }
                Add(part);
            }
        }

        private string ClenseName(string name)
        {
            return name.Trim();
        }

        private TagData GetTagWithName(string name)
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
