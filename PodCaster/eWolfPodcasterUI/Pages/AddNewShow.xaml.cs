using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Services;
using System.ComponentModel;
using System.Windows;

namespace eWolfPodcasterUI.Pages
{
    /// <summary>
    /// Interaction logic for AddNewShow.xaml
    /// </summary>
    public partial class AddNewShow : Window, INotifyPropertyChanged
    {
        private string _rssFeed = string.Empty;
        private string _showName = string.Empty;

        public AddNewShow()
        {
            InitializeComponent();
            DataContext = this;
            PopulateCategory();
        }

        public AddNewShow(ShowControl sc)
        {
            InitializeComponent();
            DataContext = this;
            PopulateCategory();

            ShowName = sc.Title;
            RSSFeed = sc.RssFeed;
            CategoryList.SelectedItem = sc.Catergery.Name;

            LocalFiles.IsChecked = (sc.ShowOption.ShowStorage == ShowStorageType.LocalStorage);
            CheckForUpdates.IsChecked = sc.ShowOption.CheckforUpdates;
            AutoDownload.IsChecked = sc.ShowOption.AudoDownloadEpisodes;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Apply { get; set; }

        public string RSSFeed
        {
            get
            {
                return _rssFeed;
            }
            set
            {
                _rssFeed = value;
                OnPropertyChanged("RSSFeed");
            }
        }

        public string ShowName
        {
            get
            {
                return _showName;
            }

            set
            {
                _showName = value;
                OnPropertyChanged("ShowName");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            Apply = false;
            Close();
        }

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            Apply = true;
            Close();
        }

        private void PopulateCategory()
        {
            CategoryList.Items.Clear();

            var groups = ShowLibraryService.GetLibrary.Groups();
            foreach (var group in groups)
            {
                CategoryList.Items.Add(group.Name);
            }
        }
    }
}
