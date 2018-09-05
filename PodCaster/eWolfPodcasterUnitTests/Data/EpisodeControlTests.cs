using eWolfPodcasterCore.Data;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterCoreUnitTests.Data
{
    public class EpisodeControlTests
    {
        [Test]
        public void ShouldNotFalloverForFakeElementDuration()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("itunes:duration", "ALongTime");

            ec.PlayedDetails.ShowLength.Should().Be(0);
        }

        [Test]
        public void ShouldSeeEpisodesAreSame()
        {
            EpisodeControl ec = new EpisodeControl
            {
                Title = "Title",
                PodcastURL = "PodcastURL"
            };

            EpisodeControl ecB = new EpisodeControl
            {
                Title = "Title",
                PodcastURL = "PodcastURL"
            };

            ec.SameAs(ecB).Should().BeTrue();
        }

        [Test]
        public void ShouldSetTheElementDescription()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("description", "Description");

            ec.Description.Should().Be("Description");
        }

        [Test]
        public void ShouldSetTheElementDuration()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("itunes:duration", "10:10");

            ec.PlayedDetails.ShowLength.Should().Be(1010);
        }

        [Test]
        public void ShouldSetTheElementItunesDescription()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("itunes:summary", "Description");

            ec.Description.Should().Be("Description");
        }

        [Test]
        public void ShouldSetTheElementLinkOnlyIfNotEmpty()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("link", "UrlFirst");

            ec.PodcastURL.Should().Be("UrlFirst");

            ec.SetTextNodeData("link", string.Empty);
            ec.PodcastURL.Should().Be("UrlFirst");
        }

        [Test]
        public void ShouldSetTheElementPubDate()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("pubDate", "Sun, 08 Jan 2017 06:47:50 GMT");

            ec.PublishedDate.ToString("dd/MM/yyyy hh:mm:ss").Should().Be("08/01/2017 06:47:50");
        }

        [Test]
        public void ShouldSetTheElementTitle()
        {
            EpisodeControl ec = new EpisodeControl();
            ec.SetTextNodeData("title", "MyTitle");

            ec.Title.Should().Be("MyTitle");
        }

        [Test]
        public void ShouldReturnTrueForSameEpisodes()
        {
            EpisodeControl episodeA = CreateEpisode("MyFirstEpisode", "Url");
            EpisodeControl episodeB = CreateEpisode("MyFirstEpisode", "Url");

            episodeA.SameAs(episodeB).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTrueForUniqueTitleEpisodes()
        {
            EpisodeControl episodeA = CreateEpisode("MyFirstEpisodeDiff", "Url");
            EpisodeControl episodeB = CreateEpisode("MyFirstEpisode", "Url");

            episodeA.SameAs(episodeB).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnTrueForUniqueUrlEpisodes()
        {
            EpisodeControl episodeA = CreateEpisode("MyFirstEpisode", "UrlDiff");
            EpisodeControl episodeB = CreateEpisode("MyFirstEpisode", "Url");

            episodeA.SameAs(episodeB).Should().BeTrue();
        }

        [Test]
        public void ShouldReturnFalseForUniqueEpisodes()
        {
            EpisodeControl episodeA = CreateEpisode("MyFirstEpisodediff", "UrlDiff");
            EpisodeControl episodeB = CreateEpisode("MyFirstEpisode", "Url");

            episodeA.SameAs(episodeB).Should().BeFalse();
        }

        private static EpisodeControl CreateEpisode(string title, string url)
        {
            return new EpisodeControl()
            {
                Title = title,
                PodcastURL = url
            };
        }
    }
}
