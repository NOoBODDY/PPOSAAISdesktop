using System;
using System.Windows.Data;
using System.Globalization;

namespace ClientDB.Converters
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime date = DateTime.Parse(value as string);
                return date;
            }
            return default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime date = (DateTime) value;
                return date.Date.ToShortDateString();
            }
            return default;
        }
    }
}
