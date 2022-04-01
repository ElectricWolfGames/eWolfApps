using System;
using static eWolfPodcasterCore.Logger.LoggerData;

namespace eWolfPodcasterCore.Interfaces
{
    public interface IDebugLoggerData
    {
        string Message { get; }
        DateTime MessageTime { get; }

        MessageTypes MessageType { get; }
    }
}