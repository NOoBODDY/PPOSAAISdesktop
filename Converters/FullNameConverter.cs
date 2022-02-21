using System;
using System.Windows.Data;
using System.Globalization;
using System.Linq;

namespace ClientDB.Converters
{
    internal class FullNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string full = (string)value;
                var list = full.Split(' ');
                string name = list[1]; 
                string lastname = list[0];
                string middlename = list[2];
                return lastname + " " + name.First() + ". " + middlename.First() + "."; 
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
