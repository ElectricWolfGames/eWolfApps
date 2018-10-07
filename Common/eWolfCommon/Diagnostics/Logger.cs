using System;
using System.IO;

namespace eWolfCommon.Diagnostics
{
    public sealed class Logger
    {
        private static Logger _logger = null;
        private readonly string _fileName;

        private Logger()
        {
            _fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + System.Environment.MachineName + "_");
        }

        public static Logger Instance
        {
            get
            {
                if (_logger == null)
                {
                    _logger = new Logger();
                }
                return _logger;
            }
        }

        public void Log(string dataToLog)
        {
            DateTime dt = DateTime.Now;
            try
            {
                string filename = _fileName + dt.ToString("yyyMMdd") + ".log";
                using (StreamWriter w = File.AppendText(filename))
                {
                    w.Write($"{dt.ToShortDateString()} {dt.ToShortTimeString()},{System.Environment.MachineName}," + dataToLog);

                    w.Write(Environment.NewLine);
                }
            }
            catch
            {
                // Failed
            }
        }
    }
}
