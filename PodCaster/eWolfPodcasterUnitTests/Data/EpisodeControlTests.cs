using eWolfPodcaster.Data;
using FluentAssertions;
using NUnit.Framework;

namespace eWolfPodcasterUnitTests.Data
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
            EpisodeControl ec = new EpisodeControl();
            ec.Title = "Title";
            ec.PodcastURL = "PodcastURL";

            EpisodeControl ecB = new EpisodeControl();
            ecB.Title = "Title";
            ecB.PodcastURL = "PodcastURL";

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
    }
}
