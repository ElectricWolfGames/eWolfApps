using AudioWolfStandard.Options;

namespace AudioWolfStandard.Services
{
    public class OptionService
    {
        private readonly OptionsHolder _optionHolder = new OptionsHolder();

        public OptionsHolder OptionsHolder
        {
            get
            {
                return _optionHolder;
            }
        }
    }
}
