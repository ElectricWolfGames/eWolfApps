using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace GenerateLogo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Make_Click(object sender, EventArgs e)
        {
            string name = LogoName.Text;
            string fileName = $"{Guid.NewGuid()}.html";

            BuildHTML buildHTML = new BuildHTML(name, fileName)
            {
                ColorA = ColorA.Text,
                ColorB = ColorB.Text
            };

            buildHTML.Build();

            string path = $@"C:\GitHub\eWolfApps\GeneratorLogos\Logos\{fileName}";
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "chrome";
            process.StartInfo.Arguments = path;
            process.Start();
        }
    }
}