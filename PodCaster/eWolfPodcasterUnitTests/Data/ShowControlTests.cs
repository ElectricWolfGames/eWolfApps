using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Services;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace eWolfPodcasterCoreUnitTests.Data
{
    public class ShowControlTests
    {
        [SetUp]
        public void Setup()
        {
            ServiceLocator.Instance.InjectService<IShows>(new FakeShows());
        }

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

        [Test]
        public void ShouldShowNameCount()
        {
            ShowControl sc = new ShowControl
            {
                Title = "MyShow"
            };

            List<EpisodeControl> episodes = new List<EpisodeControl>();
            EpisodeControl episode = new EpisodeControl()
            {
                Title = "MyFirstEpisode",
                PodcastURL = "UrlA"
            };

            episodes.Add(episode);

            sc.UpdateEpisode(episodes);
            sc.ToString().Should().StartWith("MyShow");
            sc.ToString().Should().Contain("1");
        }
    }
}
