using FileDuplicatesUI.Data;
using FileDuplicatesUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FileDuplicatesUI.Pages
{
    public partial class ViewDuplicates : Window
    {
        private ObservableCollection<MatchItems> _matchedItems = new ObservableCollection<MatchItems>();

        public ViewDuplicates()
        {
            InitializeComponent();
        }

        public FileDetails FileDetail { get; set; }

        public void PopulateItems()
        {
            ItemList.ItemsSource = _matchedItems;
            MatchItems mi = new MatchItems()
            {
                Name = FileDetail.FileName,
                Path = FileDetail.FilePath
            };

            _matchedItems.Add(mi);
            foreach (FileDetails fd in FileDetail.Matches)
            {
                MatchItems mi2 = new MatchItems()
                {
                    Name = fd.FileName,
                    Path = fd.FilePath,
                    Store = fd
                };
                _matchedItems.Add(mi2);
            }

            SetItemToDelete();

            KeepOne();
        }

        private void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            FileDetailsHolderService fileDetailsHolderService = ServiceLocator.Instance.GetService<FileDetailsHolderService>();

            List<MatchItems> itemsToDelete = _matchedItems.Where(x => x.Action == Actions.Delete).ToList();

            foreach (var mi in itemsToDelete)
            {
                try
                {
                    File.Delete(mi.FilePathName);
                    FileDetail.Matches.Remove(mi.Store);
                    _matchedItems.Remove(mi);
                    fileDetailsHolderService.Details.Remove(mi.Store);
                }
                catch
                {
                    // just in case we have problems
                }
            }
            fileDetailsHolderService.Details.Remove(FileDetail);
        }

        private void ItemList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView == null)
                return;

            MatchItems file = listView.SelectedItems[0] as MatchItems;
            if (file == null)
                return;

            if (file.Action == Actions.Keep)
                file.Action = Actions.Delete;
            else
                file.Action = Actions.Keep;

            ItemList.Items.Refresh();
        }

        private void KeepOne()
        {
            int keeping = _matchedItems.Count(x => x.Action == Actions.Keep);
            if (keeping > 0)
                return;

            _matchedItems[0].Action = Actions.Keep;
        }

        private void SetItemToDelete()
        {
            foreach (var mi in _matchedItems)
            {
                if (mi.Name.Contains("copy", StringComparison.OrdinalIgnoreCase))
                {
                    mi.Action = Actions.Delete;
                }
            }
        }
    }
}