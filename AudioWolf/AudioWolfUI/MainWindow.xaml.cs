﻿using AudioWolfStandard;
using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Services;
using AudioWolfStandard.Tags;
using AudioWolfUI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

// Need to create a custom sound object.
// need to create a tag system that we can use in the Vidoe Tagger and the image tagger.
// need to set the serach folders for all the audio.
// need to save out the main options in C:\ docs \EWolf folders.
// need to save out the list of all the sample in  the main serch folder for each one.
// need to check for any other setting in  the sub folders, EG. Save to c:\Main\ but then we also add a c:\ path, need to pick up the setting in the c:\Main\ folder.
//

namespace AudioWolfUI
{
    public partial class MainWindow : Window
    {
        private SoundHolder _soundHolder;
        private SoundItemData _currentSoundItemData = null;
        private ObservableCollection<SoundItem> _soundItemsToShow = new ObservableCollection<SoundItem>();
        private ObservableCollection<string> _tags = new ObservableCollection<string>();

        public MainWindow()
        {
            _soundHolder = new SoundHolder(GetOutputFolder());

            // need to load in the file cahce

            InitializeComponent();

            ConvertTagsToList();

            DisplayedItemsGrid.ItemsSource = _soundItemsToShow;
            Tag.ItemsSource = _tags;
        }

        public string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\AudioWolf");
        }

        private void ConvertTagsToList()
        {
            _tags.Clear();

            _tags.Add("ExtraTagTest");

            // get the global list !
            GlobalTagStore gts = ServiceLocator.Instance.GetService<GlobalTagStore>();
            foreach (TagData td in gts.Tags)
            {
                _tags.Add(td.Name);
            }
        }

        private void DisplayedItemsGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            int i = 0;
            i++;

            // _currentSoundItemData
            /*
             *  if (e.AddedItems.Count == 0)
                return;

            _currentPodcast = e.AddedItems[0] as PodcastEpisode;
             */
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            List<string> files = FileSearchHelper.GetAllFiles();

            foreach (string name in files)
            {
                SoundItemData sid = new SoundItemData();
                sid.Name = Path.GetFileNameWithoutExtension(name);
                sid.FullPath = name;
                _soundHolder.Add(sid);
            }
            _soundHolder.SaveIfNeeded();

            foreach (var s in _soundHolder.SoundItems)
            {
                SoundItem si = new SoundItem();
                si.SoundItemData = s;
                _soundItemsToShow.Add(si);
            }

            ConvertTagsToList();
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            // test button to show the fist item.
        }
    }
}
