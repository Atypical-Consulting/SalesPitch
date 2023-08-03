using System.ComponentModel;
using System.Globalization;
using SalesPitch.Services.Language;

namespace SalesPitch.TypeConverters;

public class SupportedLanguageConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string)
           || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => value is string stringValue
            ? stringValue switch
            {
                "en" or "English" => SupportedLanguage.English,
                "fr" or "Français" => SupportedLanguage.French,
                _ => null
            }
            : null;
}