using eWolfPodcasterCore.Data;
using System;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP
{
    public sealed partial class MainPage : Page
    {
        private Shows _shows = new Shows();

        public MainPage()
        {
            this.InitializeComponent();

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
    }
}
