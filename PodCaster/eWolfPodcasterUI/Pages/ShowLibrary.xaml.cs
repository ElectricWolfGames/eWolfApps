using eWolfPodcasterCore.Services;
using System.Windows;

namespace eWolfPodcasterUI.Pages
{
    /// <summary>
    /// Interaction logic for ShowLibrary.xaml
    /// </summary>
    public partial class ShowLibrary : Window
    {
        public ShowLibrary()
        {
            InitializeComponent();

            PopulateCatergies();
        }

        private void PopulateCatergies()
        {
            ShowLibraryService showLibraryService = ShowLibraryService.GetLibrary;
            LibraryCategories.ItemsSource = showLibraryService.Groups();
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
    }
}
