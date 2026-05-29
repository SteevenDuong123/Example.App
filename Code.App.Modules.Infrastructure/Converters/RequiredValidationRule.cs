using System.Globalization;
using System.Windows.Controls;

namespace Code.App.Modules.Infrastructure.Converters
{
    public class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                string message = LanguageManager.IsVietnamese
                    ? "Vui lòng nhập tên sản phẩm!"
                    : "Please enter product name!";
                return new ValidationResult(false, message);
            }
            return ValidationResult.ValidResult;
        }
    }
}