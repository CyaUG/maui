using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Youth.Helpers.Converters
{
    internal class ConversationProfileNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            String retVal = "";
            if (value != null)
            {
                try
                {
                    LastChatMessage conversation = value as LastChatMessage;
                    if (conversation.chatType == "CUSTOMER_SERVICE" || conversation.chatType == "MULTI_CAST")
                    {
                        if (conversation.myUserId == conversation.chatSenderId)
                        {
                            retVal = conversation.label;
                        }
                        else
                        {
                            retVal = "CC: " + conversation.name;
                        }
                    }
                    else
                    {
                        retVal = conversation.name;
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
