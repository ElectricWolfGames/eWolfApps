using AudioWolfStandard.Services;
using AudioWolfUI.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AudioWolfUI.UserControls
{
    /// <summary>
    /// Interaction logic for SoundListItem.xaml
    /// </summary>
    public partial class SoundListItem : UserControl
    {
        public SoundListItem()
        {
            InitializeComponent();
        }

        public SoundDetails SoundItemData { get; set; } = new SoundDetails();

        public string Title
        {
            get
            {
                return $"{SoundItemData.Name}";// {string.Join("-", SoundItemData.Tags)}";
            }
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            // play the current sound effect
            Console.WriteLine("TODO: Start playing :" + Title);
            MediaPlayerService mps = ServiceLocator.Instance.GetService<MediaPlayerService>();
            mps.PlayEpisode(SoundItemData);
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            // play the current sound effect
            Console.WriteLine("TODO: Remove from the list :" + Title);
        }
    }

    public class SoundDetails
    {
        public string Name { get; set; }
        public string FullPath { get; set; }
    }
}
