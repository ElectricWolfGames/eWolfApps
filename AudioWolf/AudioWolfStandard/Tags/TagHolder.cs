using System;
using System.Linq;

namespace AudioWolfStandard.Tags
{
    [Serializable]
    public class TagHolder : TagHolderBase
    {
        private readonly TagOptions _options;
        private string _orginalName = string.Empty;
        private string _orginalStartOfName = string.Empty;

        public TagHolder(TagOptions options)
        {
            _options = options;
        }

        public override bool Add(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            name = ClenseName(name);
            TagData tdold = GetTagWithName(name);
            if (tdold != null)
                return false;

            TagData td = new TagData
            {
                Name = name
            };

            GlobalTagStore.AddTag(name);
            Tags.Add(td);
            return true;
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

            if (_options.TagNotStoredInFileName)
                return;

            ClearTags();

            bool skipFirst = _options.KeepFirstPartOfName;
            if (_options.TagInBoxs)
            {
                name = GetBoxContants(name);
            }

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

        public static string GetBoxContants(string name)
        {
            int pos = name.IndexOf('[');
            int posEnd = name.IndexOf(']');

            if (pos > -1)
            {
                name = name.Substring(pos + 1, posEnd - pos - 1);
            }
            else
            {
                name = string.Empty;
            }

            return name;
        }

        private string ClenseName(string name)
        {
            return name.Trim();
        }
    }
}
