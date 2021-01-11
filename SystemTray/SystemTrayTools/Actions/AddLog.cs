using eWolfCommon.Diagnostics;
using System;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;
using SystemTrayTools.Pages;

namespace SystemTrayTools.Actions
{
    public class AddLog : IMenuAction
    {
        public int OrderIndex
        {
            get
            {
                return 99;
            }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("Add Log", new EventHandler(AddLogToStore));
        }

        private void AddLogToStore(object sender, EventArgs e)
        {
            Logger.Instance.Log("SystemTray: AddLogToStore");
            AddTextToLog sle = new AddTextToLog();
            sle.Show();
        }
    }
}