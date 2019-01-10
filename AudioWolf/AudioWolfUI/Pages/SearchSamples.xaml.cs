using AudioWolfStandard;
using System;
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
        //private ObservableCollection<ListItem> _soundItemsToShow = new ObservableCollection<SoundItem>();

        public SearchSamples()
        {
            _soundHolder = new SoundHolder(GetOutputFolder());
            PopulateSoundItemList();


            InitializeComponent();

            //MainItemsList.ItemsSource = 
        }
        public static string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\AudioWolf");
        }

        private void PopulateSoundItemList()
        {
            foreach (var s in _soundHolder.SoundItems)
            {
                //SoundItem si = new SoundItem();
                //si.SoundItemData = s;
                //_soundItemsToShow.Add(si);
            }
        }

    }
    
}
