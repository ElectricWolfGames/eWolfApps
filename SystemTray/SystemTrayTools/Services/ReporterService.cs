using System;
using System.IO;
using System.Text;

namespace SystemTrayTools.Services
{
    public class ReporterService
    {
        public static ReporterService Service
        {
            get
            {
                return ServiceLocator.Instance.GetService<ReporterService>();
            }
        }

        public void AddTextToReport(string textToStore)
        {
            string folder = GetWolfReportFolder();
            Directory.CreateDirectory(folder);

            string fileName = folder + "\\Report.log";
            string textInFile = string.Empty;
            try
            {
                textInFile = File.ReadAllText(fileName, Encoding.UTF8);
            }
            catch
            {
                // fail safe can load file
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(DateTime.Now.ToString("yyyy.MM.dd : hh:mm:ss"));
            sb.AppendLine(textToStore);
            sb.AppendLine();
            textInFile += sb.ToString();

            try
            {
                File.WriteAllText(fileName, textInFile);
            }
            catch
            {
                // fail safe - can't aave file
            }
        }

        private static string GetWolfReportFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"eWolf/eWolfSystem/Logs");
        }
    }
}
