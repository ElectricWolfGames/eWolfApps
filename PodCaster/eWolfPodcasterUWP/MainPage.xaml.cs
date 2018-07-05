using eWolfPodcasterCore.Data;
using System;
using System.IO;
using Windows.UI.Xaml.Controls;

namespace eWolfPodcasterUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Shows _shows = new Shows();

        public MainPage()
        {
            this.InitializeComponent();

            _shows.Load(GetOutputFolder());
            _shows.UpdateAllRSSFeeds();
            _shows.Save(GetOutputFolder());
        }

        public string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\eWolfTestApp");
        }
    }
}
