using AudioWolfStandard.Data;
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

        public SoundItemData SoundItemData { get; set; } = new SoundItemData();

        public string Title
        {
            get
            {
                return $"{SoundItemData.Name} {string.Join("-", SoundItemData.Tags)}";
            }
        }

        private void Button_Click_Play(object sender, RoutedEventArgs e)
        {
            // play the current sound effect
            Console.WriteLine("TODO: Start playing :" + Title);
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            // play the current sound effect
            Console.WriteLine("TODO: Remove from the list :" + Title);
        }
    }
}
