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

        public string Description
        {
            get
            {
                return ShowLibraryData.Description;
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

        public string Title
        {
            get
            {
                return ShowLibraryData.Name;
            }
        }
    }
}
