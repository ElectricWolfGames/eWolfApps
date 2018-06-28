using System;
using System.Globalization;

namespace eWolfPodcaster.Helpers
{
    public static class DataHelper
    {
        public static DateTime ParseDate(string publisedData)
        {
            DateTime dt = DateTime.Now;
            publisedData = publisedData.Replace("EDT", "");
            publisedData = publisedData.Replace("EST", "");
            publisedData = publisedData.Replace("GMT", "");

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
