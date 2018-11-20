using AudioWolfStandard;
using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Services;
using AudioWolfStandard.Tags;
using AudioWolfUI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        private SoundItem _currentSoundItemData = null;
        private ObservableCollection<SoundItem> _soundItemsToShow = new ObservableCollection<SoundItem>();
        private ObservableCollection<string> _tags = new ObservableCollection<string>();

        public MainWindow()
        {
            _soundHolder = new SoundHolder(GetOutputFolder());

            InitializeComponent();

            ConvertTagsToList();
            PopulateSoundItemList();

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
            PopulateSoundItemList();

            ConvertTagsToList();
        }

        private void PopulateSoundItemList()
        {
            foreach (var s in _soundHolder.SoundItems)
            {
                SoundItem si = new SoundItem();
                si.SoundItemData = s;
                _soundItemsToShow.Add(si);
            }
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            // test button to show the fist item.
        }

        private void DisplayedItemsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _currentSoundItemData = e.AddedItems[0] as SoundItem;

            MemoryStream stream = new MemoryStream();
            _currentSoundItemData.Image.Save(stream, ImageFormat.Png);

            BitmapImage tempBitmap = new BitmapImage();
            tempBitmap.BeginInit();
            tempBitmap.StreamSource = stream;
            tempBitmap.EndInit();
            SoundWaveEdit.Stretch = Stretch.Fill;
            SoundWaveEdit.Source = tempBitmap;
        }
    }
}
