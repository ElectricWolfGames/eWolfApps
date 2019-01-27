using eWolfCommon.Diagnostics;
using System;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class ExitSystemTrayAction : IMenuAction
    {
        public int OrderIndex
        {
            get
            {
                return 0;
            }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("Exit version 1.0.1", new EventHandler(ExitApplication));
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            Logger.Instance.Log("SystemTray: User exit");
            Application.Exit();
        }
    }
}
