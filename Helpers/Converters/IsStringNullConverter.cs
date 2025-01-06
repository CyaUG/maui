using Youth.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Youth.Helpers.Converters
{
    internal class IsStringNullConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            bool retVal = false;
            string str = value as string;
            if (string.IsNullOrEmpty(str) || str == null || str.Length < 1)
            {
                retVal = true;
            }

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();

        }

    }
}
