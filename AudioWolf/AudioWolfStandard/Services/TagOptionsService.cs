using AudioWolfUI.Tags;

namespace AudioWolfStandard.Services
{
    public class TagOptionsService
    {
        private readonly TagOptions _tagOptions = new TagOptions();

        public TagOptionsService()
        {
        }

        public TagOptionsService(TagOptions tagOptions)
        {
            _tagOptions = tagOptions;
        }

        public TagOptions TagOptions => _tagOptions;
    }
}
