using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using eWolfPodcasterCore.Services;
using eWolfPodcasterUI.Media;
using eWolfPodcasterUI.Pages;
using eWolfPodcasterUI.Project;
using eWolfPodcasterUI.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace eWolfPodcasterUI
{
    public partial class MainWindow : Window, INotifyPropertyChanged, IMainBase
    {
        private readonly ObservableCollection<IDebugLoggerData> _errorLog = new ObservableCollection<IDebugLoggerData>();
        private readonly IMediaPlayer _mediaPlayerWrapper = new MediaPlayerWrapper();
        private readonly ObservableCollection<PodcastEpisode> _podcasts = new ObservableCollection<PodcastEpisode>();
        private readonly ObservableCollection<PodcastEpisode> _shows = new ObservableCollection<PodcastEpisode>();
        private PodcastEpisode _currentPodcast = null;
        private List<ShowControl> _currentShows = new List<ShowControl>();
        private DispatcherTimer _rssTimer;

        public MainWindow()
        {
            InitializeComponent();
            DebugLog.LogInfo("App started");

            ProjectDetails projectDetails = new ProjectDetails();
            ServiceLocator.Instance.InjectService<IProjectDetails>(projectDetails);
            ServiceLocator.Instance.InjectService<IMainBase>(this);

            Shows.GetShowService.Load(projectDetails.GetOutputFolder());
            Shows.GetShowService.Save();

            ShowLibraryService.GetLibrary.Load(projectDetails.GetLibraryPath());

            PopulateTree();
            PopulateLogPage();
            CreatePlayerTimer();
            CreateRSSTimer();

            ServiceLocator.Instance.GetService<LoggerService>().Logs.CollectionChanged += new NotifyCollectionChangedEventHandler(LogListUpdated);

            SystemEvents.SessionSwitch += new SessionSwitchEventHandler(SystemEvents_SessionSwitch);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string PodcastDescription
        {
            get
            {
                return _currentPodcast?.Description;
            }
        }

        public string PodcastName
        {
            get
            {
                return _currentPodcast?.Title;
            }
        }

        public void PopulateEpisodes()
        {
            ShowAllEpisodesFromShows();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void AddStaredShows()
        {
            Shows shows = (Shows)Shows.GetShowService;

            TreeViewItem categoryNodeSelected = new TreeViewItem
            {
                Header = "Starred"
            };

            ShowsItemsTree.Items.Add(categoryNodeSelected);

            IEnumerable<ShowControl> showsWithStar = shows.ShowList.Where(x => x != null && x.ShowOption.Starred);

            if (!showsWithStar.Any())
                return;

            foreach (ShowControl show in showsWithStar)
            {
                TreeViewItem showNode = new TreeViewItem
                {
                    Header = show.Title + $" [ {show.EpisodesWatched}/{show.Episodes.Count} ]",
                    Tag = show
                };
                categoryNodeSelected.Items.Add(showNode);
            }
        }

        private void BtnClearHide_Click(object sender, RoutedEventArgs e)
        {
            ShowHelper.ClearHidden(_currentShows);

            Shows.GetShowService.Save();
            ShowAllEpisodesFromShows();
        }

        private void BtnClearWatch_Click(object sender, RoutedEventArgs e)
        {
            ShowHelper.ClearWatched(_currentShows);

            Shows.GetShowService.Save();
            ShowAllEpisodesFromShows();
        }

        private void BtnForwardMinute_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.Forward(1);
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.Pause();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.Play();
        }

        private void BtnRewindMinute_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.Rewind(1);
        }

        private void BtnSpeedX1_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.SetSpeed(1);
        }

        private void BtnSpeedX12_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.SetSpeed(1.2f);
        }

        private void BtnSpeedX1dot5_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.SetSpeed(1.5f);
        }

        private void BtnSpeedX2_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.SetSpeed(2);
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayerWrapper.Stop();
        }

        private void ButtonAddShowClick(object sender, RoutedEventArgs e)
        {
            AddNewShow addNewShow = new AddNewShow
            {
                ShowName = "new show name"
            };
            addNewShow.ShowDialog();

            if (addNewShow.Apply)
            {
                ShowControl sc = new ShowControl();

                UpdateShowDetails(sc, addNewShow);

                Shows.GetShowService.Add(sc);
                Shows.GetShowService.Save();
                PopulateTree();
            }
        }

        private void ButtonLibraryShowClick(object sender, RoutedEventArgs e)
        {
            ShowLibrary addNewShow = new ShowLibrary { };
            addNewShow.ShowDialog();
            Shows.GetShowService.Save();
            PopulateTree();
        }

        private void ButtonRefreshShowClick(object sender, RoutedEventArgs e)
        {
            PopulateTree();
        }

        private void ButtonStarShowClick(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)ShowsItemsTree.SelectedItem;
            if (item == null)
                return;

            ShowControl selectedItem = item.Tag as ShowControl;
            selectedItem.ShowOption.Starred = !selectedItem.ShowOption.Starred;

            Shows.GetShowService.Save();
            PopulateTree();
        }

        private void ButtonSubShowClick(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)ShowsItemsTree.SelectedItem;
            if (item == null)
                return;

            ShowControl selectedItem = item.Tag as ShowControl;
            Shows.GetShowService.RemoveShow(selectedItem);
            Shows.GetShowService.Save();

            PopulateTree();
        }

        private async void CheckNextShow()
        {
            try
            {
                await Task.Run(() =>
                {
                    if (Shows.GetShowService.UpdateNextRSSFeeds())
                    {
                        // _rssTimer.Interval = _rssTimer.Interval + TimeSpan.FromMinutes(10);
                    }
                });
            }
            catch (Exception ex)
            {
                DebugLog.LogInfo($"Check Next Show Failed with {ex.Message}");
            }
        }

        private void CreatePlayerTimer()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(2500)
            };
            timer.Tick += MediaPlayerIntervalUpdate;
            timer.Start();
        }

        private void CreateRSSTimer()
        {
            _rssTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(5000)
            };
            _rssTimer.Tick += UpdateRssFeedTimer;
            _rssTimer.Start();
        }

        private void EditSelectedItem(ShowControl sc)
        {
            AddNewShow addNewShow = new AddNewShow(sc);
            addNewShow.ShowDialog();

            if (addNewShow.Apply)
            {
                bool refresh = UpdateShowDetails(sc, addNewShow);
                Shows.GetShowService.Save();
                if (refresh)
                {
                    PopulateTree();
                }
            }
        }

        private void EpisodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            PlayEpisode(e.AddedItems[0] as PodcastEpisode);
        }

        private void LogListUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            PopulateLogPage();
        }

        private void MediaPlayerIntervalUpdate(object sender, EventArgs e)
        {
            if (_currentPodcast == null)
                return;

            bool playNextEpsoide;
            _mediaPlayerWrapper.UpDateInterval(_currentPodcast.EpisodeControlData, out playNextEpsoide);
            _currentPodcast.PlayedLengthScaled = _currentPodcast.EpisodeControlData.PlayedLengthScaled;

            Shows.GetShowService.Save();

            if (playNextEpsoide)
            {
                PlayNextEpisode();
            }
        }

        private void PlayEpisode(PodcastEpisode episode)
        {
            _currentPodcast = episode;
            _mediaPlayerWrapper.PlayEpisode(_currentPodcast.EpisodeControlData);
            OnPropertyChanged("PodcastDescription");

            UpdateDescription();
        }

        private void PlayNextEpisode()
        {
            Console.WriteLine("Play next show now!");
            for (int i = 0; i < _podcasts.Count - 1; i++)
            {
                var ep = _podcasts[i];
                if (ep.Title == _currentPodcast.Title)
                {
                    string nextPodCastName = _podcasts[i + 1].Title;
                    foreach (PodcastEpisode c in _podcasts)
                    {
                        if (c.Title == nextPodCastName)
                        {
                            PlayEpisode(c);
                            return;
                        }
                    }
                }
            }
        }

        private void PopulateLogPage()
        {
            List<LoggerData> orderedByDateList = null;

            orderedByDateList = ServiceLocator.Instance.GetService<LoggerService>().Logs.ToList();

            /*foreach (LoggerData x in orderedByDateList)
            {
                IDebugLoggerData pce = new DebugLogItem
                {
                    LoggerData = x
                };

                _errorLog.Add(pce);
            }*/

            // LogItems.ItemsSource = _errorLog;
        }

        private void PopulateTree()
        {
            ShowsItemsTree.Items.Clear();
            Shows shows = (Shows)Shows.GetShowService;
            List<string> groups = shows.Groups();

            groups.Add("Ungrouped");

            groups = groups.OrderBy(x => x).ToList();

            AddStaredShows();

            foreach (string groupName in groups)
            {
                List<ShowControl> showsInCat = shows.ShowInGroup(groupName);
                if (!showsInCat.Any())
                    continue;

                showsInCat = showsInCat.OrderBy(x => x.Title).ToList();
                TreeViewItem categoryNode = new TreeViewItem
                {
                    Header = groupName
                };

                ShowsItemsTree.Items.Add(categoryNode);

                foreach (ShowControl show in showsInCat)
                {
                    TreeViewItem showNode = new TreeViewItem
                    {
                        Header = show.Title + $" [ {show.EpisodesWatched}/{show.Episodes.Count} ]",
                        Tag = show
                    };
                    categoryNode.Items.Add(showNode);
                }
            }
        }

        private void ShowAllEpisodesForGroup(string groupName)
        {
            _currentShows = ShowHelper.GetAllShowsFromGroup(groupName);

            ShowAllEpisodesFromShows();
        }

        private void ShowAllEpisodesFromShows()
        {
            _podcasts.Clear();
            ShowEpisodes(ShowHelper.GetOrderEpisodes(_currentShows));
        }

        private void ShowEpisodes(List<EpisodeControl> orderedByDateList)
        {
            foreach (EpisodeControl x in orderedByDateList)
            {
                if (x.Hidden)
                    continue;

                PodcastEpisode pce = new PodcastEpisode();
                pce.EpisodeControlData = x;
                pce.ShowName = x.ShowName;
                _podcasts.Add(pce);
            }

            EpisodesItems.ItemsSource = _podcasts;
        }

        private void ShowsItemsTree_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)(sender as TreeView).SelectedItem;
            if (item == null)
                return;

            ShowControl sc = item.Tag as ShowControl;
            if (sc != null)
            {
                _currentShows.Clear();
                _currentShows.Add(sc);
                _podcasts.Clear();
                if (e.RightButton == System.Windows.Input.MouseButtonState.Pressed)
                {
                    EditSelectedItem(sc);
                    return;
                }

                ShowAllEpisodesFromShows();
            }
            else
            {
                ShowAllEpisodesForGroup(item.Header.ToString());
            }
        }

        private void SystemEvents_SessionSwitch(object sender, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLock)
            {
                DebugLog.LogInfo("Computer locked: Pausing playback");
                _mediaPlayerWrapper.Pause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Console.WriteLine("Welcome back - you will need to manual start the audio.");
            }
        }

        private void UpdateDescription()
        {
            Description.Items.Clear();

            Description.Items.Add(_currentPodcast.Title);

            string[] items = WordParseHelper.GetWordsPerLine(_currentPodcast.Description, 130);
            foreach (string item in items)
            {
                Description.Items.Add(item);
            }
        }

        private void UpdateRssFeedTimer(object sender, EventArgs e)
        {
            CheckNextShow();
        }

        private bool UpdateShowDetails(ShowControl sc, AddNewShow addNewShow)
        {
            sc.Modifyed = true;
            sc.Title = addNewShow.ShowName;
            sc.RssFeed = addNewShow.RSSFeed;
            if (addNewShow.LocalFiles.IsChecked.Value)
            {
                sc.ShowOption.ShowStorage = ShowStorageType.LocalStorage;
            }
            sc.ShowOption.CheckforUpdates = addNewShow.CheckForUpdates.IsChecked.Value;
            sc.ShowOption.AudoDownloadEpisodes = addNewShow.AutoDownload.IsChecked.Value;
            bool refresh = false;

            if (sc.Catergery.Name != addNewShow.CategoryList.SelectedItem.ToString())
            {
                sc.Catergery = new eWolfPodcasterCore.Library.CatergeryData(addNewShow.CategoryList.SelectedItem.ToString());
                refresh = true;
            }
            return refresh;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _mediaPlayerWrapper.Stop();
            _rssTimer.Stop();
        }
    }
}