using System.Collections.ObjectModel;
using static eWolfPodcasterCore.Logger.LoggerData;

namespace eWolfPodcasterCore.Logger
{
    public class LoggerService
    {
        private readonly ObservableCollection<LoggerData> _logs = new ObservableCollection<LoggerData>();

        public LoggerService()
        {
        }

        public ObservableCollection<LoggerData> Logs
        {
            get
            {
                return _logs;
            }
        }

        public void Add(MessageTypes messageType, string message)
        {
            LoggerData ld = new LoggerData
            {
                Message = message,
                MessageType = messageType
            };

            _logs.Add(ld);
        }

        public void AddError(string message)
        {
            Add(MessageTypes.Error, message);
        }

        public void AddInfo(string message)
        {
            Add(MessageTypes.Info, message);
        }

        public void AddWarning(string message)
        {
            Add(MessageTypes.Warning, message);
        }
    }
}
