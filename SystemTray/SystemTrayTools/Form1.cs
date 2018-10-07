using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SystemTrayTools.Helpers;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools
{
    public partial class Form1 : Form
    {
        private string _clipBoardCopy = string.Empty;
        private MenuActionsHolder _menuActionsHolder = new MenuActionsHolder();
        private List<IIntervalUpdates> _updates;

        public Form1()
        {
            InitializeComponent();

            TimeClock = new Timer
            {
                Interval = 1000
            };
            TimeClock.Start();
            TimeClock.Tick += new EventHandler(Timer_Tick);

            PopulateMenuList();

            Hide();
        }

        private Timer TimeClock { get; set; }

        public void Timer_Tick(object sender, EventArgs e)
        {
            UpdateAll();
        }

        private void NotifyIconsMouseDoubleClick(object sender, MouseEventArgs e)
        {
            Hide();
            WindowState = FormWindowState.Normal;
        }

        private void PopulateMenuList()
        {
            _menuActionsHolder.PopulateList();
            PopulateUpdateItems();
            NotifyIcon.ContextMenu = _menuActionsHolder.GetMenu(this);
        }

        private void PopulateUpdateItems()
        {
            var updates = from t in Assembly.GetExecutingAssembly().GetTypes()
                          where t.GetInterfaces().Contains(typeof(IIntervalUpdates))
                                && t.GetConstructor(Type.EmptyTypes) != null
                          select Activator.CreateInstance(t) as IIntervalUpdates;

            _updates = new List<IIntervalUpdates>();
            _updates.AddRange(updates.ToList());
        }

        private void UpdateAll()
        {
            _updates.ForEach(x => x.UpdateInterval());

            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.Text))
            {
                string str = (string)dataObject.GetData(DataFormats.Text);
                if (_clipBoardCopy != str)
                {
                    _clipBoardCopy = str;
                    _menuActionsHolder.UpdateClipboard(str);
                }
            }
        }
    }
}
