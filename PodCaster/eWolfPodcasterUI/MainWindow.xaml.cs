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

            PersistenceHelper ph = new PersistenceHelper(GetOutputFolder());

            ShowControl sc = new ShowControl();
            sc.Title = "TestShowName";

            ph.SaveData(new List<ISaveable>() { sc });
        }

        public string GetOutputFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "eWolf\\eWolfTestApp");
        }
    }
}
