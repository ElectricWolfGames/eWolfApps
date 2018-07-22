﻿using System.ComponentModel;
using System.Windows;

namespace eWolfPodcasterUI.Pages
{
    /// <summary>
    /// Interaction logic for AddNewShow.xaml
    /// </summary>
    public partial class AddNewShow : Window, INotifyPropertyChanged
    {
        private string _showName = string.Empty;
        private string _rssFeed = string.Empty;

        public AddNewShow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public bool Apply { get; set; }

        public string RSSFeed
        {
            get
            {
                return _rssFeed;
            }
            set
            {
                _rssFeed = value;
                OnPropertyChanged("RSSFeed");
            }
        }

        public string ShowName
        {
            get
            {
                return _showName;
            }

            set
            {
                _showName = value;
                OnPropertyChanged("ShowName");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Apply = false;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Apply = true;
            Close();
        }
    }
}
