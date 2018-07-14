using eWolfPodcasterCore.Data;
using eWolfPodcasterUWP.Pages;
using System;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP
{
    public sealed partial class MainPage : Page
    {
        private Shows _shows = new Shows();

        public MainPage()
        {
            Console.WriteLine("ctor");

            InitializeComponent();

            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            Uri newuri = new Uri("http://media.blubrry.com/codingblocks/s/www.podtrac.com/pts/redirect.mp3/traffic.libsyn.com/codingblocks/coding-blocks-episode-84.mp3");
            // myPlayer.Source = newuri;

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["exampleSetting"] = "Hello Windows";

            _shows.CreateFakeList();

            AddShowItems();
        }

        public string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\eWolfTestApp");
        }

        private void AddShowItems()
        {
            ShowsItems.ItemsSource = _shows.ShowList;
        }

        private void ButtonAddShowClick(object sender, RoutedEventArgs e)
        {
            var parameters = new NewShowParams();
            parameters.Title = "New show name";
            parameters.RssFeed = "Rss Feed";
            parameters.Shows = _shows;

            Frame.Navigate(typeof(AddNewShow), parameters);
        }

        private void ButtonSubShowClick(object sender, RoutedEventArgs e)
        {
            var selectedItems = ShowsItems.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            ShowControl selectedItem = (ShowControl)selectedItems[0];
            _shows.RemoveShow(selectedItem);
        }
    }
}
