using System;
using System.Globalization;
using System.Windows.Data;

namespace RadialMenu.Utility
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue && parameter is string stringParameter)
            {
                if (int.TryParse(stringParameter, out int paramInt))
                {
                    return intValue == paramInt;
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue && boolValue && parameter is string stringParameter)
            {
                if (int.TryParse(stringParameter, out int paramInt))
                {
                    return paramInt;
                }
            }
            return 0;
        }
    }
}
