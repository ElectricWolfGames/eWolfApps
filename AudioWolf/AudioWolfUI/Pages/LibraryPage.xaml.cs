using AudioWolfStandard;
using AudioWolfStandard.Helpers;
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
                    Name = Path.GetFileNameWithoutExtension(name)
                };

                SoundListItem sli = new SoundListItem();
                sli.SoundItemData = sd;
                items.Add(sli);
            }

            return items;
        }
    }
}
