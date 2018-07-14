using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddNewShow : Page
    {
        private string _showName = string.Empty;
        private string _rssFeed = string.Empty;

        public AddNewShow()
        {
            InitializeComponent();
            this.DataContext = this;
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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void buttonOKClick(object sender, RoutedEventArgs e)
        {
            Apply = false;
            ClosePage();
        }

        private void buttonCancelClick(object sender, RoutedEventArgs e)
        {
            Apply = true;
            ClosePage();
        }

        private void ClosePage()
        {
            this.Frame.GoBack();
        }
    }
}
