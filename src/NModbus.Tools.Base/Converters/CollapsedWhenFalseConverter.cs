using System;
using System.Windows;
using System.Windows.Data;

namespace NModbus.Tools.Base.Converters
{
    /// <summary>
    /// Converts the value to Visibility.Collapsed when false, Visibility.Visible otherwise.
    /// </summary>
    public class CollapsedWhenFalseConverter : IValueConverter
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
            return (bool)value ? Visibility.Visible : Visibility.Collapsed;
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
