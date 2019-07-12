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

        public ISoundDetails SoundItemData { get; set; } = new SoundDetails();

        public string Title
        {
            get
            {
                return SoundItemData.Name;
            }
            set
            {
                SoundItemData.Name = value;
                Console.WriteLine($"Update: {value}");
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
}
