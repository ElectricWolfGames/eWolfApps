﻿using System;

namespace eWolfPodcasterCore.Services
{
    public class DownloadItem
    {
        public string From { get; set; }
        public string Name { get; set; }
        public string To { get; set; }
        public Action Update { get; set; }
    }
}