using eWolfPodcasterCore.Data;
using System.Collections.Generic;

namespace eWolfPodcasterCore.Helpers
{

    public static class CategoryHelper
    {
        public static IReadOnlyCollection<string> GetAllCategoriesFromShows(IList<ShowControl> shows)
        {
            IList<string> cats = new List<string>();
            foreach (var show in shows)
            {
                if (string.IsNullOrWhiteSpace(show.ShowOption.Category))
                    continue;

                if (!cats.Contains(show.ShowOption.Category))
                {
                    cats.Add(show.ShowOption.Category);
                }
            }

            return (IReadOnlyCollection<string>)cats;
        }

        public static IReadOnlyCollection<ShowControl> GetAllShowsForCategory(IList<ShowControl> shows, string category)
        {
            IList<ShowControl> showInCat = new List<ShowControl>();
            foreach (var show in shows)
            {
                if (show.ShowOption.Category == category)
                {
                    showInCat.Add(show);
                }
            }

            return (IReadOnlyCollection<ShowControl>)showInCat;
        }
    }
}
