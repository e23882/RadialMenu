using System;
using System.Globalization;
using System.Windows.Data;

namespace RadialMenu.Utility
{
    public class DelayValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MacroStep step)
            {
                if (step.ActionType == MacroActionType.Delay)
                {
                    return step.DelayMilliseconds.ToString();
                }
            }
            return string.Empty; // Otherwise, return empty string
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
