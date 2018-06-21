using eWolfPodcaster.Interfaces;

namespace eWolfPodcaster.Data
{
    public class ShowControl : Show, ISaveable
    {
        public string GetFileName
        {
            get { return Title + ".Show"; }
        }
    }
}
