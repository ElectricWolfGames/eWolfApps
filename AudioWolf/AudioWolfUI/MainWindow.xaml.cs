using AudioWolfStandard;
using AudioWolfStandard.Data;
using AudioWolfUI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WaveFormRendererLib;

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
            // pre load the sample for testing.
            // string sample = @"D:\OffLine\Music\1991 Final Fantasy IV\Final Fantasy IV - Crickets.mp3";
            // string sample = @"D:\OffLine\Music\2016 Rogue One A Star Wars Story (Michael Giacchino)\21. Guardians of the Whills Suite.mp3";
            InitializeComponent();

            SoundItemData sid = new SoundItemData();
            sid.Name = "One";
            sid.FullPath = @"D:\OffLine\Music\2016 Rogue One A Star Wars Story (Michael Giacchino)\21. Guardians of the Whills Suite.mp3";
            _soundHolder.Add(sid);

            sid = new SoundItemData();
            sid.Name = "Other";
            sid.FullPath = @"D:\OffLine\Music\1991 Final Fantasy IV\Final Fantasy IV - Crickets.mp3";
            _soundHolder.Add(sid);

            sid = new SoundItemData();
            sid.Name = "OtherMore";
            sid.FullPath = @"D:\OffLine\Music\1991 Final Fantasy IV\Final Fantasy IV - Chocobo Forest.mp3";
            _soundHolder.Add(sid);

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
