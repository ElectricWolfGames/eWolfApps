using AudioWolfStandard;
using AudioWolfStandard.Interfaces;
using AudioWolfStandard.Services;
using AudioWolfUI.Services;
using AudioWolfUI.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace AudioWolfUI.Pages
{
    /// <summary>
    /// Interaction logic for LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : Window
    {
        private SoundEffectHolder _soundEffectHolder = new SoundEffectHolder();

        private ObservableCollection<SoundListItem> _soundItemsToShow = new ObservableCollection<SoundListItem>();
        private ObservableCollection<SoundListItem> _fullList = new ObservableCollection<SoundListItem>();

        public LibraryPage()
        {
            InitializeComponent();
            _soundEffectHolder.Populate();

            ServiceLocator.Instance.InjectService<MediaPlayerService>(new MediaPlayerService());
        }

        private void ButSearch_Click(object sender, RoutedEventArgs e)
        {
            MainItemsList.Items.Clear();

            MainItemsList.ItemsSource = GetSoundList();
        }

        private ObservableCollection<SoundListItem> GetSoundList()
        {
            ObservableCollection<SoundListItem> items = new ObservableCollection<SoundListItem>();

            List<ISoundDetails> sounds = _soundEffectHolder.Sounds;
            foreach (ISoundDetails sound in sounds)
            {
                SoundListItem sli = new SoundListItem
                {
                    SoundDetails = sound
                };
                items.Add(sli);
            }
            _fullList = items;
            return items;
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            _soundEffectHolder.RenameFiles();
        }

        private void FilterText_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string filter = FilterText.Text.ToUpper();

            ObservableCollection<SoundListItem> items = new ObservableCollection<SoundListItem>();
            foreach (SoundListItem item in _fullList)
            {
                if (item.Title.ToUpper().Contains(filter))
                {
                    items.Add(item);
                }
            }
            MainItemsList.ItemsSource = items;
        }

        private void ButFixNames_Click(object sender, RoutedEventArgs e)
        {
            _soundEffectHolder.FixNames();

            MainItemsList.ItemsSource = GetSoundList();
        }
    }
}