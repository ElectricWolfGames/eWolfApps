using eWolfPodcasterCore.Data;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;

namespace eWolfPodcasterCoreUnitTests.Data
{
    public class ShowsTests
    {
        [Test]
        public void ShouldAddShowToShowList()
        {
            Shows shows = new Shows();
            shows.Add(CreateShowControl("My Title", "RSS", "GroupA"));

            shows.Count.Should().Be(1);
        }

        [Test]
        public void ShouldBeAbleToAddTwoDifferentShows()
        {
            Shows shows = new Shows();
            shows.Add(CreateShowControl("My Title", "RSS", "GroupA"));
            shows.Add(CreateShowControl("My Title Other", "RSS Other", "GroupA"));

            shows.Count.Should().Be(2);
        }

        [Test]
        public void ShouldNotAddAnotherShowWithSameRSSFeedOrTitle()
        {
            Shows shows = new Shows();
            shows.Add(CreateShowControl("My Title", "RSS", "GroupA"));
            shows.Add(CreateShowControl("My Title", "RSS", "GroupA"));
            shows.Add(CreateShowControl("My Title Other", "RSS", "GroupA"));

            shows.Count.Should().Be(1);
        }

        [Test]
        public void ShouldGroupsReturnCorrectGroups()
        {
            Shows shows = new Shows();
            shows.Add(CreateShowControl("Show1", "RSS", "GroupA"));
            shows.Add(CreateShowControl("Show2", "RSSA", "GroupA"));
            shows.Add(CreateShowControl("Show3", "RSSB", "GroupB"));

            shows.Groups().Should().HaveCount(2);
            shows.Groups().Contains("GroupA").Should().BeTrue();
            shows.Groups().Contains("GroupB").Should().BeTrue();
        }

        [Test]
        public void ShouldGetOnlyShowInSameGroup()
        {
            Shows shows = new Shows();
            shows.Add(CreateShowControl("Show1", "RSSA", "GroupA"));
            shows.Add(CreateShowControl("Show2", "RSSB", "GroupA"));
            shows.Add(CreateShowControl("Show3", "RSSC", "GroupB"));

            List<ShowControl> showsInGroup = shows.ShowInGroup("GroupA");
            showsInGroup.Should().HaveCount(2);

            showsInGroup = shows.ShowInGroup("GroupB");
            showsInGroup.Should().HaveCount(1);
        }

        [Test]
        public void ShouldGetOnlyShowInNoGroup()
        {
            Shows shows = new Shows();
            shows.Add(CreateShowControl("Show1", "RSSA", "None"));
            shows.Add(CreateShowControl("Show2", "RSSB", "None"));
            shows.Add(CreateShowControl("Show3", "RSSC", "None"));
            shows.Add(CreateShowControl("Show4", "RSSD"));

            List<ShowControl> showsInGroup = shows.ShowInGroup("Ungrouped");
            showsInGroup.Should().HaveCount(4);
        }

        private ShowControl CreateShowControl(string name, string feed, string cat)
        {
            ShowControl s = new ShowControl()
            {
                Title = name,
                RssFeed = feed,
                Catergery = new eWolfPodcasterCore.Library.CatergeryData(cat)
            };
            return s;
        }

        private ShowControl CreateShowControl(string name, string feed)
        {
            ShowControl s = new ShowControl()
            {
                Title = name,
                RssFeed = feed,
                Catergery = null
            };
            return s;
        }
    }
}
