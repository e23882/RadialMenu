using System;
using System.Globalization;
using System.Windows.Data;

namespace RadialMenu.Utility
{
    public class ScriptDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is MacroStep step)
            {
                if (step.ActionType == MacroActionType.WindowCommand)
                {
                    return step.Script;
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
