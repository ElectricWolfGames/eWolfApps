using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Library;
using eWolfPodcasterUI.Pages;
using System.Windows;
using System.Windows.Controls;

namespace eWolfPodcasterUI.UserControls
{
    public partial class LibraryItem : UserControl
    {
        public ShowLibrary LibraryMain = null;

        public LibraryItem()
        {
            InitializeComponent();
        }

        public string Description
        {
            get
            {
                return ShowLibraryData.Description;
            }
        }

        public ShowLibraryData ShowLibraryData { get; set; } = new ShowLibraryData();

        public string Title
        {
            get
            {
                return ShowLibraryData.Name;
            }
        }

        public void butttonAddShow_Click(object sender, RoutedEventArgs e)
        {
            CatergeryData cd = new CatergeryData(ShowLibraryData.Catergery);
            ShowControl sc = new ShowControl()
            {
                Title = ShowLibraryData.Name,
                RssFeed = ShowLibraryData.URL,
                Catergery = cd
            };

            if (Shows.GetShowService.Add(sc))
            {
                System.Console.WriteLine($"Add {ShowLibraryData.Name} to main list");
                Shows.GetShowService.Save();
            }
            else
            {
                System.Console.WriteLine($"{ShowLibraryData.Name} All ready in list");
            }
            LibraryMain.RedrawList();
        }
    }
}
