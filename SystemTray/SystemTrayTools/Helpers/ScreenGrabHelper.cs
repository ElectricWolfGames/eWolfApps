using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SystemTrayTools.Helpers
{
    public static class ScreenGrabHelper
    {
        public static void OpenShellFolder()
        {
            string folderToOpen = GetWolfGrabsFolder();
            string args = string.Format("/Select, {0}", folderToOpen);

            ProcessStartInfo pfi = new ProcessStartInfo("Explorer.exe", args);
            Process.Start(pfi);
        }

        public static void TakeScreenGrab()
        {
            string userDocs = GetWolfGrabsFolder();
            Directory.CreateDirectory(userDocs);

            string timeTaken = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");

            using (Bitmap bmpScreenCapture = new Bitmap(
                                            Screen.PrimaryScreen.Bounds.Width * 2,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bmpScreenCapture))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     bmpScreenCapture.Size,
                                     CopyPixelOperation.SourceCopy);

                    string path = Path.Combine(userDocs, timeTaken + ".png");
                    bmpScreenCapture.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private static string GetWolfGrabsFolder()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "WolfScreenGrabs");
        }
    }
}
