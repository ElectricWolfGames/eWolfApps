using AudioWolfStandard.Tags;

namespace AudioWolfStandard.Services
{
    public class TagOptionsService
    {
        private TagOptions _tagOptions = new TagOptions();

        public TagOptionsService(TagOptions tagOptions)
        {
            _tagOptions = tagOptions;
        }

        public TagOptionsService()
        {
            _tagOptions.TagInBoxs = false;
        }

        public TagOptions TagOptions
        {
            get
            {
                return _tagOptions;
            }
            set
            {
                _tagOptions = value;
            }
        }
    }
}
