using eWolfPodcasterCore.Library;
using eWolfPodcasterCore.Services;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace eWolfPodcasterCoreUnitTests.Services
{
    public class ShowLibraryServiceTests
    {
        [Test]
        public void ShouldLoadInShows()
        {
            ShowLibraryService showLibraryService = new ShowLibraryService();

            eWolfPodcast pods = new eWolfPodcast();

            List<eWolfPodcastShows> shows = new List<eWolfPodcastShows>();
            eWolfPodcastShows show = new eWolfPodcastShows();
            show.Show = new eWolfPodcastShowsShow();
            show.Show.Category = "DEV";
            show.Show.Description = "My dev podcast";
            show.Show.Name = "DEV one";
            show.Show.Url = "WWW.Somewhere";
            shows.Add(show);

            pods.Shows = shows.ToArray();

            showLibraryService.ProcessFiles(pods);
            showLibraryService.GetList().Should().HaveCount(1);

            ShowLibraryData showFromLibrary = showLibraryService.GetList()[0];
            showFromLibrary.Name.Should().Be("DEV one");
            showFromLibrary.Catergery.Should().Be("DEV");

            showFromLibrary.Description.Should().Be("My dev podcast");
            showFromLibrary.URL.Should().Be("WWW.Somewhere");
        }
    }
}
