using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SystemTrayTools.Data;
using SystemTrayTools.Interfaces;

namespace SystemTrayTools.Actions
{
    public class UpdateHeartBeat : IMenuAction
    {
        public int OrderIndex
        {
            get
            {
                return 7;
            }
        }

        public MenuItem GetMeunItem()
        {
            return new MenuItem("Update Heart Beat", new EventHandler(UpdateHeartBeatFile));
        }

        public void UpdateHeartBeatFile(object sender, EventArgs e)
        {
            foreach (ServerNameStatus sns in ServerNameStatus.ServerList)
            {
                sns.UpdateStatus();
            }

            CreateOutputFile();
        }

        private static void CreateOutputFile()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h2>Server Heatbeat</h2>");
            sb.AppendLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());

            sb.AppendLine("<ul>");
            foreach (ServerNameStatus sns in ServerNameStatus.ServerList)
            {
                sb.AppendLine(sns.OutputData());
            }
            sb.AppendLine("</ul>");

            string filename = @"D:\Personal\HelpPages\HeartBeat.html";
            File.WriteAllText(filename, sb.ToString());
        }
    }
}
