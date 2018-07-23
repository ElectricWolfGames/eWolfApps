using eWolfPodcasterCore.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace eWolfPodcasterCore.Helpers
{
    public static class CategoryHelper
    {
        public static string[] GetAll(ObservableCollection<ShowControl> shows)
        {
            List<string> cats = new List<string>();
            foreach (var show in shows)
            {
                if (!string.IsNullOrWhiteSpace(show.ShowOption.Category))
                {
                    if (!cats.Contains(show.ShowOption.Category))
                    {
                        cats.Add(show.ShowOption.Category);
                    }
                }
            }

            return cats.ToArray();
        }

        public static ShowControl[] GetAllShowsInCategory(ObservableCollection<ShowControl> shows, string category)
        {
            List<ShowControl> showInCat = new List<ShowControl>();
            foreach (var show in shows)
            {
                if (show.ShowOption.Category == category)
                {
                    showInCat.Add(show);
                }
            }

            return showInCat.ToArray();
        }
    }
}
