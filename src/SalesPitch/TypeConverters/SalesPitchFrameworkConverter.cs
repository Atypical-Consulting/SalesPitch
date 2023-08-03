using System.ComponentModel;
using System.Globalization;
using SalesPitch.Commands;

namespace SalesPitch.TypeConverters;

public class SalesPitchFrameworkConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == typeof(string)
           || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        => value is string stringValue
            ? Enum.TryParse(stringValue, true, out SalesPitchFramework result)
                ? result
                : throw new ArgumentException($"Cannot convert '{stringValue}' to SalesPitchFramework.")
            : base.ConvertFrom(context, culture, value);

    public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
        => destinationType == typeof(string)
           || base.CanConvertTo(context, destinationType);

    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        => destinationType == typeof(string) && value is SalesPitchFramework salesPitchFrameworkValue
            ? salesPitchFrameworkValue.ToString()
            : base.ConvertTo(context, culture, value, destinationType);
}