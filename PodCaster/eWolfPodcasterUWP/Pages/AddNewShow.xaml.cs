using eWolfPodcasterCore.Data;
using eWolfPodcasterUWP.Data;
using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace eWolfPodcasterUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewShow : Page
    {
        private string _rssFeed = string.Empty;
        private string _showName = string.Empty;
        private Action _saveShows;

        public AddNewShow()
        {
            InitializeComponent();

            PopulateCategory();

            DataContext = this;
        }

        private void PopulateCategory()
        {
            CategoryList.Items.Add("Music");
            CategoryList.Items.Add("Developer");
            CategoryList.Items.Add("Science");
            CategoryList.Items.Add("Space");
            CategoryList.Items.Add("History");
            CategoryList.Items.Add("Tech");
        }

        public event PropertyChangedEventHandler PropertyChanged;

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

        private Shows _shows { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (NewShowParams)e.Parameter;
            ShowName = parameters.Title;
            RSSFeed = parameters.RssFeed;
            _shows = parameters.Shows;
            _saveShows = parameters.SaveCall;
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            ClosePage();
        }

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            ShowControl sc = new ShowControl()
            {
                Title = ShowName,
                RssFeed = RSSFeed
            };

            sc.ShowOption.Category = CategoryList.SelectedValue.ToString();

            _shows.Add(sc);

            _saveShows();

            ClosePage();
        }

        private void ClosePage()
        {
            Frame.GoBack();
        }
    }
}
