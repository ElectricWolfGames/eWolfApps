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
                SoundListItem sli = new SoundListItem();
                sli.SoundItemData = sound;
                items.Add(sli);
            }

            return items;
        }

        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
