using eWolfPodcasterCore.Helpers;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Timers;

namespace eWolfPodcasterCore.Services
{
    public class DownloadService
    {
        private readonly ConcurrentQueue<DownloadItem> _downloadqueue = new ConcurrentQueue<DownloadItem>();

        private bool _downloading = false;

        public DownloadService()
        {
            Timer downloadTimer = new Timer(5000);

            downloadTimer.Elapsed += OnTimedEvent;
            downloadTimer.AutoReset = true;
            downloadTimer.Enabled = true;
        }

        public static DownloadService GetDownloadService
        {
            get
            {
                return ServiceLocator.Instance.GetService<DownloadService>();
            }
        }

        public void Add(string url, string downloadFileTo)
        {
            var array = _downloadqueue.ToArray();
            if (array.Any(x => x.From == url))
            {
                Console.WriteLine("All ready in download list");
                return;
            }

            DownloadItem di = new DownloadItem
            {
                Name = "Test",
                From = url,
                To = downloadFileTo
            };

            _downloadqueue.Enqueue(di);
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (_downloadqueue.IsEmpty || _downloading)
                return;

            DownloadItem di;
            if (_downloadqueue.TryDequeue(out di))
            {
                _downloading = true;
                try
                {
                    using (WebClient webClient = new WebClient())
                    {
                        string name = DataCleansing.FileSafeFileName(Path.GetFileName(di.To));
                        string path = Path.GetDirectoryName(di.To);
                        Directory.CreateDirectory(path);

                        webClient.DownloadFile(di.From, path + "\\" + name);
                        Console.WriteLine($"Finished Downloaded {Path.GetFileName(name)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed podcast Downloaded File " + ex);
                    _downloading = false;
                }

                _downloading = false;
            }
        }
    }
}