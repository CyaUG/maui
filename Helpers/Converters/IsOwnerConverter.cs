using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    public class IsOwnerConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] users = value as string[];
            bool retVal = false;

            return retVal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
