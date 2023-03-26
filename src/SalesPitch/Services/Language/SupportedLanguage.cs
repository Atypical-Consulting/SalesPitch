using System.ComponentModel;
using SalesPitch.TypeConverters;

namespace SalesPitch.Services.Language;

[TypeConverter(typeof(SupportedLanguageConverter))]
public enum SupportedLanguage
{
    English,
    French
}