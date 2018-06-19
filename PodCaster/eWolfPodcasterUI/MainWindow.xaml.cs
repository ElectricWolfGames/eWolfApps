using eWolfPodcaster;
using eWolfPodcaster.Data;
using System.Collections.Generic;
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

            PersistenceHelper.LoadData();
        }
    }
}
