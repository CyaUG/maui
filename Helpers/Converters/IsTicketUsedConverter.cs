using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    internal class IsTicketUsedConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool showCounter = true;
            if (value != null)
            {
                try
                {
                    EventTicketToken eventTicketToken = value as EventTicketToken;

                    if (eventTicketToken.status == "CREATED" && eventTicketToken != null)
                    {
                        showCounter = false;
                    }
                    else
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
