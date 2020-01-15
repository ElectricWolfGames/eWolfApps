using eWolfPodcasterCore.Services;

namespace eWolfPodcasterCore.Logger
{
    public static class DebugLog
    {
        private static readonly object _locakable = false;

        public static void LogError(string message)
        {
            ServiceLocator.Instance.GetService<LoggerService>().AddError(message);
        }

        public static void LogInfo(string message)
        {
            /*string fileName = @"D:\OffLine\Log.log";

            DateTime currentTime = DateTime.Now;
            message = $"{currentTime.ToShortDateString()} {currentTime.ToShortTimeString()}: {message}";

            lock (_locakable)
            {
                string rawFile = File.ReadAllText(fileName);
                rawFile += "\n" + message;
                File.WriteAllText(fileName, rawFile);
            }*/
        }

        public static void LogWarning(string message)
        {
            ServiceLocator.Instance.GetService<LoggerService>().AddWarning(message);
        }
    }
}