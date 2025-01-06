using System.Globalization;

namespace Youth.Helpers.Converters;

internal class IsDarkThemeActive : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            return App.Current.RequestedTheme == AppTheme.Dark ? true : false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex); return false; }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
