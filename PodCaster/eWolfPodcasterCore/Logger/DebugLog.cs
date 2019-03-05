using eWolfPodcasterCore.Services;
using System;
using System.Threading;

namespace eWolfPodcasterCore.Logger
{
    public static class DebugLog
    {
        public static void LogInfo(string message)
        {
            // TODO Need to fid a why to log out the message on the mai UI process
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
