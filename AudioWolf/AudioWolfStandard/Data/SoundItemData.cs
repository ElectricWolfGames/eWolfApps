using AudioWolfStandard.Services;
using AudioWolfUI.Tags;
using System.Collections.Generic;

namespace AudioWolfStandard.Data
{
    public class SoundItemData
    {
        private TagHolder _tagHolder;

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                TagOptionsService tos = ServiceLocator.Instance.GetService<TagOptionsService>();
                _tagHolder = new TagHolder(tos.TagOptions);
                _tagHolder.SplitName(_name);
            }
        }

        public string FullPath { get; set; }
        public string ImagePath { get; set; }

        public List<TagData> Tags
        {
            get
            {
                return _tagHolder.Tags;
            }
        }
    }
}
