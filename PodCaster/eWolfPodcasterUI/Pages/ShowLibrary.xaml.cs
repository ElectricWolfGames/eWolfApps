using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Library;
using eWolfPodcasterCore.Services;
using eWolfPodcasterUI.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI.Pages
{
    /// <summary>
    /// Interaction logic for ShowLibrary.xaml
    /// </summary>
    public partial class ShowLibrary : Window
    {
        private string _currentGroupName;

        private List<CatergeryData> _groups;
        private List<string> _groupNames;
        private ObservableCollection<LibraryItem> _libraryItem = new ObservableCollection<LibraryItem>();

        public ShowLibrary()
        {
            InitializeComponent();

            PopulateCatergies();
        }

        public bool Apply { get; set; }

        public void RedrawList()
        {
            PopulateGroup();
        }

        private void ButtonCancelClick(object sender, RoutedEventArgs e)
        {
            Apply = false;
            Close();
        }

        private void ButtonOKClick(object sender, RoutedEventArgs e)
        {
            Apply = true;
            Close();
        }

        private bool IsShowAllReadyAdded(ShowLibraryData it)
        {
            CatergeryData cd = new CatergeryData(it.Catergery);
            ShowControl sc = new ShowControl()
            {
                Title = it.Name,
                RssFeed = it.URL,
                Catergery = cd
            };

            return Shows.GetShowService.Contains(sc);
        }

        private void LibraryCategories_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            string groupName = item as string;

            _currentGroupName = groupName;
            PopulateGroup();
        }

        private void PopulateGroup()
        {
            if (_currentGroupName != null)
            {
                _libraryItem.Clear();

                ShowLibraryService showLibraryService = ShowLibraryService.GetLibrary;
                _groups = showLibraryService.Groups();

                foreach (ShowLibraryData it in showLibraryService.GetList(_currentGroupName))
                {
                    if (!IsShowAllReadyAdded(it))
                    {
                        LibraryItem li = new LibraryItem();
                        li.ShowLibraryData = it;
                        li.LibraryMain = this;
                        _libraryItem.Add(li);
                    }
                }
            }
            LibraryShows.ItemsSource = _libraryItem;
        }

        private void PopulateCatergies()
        {
            ShowLibraryService showLibraryService = ShowLibraryService.GetLibrary;

            List<string> allGroups = showLibraryService.Groups().Select(x => x.Name).ToList();
            _groupNames = new List<string>();

            foreach (string group in allGroups)
            {
                List<ShowLibraryData> shows = showLibraryService.GetList(group);
                if (shows.Any(x => !IsShowAllReadyAdded(x)))
                {
                    _groupNames.Add(group);
                }
            }
            LibraryCategories.ItemsSource = _groupNames;
        }
    }
}