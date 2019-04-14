using eWolfPodcasterCore.Data;
using System.Collections.ObjectModel;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IShows
    {
        void Save();

        void Load(string outputFolder);

        void RemoveShow(ShowControl itemToRemove);

        bool Contains(ShowControl show);

        bool Add(ShowControl show);

        bool UpdateNextRSSFeeds();

        void ReplaceAllShows(Shows shows);

        ObservableCollection<ShowControl> ShowList { get; }
    }
}
