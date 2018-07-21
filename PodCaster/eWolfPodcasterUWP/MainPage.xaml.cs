using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterUWP.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
        private PodcastEpisode _currentPodcast = null;
        private ObservableCollection<IPodCastInfo> _podcasts = new ObservableCollection<IPodCastInfo>();

        public MainPage()
        {
            InitializeComponent();

            _localSettings = ApplicationData.Current.LocalSettings;
            _localFolder = ApplicationData.Current.LocalFolder;

            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            LoadShowsAsync();

            SaveShowsAsync();

            AddShowItems();

            TEMP_GetEpisodes();
        }

        private void TEMP_GetEpisodes()
        {
            _shows.UpdateAllRSSFeeds();
            PopulatePodCastsFromShow(_shows.ShowList[0]);
        }

        private void PopulatePodCastsFromShow(ShowControl sc)
        {
            if (sc != null)
            {
                _podcasts.Clear();

                List<EpisodeControl> orderedByDateList = null;
                orderedByDateList = sc.Episodes.OrderByDescending(x => x.PublishedDate.Ticks).ToList();

                foreach (EpisodeControl x in orderedByDateList)
                {
                    if (x.Hidden)
                        continue;

                    IPodCastInfo pce = new PodcastEpisode();
                    pce.EpisodeData = x;
                    _podcasts.Add(pce);
                }

                EpisodesItems.ItemsSource = _podcasts;
            }
        }

        private void AddShowItems()
        {
            ShowsItems.ItemsSource = _shows.ShowList;
        }

        private void ButtonAddShowClick(object sender, RoutedEventArgs e)
        {
            var parameters = new NewShowParams
            {
                Title = "New show name",
                RssFeed = "Rss Feed",
                Shows = _shows,
                SaveCall = () => SaveShowsAsync()
            };

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
                if (!sampleFile.IsAvailable)
                    return;

                var stream = await sampleFile.OpenAsync(FileAccessMode.Read);
                _shows = (Shows)formatter.Deserialize(stream.AsStream());
            }
            catch
            {
                // fail safe - can't find or load show
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
