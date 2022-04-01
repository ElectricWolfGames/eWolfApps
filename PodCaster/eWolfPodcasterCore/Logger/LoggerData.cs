using eWolfPodcasterCore.Interfaces;
using System;

namespace eWolfPodcasterCore.Logger
{
    public class LoggerData : IDebugLoggerData
    {
        public enum MessageTypes
        {
            None,
            Info,
            Warning,
            Error,
            Fatal
        }

        public string Message { get; set; }
        public DateTime MessageTime { get; set; }

        public MessageTypes MessageType { get; set; }
    }
}