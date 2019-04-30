using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using eWolfPodcasterCore.Services;
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
using System.Windows.Media;
using System.Windows.Threading;

namespace eWolfPodcasterUI
{
    public partial class MainWindow : Window, INotifyPropertyChanged, IMainBase
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        private PodcastEpisode _currentPodcast = null;
        private List<ShowControl> _currentShows = new List<ShowControl>();
        private ObservableCollection<IDebugLoggerData> _errorLog = new ObservableCollection<IDebugLoggerData>();
        private ObservableCollection<PodcastEpisode> _podcasts = new ObservableCollection<PodcastEpisode>();
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

        private void BtnClearWatch_Click(object sender, RoutedEventArgs e)
        {
            foreach (ShowControl show in _currentShows)
            {
                show.Episodes.ForEach(x => x.PlayedLength = 0);
                show.Episodes.ForEach(x => x.Hidden = false);
                show.Episodes.ForEach(x => x.PlayedLengthScaled = 0);
                show.Modifyed = true;
            }
            ShowAllEpisodesFromShows();

            Shows.GetShowService.Save();
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.Pause();
        }

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.Play();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            _mediaPlayer.Stop();
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
                        _rssTimer.Interval = _rssTimer.Interval + TimeSpan.FromMinutes(1);
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
                Interval = TimeSpan.FromMilliseconds(1000)
            };
            timer.Tick += MediaPlayerIntervalUpdate;
            timer.Start();
        }

        private void CreateRSSTimer()
        {
            _rssTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
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
            if (_mediaPlayer.Source != null && _mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                _currentPodcast.PlayedLength = _mediaPlayer.Position.Ticks;

                double MaxLength = 781;

                double totalWidth = MaxLength;

                totalWidth /= _mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                totalWidth *= _mediaPlayer.Position.TotalMilliseconds;
                _currentPodcast.PlayedLengthScaled = (float)totalWidth;
                Shows.GetShowService.Save();

                if (_currentPodcast.PlayedLengthScaled >= MaxLength - 2)
                {
                    PlayNextEpisode();
                }
            }
        }

        private void PlayEpisode(PodcastEpisode episode)
        {
            _currentPodcast = episode;

            _mediaPlayer.Open(new Uri(_currentPodcast.UrlToPlay));
            _mediaPlayer.Position = new TimeSpan(_currentPodcast.PlayedLength);
            _mediaPlayer.Play();
            OnPropertyChanged("PodcastDescription");
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

            foreach (LoggerData x in orderedByDateList)
            {
                IDebugLoggerData pce = new DebugLogItem
                {
                    LoggerData = x
                };

                _errorLog.Add(pce);
            }

            LogItems.ItemsSource = _errorLog;
        }

        private void PopulateTree()
        {
            ShowsItemsTree.Items.Clear();

            Shows shows = (Shows)Shows.GetShowService;
            List<string> groups = shows.Groups();
            groups.Add("Ungrouped");

            foreach (string groupName in groups)
            {
                List<ShowControl> showsInCat = shows.ShowInGroup(groupName);
                if (!showsInCat.Any())
                    continue;

                TreeViewItem categoryNode = new TreeViewItem();
                categoryNode.Header = groupName;

                ShowsItemsTree.Items.Add(categoryNode);

                foreach (ShowControl show in showsInCat)
                {
                    TreeViewItem showNode = new TreeViewItem();
                    showNode.Header = show.Title;
                    showNode.Tag = show;
                    categoryNode.Items.Add(showNode);
                }
            }
        }

        private void ShowAllEpisodesForGroup(string groupName)
        {
            _currentShows.Clear();
            Shows shows = (Shows)Shows.GetShowService;

            List<ShowControl> showsInCat = shows.ShowInGroup(groupName);

            foreach (ShowControl show in showsInCat)
            {
                _currentShows.Add(show);
            }
            ShowAllEpisodesFromShows();
        }

        private void ShowAllEpisodesFromShows()
        {
            _podcasts.Clear();

            List<EpisodeControl> orderedByDateList = new List<EpisodeControl>();
            foreach (ShowControl show in _currentShows)
            {
                show.Episodes.ForEach(x => x.ShowName = show.Title);
                orderedByDateList.AddRange(show.Episodes);
            }

            orderedByDateList = orderedByDateList.OrderByDescending(x => x.PublishedDate.Ticks).ToList();

            ShowEpisodes(orderedByDateList);
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
                _mediaPlayer?.Pause();
            }
            else if (e.Reason == SessionSwitchReason.SessionUnlock)
            {
                Console.WriteLine("Welcome back - you will need to manual start the audio.");
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
            _rssTimer.Stop();
        }
    }
}
