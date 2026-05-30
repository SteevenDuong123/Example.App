using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Example.App.Convnerters
{
    public class LeadingZeroConverter : IValueConverter
    {
        public int DigitCount { get; set; } = 5;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            string strValue = value.ToString();

            if (int.TryParse(strValue, out int number))
            {
                return number.ToString($"D{DigitCount}");
            }

            return strValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && int.TryParse(str, out int number))
            {
                return number.ToString();
            }
            return value?.ToString() ?? "";

        }
    }
}
