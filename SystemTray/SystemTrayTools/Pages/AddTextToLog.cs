using System;
using System.Windows.Forms;
using SystemTrayTools.Services;

namespace SystemTrayTools.Pages
{
    public partial class AddTextToLog : Form
    {
        public AddTextToLog()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            string textToStore = textToLog.Text;
            if (!string.IsNullOrWhiteSpace(textToStore))
            {
                ReporterService.Service.AddTextToReport(textToStore);
            }

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
