using AudioWolfStandard;
using AudioWolfStandard.Helpers;
using AudioWolfStandard.Services;
using AudioWolfUI.Services;
using AudioWolfUI.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace AudioWolfUI.Pages
{
    /// <summary>
    /// Interaction logic for LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : Window
    {
        private ObservableCollection<SoundListItem> _soundItemsToShow = new ObservableCollection<SoundListItem>();

        public LibraryPage()
        {
            InitializeComponent();

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

            List<string> list = FileSearchHelper.GetAllFiles();
            foreach (string name in list)
            {
                SoundDetails sd = new SoundDetails()
                {
                    Name = Path.GetFileNameWithoutExtension(name),
                    FullPath = name
                };

                SoundListItem sli = new SoundListItem();
                sli.SoundItemData = sd;
                items.Add(sli);
            }

            return items;
        }
    }
}
