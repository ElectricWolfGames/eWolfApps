using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GeneratorLogos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = LogoName.Text;
            string fileName = $"{Guid.NewGuid()}.html";

            BuildHTML buildHTML = new BuildHTML(name, fileName)
            {
                ColorA = ColorA.Text,
                ColorB = ColorB.Text
            };

            buildHTML.Build();

            string path = @$"C:\GitHub\eWolfApps\GeneratorLogos\Logos\{fileName}";
            Process process = new Process();
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.FileName = "chrome";
            process.StartInfo.Arguments = path;
            process.Start();
        }
    }
}
