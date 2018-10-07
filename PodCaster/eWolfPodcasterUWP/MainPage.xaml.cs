using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterUWP.BackGround;
using eWolfPodcasterUWP.Data;
using eWolfPodcasterUWP.Pages;
using eWolfPodcasterUWP.UserControls;
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
    public partial class MainPage : Page, INotifyPropertyChanged
    {
        private PodcastEpisodeUC _currentPodcast = null;
        private ShowControl _currentShow = null;
        private StorageFolder _localFolder;
        private ApplicationDataContainer _localSettings;
        private ObservableCollection<IPodCastInfo> _podcasts = new ObservableCollection<IPodCastInfo>();
        private long _setPlayBackTime = -1;

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            Application.Current.Suspending += new SuspendingEventHandler(OnSuspending);

            _localSettings = ApplicationData.Current.LocalSettings;
            _localFolder = ApplicationData.Current.LocalFolder;

            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            LoadShowsAsync();

            CreateRssBackGround();

            PopulateTree();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += TimerTick;
            timer.Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string PodcastDescription
        {
            get
            {
                return _currentPodcast?.Description;
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        async protected void OnSuspending(object sender, SuspendingEventArgs args)
        {
            SuspendingDeferral deferral = args.SuspendingOperation.GetDeferral();
            await SaveShowsAsync();
            deferral.Complete();
        }

        private void ButtonAddShowClick(object sender, RoutedEventArgs e)
        {
            var parameters = new NewShowParams
            {
                Title = "New show name",
                RssFeed = "Rss Feed",
                Shows = Shows.GetShows,
                SaveCall = () => SaveShowsAsync()
            };

            Frame.Navigate(typeof(AddNewShow), parameters);
        }

        private void ButtonSubShowClick(object sender, RoutedEventArgs e)
        {
            if (_currentShow != null)
            {
                Shows.GetShows.RemoveShow(_currentShow);
                _currentShow = null;
                SaveShowsAsync();
                PopulateTree();
            }
        }

        private async void CreateRssBackGround()
        {
            RssBackGround rbg = new RssBackGround(Shows.GetShows);
            await Task.Run(() => rbg.Runner());
        }

        private void EpisodesItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            _currentPodcast = e.AddedItems[0] as PodcastEpisodeUC;

            MediaPlayer.Stop();
            MediaPlayer.AutoPlay = true;
            MediaPlayer.Source = new Uri(_currentPodcast.UrlToPlay);
            MediaPlayer.Position = new TimeSpan(_currentPodcast.PlayedLength);
            _setPlayBackTime = _currentPodcast.PlayedLength;
            MediaPlayer.Play();

            OnPropertyChanged("PodcastDescription");
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
                Shows.GetShows.ReplaceAllShows((Shows)formatter.Deserialize(stream.AsStream()));
            }
            catch
            {
                // fail safe - can't find or load show
            }
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

                    IPodCastInfo pce = new PodcastEpisodeUC(x);
                    _podcasts.Add(pce);
                }

                EpisodesItems.ItemsSource = _podcasts;
            }
        }

        private void PopulateTree()
        {
            var allCategorys = CategoryHelper.GetAllCategoriesFromShows(Shows.GetShows.ShowList);
            IReadOnlyCollection<ShowControl> showsInCat;
            TreeViewNode categoryNode;

            ShowsItemsTree.RootNodes.Clear();

            foreach (string category in allCategorys)
            {
                categoryNode = new TreeViewNode
                {
                    Content = category
                };
                ShowsItemsTree.RootNodes.Add(categoryNode);

                showsInCat = CategoryHelper.GetAllShowsForCategory(Shows.GetShows.ShowList, category);
                foreach (ShowControl show in showsInCat)
                {
                    TreeViewNode showNode = new TreeViewNode
                    {
                        Content = show
                    };
                    categoryNode.Children.Add(showNode);
                }
            }
        }

        private async Task<bool> SaveShowsAsync()
        {
            IFormatter formatter = new BinaryFormatter();

            StorageFile sampleFile = await _localFolder.CreateFileAsync("Shows.list", CreationCollisionOption.ReplaceExisting);
            MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, Shows.GetShows);

            await FileIO.WriteBytesAsync(sampleFile, stream.ToArray());
            PopulateTree();
            return true;
        }

        private void ShowsItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            ShowControl sc = item as ShowControl;
            PopulatePodCastsFromShow(sc);
        }

        private void ShowsItemsTree_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as TreeViewNode;

            if (node.Content is ShowControl sc)
            {
                _currentShow = sc;
                PopulatePodCastsFromShow(sc);
            }
            else
            {
                _currentShow = null;
                node.IsExpanded = !node.IsExpanded;
            }
        }

        private void TimerTick(object sender, object e)
        {
            if (MediaPlayer.Source != null && MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                if (_setPlayBackTime > 0)
                {
                    if (MediaPlayer.Position.Ticks < _setPlayBackTime)
                    {
                        MediaPlayer.Position = new TimeSpan(_setPlayBackTime);
                        return;
                    }
                    else
                    {
                        _setPlayBackTime = -1;
                    }
                }

                _currentPodcast.PlayedLength = MediaPlayer.Position.Ticks;

                double totalWidth = 700;

                totalWidth /= MediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                totalWidth *= MediaPlayer.Position.TotalMilliseconds;
                _currentPodcast.PlayedLengthScaled = (float)totalWidth;
            }
        }
    }
}
