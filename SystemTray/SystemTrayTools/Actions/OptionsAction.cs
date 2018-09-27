using System;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class OptionsAction : IMenuAction
    {
        public int OrderIndex
        {
            get
            {
                return 20;
            }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("Options", new EventHandler(DisplayOptions));
        }

        private void DisplayOptions(object sender, EventArgs e)
        {
            // TODO: add the options page here
            //ShowOptions sle = new ShowOptions();
            //sle.Show();
        }
    }
}
