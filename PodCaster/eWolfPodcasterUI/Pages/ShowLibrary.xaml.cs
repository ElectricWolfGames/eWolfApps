using eWolfPodcasterCore.Library;
using eWolfPodcasterCore.Services;
using eWolfPodcasterUI.UserControls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI.Pages
{
    /// <summary>
    /// Interaction logic for ShowLibrary.xaml
    /// </summary>
    public partial class ShowLibrary : Window
    {
        private List<CatergeryData> _groups;
        private ObservableCollection<LibraryItem> _libraryItem = new ObservableCollection<LibraryItem>();

        public ShowLibrary()
        {
            InitializeComponent();

            PopulateCatergies();
        }

        private void PopulateCatergies()
        {
            ShowLibraryService showLibraryService = ShowLibraryService.GetLibrary;
            _groups = showLibraryService.Groups();
            LibraryCategories.ItemsSource = _groups;
        }

        public bool Apply { get; set; }

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

        private void LibraryCategories_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            CatergeryData sc = item as CatergeryData;

            if (sc != null)
            {
                _libraryItem.Clear();

                // _libraryItem
                ShowLibraryService showLibraryService = ShowLibraryService.GetLibrary;
                _groups = showLibraryService.Groups();

                foreach (var it in showLibraryService.GetList(sc.Name))
                {
                    LibraryItem li = new LibraryItem();
                    li.ShowLibraryData = it;
                    _libraryItem.Add(li);
                }
            }
            LibraryShows.ItemsSource = _libraryItem;
        }
    }
}
