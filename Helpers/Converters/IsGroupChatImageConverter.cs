using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    internal class IsGroupChatImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            String retVal = "";
            if (value != null)
            {
                try
                {
                    Chat conversation = value as Chat;
                    if (conversation.chatType == "CUSTOMER_SERVICE" || conversation.chatType == "MULTI_CAST")
                    {
                        if (conversation.myAccount.id == conversation.userAccount.id)
                        {
                            retVal = "https://cya.wagaana.com/" + conversation.logoUrl;
                        }
                        else
                        {
                            retVal = "https://cya.wagaana.com/" + conversation.userAccount.profile_picture;
                        }
                    }
                    else
                    {
                        retVal = "https://cya.wagaana.com/" + conversation.userAccount.profile_picture;
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
