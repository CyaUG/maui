using Youth.Utils;
using System.Diagnostics;
using System.Globalization;
using System;

namespace Youth.Helpers.Converters
{
    internal class IsImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            UriImageSource retVal = null;
            if (value != null)
            {
                try
                {
                    String imageSource = value as String;
                    if (!string.IsNullOrEmpty(imageSource))
                    {
                        retVal = new UriImageSource
                        {
                            Uri = new Uri(Constants.appDomain + imageSource),
                            CachingEnabled = true,
                            CacheValidity = new TimeSpan(7, 0, 0, 0)
                        };
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("IsImageSourceConverter: " + ex);
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
