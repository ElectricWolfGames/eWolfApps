using eWolfPodcasterCore.Data;
using eWolfPodcasterUWP.Pages;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP
{
    public sealed partial class MainPage : Page
    {
        private StorageFolder _localFolder;
        private ApplicationDataContainer _localSettings;
        private Shows _shows = new Shows();

        public MainPage()
        {
            InitializeComponent();

            _localSettings = ApplicationData.Current.LocalSettings;
            _localFolder = ApplicationData.Current.LocalFolder;

            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            LoadShowsAsync();

            SaveShowsAsync();

            AddShowItems();
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
            parameters.SaveCall = () => SaveShowsAsync();

            Frame.Navigate(typeof(AddNewShow), parameters);
        }

        private void ButtonSubShowClick(object sender, RoutedEventArgs e)
        {
            var selectedItems = ShowsItems.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            ShowControl selectedItem = (ShowControl)selectedItems[0];
            _shows.RemoveShow(selectedItem);

            SaveShowsAsync();
        }

        private async void LoadShowsAsync()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                StorageFile sampleFile = await _localFolder.CreateFileAsync("Shows.list", CreationCollisionOption.OpenIfExists);

                var stream = await sampleFile.OpenAsync(FileAccessMode.Read);
                _shows = (Shows)formatter.Deserialize(stream.AsStream());
            }
            catch
            {
            }
        }

        private async void SaveShowsAsync()
        {
            IFormatter formatter = new BinaryFormatter();

            StorageFile sampleFile = await _localFolder.CreateFileAsync("Shows.list", CreationCollisionOption.ReplaceExisting);
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, _shows);

            await FileIO.WriteBytesAsync(sampleFile, stream.ToArray());
        }
    }
}
