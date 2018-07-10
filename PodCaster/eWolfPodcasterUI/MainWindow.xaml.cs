using eWolfPodcasterCore.Data;
using System;
using System.IO;
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
                EpisodesItems.ItemsSource = sc.Episodes;
            }
        }
    }
}
