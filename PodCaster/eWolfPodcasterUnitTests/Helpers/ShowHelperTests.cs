using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace eWolfPodcasterCoreUnitTests.Helpers
{
    public class ShowHelperTests
    {
        [Test]
        public void ShouldSortEpisodesByDate()
        {
            List<ShowControl> shows = new List<ShowControl>();
            var show = CreateBasicShowcontrol("ShowOne");
            shows.Add(show);

            EpisodeControl e = new EpisodeControl();
            e.Title = "A";
            e.PublishedDate = new System.DateTime(2018, 06, 15);
            show.Episodes.Add(e);

            e = new EpisodeControl();
            e.Title = "B";
            e.PublishedDate = new System.DateTime(2019, 06, 15);
            show.Episodes.Add(e);

            List<EpisodeControl> orderEpisodes = ShowHelper.GetOrderEpisodes(shows);
            orderEpisodes.Should().HaveCount(2);
            orderEpisodes[0].PublishedDate.Year.Should().Be(2019);
            orderEpisodes[1].PublishedDate.Year.Should().Be(2018);
        }

        private ShowControl CreateBasicShowcontrol(string name)
        {
            ShowControl s = new ShowControl()
            {
                Title = name,
                RssFeed = "Somewhere",
            };
            s.ShowOption.ShowStorage = ShowStorageType.RssFeed;
            s.ShowOption.Category = "Cat";

            return s;
        }
    }
}
