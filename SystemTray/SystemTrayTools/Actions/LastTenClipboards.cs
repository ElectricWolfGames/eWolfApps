using eWolfCommon.Collections;
using eWolfCommon.Diagnostics;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;

// TODO : BUG not showing the list of stored clipboard itmes !
namespace SystemTrayTools.Actions
{
    public class LastTenClipboards : IMenuAction, IUpdateWithClipBoard
    {
        private readonly StoreUniqueList<string> _clips;

        public LastTenClipboards()
        {
            _clips = new StoreUniqueList<string>(10);
        }

        public int OrderIndex
        {
            get
            {
                return 1;
            }
        }

        public MenuItem GetMeunItem()
        {
            Logger.Instance.Log("SystemTray: Show the recent clipboards");
            List<MenuItem> dd = new List<MenuItem>();

            foreach (string text in _clips.Items)
            {
                MenuItem mi = new MenuItem(text, OnDisplayitem_SetFromClipboard);
                dd.Add(mi);
            }

            return new MenuItem("Last Clips", dd.ToArray());
        }

        public void OnDisplayitem_SetFromClipboard(object sender, EventArgs e)
        {
            // get the index from the sender text.
            if (!(sender is ToolStripItem tsi))
                return;

            int value = int.Parse(tsi.Name);

            Clipboard.SetText(_clips.Items[value]);
        }

        public void UpdateFromClipBoard(string clipboardData)
        {
            _clips.Add(clipboardData);
            Console.WriteLine($"Add item to clip board list {clipboardData}");
        }

        private ToolStripItem GetToolStripItem(int index)
        {
            ToolStripMenuItem tsi = new ToolStripMenuItem();
            string text = _clips.Items[index];
            if (text.Length > 256)
            {
                text = text.Substring(0, 256);
            }

            text = index.ToString() + " : " + text;

            tsi.Name = index.ToString();
            tsi.Text = text;
            tsi.CheckOnClick = true;
            tsi.Click += new EventHandler(OnDisplayitem_SetFromClipboard);

            return tsi;
        }
    }
}
