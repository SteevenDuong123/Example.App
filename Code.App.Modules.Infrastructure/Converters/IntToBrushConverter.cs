using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Code.App.Modules.Infrastructure.Converters
{
    //public class IntToBrushConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is int intValue)
    //        {
    //            return intValue switch
    //            {
    //                0 => new SolidColorBrush(Colors.Green),
    //                1 => new SolidColorBrush(Colors.Orange),
    //                2 => new SolidColorBrush(Colors.Red),
    //                _ => new SolidColorBrush(Colors.Gray)
    //            };
    //        }
    //        return Brushes.Transparent;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //        => throw new NotImplementedException();
    //}
    public class IntToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int count && count > 0)
                return new SolidColorBrush(Color.FromRgb(244, 67, 54));
            return new SolidColorBrush(Color.FromRgb(76, 175, 80));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
