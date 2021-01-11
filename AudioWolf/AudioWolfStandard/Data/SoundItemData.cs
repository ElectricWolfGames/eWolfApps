using AudioWolfStandard.Services;
using AudioWolfStandard.Tags;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace AudioWolfStandard.Data
{
    [Serializable]
    public class SoundItemData
    {
        [NonSerialized]
        private BitmapImage _image = null;

        private string _name;

        private TagHolder _tagHolder;

        public string FullPath { get; set; }

        public BitmapImage Image { get => _image; set => _image = value; }

        public string ImagePath { get; set; }

        public float Length { get; set; }

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
                // tood need to update the length
            }
        }

        public bool AddTag(string name)
        {
            return _tagHolder.Add(name);
        }

        public void Clear()
        {
            _tagHolder.ClearTags();
        }

        public List<TagData> Tags
        {
            get
            {
                return _tagHolder.Tags;
            }
        }
    }
}