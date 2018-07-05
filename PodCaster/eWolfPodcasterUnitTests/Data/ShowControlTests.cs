using eWolfPodcasterCore.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace eWolfPodcasterUnitTests.Data
{
    public class ShowControlTests
    {
        [Test]
        public void ShouldUpdateNewEpisodes()
        {
            ShowControl sc = new ShowControl();

            List<EpisodeControl> episodes = new List<EpisodeControl>();
            EpisodeControl episode = new EpisodeControl()
            {
                Title = "MyFirstEpisode",
                PodcastURL = "UrlA"
            };

            episodes.Add(episode);

            episode = new EpisodeControl()
            {
                Title = "MySecondEpisode",
                PodcastURL = "UrlB"
            };

            episodes.Add(episode);

            sc.UpdateEpisode(episodes);

            sc.Episodes.Should().HaveCount(2);
        }

        [Test]
        public void ShouldNotAddSameEpisodes()
        {
            ShowControl sc = new ShowControl();

            List<EpisodeControl> episodes = new List<EpisodeControl>();
            EpisodeControl episode = new EpisodeControl()
            {
                Title = "MyFirstEpisode",
                PodcastURL = "UrlA"
            };

            episodes.Add(episode);
            episodes.Add(episode);

            sc.UpdateEpisode(episodes);
            sc.Episodes.Should().HaveCount(1);
        }
    }
}
