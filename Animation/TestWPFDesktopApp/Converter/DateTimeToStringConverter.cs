using System;
using System.Globalization;
using System.Windows.Data;

namespace g2.Animation.TestWPFDesktopApp.Converter;
public class DateTimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is DateTime ? DateTime.Now.ToString(parameter as string ?? "G") : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string str)
        {
            if (DateTime.TryParse(str, out DateTime dateTime))
            {
                return dateTime;
            }
        }

        return value;
    }
}

