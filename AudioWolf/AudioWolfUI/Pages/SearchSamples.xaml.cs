using AudioWolfStandard;
using AudioWolfStandard.Data;
using AudioWolfStandard.Helpers;
using AudioWolfUI.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Path = System.IO.Path;

namespace AudioWolfUI
{
    /// <summary>
    /// Interaction logic for SearchSamples.xaml
    /// </summary>
    public partial class SearchSamples : Window
    {
        private SoundHolder _soundHolder;
        private ObservableCollection<SoundListItem> _soundItemsToShow = new ObservableCollection<SoundListItem>();

        public SearchSamples()
        {
            _soundHolder = new SoundHolder(GetOutputFolder());
            PopulateSoundItemList();
            InitializeComponent();
            MainItemsList.ItemsSource = _soundItemsToShow;
        }

        public static string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\AudioWolf");
        }

        private void FilterTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            // the filter box has changed.
        }

        private void PopulateSoundItemList()
        {
            SoundItemData sid = new SoundItemData();
            sid.Name = "Test";
            _soundHolder.SoundItems.Add(sid);

            foreach (SoundItemData s in _soundHolder.SoundItems)
            {
                //SoundListItem si = new SoundListItem();
                //si.SoundItemData = s;
                //_soundItemsToShow.Add(si);
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

            // TODO NEXT ConvertTagsToList();
        }
    }
}