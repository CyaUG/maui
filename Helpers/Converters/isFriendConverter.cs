using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace Youth.Helpers.Converters
{
    public class IsFriendConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string uid = value as string;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
