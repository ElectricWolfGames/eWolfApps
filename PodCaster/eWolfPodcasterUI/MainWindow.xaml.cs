using eWolfPodcaster;
using eWolfPodcaster.Data;
using eWolfPodcaster.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace eWolfPodcasterUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICollection<Show> _shows;

        public MainWindow()
        {
            InitializeComponent();

            PersistenceHelper<ShowControl> ph = new PersistenceHelper<ShowControl>(GetOutputFolder());

            List<ShowControl> showList = ph.LoadData();
            if (showList.Count == 0)
            {
                ShowControl sc = CreateFakeShow();
                showList.Add(sc);
                ph.SaveData(showList);
            }
        }

        private ShowControl CreateFakeShow()
        {
            ShowControl sc = new ShowControl();
            sc.Title = "CodingBlocks";
            sc.RssFeed = "http://www.codingblocks.net/feed/podcast";
            sc.ShowOption.AudoDownloadEpisodes = false;
            sc.ShowOption.Category = "Dev";
            sc.ShowOption.CheckforUpdates = true;

            return sc;
        }

        public string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\eWolfTestApp");
        }
    }
}
