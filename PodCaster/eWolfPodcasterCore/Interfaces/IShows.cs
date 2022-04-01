using eWolfPodcasterCore.Data;
using System.Collections.ObjectModel;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IShows
    {
        ObservableCollection<ShowControl> ShowList { get; }

        bool Add(ShowControl show);

        bool Contains(ShowControl show);

        ShowControl GetShowFromName(string showName);

        void Load(string outputFolder);

        void RemoveEpisodeFromShow(string showName, string episodeName);

        void RemoveShow(ShowControl itemToRemove);

        void ReplaceAllShows(Shows shows);

        void Save();

        bool UpdateNextRSSFeeds();
    }
}