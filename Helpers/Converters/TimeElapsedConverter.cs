using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Youth.Helpers.Converters
{
    internal class TimeElapsedConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            String retVal = "";
            if (value != null)
            {
                try
                {
                    string val = value as string;
                    string dateStr = System.Uri.UnescapeDataString(val);
                    DateTime date;
                    if (!DateTime.TryParse(dateStr, out date))
                    {
                        Console.WriteLine("Invalid date format.");
                    }
                    retVal = GetElapsed(date);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsViewerConverter: " + ex);
                }
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }

        static string GetElapsed(DateTime date)
        {
            DateTime now = DateTime.Now;

            int days = (now - date).Days;
            int weeks = days / 7;
            int months = (now.Year - date.Year) * 12 + (now.Month - date.Month);
            int years = months / 12;

            if (years >= 1)
            {
                return years + " year" + (years > 1 ? "s ago" : " ago");
            }
            if (months >= 1)
            {
                return months + " month" + (months > 1 ? "s ago" : " ago");
            }
            if (weeks >= 1)
            {
                return weeks + " week" + (weeks > 1 ? "s ago" : " ago");
            }
            if (days == 1)
            {
                return "Yesterday";
            }
            if (days == 0)
            {
                return "Today at " + date.ToString("t");
            }
            return days + " day" + (days > 1 ? "s ago" : " ago");
        }
    }
}
