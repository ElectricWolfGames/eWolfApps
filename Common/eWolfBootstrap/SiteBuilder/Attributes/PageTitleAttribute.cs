﻿using System;

namespace eWolfBootstrap.SiteBuilder.Attributes
{
    [AttributeUsage(AttributeTargets.Class |
                       AttributeTargets.Struct,
                       AllowMultiple = true)]
    public sealed class PageTitleAttribute : Attribute
    {
        private readonly string _title;

        public PageTitleAttribute(string title)
        {
            _title = title;
        }

        public string Title
        {
            get { return _title; }
        }
    }
}