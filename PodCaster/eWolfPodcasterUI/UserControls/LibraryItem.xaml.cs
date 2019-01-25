﻿using eWolfPodcasterCore.Data;
using eWolfPodcasterCore.Library;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace eWolfPodcasterUI.UserControls
{
    /// <summary>
    /// Interaction logic for LibraryItem.xaml
    /// </summary>
    public partial class LibraryItem : UserControl
    {
        private ShowLibraryData _showLibraryData = new ShowLibraryData();

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

        public ShowLibraryData ShowLibraryData
        {
            get
            {
                return _showLibraryData;
            }
            set
            {
                _showLibraryData = value;
            }
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
            ShowControl sc = new ShowControl()
            {
                Title = _showLibraryData.Name,
                RssFeed = _showLibraryData.URL,
            };
            if (Shows.GetShowService.Add(sc))
            {
                System.Console.WriteLine($"Add {_showLibraryData.Name} to main list");
            }
            else
            {
                System.Console.WriteLine($"{_showLibraryData.Name} All ready in list");
            }
        }
    }
}