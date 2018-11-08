using AudioWolfStandard.Services;
using System.Linq;

namespace AudioWolfStandard.Tags
{
    public class TagHolder : TagHolderBase
    {
        private readonly TagOptions _options;
        private string _orginalName = string.Empty;
        private string _orginalStartOfName = string.Empty;

        public TagHolder(TagOptions options)
        {
            _options = options;
        }

        public override void Add(string name)
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

            GlobalTagStore gts = ServiceLocator.Instance.GetService<GlobalTagStore>();

            Tags.Add(td);
            gts.Add(td);
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

            return name;
        }

        private string ClenseName(string name)
        {
            return name.Trim();
        }
    }
}
