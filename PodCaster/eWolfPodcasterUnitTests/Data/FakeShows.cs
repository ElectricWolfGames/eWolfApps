﻿using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using System.Collections.ObjectModel;

namespace eWolfPodcasterCoreUnitTests.Data
{
    public class FakeShows : IShows
    {
        public ObservableCollection<ShowControl> ShowList => throw new System.NotImplementedException();

        public bool Add(ShowControl show)
        {
            return false;
        }

        public bool Contains(ShowControl show)
        {
            return false;
        }

        public ShowControl GetShowFromName(string showName)
        {
            return null;
        }

        public void Load(string outputFolder)
        {
            // Fake do nothing service.
        }

        public void RemoveEpisodeFromShow(string showName, string episodeName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveShow(ShowControl itemToRemove)
        {
            // Fake do nothing service.
        }

        public void ReplaceAllShows(Shows shows)
        {
            // Fake do nothing service.
        }

        public void Save()
        {
            // Fake do nothing service.
        }

        public bool UpdateNextRSSFeeds()
        {
            return true;
        }
    }
}