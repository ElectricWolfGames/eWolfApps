﻿using eWolfBootstrap.SiteBuilder.Interfaces;
using System;
using System.Collections.Generic;

namespace eWolfBootstrap.SiteBuilder.Helpers
{
    public static class PagePath
    {
        public static string GetPath(ISitePageDetails pageDetails)
        {
            Type types = pageDetails.GetType();

            return GetPath(types.Namespace);
        }

        private static string GetPath(string namespaces)
        {
            string[] parts = namespaces.Split(".");

            bool start = false;
            List<string> folders = new();

            foreach (var part in parts)
            {
                if (part == "_Site")
                {
                    start = true;
                    continue;
                }
                if (start == true)
                {
                    folders.Add(part);
                }
            }


            string fullPath = string.Join("\\", folders);


            return fullPath.Replace("_", "-");
        }
    }
}