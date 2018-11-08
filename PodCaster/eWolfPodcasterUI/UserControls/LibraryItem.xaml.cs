using eWolfPodcasterCore.Library;
using System.Windows.Controls;

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

        public string Name
        {
            get
            {
                return ShowLibraryData.Name;
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
    }
}
