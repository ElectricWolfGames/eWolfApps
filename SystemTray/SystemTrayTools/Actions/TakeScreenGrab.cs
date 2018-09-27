using eWolfCommon.Diagnostics;
using System;
using System.Windows.Forms;
using SystemTrayTools.Helpers;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class TakeScreenGrab : IMenuAction
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
            return new MenuItem("Take Screen Grab", new EventHandler(TakeScreenGrabImage));
        }

        private void TakeScreenGrabImage(object sender, EventArgs e)
        {
            Logger.Instance.Log("SystemTray: Take screen grab");
            ScreenGrabHelper.TakeScreenGrab();
        }
    }
}
