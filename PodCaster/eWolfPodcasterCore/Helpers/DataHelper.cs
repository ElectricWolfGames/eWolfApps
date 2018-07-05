using System;
using System.Collections.Generic;
using System.Globalization;

namespace eWolfPodcasterCore.Helpers
{
    public static class DataHelper
    {
        private static List<string> _textToRemove = new List<string>() { "EDT", "EST", "GMT" };

        public static DateTime ParseDate(string publisedData)
        {
            DateTime dt = DateTime.Now;

            publisedData = DataCleansing.RemoveAllStrings(publisedData, _textToRemove);

            try
            {
                dt = DateTime.Parse(publisedData,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None);
            }
            catch (FormatException)
            {
                dt = ParseDate(publisedData.Substring(4));
            }
            catch
            {
                Console.WriteLine("Failed to format DataTime");
            }

            return dt;
        }
    }
}
