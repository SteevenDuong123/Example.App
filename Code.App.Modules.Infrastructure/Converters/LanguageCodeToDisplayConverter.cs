using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Code.App.Modules.Infrastructure.Converters
{
    public static class LanguageManager
    {
        private static ResourceDictionary? _currentLanguageDict;
        private static string _currentLanguageCode = "vi";

        public static string CurrentLanguageCode
        {
            get => _currentLanguageCode;
            private set
            {
                _currentLanguageCode = value;

            }
        }
        public static bool IsVietnamese => CurrentLanguageCode == "vi";
        public static bool IsEnglish => CurrentLanguageCode == "en";

        public static void ChangeLanguage(string languageCode)
        {
            languageCode = languageCode.ToLower();

            string resourcePath = languageCode switch
            {
                "en" => "/Code.App.Modules.Infrastructure;component/Converters/StringResources.en.xaml",
                "vi" => "/Code.App.Modules.Infrastructure;component/Converters/StringResources.vi.xaml",
                _ => "/Code.App.Modules.Infrastructure;component/Converters/StringResources.vi.xaml"
            };

            var uri = new Uri($"pack://application:,,,{resourcePath}", UriKind.Absolute);

            var newDict = new ResourceDictionary { Source = uri };

            if (_currentLanguageCode is not null)
            {
                Application.Current.Resources.MergedDictionaries.Remove(_currentLanguageDict);
            }

            Application.Current.Resources.MergedDictionaries.Add(newDict);
            _currentLanguageDict = newDict;

            CurrentLanguageCode = languageCode;
        }
    }

    // Converter Text
    public class LanguageCodeToDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text)
            {
                return text switch
                {
                    "vi" => "Tiếng Việt",
                    "en" => "English",
                    _ => "Unknown"
                };
            }
            return "Unknown";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}