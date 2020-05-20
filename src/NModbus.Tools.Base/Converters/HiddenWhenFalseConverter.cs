using System;
using System.Windows;
using System.Windows.Data;

namespace NModbus.Tools.Base.Converters
{
    /// <summary>
    /// Converts the value to Hidden when false, Visible otherwise.
    /// </summary>
    public class HiddenWhenFalseConverter : IValueConverter
    {
        /// <summary>
        /// Converts
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        /// <summary>
        /// Converts back.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
