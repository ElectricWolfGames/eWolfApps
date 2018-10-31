﻿using AudioWolfStandard;
using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfUI.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

// Need to create a ccustom sound object.
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
        private SoundHolder _soundHolder = new SoundHolder();
        private ObservableCollection<SoundItem> _soundItemsToShow = new ObservableCollection<SoundItem>();

        public MainWindow()
        {
            List<string> files = FileSearchHelper.GetAllFiles();

            foreach (string name in files)
            {
                SoundItemData sid = new SoundItemData();
                sid.Name = Path.GetFileNameWithoutExtension(name);
                sid.FullPath = name;
                _soundHolder.Add(sid);
            }

            InitializeComponent();

            foreach (var s in _soundHolder.SoundItems)
            {
                SoundItem si = new SoundItem();
                si.SoundItemData = s;
                _soundItemsToShow.Add(si);
            }
            DisplayedItems.ItemsSource = _soundItemsToShow;
        }
    }
}
