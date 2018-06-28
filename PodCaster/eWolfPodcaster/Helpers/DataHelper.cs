using System;
using System.Globalization;

namespace eWolfPodcaster.Helpers
{
    public static class DataHelper
    {
        public static DateTime ParseDate(string publisedData)
        {
            DateTime dt = DateTime.Now;
            publisedData = publisedData.Replace("EDT", "GMT");
            publisedData = publisedData.Replace("EST", "GMT");

            try
            {
                dt = DateTime.Parse(publisedData, new CultureInfo("en-GB"));
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
