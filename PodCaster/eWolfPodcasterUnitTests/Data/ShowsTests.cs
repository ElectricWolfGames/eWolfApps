using eWolfPodcaster.Data;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterUnitTests
{
    public class ShowsTests
    {
        [Test]
        public void ShouldAddShowToShowList()
        {
            ShowControl s = new ShowControl()
            {
                Title = "My Title",
                RssFeed = "Somewhere"
            };

            Shows shows = new Shows();
            shows.Add(s);

            shows.Count.Should().Be(1);
        }

        [Test]
        public void ShouldNotAddAnotherShowWithSameRSSFeedOrTitle()
        {
            ShowControl s = new ShowControl()
            {
                Title = "My Title",
                RssFeed = "Somewhere"
            };
            ShowControl sb = new ShowControl()
            {
                Title = "My Title",
                RssFeed = "elseWhere"
            };

            Shows shows = new Shows();
            shows.Add(s);
            shows.Add(s);
            shows.Add(sb);

            shows.Count.Should().Be(1);
        }

        [Test]
        public void ShouldBeAbleToAddTwoDifferentShows()
        {
            ShowControl s = new ShowControl()
            {
                Title = "My Title A",
                RssFeed = "Somewhere"
            };
            ShowControl sb = new ShowControl()
            {
                Title = "My Title B",
                RssFeed = "elseWhere"
            };

            Shows shows = new Shows();
            shows.Add(s);
            shows.Add(sb);

            shows.Count.Should().Be(2);
        }
    }
}
