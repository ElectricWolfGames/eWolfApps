using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace eWolfPodcasterCoreUnitTests.Helpers
{
    public class ShowHelperTests
    {
        [Test]
        public void ShouldSortEpisodesByDate()
        {
            List<ShowControl> shows = new List<ShowControl>();
            ShowControl show = CreateBasicShowcontrol("ShowOne");
            shows.Add(show);

            show.Episodes.Add(CreateEpisode("a", new DateTime(2018, 06, 15)));
            show.Episodes.Add(CreateEpisode("b", new DateTime(2019, 07, 15)));

            List<EpisodeControl> orderEpisodes = ShowHelper.GetOrderEpisodes(shows);
            orderEpisodes.Should().HaveCount(2);
            orderEpisodes[0].PublishedDate.Year.Should().Be(2019);
            orderEpisodes[1].PublishedDate.Year.Should().Be(2018);
        }

        [Test]
        public void ShouldSortLocalFilesEpisodesByUrl()
        {
            List<ShowControl> shows = new List<ShowControl>();
            ShowControl show = CreateBasicShowcontrol("ShowOne");
            show.ShowOption.ShowStorage = ShowStorageType.LocalStorage;
            shows.Add(show);

            show.Episodes.Add(CreateEpisode("b", new DateTime(2018, 06, 15)));
            show.Episodes.Add(CreateEpisode("a", new DateTime(2019, 07, 15)));

            List<EpisodeControl> orderEpisodes = ShowHelper.GetOrderEpisodes(shows);
            orderEpisodes.Should().HaveCount(2);
            orderEpisodes[0].PodcastURL.Should().Be("a");
            orderEpisodes[0].PublishedDate.Year.Should().Be(2019);
            orderEpisodes[1].PodcastURL.Should().Be("b");
            orderEpisodes[1].PublishedDate.Year.Should().Be(2018);
        }

        [Test]
        public void ShouldSortEpisodesByDateForManyShows()
        {
            List<ShowControl> shows = new List<ShowControl>();
            ShowControl show = CreateBasicShowcontrol("ShowOne");
            shows.Add(show);
            ShowControl showB = CreateBasicShowcontrol("ShowTwo");
            shows.Add(showB);

            show.Episodes.Add(CreateEpisode("a", new DateTime(2018, 06, 15)));
            showB.Episodes.Add(CreateEpisode("b", new DateTime(2019, 07, 15)));

            List<EpisodeControl> orderEpisodes = ShowHelper.GetOrderEpisodes(shows);

            orderEpisodes.Should().HaveCount(2);
            orderEpisodes[0].PublishedDate.Year.Should().Be(2019);
            orderEpisodes[1].PublishedDate.Year.Should().Be(2018);
        }

        [Test]
        public void ShouldClearAllWatchShows()
        {
            List<ShowControl> shows = new List<ShowControl>();
            ShowControl show = CreateBasicShowcontrol("ShowOne");
            shows.Add(show);
            ShowControl showB = CreateBasicShowcontrol("ShowTwo");
            shows.Add(showB);

            show.Episodes.Add(CreateEpisode("a", new DateTime(2018, 06, 15)));
            showB.Episodes.Add(CreateEpisode("b", new DateTime(2019, 07, 15)));

            ShowHelper.ClearWatched(shows);

            shows[0].Episodes[0].Hidden.Should().BeFalse();
            shows[0].Episodes[0].PlayedLength.Should().Be(0);
            shows[0].Episodes[0].PlayedLengthScaled.Should().Be(0);

            shows[1].Episodes[0].Hidden.Should().BeFalse();
            shows[1].Episodes[0].PlayedLength.Should().Be(0);
            shows[1].Episodes[0].PlayedLengthScaled.Should().Be(0);
        }

        private EpisodeControl CreateEpisode(string title, DateTime date)
        {
            EpisodeControl e = new EpisodeControl
            {
                Title = title,
                PublishedDate = date,
                PodcastURL = title,
                PlayedLength = 150,
                PlayedLengthScaled = 150,
                Hidden = true
            };
            return e;
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