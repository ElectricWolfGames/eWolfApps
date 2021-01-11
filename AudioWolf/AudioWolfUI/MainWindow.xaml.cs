using AudioWolfStandard;
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
using System.Windows.Media;

// Need to create a custom sound object.
// need to create a tag system that we can use in the Vidoe Tagger and the image tagger.
// need to set the serach folders for all the audio.
// need to save out the main options in C:\ docs \EWolf folders.
// need to save out the list of all the sample in  the main serch folder for each one.
// need to check for any other setting in  the sub folders, EG. Save to c:\Main\ but then we also add a c:\ path, need to pick up the setting in the c:\Main\ folder.

namespace AudioWolfUI
{
    public partial class MainWindow : Window
    {
        private SoundItem _currentSoundItemData = null;
        private ObservableCollection<string> _itemTags = new ObservableCollection<string>();
        private SoundHolder _soundHolder;
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

        public static string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\AudioWolf");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GlobalTagStore.AddTag(NewTagName.Text);
            ConvertTagsToList();
        }

        private void ConvertItemTagsToList()
        {
            _itemTags.Clear();

            if (_currentSoundItemData == null)
                return;

            foreach (TagData td in _currentSoundItemData.SoundItemData.Tags)
            {
                _itemTags.Add(td.Name);
            }
            ItemTags.ItemsSource = _itemTags;
        }

        private void ConvertTagsToList()
        {
            _tags.Clear();

            GlobalTagStore gts = ServiceLocator.Instance.GetService<GlobalTagStore>();
            foreach (TagData td in gts.Tags)
            {
                _tags.Add(td.Name);
            }
        }

        private void DisplayedItemsGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _currentSoundItemData = e.AddedItems[0] as SoundItem;
            PopulateSelectItem();
        }

        private void PopulateSelectItem()
        {
            SelectedName.Text = _currentSoundItemData.SoundItemData.Name;
            SoundWaveEdit.Stretch = Stretch.Fill;
            SoundWaveEdit.Source = _currentSoundItemData.SoundItemData.Image;
            ConvertItemTagsToList();
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

        private void Tag_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string tagName = e.AddedItems[0] as string;

            if (_currentSoundItemData == null || string.IsNullOrWhiteSpace(tagName))
                return;

            _currentSoundItemData.SoundItemData.AddTag(tagName);
            PopulateSelectItem();
            _soundHolder.SaveIfNeeded(true);
        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            // test button to show the fist item.
        }

        private void ClearList_Click(object sender, RoutedEventArgs e)
        {
            if (_currentSoundItemData == null)
                return;

            _currentSoundItemData.SoundItemData.Clear();
            PopulateSelectItem();
            _soundHolder.SaveIfNeeded(true);
        }

        private void TagFilter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string filters = TagFilters.Text;

            // List<TagData> tags =
        }
    }
}