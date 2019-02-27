using eWolfPodcasterCore.Services;

namespace eWolfPodcasterCore.Logger
{
    public static class DebugLog
    {
        public static void LogInfo(string message)
        {
            ServiceLocator.Instance.GetService<LoggerService>().AddInfo(message);
        }

        public static void LogError(string message)
        {
            ServiceLocator.Instance.GetService<LoggerService>().AddError(message);
        }

        public static void LogWarning(string message)
        {
            ServiceLocator.Instance.GetService<LoggerService>().AddWarning(message);
        }
    }
}
