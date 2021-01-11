using eWolfPodcasterCore.Interfaces;
using eWolfPodcasterCore.Logger;
using System;
using System.Windows.Controls;

namespace eWolfPodcasterUI.UserControls
{
    public partial class DebugLogItem : UserControl, IDebugLoggerData
    {
        public DebugLogItem()
        {
            InitializeComponent();
        }

        public IDebugLoggerData LoggerData { get; set; }

        public string Message
        {
            get
            {
                return LoggerData.Message;
            }
        }

        public DateTime MessageTime
        {
            get
            {
                return LoggerData.MessageTime;
            }
        }

        public LoggerData.MessageTypes MessageType
        {
            get
            {
                return LoggerData.MessageType;
            }
        }

        public string MessageTypeDisplay
        {
            get
            {
                return LoggerData.MessageType.ToString();
            }
        }
    }
}