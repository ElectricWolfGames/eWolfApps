using eWolfPodcasterCore.Data;
using System;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Shows _shows = new Shows();
        // private List<string> _items;

        public MainPage()
        {
            this.InitializeComponent();

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["exampleSetting"] = "Hello Windows";

            _shows.CreateFakeList();

            // _items = new List<string>();

            /*
            _items.Add("item A");
            _items.Add("item B");*/

            AddShowItems();

            /*_shows.Load(GetOutputFolder());
            _shows.UpdateAllRSSFeeds();
            _shows.Save(GetOutputFolder());*/
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
