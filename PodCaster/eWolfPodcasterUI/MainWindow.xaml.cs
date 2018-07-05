using eWolfPodcasterCore;
using eWolfPodcasterCore.Data;
using System;
using System.IO;
using System.Windows;

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
        }

        public string GetOutputFolder()
        {
            return Path.Combine("c:\\eWolf\\eWolfTestApp");
        }
    }
}
