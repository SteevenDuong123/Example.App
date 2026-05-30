using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Example.App.Converters
{
    public class DateTimeToStringConverter : IValueConverter
    {
        public string Format { get; set; } = "dd/MM/yyyy";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dt)
                return dt.ToString(Format);
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string str && DateTime.TryParse(str, out DateTime dt))
                return dt;
            return DateTime.Now;
        }
    }
}
