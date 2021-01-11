using eWolfCommon.Diagnostics;
using System;
using System.Windows.Forms;
using SystemTrayTools.Helpers;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class OpenScreenGrabFolderAction : IMenuAction
    {
        public int OrderIndex
        {
            get
            {
                return 10;
            }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("Open Screen Grab folder", new EventHandler(OpenScreenGrab));
        }

        private void OpenScreenGrab(object sender, EventArgs e)
        {
            Logger.Instance.Log("SystemTray: Open Screen Grab folder");
            ScreenGrabHelper.OpenShellFolder();
        }
    }
}