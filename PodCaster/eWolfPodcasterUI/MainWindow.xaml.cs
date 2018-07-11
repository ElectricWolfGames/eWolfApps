using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterUI.CustomDialog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Shows _shows = new Shows();
        public ObservableCollection<IPodCastInfo> Podcasts = new ObservableCollection<IPodCastInfo>();

        public MainWindow()
        {
            InitializeComponent();

            _shows.Load(GetOutputFolder());
            _shows.UpdateAllRSSFeeds();
            _shows.Save(GetOutputFolder());

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
            AddNewShow addNewShow = new AddNewShow();
            addNewShow.ShowName = "new show name";
            addNewShow.ShowDialog();

            if (addNewShow.Apply)
            {
                ShowControl sc = new ShowControl()
                {
                    Title = addNewShow.ShowName,
                    RssFeed = addNewShow.RSSFeed
                };
                _shows.Add(sc);
            }
        }

        private void ButtonSubShowClick(object sender, RoutedEventArgs e)
        {
            var selectedItems = ShowsItems.SelectedItems;
            if (selectedItems.Count == 0)
                return;

            ShowControl selectedItem = (ShowControl)selectedItems[0];
            _shows.RemoveShow(selectedItem);
        }

        private void EpisodesItems_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void ShowsItems_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            ShowControl sc = item as ShowControl;

            if (sc != null)
            {
                Podcasts.Clear();

                List<EpisodeControl> orderedByDateList = null;
                orderedByDateList = sc.Episodes.OrderByDescending(x => x.PublishedDate.Ticks).ToList();

                foreach (EpisodeControl x in orderedByDateList)
                {
                    if (x.Hidden)
                        continue;

                    IPodCastInfo pce = new PodcastEpisode();
                    pce.EpisodeData = x;
                    // x.NewEpisode = false;
                    Podcasts.Add(pce);
                }

                EpisodesItems.ItemsSource = Podcasts;
            }
        }
    }
}
