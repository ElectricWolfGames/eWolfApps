using eWolfPodcasterCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eWolfPodcasterCore.Helpers
{
    public static class ShowHelper
    {
        public static List<EpisodeControl> GetOrderEpisodes(List<ShowControl> shows)
        {
            List<EpisodeControl> orderedByDateList = new List<EpisodeControl>();
            ShowStorageType stype = ShowStorageType.RssFeed;
            foreach (ShowControl show in shows)
            {
                stype = show.ShowOption.ShowStorage;
                show.Episodes.ForEach(x => x.ShowName = show.Title);
                orderedByDateList.AddRange(show.Episodes);
            }

            if (stype == ShowStorageType.RssFeed)
            {
                orderedByDateList = orderedByDateList.OrderByDescending(x => x.PublishedDate.Ticks).ToList();
            }
            else
            {
                orderedByDateList = orderedByDateList.OrderBy(x => x.PodcastURL).ToList();
            }
            return orderedByDateList;
        }

        public static void ClearWatched(List<ShowControl> shows)
        {
            foreach (ShowControl show in shows)
            {
                show.UnwatchAll();
            }
        }

        public static List<ShowControl> GetAllShowsFromGroup(string groupName)
        {
            List<ShowControl> groupedShows = new List<ShowControl>();
            Shows shows = (Shows)Shows.GetShowService;

            List<ShowControl> showsInCat = shows.ShowInGroup(groupName);
            foreach (ShowControl show in showsInCat)
            {
                groupedShows.Add(show);
            }
            return groupedShows;
        }
    }
}
