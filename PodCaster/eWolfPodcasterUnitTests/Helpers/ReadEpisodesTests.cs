using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace eWolfPodcasterCoreUnitTests.Helpers
{
    public class ReadEpisodesTests
    {
        [Test]
        public void ShouldParse4Episodes()
        {
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "eWolfPodcasterCoreUnitTests.Examples.4Episodes.xml";

            List<EpisodeControl> episodes;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                reader = XmlReader.Create(stream, settings);
                episodes = RSSHelper.ReadEpisodes(reader);
            }

            episodes.Should().HaveCount(4);

            EpisodeControl episode = episodes[0];
            episode.Title.Should().Be("84. Algorithms You Should Know");
            episode.PodcastURL.Should().Be("http://media.blubrry.com/codingblocks/www.podtrac.com/pts/redirect.mp3/traffic.libsyn.com/codingblocks/coding-blocks-episode-84.mp3");

            episode = episodes[1];
            episode.Title.Should().Be("83. Search Driven Apps");
            episode.PodcastURL.Should().Be("http://media.blubrry.com/codingblocks/www.podtrac.com/pts/redirect.mp3/traffic.libsyn.com/codingblocks/coding-blocks-episode-83.mp3");

            episode = episodes[2];
            episode.Title.Should().Be("82. Programmer Strengths and Weaknesses");
            episode.PodcastURL.Should().Be("http://media.blubrry.com/codingblocks/www.podtrac.com/pts/redirect.mp3/traffic.libsyn.com/codingblocks/coding-blocks-episode-82.mp3");

            episode = episodes[3];
            episode.Title.Should().Be("81. Understanding Complexity Theory");
            episode.PodcastURL.Should().Be("http://media.blubrry.com/codingblocks/www.podtrac.com/pts/redirect.mp3/traffic.libsyn.com/codingblocks/coding-blocks-episode-81.mp3");
        }

        [Test]
        public void ShouldParseIsaacArthurEpisodes()
        {
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "eWolfPodcasterCoreUnitTests.Examples.Isaac Arthur.xml";

            List<EpisodeControl> episodes;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                reader = XmlReader.Create(stream, settings);
                episodes = RSSHelper.ReadEpisodes(reader);
            }

            EpisodeControl episode = episodes[0];
            episode.Title.Should().Be("Programmable & Smart Matter");
            episode.PodcastURL.Should().Be("https://feeds.soundcloud.com/stream/1241912254-isaac-arthur-148927746-programmable-smart-matter.mp3");
        }
    }
}