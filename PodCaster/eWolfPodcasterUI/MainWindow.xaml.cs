using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using eWolfPodcasterCore.Services;
using eWolfPodcasterUI.Pages;
using eWolfPodcasterUI.UserControls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace eWolfPodcasterUI
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private readonly MediaPlayer _mediaPlayer = new MediaPlayer();
        private PodcastEpisode _currentPodcast = null;
        private ObservableCollection<IDebugLoggerData> _errorLog = new ObservableCollection<IDebugLoggerData>();
        private ObservableCollection<IPodCastInfo> _podcasts = new ObservableCollection<IPodCastInfo>();
        private DispatcherTimer _rssTimer;

        public MainWindow()
        {
            InitializeComponent();
            DebugLog.LogInfo("App started");

            Shows.GetShowService.Load(GetOutputFolder());
            Shows.GetShowService.Save();

            ShowLibraryService.GetLibrary.Load(GetLibraryPath());

            PopulateTree();
            PopulateLogPage();
            CreatePayerTimer();
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

        private string GetBaseFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf", "PodCaster");
        }

        public string GetLibraryPath()
        {
            return Path.Combine(GetBaseFolder(), "PodcastList.xml");
        }

        public string GetOutputFolder()
        {
            return GetBaseFolder();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
                ShowControl sc = new ShowControl()
                {
                    Title = addNewShow.ShowName,
                    RssFeed = addNewShow.RSSFeed,
                    LocalFiles = addNewShow.LocalFiles.IsChecked.Value,
                    CheckForUpdated = addNewShow.CheckForUpdates.IsChecked.Value,
                    AutoDownloadEpisodes = addNewShow.AutoDownload.IsChecked.Value
                };

                Shows.GetShowService.Add(sc);
                Shows.GetShowService.Save();
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
                        _rssTimer.Interval = TimeSpan.FromSeconds(_rssTimer.Interval.Seconds * 20);
                    }
                });
            }
            catch (Exception ex)
            {
                DebugLog.LogInfo($"Check Next Show Failed with {ex.Message}");
            }
        }

        private void CreatePayerTimer()
        {
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void CreateRSSTimer()
        {
            _rssTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(5)
            };
            _rssTimer.Tick += UpdateRssFeedTimer;
            _rssTimer.Start();
        }

        private void EpisodeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0)
                return;

            _currentPodcast = e.AddedItems[0] as PodcastEpisode;

            _mediaPlayer.Open(new Uri(_currentPodcast.UrlToPlay));
            _mediaPlayer.Position = new TimeSpan(_currentPodcast.PlayedLength);
            _mediaPlayer.Play();
            OnPropertyChanged("PodcastDescription");
        }

        private void LogListUpdated(object sender, NotifyCollectionChangedEventArgs e)
        {
            PopulateLogPage();
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

            Shows shows = Shows.GetShowService;
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

        private void ShowsItemsTree_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)(sender as TreeView).SelectedItem;
            ShowControl sc = item.Tag as ShowControl;
            if (sc != null)
            {
                _podcasts.Clear();

                List<EpisodeControl> orderedByDateList = null;
                if (sc.LocalFiles)
                {
                    orderedByDateList = sc.Episodes.OrderBy(x => x.PodcastURL).ToList();
                }
                else
                {
                    orderedByDateList = sc.Episodes.OrderByDescending(x => x.PublishedDate.Ticks).ToList();
                }

                foreach (EpisodeControl x in orderedByDateList)
                {
                    if (x.Hidden)
                        continue;

                    IPodCastInfo pce = new PodcastEpisode
                    {
                        EpisodeData = x
                    };
                    _podcasts.Add(pce);
                }

                EpisodesItems.ItemsSource = _podcasts;
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

        private void TimerTick(object sender, EventArgs e)
        {
            if (_mediaPlayer.Source != null && _mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                _currentPodcast.PlayedLength = _mediaPlayer.Position.Ticks;

                double totalWidth = 781;

                totalWidth /= _mediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;
                totalWidth *= _mediaPlayer.Position.TotalMilliseconds;
                _currentPodcast.PlayedLengthScaled = (float)totalWidth;
            }
        }

        private void UpdateRssFeedTimer(object sender, EventArgs e)
        {
            CheckNextShow();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _rssTimer.Stop();
        }
    }
}
