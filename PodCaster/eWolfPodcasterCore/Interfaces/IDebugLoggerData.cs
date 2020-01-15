using System;
using static eWolfPodcasterCore.Logger.LoggerData;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IDebugLoggerData
    {
        DateTime MessageTime { get; }

        MessageTypes MessageType { get; }

        string Message { get; }
    }
}