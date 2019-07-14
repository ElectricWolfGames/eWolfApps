using AudioWolfStandard.Data;
using AudioWolfStandard.Interfaces;
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
            DataContext = this;
        }

        public ISoundDetails SoundDetails { get; set; } = new SoundDetails();

        public string Title
        {
            get
            {
                return SoundDetails.Name;
            }
            set
            {
                SoundDetails.Name = value;
            }
        }

        public string Path
        {
            get
            {
                return SoundDetails.FullPath;
            }
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("TODO: Start playing :" + Title);
            MediaPlayerService mps = ServiceLocator.Instance.GetService<MediaPlayerService>();
            mps.PlayEpisode(SoundDetails);
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            // play the current sound effect
            Console.WriteLine("TODO: Remove from the list :" + Title);
        }
    }
}
