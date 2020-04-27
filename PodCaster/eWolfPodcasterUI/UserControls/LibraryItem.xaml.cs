using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Helpers;
using eWolfPodcasterCore.Library;
using eWolfPodcasterUI.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Xml;

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

        public string EpisodeCount
        {
            get
            {
                /*if (string.IsNullOrWhiteSpace(ShowLibraryData.LastDownloadMessage))
                {
                    GetEpisodeCount();
                }*/
                return ShowLibraryData.LastDownloadMessage;
            }
        }

        private void GetEpisodeCount()
        {
            XmlReader downloadRSS = DownloadRSSFile();
            if (downloadRSS == null)
                return;

            CountEpisodes(downloadRSS);
        }

        private void CountEpisodes(XmlReader downloadRSS)
        {
            var items = RSSHelper.ReadEpisodes(downloadRSS);
            ShowLibraryData.LastDownloadMessage = $"{items.Count} Episodes";
        }

        internal XmlReader DownloadRSSFile()
        {
            XmlReader reader = null;
            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            try
            {
                reader = XmlReader.Create(ShowLibraryData.URL, settings);
                ShowLibraryData.LastDownloadMessage = "Downloaded";
            }
            catch
            {
                ShowLibraryData.LastDownloadMessage = "Failed";
                reader = null;
            }

            return reader;
        }

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