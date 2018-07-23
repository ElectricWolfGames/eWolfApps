using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterUWP.Data;
using eWolfPodcasterUWP.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private PodcastEpisode _currentPodcast = null;
        private StorageFolder _localFolder;
        private ApplicationDataContainer _localSettings;
        private ObservableCollection<IPodCastInfo> _podcasts = new ObservableCollection<IPodCastInfo>();
        private Shows _shows = new Shows();

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            Application.Current.Suspending += new SuspendingEventHandler(OnSuspending);

            _localSettings = ApplicationData.Current.LocalSettings;
            _localFolder = ApplicationData.Current.LocalFolder;

            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            LoadShowsAsync();

            _shows.UpdateAllRSSFeeds();

            AddShowItems();

            PopulateTree();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += TimerTick;
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event SuspendingEventHandler Suspending;

        public string PodcastDescription
        {
            get
            {
                return _currentPodcast?.Description;
            }
        }

        async protected void OnSuspending(object sender, SuspendingEventArgs args)
        {
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();
            await SaveShowsAsync();
            deferral.Complete();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        private void EpisodesItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            _currentPodcast = e.AddedItems[0] as PodcastEpisode;

            MediaPlayer.Source = new Uri(_currentPodcast.UrlToPlay);
            MediaPlayer.AutoPlay = true;

            MediaPlayer.Position = new TimeSpan(_currentPodcast.PlayedLength);
            OnPropertyChanged("PodcastDescription");
        }

        private async Task<bool> LoadShowsAsync()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                StorageFile sampleFile = await _localFolder.CreateFileAsync("Shows.list", CreationCollisionOption.OpenIfExists);
                if (!sampleFile.IsAvailable)
                    return true;

                var stream = await sampleFile.OpenAsync(FileAccessMode.Read);
                _shows = (Shows)formatter.Deserialize(stream.AsStream());
            }
            catch
            {
                // fail safe - can't find or load show
            }
            return true;
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

        private async Task<bool> SaveShowsAsync()
        {
            IFormatter formatter = new BinaryFormatter();

            StorageFile sampleFile = await _localFolder.CreateFileAsync("Shows.list", CreationCollisionOption.ReplaceExisting);
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, _shows);

            await FileIO.WriteBytesAsync(sampleFile, stream.ToArray());
            return true;
        }

        private void ShowsItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            ShowControl sc = item as ShowControl;
            PopulatePodCastsFromShow(sc);
        }

        private void TimerTick(object sender, object e)
        {
            if (MediaPlayer.Source != null && MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                _currentPodcast.PlayedLength = MediaPlayer.Position.Ticks;
            }
        }

        private void PopulateTree()
        {
            string[] allCategorys = CategoryHelper.GetAll(_shows.ShowList);
            ShowControl[] showsInCat;
            TreeViewNode categoryNode;

            foreach (string category in allCategorys)
            {
                categoryNode = new TreeViewNode();
                categoryNode.Content = category;
                ShowsItemsTree.RootNodes.Add(categoryNode);

                showsInCat = CategoryHelper.GetAllShowsInCategory(_shows.ShowList, category);
                foreach (ShowControl show in showsInCat)
                {
                    TreeViewNode showNode = new TreeViewNode();
                    showNode.Content = show;

                    categoryNode.Children.Add(showNode);
                }
            }
        }

        private void ShowsItemsTree_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as TreeViewNode;
            ShowControl sc = node.Content as ShowControl;

            if (sc != null)
            {
                PopulatePodCastsFromShow(sc);
            }
        }
    }
}
