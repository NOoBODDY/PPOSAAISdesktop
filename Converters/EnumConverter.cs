using System;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using System.Reflection;
using ClientDB.Model;

namespace ClientDB.Converters
{
    public class EnumConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            foreach (var one in Enum.GetValues(parameter as Type))
            {
                if (value.Equals(one))
                    return EnumHelper.GetDescription((Enum)one);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            Type type = parameter as Type;
            return EnumHelper.GetEnumFromDecription((string)value, type );
        }
   
    }
}
