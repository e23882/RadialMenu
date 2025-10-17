
using System;
using System.Globalization;
using System.Windows.Data;

namespace RadialMenu.Utility
{
    public class UsageToStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int usage)
            {
                if (usage > 70)
                {
                    return "High";
                }
                else if (usage > 40)
                {
                    return "Medium";
                }
                else
                {
                    return "Low";
                }
            }
            return "Low";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
