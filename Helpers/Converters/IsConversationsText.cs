using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    internal class IsConversationsText : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool retVal = false;
            if (value != null)
            {
                try
                {
                    LastChatMessage lastChatMessage = value as LastChatMessage;
                    if (lastChatMessage.contentType == "text")
                    {
                        retVal = true;
                    }
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
    }
}
