using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace eWolfPodcasterCoreUnitTests.Helpers
{
    public class CategoryHelperTests
    {
        [Test]
        public void ShouldGetAllCategory()
        {
            IList<ShowControl> shows = new List<ShowControl>();

            ShowControl s = new ShowControl()
            {
                Title = "My Title A",
                RssFeed = "Somewhere"
            };
            s.ShowOption.Category = "Cat One";

            ShowControl sb = new ShowControl()
            {
                Title = "My Title B",
                RssFeed = "elseWhere"
            };
            sb.ShowOption.Category = "Cat Two";

            shows.Add(s);
            shows.Add(sb);

            IReadOnlyCollection<string> cats = CategoryHelper.GetAllCategoriesFromShows(shows);
            cats.Should().HaveCount(2);
            cats.Should().Contain("Cat Two");
            cats.Should().Contain("Cat One");
        }

        [Test]
        public void ShouldGetAllShowsForCat()
        {
            IList<ShowControl> shows = new List<ShowControl>();

            ShowControl s = new ShowControl()
            {
                Title = "My Title A",
                RssFeed = "Somewhere"
            };
            s.ShowOption.Category = "Cat One";

            ShowControl sb = new ShowControl()
            {
                Title = "My Title B",
                RssFeed = "elseWhere"
            };
            sb.ShowOption.Category = "Cat Two";

            ShowControl sc = new ShowControl()
            {
                Title = "My Title C",
                RssFeed = "elseWhere new"
            };
            sc.ShowOption.Category = "Cat Two";

            shows.Add(s);
            shows.Add(sb);
            shows.Add(sc);

            IReadOnlyCollection<ShowControl> cats = CategoryHelper.GetAllShowsForCategory(shows, "Cat Two");
            cats.Should().HaveCount(2);

            var showTest = cats.First(x => x.Title == "My Title B");
            showTest.Should().NotBeNull();

            showTest = cats.First(x => x.Title == "My Title C");
            showTest.Should().NotBeNull();
        }
    }
}
