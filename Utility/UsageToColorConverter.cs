
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace RadialMenu.Utility
{
    public class UsageToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int usage)
            {
                if (usage > 70)
                {
                    return System.Drawing.Brushes.Red;
                }
                else if (usage > 40)
                {
                    return System.Drawing.Brushes.Orange;
                }
                else
                {
                    return System.Drawing.Brushes.Green;
                }
            }
            return System.Drawing.Brushes.Gray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
