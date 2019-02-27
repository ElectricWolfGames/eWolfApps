using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using System;
using System.Windows.Controls;

namespace eWolfPodcasterUI.UserControls
{
    public partial class DebugLogItem : UserControl, IDebugLoggerData
    {
        public IDebugLoggerData _loggerData;

        public DebugLogItem()
        {
            InitializeComponent();
        }

        public DateTime MessageTime
        {
            get
            {
                return _loggerData.MessageTime;
            }
        }

        public LoggerData.MessageTypes MessageType
        {
            get
            {
                return _loggerData.MessageType;
            }
        }

        public string MessageTypeDisplay
        {
            get
            {
                return _loggerData.MessageType.ToString();
            }
        }

        public string Message
        {
            get
            {
                return _loggerData.Message;
            }
        }
    }
}
