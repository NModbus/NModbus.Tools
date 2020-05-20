using System;
using System.Windows.Data;

namespace NModbus.Tools.Base.Converters
{
    /// <summary>
    /// 
    /// </summary>
    public class InvertBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool) value;
        }
    }
}
