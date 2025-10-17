
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace RadialMenu.Utility
{
    public class UsageToBlocksConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int usage)
            {
                var litCount = (int)Math.Ceiling(usage / 10.0);
                var blocks = new List<bool>(10);
                for (int i = 0; i < 10; i++)
                {
                    blocks.Add(i < litCount);
                }
                return blocks;
            }
            return new List<bool>(10);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
