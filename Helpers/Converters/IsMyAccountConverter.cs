using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    public class IsMyAccountConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool retVal = false;
            if (value != null)
            {
                try
                {
                    UserAccount userAccount = value as UserAccount;
                    if (userAccount.myAccount.id == userAccount.id)
                    {
                        retVal = true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsMyAccountConverter: " + ex);
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
