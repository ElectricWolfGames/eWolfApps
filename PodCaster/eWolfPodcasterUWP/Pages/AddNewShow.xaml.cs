using eWolfPodcasterCore.Data;
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

        public AddNewShow()
        {
            InitializeComponent();
            DataContext = this;
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
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
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

            _shows.Add(sc);

            ClosePage();
        }

        private void ClosePage()
        {
            Frame.GoBack();
        }
    }
}
