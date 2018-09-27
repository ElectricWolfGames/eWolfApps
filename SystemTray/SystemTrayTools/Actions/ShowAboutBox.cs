using System;
using System.Text;
using System.Windows.Forms;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class ShowAboutBox : IMenuAction
    {
        public int OrderIndex
        {
            get { return 99; }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("About", new EventHandler(HandlerMethod));
        }

        private void HandlerMethod(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("System Tray Tool");

            sb.AppendLine("Built on [Need to add the build date here]");

            MessageBox.Show(sb.ToString(), "About Box", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
