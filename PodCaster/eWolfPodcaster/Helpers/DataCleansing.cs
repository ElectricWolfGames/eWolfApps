﻿using System;
using System.Collections.Generic;

namespace eWolfPodcaster.Helpers
{
    public static class DataCleansing
    {
        public static string RemoveAllStrings(string inbound, List<string> stringToRemove)
        {
            foreach (string str in stringToRemove)
            {
                inbound = inbound.Replace(str, "");
            }

            return inbound;
        }

        public static string RemoveDoubleSpaces(string str)
        {
            string[] words = str.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return string.Join(" ", words);
        }
    }
}