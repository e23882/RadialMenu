using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Input;

namespace RadialMenu.Utility
{
    public class KeyDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MacroStep step)
            {
                if (step.ActionType == MacroActionType.Delay)
                {
                    return string.Empty;
                }
                else
                {
                    return step.Key.ToString();
                }
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
