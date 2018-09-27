using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SystemTrayTools.Helpers;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class InsertFieldInCSV : IMenuAction
    {
        public MenuItem GetMeunItem()
        {
            return new MenuItem("InsertField", new EventHandler(handler_method));
        }

        public int OrderIndex
        {
            get { return 3; }
        }

        #region Private Methods

        private void handler_method(object sender, EventArgs e)
        {
            string csv = ClipboardHelpers.GetTextFromClipboard();
            List<string> fields = csv.Split(',').ToList();
            fields.Insert(2, "newDataHere");

            ClipboardHelpers.SetTextForClipboard(string.Join(",", fields));
        }

        #endregion Private Methods
    }
}
