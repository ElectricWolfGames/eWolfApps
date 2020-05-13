﻿using FileDuplicates.Data;
using FileDuplicates.Pages;
using FileDuplicates.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace FileDuplicates
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileDetailsObservableCollection<FileDetails> ShowDetails = new FileDetailsObservableCollection<FileDetails>();

        public MainWindow()
        {
            InitializeComponent();

            FileDetailsHolderService fileDetailsHolderService = Services.ServiceLocator.Instance.GetService<FileDetailsHolderService>();

            FileDetailsHolderService f = FileDetailsHolderService.Load(@"C:\Temp\");
            fileDetailsHolderService.AddFrom(f);
            ShowDetails.Clear();
            foreach (var f2 in f.Details)
            {
                ShowDetails.Add(f2);
            }

            ItemList.ItemsSource = ShowDetails;
        }

        private async void CheckAllFiles_Click(object sender, RoutedEventArgs e)
        {
            await GetFiles(FolderLocation.Text);
        }

        private async Task GetFiles(string fileName)
        {
            CheckAllFiles.Content = "Go Started";

            FileDetailsHolderService fileDetailsHolderService = ServiceLocator.Instance.GetService<FileDetailsHolderService>();
            fileDetailsHolderService.Details.Clear();

            string[] files = Directory.GetFiles(fileName, "*.*", SearchOption.AllDirectories);

            await Task.Run(() =>
            {
                foreach (string file in files)
                {
                    FileDetails fd = new FileDetails(file);
                    fileDetailsHolderService.Details.Add(fd);
                    ShowDetails.Add(fd);
                }
            });

            CheckAllFiles.Content = "Go Done";
        }

        private void ItemList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var listView = sender as ListView;
            if (listView == null)
                return;

            var file = listView.SelectedItems[0] as FileDetails;
            if (file == null)
                return;

            ViewDuplicates viewDuplicate = new ViewDuplicates
            {
                FileDetail = file
            };

            viewDuplicate.PopulateItems();
            viewDuplicate.ShowDialog();
        }

        private async void MatchAllFiles_Click(object sender, RoutedEventArgs e)
        {
            /*FileDetailsHolderService fileDetailsHolderService = Services.ServiceLocator.Instance.GetService<FileDetailsHolderService>();

            ObservableCollection<FileDetails> fileDetails = fileDetailsHolderService.Details;

            List<FileDetails> copy = new List<FileDetails>();
            foreach (var fd in fileDetails)
            {
                copy.Add(fd);
            }

            List<string> exludedList = new List<string>();
            foreach (var fd in copy)
            {
                if (exludedList.Contains(fd.FullFilePath))
                    continue;

                foreach (var masterfd in fileDetails)
                {
                    if (fd == masterfd)
                        continue;

                    if (fd.HashCode == masterfd.HashCode)
                    {
                        fd.Matches.Add(masterfd);
                        exludedList.Add(masterfd.FullFilePath);
                    }
                }

                foreach (var removed in fd.Matches)
                {
                    fileDetails.Remove(removed);
                }
            }*/
            this.ItemList.Items.Refresh();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            FileDetailsHolderService.Save(Services.ServiceLocator.Instance.GetService<FileDetailsHolderService>(), @"C:\Temp\");
        }
    }
}
