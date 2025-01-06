using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Youth.Helpers.Converters
{
    internal class ShowEventCartCountConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool showCounter = false;
            if (value != null)
            {
                try
                {
                    DashboardSummary dashboardSummary = value as DashboardSummary;

                    if (dashboardSummary.eventCartCount > 0)
                    {
                        showCounter = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsViewerConverter: " + ex);
                }
            }

            return showCounter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }

    }
}
